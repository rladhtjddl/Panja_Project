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
    public partial class MainUI : Form
    {
        private JToken json_s;

        public MainUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void local_btn_Click(object sender, EventArgs e)
        {
            Local_Explorer exp = new Local_Explorer();
            exp.ShowDialog();
        }

        private void Cloud_btn_Click(object sender, EventArgs e)
        {
            //JObject json = new JObject();
            //JArray jjson = new JArray();
            //long count;
            //string json_t;
            //Filecontrol fi = new Filecontrol();
            //fi.Get_json();

            //using (StreamReader r = new StreamReader(@"../../Properties/file_list.Json"))
            //{
            //    json_s = r.ReadToEnd();
            //    json_t = json_s.ToString();
            //}

            //json = JObject.Parse(json_t);
            //count = json["link"].LongCount();
            //string[] sum_link = new string[10000]; 

            //for(int q=0; q< count; q++)
            //{
            //    sum_link[q] = json["link"][q]["flink"].ToString();
            //    Console.WriteLine(sum_link[q]);
            //}
            ////동근 sum_link[] -> 0~count 까지가 경로 저장해둔거

            ////cmd창
            //System.Diagnostics.ProcessStartInfo proinfo = new System.Diagnostics.ProcessStartInfo();
            //System.Diagnostics.Process pro = new System.Diagnostics.Process();

            //proinfo.FileName = @"cmd";
            //proinfo.CreateNoWindow = true; //띄우기 안띄우기
            //proinfo.UseShellExecute = false;
            //proinfo.RedirectStandardOutput = true;
            //proinfo.RedirectStandardInput = true;
            //proinfo.RedirectStandardError = true;


            //pro.StartInfo = proinfo;
            //pro.Start();

            //pro.StandardInput.Write("cd ../../Properties" + Environment.NewLine);
            //pro.StandardInput.Write("psftp -pw ubuntu ubuntu@54.187.238.235" + Environment.NewLine); //우분투 접속
            //pro.StandardInput.Write("cd panja/user1" + Environment.NewLine);
            //pro.StandardInput.Write(@"ls" + Environment.NewLine); //파일 전송 (경로 나중에 바꿀것)
            //pro.StandardInput.Close();

            //string resultValue = pro.StandardOutput.ReadToEnd();
            //pro.WaitForExit();
            //pro.Close();

            //Console.WriteLine(resultValue);

            //Cloud_Explorer cld = new Cloud_Explorer();
            //cld.ShowDialog();

            //ftp서버 연결
            string[] Jenjang_string = new string[50];
            int Jenjang_index = 0;

            string host = @"54.185.231.100";
            string username = "os";
            string password = "tlqkf";
            string localFileName = System.IO.Path.GetFileName(@"localfilename");
            string remoteDirectory = ".";

            var sftp = new SftpClient(host, username, password);

            sftp.Connect();

            var files = sftp.ListDirectory(remoteDirectory);
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }

            Console.WriteLine("파일 폴더로 이동");
            sftp.ChangeDirectory("./file");

            files = sftp.ListDirectory(remoteDirectory);
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
                Jenjang_string[Jenjang_index++]= file.FullName;
                
                
            }

            Console.WriteLine("폴더1로 이동");
            sftp.ChangeDirectory("./folder1");

            files = sftp.ListDirectory(remoteDirectory);
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
                Jenjang_string[Jenjang_index++] = file.FullName;
            }

            sftp.Disconnect();

            for(int i=0; i<Jenjang_index; i++)
            {
                Console.WriteLine("동근스트링["+i+"]" + Jenjang_string[i]);
            }


        }

        


    private void settings_Click(object sender, EventArgs e)
        {

        }
        
    }
}
