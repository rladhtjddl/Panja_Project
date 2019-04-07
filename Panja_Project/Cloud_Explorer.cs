using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Tamir.Streams;
using Tamir.SharpSsh;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;

namespace Panja_Project
{
   



public partial class Cloud_Explorer : Form
    {
        

        public string host = @"54.185.231.100";
        public string username = "os";
        public string password = "tlqkf";
        string localFileName = System.IO.Path.GetFileName(@"localfilename");
        string remoteDirectory = ".";
        public SftpClient sftp;

        

        public Cloud_Explorer()
        {
            InitializeComponent();
            
        }

        private void Cloud_Explorer_Load(object sender, EventArgs e)
        {
            sftp = new SftpClient(host, username, password);
            sftp.Connect();
            
            path_now.Text = remoteDirectory;
            settingListview();
        }

        public void settingListview() {

            string[] Jenjang_file = new string[50];
            string[] Jenjang_folder = new string[50];
            string[] Jenjang_file_fullname = new string[50];
            string[] Jenjang_folder_fullname = new string[50];
            int file_index = 0;
            int folder_index = 0;

            try
            {
                //기존의 파일 목록 제거
                cloud_list.Items.Clear();

                

                

                var files = sftp.ListDirectory(remoteDirectory);

                //Console.WriteLine("파일 폴더로 이동");
                sftp.ChangeDirectory(remoteDirectory);
                path_now.Text = sftp.WorkingDirectory;

                foreach (var file in files)
                {
                    //Console.WriteLine(file.FullName);
                    //Console.WriteLine(file.Name);
                    if (file.IsDirectory)
                    {
                        if (file.Name != ".")
                        {
                            Jenjang_folder[folder_index++] = file.Name;
                            Jenjang_folder_fullname[folder_index] = file.FullName;
                            
                        }
                    }
                    else
                    {
                        Jenjang_file[file_index++] = file.Name;
                        Jenjang_file_fullname[file_index] = file.FullName;
                    }


                }


                for (int j = 0; j < folder_index; j++)
                {
                    ListViewItem cldfolder = new ListViewItem();
                    cldfolder.ImageIndex = 0;
                    cldfolder.Text = Jenjang_folder[j];
                    cloud_list.Items.Add(cldfolder);
                }


                for (int i = 0; i < file_index; i++)
                {
                    ListViewItem cldItem = new ListViewItem();
                    cldItem.ImageIndex = 1;
                    cldItem.Text = Jenjang_file[i];
                    cloud_list.Items.Add(cldItem);
                }

                //sftp.Disconnect();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
            }

        }



        private void cloud_list_DoubleClick(object sender, EventArgs e)
        {
            
            if (cloud_list.SelectedItems.Count == 1)
            {
                string pathnow;
                
                if (cloud_list.SelectedItems[0].ImageIndex == 0)
                {

                    pathnow = cloud_list.SelectedItems[0].Text;
                    Console.WriteLine(pathnow + "로 이동");
                    remoteDirectory = path_now.Text + "/" + pathnow;
                    
                    cloud_list.Items.Clear();
                    settingListview();


                }
                else if (cloud_list.SelectedItems[0].ImageIndex == 0)
                {


                }

                
            }
        }

        private void cloud_list_MouseClick(object sender, MouseEventArgs e)
        {
            
            if(cloud_list.SelectedItems.Count != 0)
            { 
            if (e.Button.Equals(MouseButtons.Right))
            {
                string selectedNickname = cloud_list.SelectedItems[0].Text;


                //오른쪽 메뉴를 만듭니다 
                ContextMenu m = new ContextMenu();
                    //메뉴에 들어갈 아이템을 만듭니다
                    MenuItem m1 = new MenuItem();
                    MenuItem m2 = new MenuItem();

                    string fullname_select;
                    m1.Text = "다운로드(폴더)";
                    m2.Text = "다운로드(파일)";
                    fullname_select = path_now.Text + "/" + selectedNickname;

                    //다운로드(폴더) 클릭시 이벤트
                    m1.Click += (senders, es) => {
                        Console.WriteLine("클릭됌!");
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        dialog.ShowDialog();
                        string select_path = dialog.SelectedPath;

                        //폴더만들기
                        string sDirPath;
                        sDirPath = select_path + @"\"+ selectedNickname;
                        DirectoryInfo di = new DirectoryInfo(sDirPath);
                        if (di.Exists == false)
                        {
                            di.Create();
                        }




                        Console.WriteLine("어디로가니 : " + sDirPath);
                        DownloadDirectory(sftp, fullname_select, sDirPath);
                        MessageBox.Show("Download Complete");












                    };



                    //다운로드(파일) 클릭시 이벤트
                    m2.Click += (senders, es) => {
                    Console.WriteLine(selectedNickname);
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog();
                    string select_path = dialog.SelectedPath;
                    
                    using (Stream fileStream = File.Create(select_path + "\\" + selectedNickname))
                    {
                        sftp.DownloadFile(fullname_select, fileStream);
                    }
                    MessageBox.Show("Download Complete");
                };



                m.MenuItems.Add(m1);
                m.MenuItems.Add(m2);
                m.Show(cloud_list, new Point(e.X, e.Y));
            }
            }


        }

        private void cloud_list_Click(object sender, EventArgs e)
        {
          
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            //ftp연결
            sftp = new SftpClient(host, username, password);
            sftp.Connect();

            Console.WriteLine("업로드클릭");
            


            FolderBrowserDialog dialog = new FolderBrowserDialog();
            
            DialogResult result = dialog.ShowDialog();

            string select_path = dialog.SelectedPath;
            string select_name;
            if (result == DialogResult.OK)
            {
                DirectoryInfo dirinfo = new DirectoryInfo(select_path);
                select_name = dirinfo.Name;

                sftp.CreateDirectory("./folder1/" + select_name);
                UploadDirectory(sftp, select_path, "./folder1/" + select_name);

                MessageBox.Show("Upload Complete");
            }
            else if (result == DialogResult.Cancel)
            {

            }
            else { }
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

        


        private static void DownloadDirectory(SftpClient client, string source, string destination)
        {
            var files = client.ListDirectory(source);
            foreach (var file in files)
            {
                if (!file.IsDirectory && !file.IsSymbolicLink)
                {
                    DownloadFile(client, file, destination);
                }
                else if (file.IsSymbolicLink)
                {
                    Console.WriteLine("Ignoring symbolic link {0}", file.FullName);
                }
                else if (file.Name != "." && file.Name != "..")
                {
                    var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                    DownloadDirectory(client, file.FullName, dir.FullName);
                }
            }
        }

        private static void DownloadFile(SftpClient client, SftpFile file, string directory)
        {
            Console.WriteLine("Downloading {0}", file.FullName);
            using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name)))
            {
                client.DownloadFile(file.FullName, fileStream);
            }
        }


    }
}
