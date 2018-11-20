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
      
        public void RegistryChecker()
        {
            /*
             if(!Registry.LocalMachine.Equals("SOFTWARE\\Classes\\Folder\\shell\\PANJA로 폴더 보호"))
            {
                WriteRegistry();
            }
            */
             if(!Registry.ClassesRoot.Equals("*\\shell\\PANJA로 안전하게 실행"))
            {
                WriteRegistry();
            }
            return;
        }


        public void WriteRegistry()
        {
            //일반 바탕화면추가
            //RegistryKey reg1 = Registry.ClassesRoot.CreateSubKey("Folder\\shell\\Protect As Panja\\command").CreateSubKey("command");
            // 폴더 추가 
            RegistryKey protect_reg = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA로 폴더 보호").CreateSubKey("command");
            // 파일 스캔r
            RegistryKey scan_reg = Registry.ClassesRoot.CreateSubKey("*\\shell\\PANJA로 안전하게 실행").CreateSubKey("command");
            //폴더관련 
            RegistryKey sample1 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA실행 sample1").CreateSubKey("command");
            RegistryKey sample2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA실행 sample2").CreateSubKey("command");
            RegistryKey sample3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA 회복 smaple1 ").CreateSubKey("command");
            RegistryKey sample4 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\PANJA 회복 smaple2 ").CreateSubKey("command");


            String setValue = "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect";
            
            //sampel1
            setValue = "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect_normal";
            sample1.SetValue("",setValue);
            

            //sample2
            setValue = "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect_test01";
            sample2.SetValue("",setValue);
            

            //sample3
            setValue ="C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 recover01";
            sample2.SetValue("",setValue);
            
            //sample4
            setValue ="C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 recover02";
            sample3.SetValue("",setValue);

            //해당 프로젝트 실행 절대 경로 삽입

            //레지스트리 경로 커넥팅
            protect_reg.SetValue("", "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect");
            sample4.SetValue("", "C:\\Users\\J3N_JAN6\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\detect_ransom.exe \"%1\"");
            
        }


        
    }
}
