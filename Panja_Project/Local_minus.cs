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
        public String[] subed_Folder = new string[10000];

        public Local_Minus()
        {
            InitializeComponent();
        }

        private void Local_Minus_Load(object sender, EventArgs e)
        {
            //폼실행시 기준 경로가 파일이라 ㄱㅊ 
            string[] allLines = File.ReadAllLines(@"../../Properties\test.txt", Encoding.Default);

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


            List_own.FullRowSelect = true;
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
        }

        private void button3_Click(object sender, EventArgs e)
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

            int i;
            //added 폴더 
            int lastnum = List_go.Items.Count;
            for (i = 0; i < lastnum; i++)
            {
                //added_Folder : 해당 보호폴더 첫 헤더 폴더 (타겟폴더)
                subed_Folder[i] = List_go.Items[i].Text;

                //윤식 : 이부분 input ACL 

                FolderAccess rgs = new FolderAccess();
                rgs.panja_recover(subed_Folder[i]);

                pro.StandardInput.Write("attrib " + '\u0022' + subed_Folder[i] + '\u0022' + " -r -s -h" + Environment.NewLine);
            }
            pro.StandardInput.Close();

            pro.Close();

        }
    }
}