﻿using System;
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
    public partial class Local_Explorer : Form
    {
        string getURI = "C:\\Temp"; //전달받은 폴더경로 지금은 하드코딩해둠

        public Local_Explorer()
        {
            InitializeComponent();
            listView1.View = View.LargeIcon;

            if(getURI != null){ //전달받은 경로가 있으면 그쪽으로 탐색기 실행
                SettingListVeiw(getURI);
                textBox1.Text = getURI;
                }

        }

        private void Local_Explorer_Load(object sender, EventArgs e)
        {
            //현재 로컬 컴퓨터에 존재하는 드라이브 정보 검색하여 트리노드에 추가
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo dname in allDrives)
            {
                if (dname.DriveType == DriveType.Fixed)
                {
                    if (dname.Name == @"C:\")
                    {
                        TreeNode rootNode = new TreeNode(dname.Name);
                        rootNode.ImageIndex = 0;
                        rootNode.SelectedImageIndex = 0;
                        treeView1.Nodes.Add(rootNode);
                        Fill(rootNode);
                    }
                    else
                    {
                        TreeNode rootNode = new TreeNode(dname.Name);
                        rootNode.ImageIndex = 1;
                        rootNode.SelectedImageIndex = 1;
                        treeView1.Nodes.Add(rootNode);
                        Fill(rootNode);
                    }
                }
            }

            //첫번째 노드 확장
            treeView1.Nodes[0].Expand();
            listView1.FullRowSelect = true;

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
        /// <summary>
        /// 트리를 마우스로 클릭할 때 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SettingListVeiw(e.Node.FullPath);
        }

        /// <summary>
        /// 오른쪽 ListView를 그린다.
        /// </summary>
        /// <param name="sFullPath"></param>
        private void SettingListVeiw(string sFullPath)
        {
            
            try
            {
                //기존의 파일 목록 제거
                listView1.Items.Clear();
                //현재 경로를 표시
                    textBox1.Text = "C:\\" + sFullPath.Substring(4);


                DirectoryInfo dir = new DirectoryInfo(sFullPath);
                

                int DirectCount = 0;
                //하부 데렉토르 보여주기
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    //하부 디렉토리가 존재할 경우 ListView에 추가
                    //ListViewItem 객체를 생성
                    ListViewItem lsvitem = new ListViewItem();

                    //생성된 ListViewItem 객체에 똑같은 이미지를 할당
                    lsvitem.ImageIndex = 2;
                    lsvitem.Text = dirItem.Name;

                    //아이템을 ListView(listView1)에 추가
                    listView1.Items.Add(lsvitem);

                    listView1.Items[DirectCount].SubItems.Add(dirItem.CreationTime.ToString());
                    listView1.Items[DirectCount].SubItems.Add("폴더");
                    listView1.Items[DirectCount].SubItems.Add(dirItem.GetFiles().Length.ToString() + " files");
                    DirectCount++;
                }

                //디렉토리에 존재하는 파일목록 보여주기
                FileInfo[] files = dir.GetFiles();
                int Count = 0;
                foreach (FileInfo fileinfo in files)
                {
                    ListViewItem lsvitem = new ListViewItem();
                    lsvitem.ImageIndex = 4;
                    lsvitem.Text = fileinfo.Name;
                    listView1.Items.Add(lsvitem);

                    if (fileinfo.LastWriteTime != null)
                    {
                        listView1.Items[Count].SubItems.Add(fileinfo.LastWriteTime.ToString());
                    }
                    else
                    {
                        listView1.Items[Count].SubItems.Add(fileinfo.CreationTime.ToString());
                    }
                    listView1.Items[Count].SubItems.Add(fileinfo.Attributes.ToString());
                    listView1.Items[Count].SubItems.Add(fileinfo.Length.ToString());
                    Count++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
            }
            //treeView1.Nodes[0].Expand();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)  //해당 아이템 더블클릭하면 실행함
        {

            if (listView1.SelectedItems.Count == 1)
            {
                string processPath;
                string pathnow = textBox1.Text;
               
                

                if (listView1.SelectedItems[0].Text.IndexOf("\\") > 0)
                    processPath = listView1.SelectedItems[0].Text;
                else
                    processPath = pathnow + "\\" + listView1.SelectedItems[0].Text;

                Process.Start("explorer.exe", processPath);
            }
        }

      

        private void button1_Click_1(object sender, EventArgs e)
        {
            Local_Plus plus = new Local_Plus();
            plus.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}