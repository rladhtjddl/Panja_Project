﻿using System;
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
    public partial class foler_imsi : Form
    {
        public foler_imsi()
        {
            InitializeComponent();
        }

        private void foler_imsi_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string sDirPath;
            string panjap = "panja";
            string username = "rladh";
            sDirPath = "C:\\Temp\\example\\dir\\" + panjap;
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }
        }

        private void label2_Click(object sender, EventArgs e)
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

            pro.StandardInput.Write("attrib " + '\u0022' + "C:\\Temp\\example\\dir\\panja" + '\u0022' + " +r +s +h" + Environment.NewLine);
            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            Console.WriteLine(resultValue);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //panja 폴더 생성 (여기선 txt)



            string path = "C:\\Temp\\example\\dir\\panja.txt";
            string textValue = Console.ReadLine();
            System.IO.File.WriteAllText(path, textValue, Encoding.Default);
        }
    }
}
