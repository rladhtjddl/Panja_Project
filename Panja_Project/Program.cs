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
            //폴더 권한 제어 객체 생성
            FolderAccess rgs = new FolderAccess();
            string total_dir = Environment.CurrentDirectory + "\\Panja_Project.exe";

            //args(파라미터 갯수 체크 )
            int argsCount = args.Length;

            //레지스트 체크 및  등록
            rgs.RegistryChecker(total_dir);

            //파라미터 2개 존재시 --> 우클릭 했을 케이스 
            if (argsCount == 2)
            {

                //Switch Checker & data confirm
                MessageBox.Show("this is 2");
                MessageBox.Show("[0] :" + args[0]+"[1] : "+args[1] );

                //해당 명령을 우클릭에서 처리 
                string command = args[1];
                string target_folder_dir = args[0];
                //rdg.RegistryChecker(targetAddress);
                rgs.rightClick(command, target_folder_dir);
            
            }
            //파라미터 3개 존재시 --> 바로가기 더블 클릭 케이스 
            else if (argsCount == 3)
            {


                MessageBox.Show("this is 3");
                MessageBox.Show("[0] :" + args[0] + " [1] :" + args[1]+" [2] :"+args[2]);


                //파라미터 체크용 

                string sample1 = args[0];
                string sample2 = args[1];
                string sample3 = args[2];
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Local_Explorer());



            }
            //우클릭이 아닌 일반적인 실행의 경우
            else
            {

                MessageBox.Show("this is exe");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new MainUI());

            }

        
            

        }
    }
}
