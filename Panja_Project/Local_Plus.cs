using System;
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
    public partial class Local_Plus : Form
    {
        

        public Local_Plus()
        {
            InitializeComponent();
        }

        private void Local_Plus_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// 디렉토리 선택
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>

            // 폴더브라우저 객체 생성

            //string[] files = Directory.GetFiles(dir, "*");


            // 파일리스트 저장 
            string[] files = Directory.GetFiles("C:\\Users\\J3N_JAN6\\Downloads", "*");

           
            List_own.Columns.Add("a", 50);
            List_own.Columns.Add("b", 50);
            List_own.Columns.Add("c", 50);
            List_own.Columns.Add("d", 50);

            /*
            string[] aa = { "aa1", "aa2", "a3", "a4 "};
            ListViewItem newitem = new ListViewItem(aa);
            List_own.Items.Add(newitem);

            newitem = new ListViewItem(new string[] { "bb1", "bb2", "bb3", "b4" });
            List_own.Items.Add(newitem);
            */




            // 파일 개수만큼 리스트에 추가

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);




                ListViewItem item = new ListViewItem(fileInfo.Name, 0);
                item.SubItems.Add(fileInfo.FullName.ToString());
                item.SubItems.Add(fileInfo.Length.ToString()); // 파일크기
                item.SubItems.Add(fileInfo.Extension.ToString()); // 파일 종류
                item.SubItems.Add(fileInfo.LastWriteTime.ToString()); // 파일 마지막 수정날짜

                List_own.Items.Add(item);

            }





        }

        private void List_own_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
