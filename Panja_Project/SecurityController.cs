 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panja_Project
{
    class SecurityController
    {
       
            
        private Regedit rgd;
        private AccessAuthority aauth;
        private string parentPath;
        private Shortcut shortcut;

        public SecurityController()
        {

        }

        public void rightClickSecurity(string command, string address)
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

            //pro.StandardInput.Write("attrib " + '\u0022' + address + '\u0022' + " +r +s +h" + Environment.NewLine);
            //String dir = Environment.CurrentDirectory;


            if (command.Equals("protect_normal"))
            {

                pro.StandardInput.Write("attrib " + '\u0022' + address + '\u0022' + " +r +s " + Environment.NewLine);


            }
            else if (command.Equals("protect_test01"))
            {
                Regedit rgd = new Regedit();
                Shortcut shortcut = Shortcut.getInstance(rgd.getAbsDir());
                shortcut.createShortcut(Environment.CurrentDirectory, Path.GetFileName(address));

                pro.StandardInput.Write("-- Test 01 -- " + Environment.NewLine);
                AccessAuthority auth = new AccessAuthority(address);
                auth.folderSecu_Test3();



            }
            else if (command.Equals("recover01"))
            {

                // +h 까지 추가하면 생각해보니 사실상의미가 없음 
                pro.StandardInput.Write("attrib " + '\u0022' + address + '\u0022' + " -r -s " + Environment.NewLine);

            }
            else if (command.Equals("recover02"))
            {
                AccessAuthority auth = new AccessAuthority(address);
                auth.folderSecu_Recover();
            }


            //문제 발생지역  address 추적해볼필요가있음 문제 발생 ==> dir 현재 디렉토리로 변경 (문제 :  lnk 이름을 따라가므로 링크 변경이 필요)
            // copy lnk 위치 , 현재 dir 
            //pro.StandardInput.Write("copy C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\sample.lnk "+ dir + Environment.NewLine);


            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();



        }

        //In program, do panja process , object_dir is Directory that we 'll make replica
        public void programSecurity(string object_dir )
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

            Regedit rgd = new Regedit();
            AccessAuthority aauth = new AccessAuthority(object_dir);
            string parentPath = Path.GetDirectoryName(object_dir);
            Shortcut shortcut = Shortcut.getInstance();
            shortcut.createShortcut(parentPath, Path.GetFileName(object_dir));
            aauth.folderSecu_Test2();
            pro.StandardInput.Write("attrib " + '\u0022' + object_dir + '\u0022' + " +r +s +h" + Environment.NewLine);
            pro.StandardInput.Close();

            pro.Close();
        }
    }
}
