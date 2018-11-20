using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Panja_Project
{
    



    public partial class upload : Form
    {
        public upload()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            pro.StandardInput.Write("psftp -pw ubuntu ubuntu@34.216.228.162" + Environment.NewLine); //우분투 접속
            pro.StandardInput.Write("put C:\\Users\\KOS\\Desktop\\2018수업\\exam\\kkk.txt ~/panja/imsi" + Environment.NewLine); //파일 전송 (경로 나중에 바꿀것)
            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            Console.WriteLine(resultValue);



        }

        private void button2_Click(object sender, EventArgs e)
        {

            //-----------------------------------------------------------------------------
            JObject json = new JObject();
            JArray jjson = new JArray();
          


            string[] file_list = new string[4];  //0 : name , 1 : byte, 2 : format

            //파일정보 가져오기
            file_list a = new file_list { Id = "Locu", Name = "dlfma", By = "32048" };
            json = JObject.FromObject(a);
            jjson.Add(json);
            file_list b = new file_list { Id = "Tim", Name = "one", By = "20448" };
            json = JObject.FromObject(b);
            jjson.Add(json);
            file_list c = new file_list { Id = "Kim", Name = "three", By = "20648" };
            json = JObject.FromObject(c);
            jjson.Add(json);

           
            

            //리스트로 저장
            IList<file_list> person = jjson.ToObject<IList<file_list>>();
            
            //출력
            Console.WriteLine(person[0].Id);
            Console.WriteLine(person[1].Name);

            //-----------------------------------------------------------------------------

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(@"c:\Temp\file_list.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                jjson.WriteTo(writer);
            }


            //-----------------------------------------------------------------------------

            file_info temp = new file_info("dd");

            Console.WriteLine(temp.fname);
            Console.WriteLine(temp.fbyte);
            Console.WriteLine(temp.ftime);
            Console.WriteLine(temp.ftype);














        }

        private void Method(ref string[] reff)
        {
            reff[0] = reff[0] + " bbb";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a;
            DirectoryInfo dir = new DirectoryInfo("C:\\Temp");
            Console.WriteLine(dir.Name);
            Console.WriteLine(dir.FullName);
            Console.WriteLine(dir.GetFiles().Length);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //스트링
            string dirPath = @"C:\Temp";
       

            string[] files = Directory.GetFiles(dirPath ,"*.*",SearchOption.AllDirectories);
            foreach(string s in files)
            {
                Console.WriteLine(s);
            }



        }

       





    }
}

