using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace Panja_Project
{
    public partial class Local_Minus : Form
    {
        
        public int length;
        public string[] new_list = new string[100];


        public String[] subed_Folder = new string[10000];

        public Local_Minus()
        {
            InitializeComponent();
        }

        public void get_list(string[] list, int count)
        {
            length = count;
            new_list = list;
        }


        private void Local_Minus_Load(object sender, EventArgs e)
        {
            //폼실행시 기준 경로가 파일이라 ㄱㅊ 
            //string[] allLines = File.ReadAllLines(@"C:\Temp\test.txt", Encoding.Default);
            string[] allLines = new_list;

            hex_handler hex = new hex_handler();

            int hex_length = hex.hex_length();
            int start_point = 0x500;

            //string[] allLines = new string[j];

            allLines[0] = hex.hex_read(0x500);

            for (int y = 0; y < hex_length; y++)
            {
                allLines[y] = hex.hex_read(start_point);
                start_point += 0x100;
            }

            MessageBox.Show("얻은 길이 " + hex.hex_length());

            int i;
            for (i = 0; i < hex_length; i++)
            {
                if (!allLines[i].Equals(""))
                {
                    FolderAccess access = new FolderAccess();
                    //access.panja_inherit_recover(allLines[i]);
                }
                TreeNode rootNode = new TreeNode(allLines[i]);
                //rootNode.Text = allLines[i];
                rootNode.ImageIndex = 2;
                rootNode.SelectedImageIndex = 2;
                List_own.Nodes.Add(rootNode);
            }



            List_own.FullRowSelect = true;
            /*
            int i;
            for (i = 0; i < allLines.Length; i++)
            {
                TreeNode rootNode = new TreeNode(allLines[i]);
                //rootNode.Text = allLines[i];
                rootNode.ImageIndex = 2;
                rootNode.SelectedImageIndex = 2;
                List_own.Nodes.Add(rootNode);
                //Fill(rootNode, allLines[i]);
            }
            */
        }
        /* 하위를 해제할수는 없자나
        private void Fill(TreeNode dirNode, String url)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(url);
                //드라이브의 하위 폴더 추가
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(dirItem.Name);
                    newNode.ImageIndex = 2;
                    newNode.SelectedImageIndex = 2;
                    dirNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
            }
        }

        private void Fill(TreeNode dirNode)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(dirNode.FullPath);
                //드라이브의 하위 폴더 추가
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(dirItem.Name);
                    newNode.ImageIndex = 2;
                    newNode.SelectedImageIndex = 2;
                    dirNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
            }
        }
        /// <summary>
        /// 트리가 확장되기 전에 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                e.Node.ImageIndex = 3;
                e.Node.SelectedImageIndex = 3;
                Fill(e.Node);
            }
        }
        /// <summary>
        /// 트리가 닫히기 전에 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }
        */
        private void btn_plus_Click(object sender, EventArgs e)
        {
            if (List_own.SelectedNode.FullPath != null)
            {
                String path = List_own.SelectedNode.FullPath;
                List_go.Items.Add(path);
            }
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            if (List_go.SelectedItems[0] != null)
                List_go.Items.Remove(List_go.SelectedItems[0]);
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (List_go.Items.Count != 0)
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

                hex_handler hex = new hex_handler();

                int i;

                length = hex.hex_length();

                //added 폴더 
                int lastnum = List_go.Items.Count;


                for (i = 0; i < length; i++)
                {
                    for (int z = 0; z < lastnum; z++)
                    {
                        if (new_list[i] == List_go.Items[z].Text)
                        {
                            new_list[i] = null;
                            hex.hex_length_set(hex.hex_length() - 1);
                        }
                    }

                    //윤식 : 이부분 input ACL 
                    FolderAccess rgs = new FolderAccess();

                    if(new_list[i] != null)
                        rgs.panja_inherit_recover(new_list[i]);
                    pro.StandardInput.Write("attrib " + '\u0022' + subed_Folder[i] + '\u0022' + " -r -s -h" + Environment.NewLine);
                }

                //length = hex.hex_length();

                int start_point = 0x500 - 0x001;

                for (int j = 0; j < length; j++)
                {
                    if (new_list[j] == null)
                        continue;
                    hex.hex_write1(new_list[j], start_point);
                    start_point += 0x100;
                    //System.IO.File.AppendAllText(@"C:\Temp\test.txt", Environment.NewLine + folder_path[i] , Encoding.Default);
                }


                pro.StandardInput.Close();
                pro.Close();
                this.Close();
            }
            

        }
    }
}