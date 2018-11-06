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
             if(!Registry.LocalMachine.Equals("SOFTWARE\\Classes\\Folder\\shell\\Protect As Panja_S"))
            {
                WriteRegistry();
            }
            return;
        }


        public void WriteRegistry()
        {
            //일반 바탕화면추가
            RegistryKey reg1 = Registry.ClassesRoot.CreateSubKey("Folder\\shell\\Protect As Panja\\command").CreateSubKey("command");
            // 폴더 추가 
            RegistryKey reg2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shell\\Protect As Panja_S").CreateSubKey("command");


            //현재 실행 경로 로딩
            String currentPath = Environment.CurrentDirectory;

            currentPath += "\\Panja_Project.exe";
            

            //레지스트리 경로 커넥팅
            reg2.SetValue("", currentPath + " 1% protect");

            
        }
    }
}
