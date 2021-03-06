﻿using IWshRuntimeLibrary;
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

        public string getPropertiesDir()
        {
            string excute_dir = Application.StartupPath;
            //----test line
            string cut_dir = "\\bin\\Debug";


            int data_len = excute_dir.Length - cut_dir.Length;
            //string sub_dir = excute_dir.Substring(0, data_len);

            //----
            
            char[] sp = @"\".ToCharArray();
            string[] cutting_dir = excute_dir.Split(sp);
            string sub_dir ="";
            for(int i = 0; i<cutting_dir.Length-2; i++)
            {
                sub_dir += cutting_dir[i]+@"\";
            }
            string properties_dir = sub_dir + @"Properties\";

          //  MessageBox.Show("dir is " + properties_dir.ToString());
            return properties_dir;
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


        ////디렉토리 내부파일 모두 쓰기 권한 막기
        //protected void fileclear (string dir)
        //{
        //    string[] target_files = Directory.GetFiles(dir);
        //    //파일마다 초기권한 삭제 진행
        //    foreach (string filename in target_files)
        //    {

        //        FileSecurity fSecurity = System.IO.File.GetAccessControl(filename);

        //        //모든 파일 권한 삭제 
        //        AuthorizationRuleCollection rules2 = fSecurity.GetAccessRules(true, false, typeof(System.Security.Principal.NTAccount));
        //        foreach (FileSystemAccessRule rule in rules2)
        //            fSecurity.RemoveAccessRule(rule);

        //        fSecurity.SetAccessRule(new FileSystemAccessRule(
        //            cur_user,
        //            FileSystemRights.Write,
        //            AccessControlType.Deny));
        //        System.IO.File.SetAccessControl(filename, fSecurity);

        //    }
        //}

         
  


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

            
 
            ////상속해제

            dSecurity.SetAccessRuleProtection(true, false);
            //////세팅
            //Directory.SetAccessControl(target_folder_dir, dSecurity);


            //디폴트 권한 클리어
            AuthorizationRuleCollection rules = dSecurity.GetAccessRules(true, false, typeof(System.Security.Principal.NTAccount));
            foreach (FileSystemAccessRule rule in rules)
                dSecurity.RemoveAccessRule(rule);


            ////세팅
            //Directory.SetAccessControl(target_folder_dir, dSecurity);


            //ACL 설정
            dSecurity.AddAccessRule(new FileSystemAccessRule(
            cur_user,
            FileSystemRights.ReadAndExecute,
            inheritanceFlags:InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            propagationFlags:PropagationFlags.None,
            AccessControlType.Allow));



            ////이폴더 및 하위폴더만 
            //inheritanceFlags: InheritanceFlags.ContainerInherit,
            //    propagationFlags: PropagationFlags.NoPropagateInherit


            // //ACL 설정
            // dSecurity.SetAccessRule(new FileSystemAccessRule(
            // cur_user,
            // FileSystemRights.ReadAndExecute,
            // //하위폴더 및 파일만  
            // inheritanceFlags: InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit ,
            // propagationFlags: PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly | PropagationFlags.None,
            // AccessControlType.Allow));




           dSecurity.SetAccessRule(new FileSystemAccessRule(
           cur_user,
           FileSystemRights.Delete | FileSystemRights.Write,
           //하위
           inheritanceFlags: InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            propagationFlags: PropagationFlags.None,
           AccessControlType.Deny));

            //세팅
            Directory.SetAccessControl(target_folder_dir, dSecurity);



            //내부 디폴트 권한 수정 

            //파일 권한조정 
            //fileclear(target_folder_dir);

         

            
        }


        //판자식 보호 상속삭제해제(타겟 문서의 폴더 경로 ) : 상속 삭제 
        public void panja_inherit_recover(string target_folder_dir)
        {
            DirectoryInfo dInfo = new DirectoryInfo(target_folder_dir);

            DirectorySecurity dSecurity = dInfo.GetAccessControl();



            //디폴트 권한 클리어
            AuthorizationRuleCollection rules = dSecurity.GetAccessRules(true, false, typeof(System.Security.Principal.NTAccount));
            foreach (FileSystemAccessRule rule in rules)
                dSecurity.RemoveAccessRule(rule);



            Directory.SetAccessControl(target_folder_dir, dSecurity);

            //상속 다시 추가 
            dSecurity.SetAccessRuleProtection(false, true);
          
            Directory.SetAccessControl(target_folder_dir, dSecurity);

            Directory.SetAccessControl(target_folder_dir, dSecurity);

        }
        
        //판자식 파일 접근 (타겟 문서의 폴더 경로 ) : 상속 삭제 씨발필요없어 
        public void panja_file_access(string target_folder_dir)
        {

            //File test  
            string target_file_dir = target_folder_dir + @"\" + "TEXT SAMPLE .txt";
            FileInfo fInfo = new FileInfo(target_file_dir);

            FileSecurity fSecurity = fInfo.GetAccessControl();


            fSecurity.SetAccessRule(new FileSystemAccessRule(
                cur_user,
                FileSystemRights.Modify,
                AccessControlType.Allow));

            System.IO.File.SetAccessControl(target_file_dir, fSecurity);

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

                hex_handler new_hex = new hex_handler();

                int length = new_hex.hex_length();
                int start_point = 0x499;
                for (int h = 0; h < length; h++) {
                    start_point += 0x100;
                }
                new_hex.hex_write(target_folder_dir,start_point);
                new_hex.hex_length_set(length + 1);


            }
            else if (command.Equals("recover"))
            {
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


            //모듈화
            //string excute_dir = Application.StartupPath;
            //string cut_dir = "\\bin\\Debug";
            //int data_len = excute_dir.Length - cut_dir.Length;
            //string icon_dir = excute_dir.Substring(0, data_len);

            // 바로가기 아이콘을 지정한다.
            Myshortcut.IconLocation = getPropertiesDir() + @"panja2_256px.ico";
            

            // 바로가기를 저장한다.
            Myshortcut.Save();
        }




    }



}
