 using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panja_Project
{
    class Regedit
    {
        private string dir;


        public void RegistryChecker(string dir)
        {
            this.dir = dir;
            RegistryKey ch_Key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA실행 sample1", true);

            if (ch_Key ==null){
                WriteRegistry(dir);
            }

            return;
        }


        public void WriteRegistry(string dir)
        {
          
            // 파일 스캔r
            RegistryKey scan_reg = Registry.ClassesRoot.CreateSubKey("*\\shell\\PANJA로 안전하게 실행").CreateSubKey("command");
          
            
            //폴더관련 
            //RegistryKey sample1 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA실행 sample1").CreateSubKey("command");
            RegistryKey sample2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA실행 sample2").CreateSubKey("command");
            //RegistryKey sample3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA 회복 smaple1 ").CreateSubKey("command");
            RegistryKey sample4 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA 회복 smaple2 ").CreateSubKey("command");


            String setValue;// = "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect";

            ////sampel1
            //setValue = dir + " %1 protect_normal";
            //sample1.SetValue("", setValue);


            //sample2
            setValue = dir + " %1 protect_test01";
            sample2.SetValue("", setValue);


            ////sample3
            //setValue = dir +" %1 recover01";
            //sample3.SetValue("", setValue);

            //sample4
            setValue = dir+" %1 recover02";
            sample4.SetValue("", setValue);

            //해당 프로젝트 실행 절대 경로 삽입

            //레지스트리 경로 커넥팅
            //protect_reg.SetValue("", "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect");
            //scan_reg.SetValue("", "C:\\Users\\J3N_JAN6\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\detect_ransom.exe \"%1\"");

        }

        public string getAbsDir()
        {
            RegistryKey reg_Key = Registry.LocalMachine;
            reg_Key = reg_Key.OpenSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA실행 sample1\\command", true);

            string val = (string)reg_Key.GetValue("");
            string[] split = val.Split(' ');
          
            return split[0];
        }

    }
}
