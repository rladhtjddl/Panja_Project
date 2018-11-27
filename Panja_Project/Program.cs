using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panja_Project
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Regedit rdg = new Regedit();
            string dir = Environment.CurrentDirectory + "\\Panja_Project.exe";


            int argsCount = args.Length;

            rdg.RegistryChecker(dir);

            if (argsCount == 2)
            {
                string command = args[1];
                string targetAddress = args[0];
                //rdg.RegistryChecker(targetAddress);
                if (command.Equals("protect"))
                {

                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new foler_imsi(command, targetAddress));
                Application.Run(new Local_Explorer(command, targetAddress));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
 
            Application.Run(new Local_Explorer());
            

        }
    }
}
