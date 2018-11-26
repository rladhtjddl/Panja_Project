using Newtonsoft.Json;
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

namespace Panja_Project
{
    public partial class Local_Plus : Form
    {
        public String[] added_Folder = new string[10000];

        public Local_Plus()
        {
            InitializeComponent();
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

        private void button3_Click(object sender, EventArgs e)
        {
          
            JObject json = new JObject();
            JArray jjson = new JArray();

            string file_name, file_byte, file_link;
            string[] file_save = new string[100];
            int count = 0; 


            int i;
            //added 폴더 
            int lastnum = selectlist.Items.Count;
            for (i = 0; i < lastnum; i++) {
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

                //added_Folder : 해당 보호폴더 첫 헤더 폴더 (타겟폴더)
                added_Folder[i] = selectlist.Items[i].Text;
                
                //윤식 : 이부분 input ACL 

                Regedit rgd = new Regedit();
                pro.StandardInput.WriteLine("attrib " + '\u0022' + added_Folder[i] + '\u0022' + " +r +s +h" + Environment.NewLine);

                AccessAuthority aauth = new AccessAuthority(added_Folder[i]);
                
                Shortcut shortcut = Shortcut.getInstance();
                shortcut.createShortcut(Path.GetDirectoryName(added_Folder[i]),  Path.GetFileName(added_Folder[i]));
                //Console.WriteLine(added_Folder[i]);
              
                string dirPath = added_Folder[i];

                aauth.folderSecu_Test3();
                string[]  files = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                    file_save[count++] = s;
                    
                }

                pro.StandardInput.Close();

                pro.Close();
            }
    
            for (i = 0; i < count - 1; i++)
            {
                file_info file_Inf = new file_info(file_save[i]);
                Console.WriteLine(file_Inf.fname);
                Console.WriteLine(file_Inf.flink);
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


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Local_Plus plus = new Local_Plus();
            plus.Close();
        }

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

                Regedit rgd = new Regedit();
                AccessAuthority aauth = new AccessAuthority(added_Folder[i]);
                aauth.folderSecu_Recover();
                pro.StandardInput.Write("attrib " + '\u0022' + added_Folder[i] + '\u0022' + " -r -s -h" + Environment.NewLine);
            }
            pro.StandardInput.Close();

            pro.Close();
        }
        /// <summary>
        /// 트리를 마우스로 클릭할 때 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
