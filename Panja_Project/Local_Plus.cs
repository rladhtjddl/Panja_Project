﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tamir.Streams;
using Tamir.SharpSsh;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;

namespace Panja_Project
{
    public partial class Local_Plus : Form
    {

        public string host = @"54.185.231.100";
        public string username = "os";
        public string password = "tlqkf";
        string localFileName = System.IO.Path.GetFileName(@"localfilename");
        string remoteDirectory = ".";
        public SftpClient sftp;

        public int length;
        public string new_key;
        public string[] new_list = new string[100];

        public string[] folder_path = new string[1000];
        public String[] added_Folder = new string[10000];

        public Local_Plus()
        {
            InitializeComponent();
        }
        public Local_Plus(string key)
        {
            InitializeComponent();
            new_key = key;
        }



        public void get_list(string[] list,int count) {
            length = count;
            new_list = list;
        }



        private void Local_Plus_Load(object sender, EventArgs e)
        {  //현재 로컬 컴퓨터에 존재하는 드라이브 정보 검색하여 트리노드에 추가
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo dname in allDrives)
            {
                if (dname.DriveType == DriveType.Fixed)
                {
                    if (dname.Name == @"C:")
                    {
                        TreeNode rootNode = new TreeNode(dname.Name);
                        rootNode.ImageIndex = 0;
                        rootNode.SelectedImageIndex = 0;
                        addlist.Nodes.Add(rootNode);
                        Fill(rootNode);
                    }
                    else
                    {
                        TreeNode rootNode = new TreeNode(dname.Name);
                        rootNode.ImageIndex = 1;
                        rootNode.SelectedImageIndex = 1;
                        addlist.Nodes.Add(rootNode);
                        Fill(rootNode);
                    }
                }
            }

            //첫번째 노드 확장
            addlist.Nodes[0].Expand();
            //listView1.FullRowSelect = true;


        }



