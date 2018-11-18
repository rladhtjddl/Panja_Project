using System;
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
            proinfo.CreateNoWindow = false; //띄우기 안띄우기
            proinfo.UseShellExecute = false;
            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;

            pro.StartInfo = proinfo;
            pro.Start();

            pro.StandardInput.Write("psftp -pw ubuntu ubuntu@34.216.228.162" + Environment.NewLine); //우분투 접속
            pro.StandardInput.Write("put C:\\Users\\KOS\\Desktop\\2018수업\\exam\\kkk.txt ~/panja/imsi" + Environment.NewLine); //파일 전송 (경로 나중에 바꿀것)
            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            Console.WriteLine(resultValue);



        }
    }
}
