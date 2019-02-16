using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace Panja_Project
{
    public partial class Local_Explorer : Form
    {
        public string freepath;

        public string host = @"54.185.231.100";
        public string username = "os";
        public string password = "tlqkf";
        public string pub_link;
        string localFileName = System.IO.Path.GetFileName(@"localfilename");
        string remoteDirectory = ".";
        public SftpClient sftp;


        internal static class NativeMethods
        {
            public const uint LVM_FIRST = 0x1000;
            public const uint LVM_GETIMAGELIST = (LVM_FIRST + 2);
            public const uint LVM_SETIMAGELIST = (LVM_FIRST + 3);

            public const uint LVSIL_NORMAL = 0;
            public const uint LVSIL_SMALL = 1;
            public const uint LVSIL_STATE = 2;
            public const uint LVSIL_GROUPHEADER = 3;

            [DllImport("user32")]
            public static extern IntPtr SendMessage(IntPtr hWnd,
                      uint msg,
                      uint wParam,
                      IntPtr lParam);

            [DllImport("comctl32")]
            public static extern bool ImageList_Destroy(IntPtr hImageList);

            public const uint SHGFI_DISPLAYNAME = 0x200;
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0;
            public const uint SHGFI_SMALLICON = 0x1;
            public const uint SHGFI_SYSICONINDEX = 0x4000;

            [StructLayout(LayoutKind.Sequential)]
            public struct SHFILEINFO
            {
                public IntPtr hIcon;
                public int iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260 /* MAX_PATH */)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            };

            [DllImport("shell32")]
            public static extern IntPtr SHGetFileInfo(string pszPath,
                      uint dwFileAttributes,
                      ref SHFILEINFO psfi,
                      uint cbSizeFileInfo,
                      uint uFlags);

            [DllImport("uxtheme", CharSet = CharSet.Unicode)]
            public static extern int SetWindowTheme(IntPtr hWnd,
                      string pszSubAppName,
                      string pszSubIdList);
        }


        public Local_Explorer()
        {
            InitializeComponent();
            listView1.View = View.LargeIcon;
        //    SettingListVeiw(@"C:\");
              SettingListVeiw(Environment.CurrentDirectory);


        }

        public Local_Explorer(string command, string targetAddress)
        {
            string getURI = targetAddress; //전달받은 폴더경로 지금은 하드코딩해둠

            if (getURI != null)
            { //전달받은 경로가 있으면 그쪽으로 탐색기 실행
                SettingListVeiw(getURI);
                textBox1.Text = getURI;
            }
        }

        private void Local_Explorer_Load(object sender, EventArgs e)
        {

            /*
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
            */

            // 프로퍼티 경로를 지정하기 위한 코드 
            // 레지스트리에서 절대경로를 통해 ㅡ프로퍼티 파일 위치 탐색 
            Register rgs = new Register();
            string[] absExcuteDir = rgs.getAbsDir().Split('\\');
            
            string absPro="";
            for(int k = 0; k < absExcuteDir.Length-3; k++)
            {
                absPro += absExcuteDir[k];
                absPro += @"\";
            }
            //absPro += @"Properties\test.txt";
            absPro = @"C:\Temp\test.txt";


            //string absTemp = @"C:\Users\rooto\Source\Repos\Panja_Project\Panja_Project\Properties";
          
            //삭제 
            //absPro = @"C:\Program Files (x86)\Default Company Name\SetupSample\Panja\Properties\test.txt";
            
            //확인용
            MessageBox.Show("Path : " + absPro);


            string[] allLines = File.ReadAllLines(absPro, Encoding.Default);

            int i;
            for (i = 0; i < allLines.Length; i++)
            {
                TreeNode rootNode = new TreeNode(allLines[i]);
                //rootNode.Text = allLines[i];
                rootNode.ImageIndex = 2;
                rootNode.SelectedImageIndex = 2;
                treeView1.Nodes.Add(rootNode);
                Fill(rootNode, allLines[i]);
            }


            listView1.FullRowSelect = true;
        }

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
                //MessageBox.Show("ERROR : " + ex.Message);
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
                //기존의 파일 목록 제거
                listView1.Items.Clear();
                //현재 경로를 표시
                textBox1.Text = sFullPath;
                pub_link = textBox1.Text;
                freepath = "C:\\" + sFullPath.Substring(4);


                DirectoryInfo dir = new DirectoryInfo(sFullPath);



                // Obtain a handle to the system image list. 
                NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
                IntPtr hSysImgList = NativeMethods.SHGetFileInfo("",
                            0,
                            ref shfi,
                            (uint)Marshal.SizeOf(shfi),
                            NativeMethods.SHGFI_SYSICONINDEX
                            | NativeMethods.SHGFI_LARGEICON);
                //Debug.Assert(hSysImgList != IntPtr.Zero); // cross our fingers and hope to succeed! 

                // Set the ListView control to use that image list. 
                IntPtr hOldImgList = NativeMethods.SendMessage(listView1.Handle,
                            NativeMethods.LVM_SETIMAGELIST,
                            NativeMethods.LVSIL_NORMAL,
                            hSysImgList);

                // If the ListView control already had an image list, delete the old one. 
                if (hOldImgList != IntPtr.Zero)
                {
                    NativeMethods.ImageList_Destroy(hOldImgList);
                }

                // Set up the ListView control's basic properties. 
                // Put it in "Details" mode, create a column so that "Details" mode will work, 
                // and set its theme so it will look like the one used by Explorer. 
                listView1.View = View.LargeIcon;
                //listView1.Columns.Add("Name", 100);
                NativeMethods.SetWindowTheme(listView1.Handle, "Explorer", null);

                // Get the items from the file system, and add each of them to the ListView, 
                // complete with their corresponding name and icon indices. 
                string[] s = Directory.GetFileSystemEntries(sFullPath);
                foreach (string file in s)
                {
                    IntPtr himl = NativeMethods.SHGetFileInfo(file,
                              0,
                              ref shfi,
                              (uint)Marshal.SizeOf(shfi),
                              NativeMethods.SHGFI_DISPLAYNAME
                               | NativeMethods.SHGFI_SYSICONINDEX
                               | NativeMethods.SHGFI_LARGEICON);
                    //Debug.Assert(himl == hSysImgList); // should be the same imagelist as the one we set 
                    listView1.Items.Add(shfi.szDisplayName, shfi.iIcon);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
            }
            //treeView1.Nodes[0].Expand();
        }

        private void listView1_Click(object sender, EventArgs e)
        {

        }

        private void listv1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                

                //오른쪽 메뉴를 만듭니다 
                ContextMenu m = new ContextMenu();
                //메뉴에 들어갈 아이템을 만듭니다
                MenuItem m1 = new MenuItem();
                MenuItem m2 = new MenuItem();

                m1.Text = "업로드하기";
                m2.Text = "다운로드하기";


                //업로드하기 클릭시 이벤트
                m1.Click += (senders, es) => {

                    //ftp연결
                    sftp = new SftpClient(host, username, password);
                    sftp.Connect();

                    Console.WriteLine("업로드클릭");

                    Console.WriteLine(pub_link);
                    

                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog();
                    string select_path = dialog.SelectedPath;
                    string select_name;
                    DirectoryInfo dirinfo = new DirectoryInfo(select_path);
                    select_name = dirinfo.Name;
           
                   
                    
                    sftp.CreateDirectory("./folder1/"+ select_name );
                    UploadDirectory(sftp, select_path , "./folder1/"+ select_name);

                };




                m.MenuItems.Add(m1);
                m.MenuItems.Add(m2);

                m.Show(listView1, new Point(e.X, e.Y));

            }
        }

        void UploadDirectory(SftpClient client, string localPath, string remotePath)
        {
            Console.WriteLine("Uploading directory {0} to {1}", localPath, remotePath);

            IEnumerable<FileSystemInfo> infos =
                new DirectoryInfo(localPath).EnumerateFileSystemInfos();
            foreach (FileSystemInfo info in infos)
            {
                if (info.Attributes.HasFlag(FileAttributes.Directory))
                {
                    string subPath = remotePath + "/" + info.Name;
                    if (!client.Exists(subPath))
                    {
                        client.CreateDirectory(subPath);
                    }
                    UploadDirectory(client, info.FullName, remotePath + "/" + info.Name);
                }
                else
                {
                    using (Stream fileStream = new FileStream(info.FullName, FileMode.Open))
                    {
                        Console.WriteLine(
                            "Uploading {0} ({1:N0} bytes)",
                            info.FullName, ((FileInfo)info).Length);

                        client.UploadFile(fileStream, remotePath + "/" + info.Name);
                    }
                }
            }
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

                FileAttributes attr = File.GetAttributes(processPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    SettingListVeiw(processPath);
                }
                else
                {   
                    Process.Start("../../Properties\\detect_ransom.exe", processPath);
                }


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

        private void button2_Click(object sender, EventArgs e)
        {
            //코드확인 필요 ShowDialog

            //foler_imsi ii = new foler_imsi();
            //ii.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Local_Minus minus = new Local_Minus();
            minus.ShowDialog();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }


    }
}