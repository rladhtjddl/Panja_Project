using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panja_Project
{
    class Register
    {
        /*
         * 작성자 : 정윤식 
         * 클래스 용도 : 레지스트리 등록용 
         * 포함 메소드 : 레지스트리 등록 및 등록 여부 확인,해당 레지스트리에서 경로 로딩
         * 비고 
         */

        private const string CONST_REGEDIT_DIR = "SOFTWARE\\Classes\\Folder\\shell\\PANJA로 보호하기";

        //해당 레지스트리에 들어갈 프로그램 절대 경로
        private string total_dir;


        //getter &  settter
        public void set_totaldir (string totaldir)
        {
            this.total_dir = totaldir;
        }

        public string get_totaldir()
        {
            return this.total_dir ;
        }


        //레지스트리 등록 체크 
        public void RegistryChecker(string totaldir)
        {
            //해당 레지스트리가 있는지 스캔
            RegistryKey ch_Key = Registry.LocalMachine.OpenSubKey(CONST_REGEDIT_DIR, true);

            //미등록시 
            if (ch_Key == null)
            {
                //등록 메소드
                Regist(totaldir);
            }

            return;
        }





        //레지스트리 등록 (실행할 Panja의 절대경로) : Panja의 절대경로를 레지스트리로 등록
        private void Regist(string totaldir)
        {

            //폴더관련 
           
            RegistryKey regist_Panja = Registry.LocalMachine.CreateSubKey(CONST_REGEDIT_DIR).CreateSubKey("command");
   
            String setValue;// = "C:\\Users\\ykmga\\Source\\Repos\\rladhtjddl\\Panja_Project\\Panja_Project\\bin\\Debug\\Panja_Project.exe %1 protect";

            //레지스트리에 파라미터를 추가하여 등록 
            /*
             * 프로그램경로 [실행위치(=%1)] [명령어(protect)]
             * 
             */
            setValue =totaldir + " %1 protect";
            regist_Panja.SetValue("", setValue);
            

        }


        //레지스트리에서 절대경로  반환
        public string getAbsDir()
        {
            /*
             * 프로그램경로 [실행위치(=%1)] [명령어(protect)] 이므로 ' '으로 분리하여
             * 프로그램 경로만 반환
             */

            RegistryKey reg_Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\Folder\shell\PANJA로 보호하기\command", true);
            string val = (string)reg_Key.GetValue("") as string;
            string[] split = val.Split(' ');
            return split[0];
        }

    }


    class FolderAccess : Register
    {

        /*
       * 작성자 : 정윤식 
       * 클래스 용도 : 폴더 권한 제어용
       * 포함 메소드 : 폴더 권한 제어 , 해제 
       * 비고 
       */

        //현재 사용자 
        private string cur_user;


        //생성자
        public FolderAccess()
        {
            //현재 사용자 확인
            cur_user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }



        //판자식 보호(타겟 문서의 폴더 경로 ) : 폴더경로를 가져와 보호  
        public void panja_protect(string target_folder_dir)
        {
            //해당 디렉토리 보호 객체
            DirectorySecurity dSecurity = Directory.GetAccessControl(target_folder_dir);


            //// Remove all------------------------------------------------------------
            //AccessRule accessRule = new FileSystemAccessRule(cur_user, 0, 0);
            //dSecurity.RemoveAccessRuleAll((FileSystemAccessRule)accessRule);
            //Directory.SetAccessControl(target_folder_dir, dSecurity);
            //// -------------------------------------------------------------------------


            /* 접근 제어 리스트 
             *      쓰기 , 덮어 쓰기 , 실행 , 삭제 -> 거부
             *      읽기 , 읽고 실행 -> 허용
             */
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                target_folder_dir,
                FileSystemRights.Write | FileSystemRights.AppendData | FileSystemRights.ExecuteFile | FileSystemRights.Delete,
                AccessControlType.Deny));
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                target_folder_dir,
                FileSystemRights.Read | FileSystemRights.ReadAndExecute,
                AccessControlType.Allow));

            // 접근제어리스트 적용 
            Directory.SetAccessControl(target_folder_dir, dSecurity);



        }

        //판자식 보호 해제(타겟 문서의 폴더 경로 ) : 폴더경로를 가져와 보호한것 모두 해제 
        public void panja_recover(string target_folder_dir)
        {

            //접근 제어 리스트
            //  모든 권한 -> 허용
            DirectorySecurity dSecurity = Directory.GetAccessControl(target_folder_dir);
            dSecurity.ResetAccessRule((new FileSystemAccessRule(
                cur_user,
                FileSystemRights.FullControl,
                AccessControlType.Allow)));

            Directory.SetAccessControl(target_folder_dir, dSecurity);

        }

        //판자식 보호 상속삭제(타겟 문서의 폴더 경로 ) : 상속 삭제 
        public void panja_inherit_delete(string target_folder_dir)
        {
            DirectoryInfo dInfo = new DirectoryInfo(target_folder_dir);
            
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            //DirectorySecurity dSecurity = Directory.GetAccessControl(target_folder_dir);
            
            dSecurity.SetAccessRuleProtection(true, false);
            Directory.SetAccessControl(target_folder_dir, dSecurity);

            dSecurity.SetAccessRule(new FileSystemAccessRule(
                "administrators",
                FileSystemRights.ReadAndExecute,
                AccessControlType.Allow));
            Directory.SetAccessControl(target_folder_dir, dSecurity);

        }


        //판자식 보호 상속삭제해제(타겟 문서의 폴더 경로 ) : 상속 삭제 
        public void panja_inherit_recover(string target_folder_dir)
        {
            DirectoryInfo dInfo = new DirectoryInfo(target_folder_dir);

            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            

            dSecurity.SetAccessRuleProtection(false, true);
          
            Directory.SetAccessControl(target_folder_dir, dSecurity);

            dSecurity.SetAccessRule(new FileSystemAccessRule(
                "administrators",
                FileSystemRights.FullControl,
                AccessControlType.Allow));

            Directory.SetAccessControl(target_folder_dir, dSecurity);

        }


        //판자식 보호 우클릭 (명령어 , 타겟 프로그램의 폴더 경로) 
        public void rightClick(string command, string target_folder_dir)
        {
            //dos 창을 사용을 위한 세팅
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

            
            //명령어에 따라 보호 or 해제 
            if (command.Equals("protect"))
            {
                //폴더 하이딩
                pro.StandardInput.Write("attrib " + target_folder_dir + " +r +s +h" + Environment.NewLine);
               
                //Warning
                //cmd 실행속도 고려
                System.Threading.Thread.Sleep(500);

                icon shortcut = new icon();            
                
                shortcut.createShortcut(Environment.CurrentDirectory, Path.GetFileName(target_folder_dir));
                //AccessAuthority auth = new AccessAuthority(target_folder_dir);
                //auth.folderSecu_Test3();
                shortcut.panja_inherit_delete(target_folder_dir);
                //MessageBox.Show("Target : " + address + "\n" + "shortcut a : " + Environment.CurrentDirectory + "\n shortcut b :" + Path.GetFileName(address));

            }
            else if (command.Equals("recover"))
            {
                AccessAuthority auth = new AccessAuthority(target_folder_dir);
                panja_recover(target_folder_dir);
            }


            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            
            //cmd 종료 
            pro.Close();


        }


    }


    class icon :FolderAccess
    {
        /*
         * 작성자 : 정윤식 
         * 클래스 용도 : 아이콘 생성용
         * 포함 메소드 : 아이콘 생성
         * 비고 :
         */

           
        //아이콘 생성 (해당 타겟의 폴더 경로 , 타겟 명)
        public void createShortcut(string target_folder_dir, string name)
        {
            //저장할 경로 //파라미터 가능한지 실험 필요
            string dir_shortcut = target_folder_dir + @"\" + name + ".lnk";

            IWshShortcut Myshortcut;
            WshShell wshShell = new WshShell();


            // 바로가기를 저장할 경로를 지정한다.
            Myshortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(dir_shortcut);
            

            // 바로가기에 프로그램의 경로를 지정한다.
            Myshortcut.TargetPath = Application.ExecutablePath;

            Myshortcut.Arguments = "1 1 " + target_folder_dir + @"\" + name;

            // 바로가기의 description을 지정한다.
            Myshortcut.Description = "Launch My Application";


            string excute_dir = Application.StartupPath;
            string cut_dir = "\\bin\\Debug";
            int data_len = excute_dir.Length - cut_dir.Length;
            string icon_dir = excute_dir.Substring(0, data_len);
            // 바로가기 아이콘을 지정한다.
            Myshortcut.IconLocation = icon_dir + @"\Properties\panja2_256px.ico";
            

            // 바로가기를 저장한다. 에러떠서 일단 주석처리해놓을게(동근)
            Myshortcut.Save();
        }

    }



}
