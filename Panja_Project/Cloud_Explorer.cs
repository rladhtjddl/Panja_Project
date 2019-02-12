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

namespace Panja_Project
{
    public partial class Cloud_Explorer : Form
    {
        

        string host = @"54.185.231.100";
        string username = "os";
        string password = "tlqkf";
        string localFileName = System.IO.Path.GetFileName(@"localfilename");
        string remoteDirectory = ".";
        string old_path = "";
        

        public Cloud_Explorer()
        {
            InitializeComponent();
        }

        private void Cloud_Explorer_Load(object sender, EventArgs e)
        {
            /*
            JObject json = new JObject();
            JArray jjson = new JArray();
            long count;
            string json_t;
            Filecontrol fi = new Filecontrol();
            fi.Get_json();

            using (StreamReader r = new StreamReader(@"../../Properties/file_list.Json"))
            {
                json_s = r.ReadToEnd();
                json_t = json_s.ToString();
            }

            json = JObject.Parse(json_t);
            count = json["link"].LongCount();
            

            for (int q = 0; q < count; q++)
            {
                sum_link[q] = json["link"][q]["flink"].ToString();
                Console.WriteLine(sum_link[q]);
            }
            //동근 sum_link[] -> 0~count 까지가 경로 저장해둔거
  
            
            //String addr = "/home/ubuntu/panja/imsi";

            //cmd창
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

            pro.StandardInput.Write("cd ../../Properties" + Environment.NewLine);
            pro.StandardInput.Write("psftp -pw ubuntu ubuntu@54.187.238.235" + Environment.NewLine); //우분투 접속
            pro.StandardInput.Write("cd /home/ubuntu/panja/user1" + Environment.NewLine);
            pro.StandardInput.Write("ls" + Environment.NewLine); //파일 전송 (경로 나중에 바꿀것)
            

            //SettingListView(addr);

            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            
            Console.WriteLine(resultValue);
            pro.WaitForExit();
            pro.Close();
            */

            //string[] allLines = File.ReadAllLines(@"../../Properties\test2.txt", Encoding.Default);
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

                var sftp = new SftpClient(host, username, password);

                sftp.Connect();

                var files = sftp.ListDirectory(remoteDirectory);

                Console.WriteLine("파일 폴더로 이동");

                sftp.ChangeDirectory(remoteDirectory);
                

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

                sftp.Disconnect();

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
                old_path = remoteDirectory;

                
                if (cloud_list.SelectedItems[0].ImageIndex == 0)
                {

                    pathnow = cloud_list.SelectedItems[0].Text;
                    Console.WriteLine(pathnow + "로 이동");
                    remoteDirectory = path_now.Text + "/" + pathnow;

                    if (cloud_list.SelectedItems[0].Text == "..")
                    {
                        path_now.Text = old_path;
                    }
                    else
                    {
                        path_now.Text = remoteDirectory;
                    }


                    cloud_list.Items.Clear();
                    settingListview();


                }
                else if (cloud_list.SelectedItems[0].ImageIndex == 0)
                {


                }

                
            }
        }
    }
}