        private void Fill(TreeNode dirNode)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(dirNode.FullPath);
                //드라이브의 하위 폴더 추가
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(dirItem.Name);
                    newNode.ImageIndex = 2;
                    newNode.SelectedImageIndex = 2;
                    dirNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
            }
        }

        /// <summary>
        /// 트리가 확장되기 전에 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addlist_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                e.Node.ImageIndex = 3;
                e.Node.SelectedImageIndex = 3;
                Fill(e.Node);
            }
        }
        /// <summary>
        /// 트리가 닫히기 전에 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addlist_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            
            if(addlist.SelectedNode.FullPath != null) { 
            String path = "C:\\" + addlist.SelectedNode.FullPath.Substring(4);
            selectlist.Items.Add(path);
            }
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            if(selectlist.SelectedItems[0] != null) { 
            selectlist.Items.Remove(selectlist.SelectedItems[0]);
            }
        }
        
        //이건 보호해제하는건데 갖다버려라
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo proinfo = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process pro = new System.Diagnostics.Process();

            proinfo.FileName = @"cmd";
            proinfo.CreateNoWindow = false; //띄우기 안띄우기
            proinfo.UseShellExecute = false;
            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;

            pro.StartInfo = proinfo;
            pro.Start();

            int i;
            //added 폴더 
            int lastnum = selectlist.Items.Count;
            for (i = 0; i < lastnum; i++)
            {
                //added_Folder : 해당 보호폴더 첫 헤더 폴더 (타겟폴더)
                added_Folder[i] = selectlist.Items[i].Text;

                //윤식 : 이부분 input ACL 
                
                //AccessAuthority aauth = new AccessAuthority(added_Folder[i]);
                //aauth.folderSecu_Recover();

                FolderAccess rgs = new FolderAccess();
                rgs.panja_inherit_recover(added_Folder[i]);


                pro.StandardInput.Write("attrib " + '\u0022' + added_Folder[i] + '\u0022' + " -r -s -h" + Environment.NewLine);
            }
            pro.StandardInput.Close();

            pro.Close();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void UploadDirectory(SftpClient client, string localPath, string remotePath)
        {
            Console.WriteLine("Uploading directory {0} to {1}", localPath, remotePath);

            IEnumerable<FileSystemInfo> infos =
                new DirectoryInfo(localPath).EnumerateFileSystemInfos();
            foreach (FileSystemInfo info in infos)
            {
                if (info.Attributes.HasFlag(FileAttributes.Directory))
                {
                    string subPath = remotePath + "/" + info.Name;
                    if (!client.Exists(subPath))
                    {
                        client.CreateDirectory(subPath);
                    }
                    UploadDirectory(client, info.FullName, remotePath + "/" + info.Name);
                }
                else
                {
                    using (Stream fileStream = new FileStream(info.FullName, FileMode.Open))
                    {
                        Console.WriteLine(
                            "Uploading {0} ({1:N0} bytes)",
                            info.FullName, ((FileInfo)info).Length);

                        client.UploadFile(fileStream, remotePath + "/" + info.Name);
                    }
                }
            }
        }

        private void proctect_click(object sender, EventArgs e)
        {
            //int count = 0;


            int i;
            //added 폴더 
            int lastnum = selectlist.Items.Count;


            if (lastnum != 0)
            {

                //JObject json = new JObject();
                //JArray jjson = new JArray();

                //string file_name, file_byte, file_link;
                //string[] file_save = new string[1000];
                for (i = 0; i < lastnum; i++)
                {


                    //added_Folder : 해당 보호폴더 첫 헤더 폴더 (타겟폴더)
                    added_Folder[i] = selectlist.Items[i].Text;

                    folder_path[i] = added_Folder[i];

                    //Console.WriteLine(added_Folder[i]);

                    //string dirPath = added_Folder[i];

                    if (upload_chk.Checked == true)
                    {
                        Name = Path.GetFileName(selectlist.Items[i].Text);
                        sftp = new SftpClient(host, username, password);
                        sftp.Connect();

                        sftp.CreateDirectory("./folder1/" + Name);

                        UploadDirectory(sftp, selectlist.Items[i].Text, "./folder1/" + Name);
                        sftp.Disconnect();

                        MessageBox.Show(Name + "가 클라우드 저장소에 업로드 되었습니다.");
                    }
                    /*
                    string[] files = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                        file_save[count++] = s;

                    }
                    */
                }
                /*
                for (i = 0; i < count - 1; i++)
                {
                    file_info file_Inf = new file_info(file_save[i]);
                    Console.WriteLine(file_Inf.fname);

                    json = JObject.FromObject(file_Inf);
                    jjson.Add(json);

                }




                json.Add("link", jjson);
                //리스트로 저장
                //IList<file_list> save_json = json.ToObject<IList<file_list>>();

                // write JSON directly to a file
                using (StreamWriter file = File.CreateText(@"C:\Temp\file_list.json"))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    json.WriteTo(writer);
                }

                //저장한파일 클라우드의 유저디렉토리에 저장
                Filecontrol fi = new Filecontrol();
                //fi.Put_json();


                for (i = 0; i < lastnum; i++)
                {
                    fi.Put_json(folder_path[i]);
                }
                */



                hex_handler hex = new hex_handler();

                int totalnum = hex.hex_length();

                string[] all_string = new string[totalnum + lastnum];

                int num;

                for (num = 0; num < totalnum; num++)
                {
                    all_string[num] = new_list[num];
                }


                int k = 0;

                for (i = 0; i < lastnum; i++)
                {

                    Register rgs = new Register();
                    // 임시 폴더용 코드임 
                    //string absPRo = @"C:\Temp\test.txt";
                    for (num = totalnum; num < lastnum + totalnum; num++)
                    {
                        if (k != lastnum)
                        {
                            all_string[num] = added_Folder[k];
                            k++;
                        }
                    }

                    //프로젝트 기반 상대경로
                    //property 상대경로 불러오는 코드 
                    //Register rgs = new Register();
                    //string[] absExcuteDir = rgs.getAbsDir().Split('\\');

                    //string absPro = "";
                    //for (int k = 0; k < absExcuteDir.Length - 3; k++)
                    //{
                    //    absPro += absExcuteDir[k];
                    //    absPro += @"\";
                    //}
                    //absPro += @"Properties\test.txt";
                    //System.IO.File.AppendAllText(absPro, "\n" + folder_path[i], Encoding.Default);
                }

                int start_point = 0x500 - 0x001;

                for (num = 0; num < lastnum + totalnum; num++)
                {

                    hex.hex_write1(all_string[num], start_point);
                    start_point += 0x100;
                    //System.IO.File.AppendAllText(@"C:\Temp\test.txt", Environment.NewLine + folder_path[i] , Encoding.Default);
                }

                hex.hex_length_set(totalnum + lastnum);

                
                //-------------------------------폴더보호

                System.Diagnostics.ProcessStartInfo proinfo = new System.Diagnostics.ProcessStartInfo();
                System.Diagnostics.Process pro = new System.Diagnostics.Process();

                proinfo.FileName = @"cmd";
                proinfo.CreateNoWindow = true; //띄우기 안띄우기
                proinfo.UseShellExecute = false;
                proinfo.RedirectStandardOutput = true;
                proinfo.RedirectStandardInput = true;
                proinfo.RedirectStandardError = true;

                pro.StartInfo = proinfo;
                pro.Start();

                for (i = 0; i < lastnum; i++)
                {
                    Console.WriteLine(folder_path[i]);

                    //-----------------------------------------
                    //윤식 : 이부분 input ACL 
                    pro.StandardInput.WriteLine("attrib " + '\u0022' + folder_path[i] + '\u0022' + " +r +s +h" + Environment.NewLine);
                    icon rgs = new icon();


                    rgs.createShortcut(Path.GetDirectoryName(folder_path[i]), Path.GetFileName(folder_path[i]));

                    //rgs.panja_protect(folder_path[i]);
                    MessageBox.Show("Inherit Delete Excution");
                    rgs.panja_inherit_delete(folder_path[i]);

                    
                }

                pro.StandardInput.Close();

                pro.Close();


               
                this.Close();
            }

        }

        /// <summary>
        /// 트리를 마우스로 클릭할 때 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
