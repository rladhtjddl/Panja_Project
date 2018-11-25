using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;


namespace Panja_Project
{
    class Shortcut
    {

        private string dir_origin; 


        private Shortcut(String dir_origin)
        {

            this.dir_origin = dir_origin;
         


        }

        //Create shortcut  dir : create shortcut in dir  name : shortcut name
        public void createShortcut(string dir,string name)
        {
            //저장할 경로 
            string dir_shortcut =  dir +@"\" + name+".lnk";

            IWshShortcut Myshortcut;
            WshShell wshShell = new WshShell();
         
            //// 바로가기를 저장할 경로를 지정한다.
            Myshortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(dir_shortcut);

            //// 바로가기에 프로그램의 경로를 지정한다.
            Myshortcut.TargetPath = Application.ExecutablePath;

            //// 바로가기의 description을 지정한다.
            Myshortcut.Description = "Launch My Application";

            // 바로가기 아이콘을 지정한다.
            Myshortcut.IconLocation = Application.StartupPath + @"\app.ico";

            // 바로가기를 저장한다.
            Myshortcut.Save();
        }


        private static class LazyHolder
        {
            public static String dir;
            public static Shortcut INSTANCE = new Shortcut(dir);
        }

        public static Shortcut getInstance(String dir)
        {
            LazyHolder.dir = dir;
            return LazyHolder.INSTANCE;
        }

        public static Shortcut getInstance()
        {
            return LazyHolder.INSTANCE;
        }

    }
}
