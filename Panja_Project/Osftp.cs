using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Windows.Forms;

namespace Panja_Project
{
    class Osftp
    {
        //파일 뿌릴 구조체
        public struct file_list
        {
            public string[] Name;
            public file_list(string[] name)
            {
                Name = name;
            }
        }

        //ftp서버 연결
        static string host = @"54.185.231.100";
        static string username = "os";
        static string password = "tlqkf";
        static string localFileName = System.IO.Path.GetFileName(@"localfilename");
        SftpClient sftp = new SftpClient(host, username, password);

        //처음에 컨넥 해줄것
        public void connect()
        {
        sftp.Connect();
        }
        

        //현재지점 파일들 뿌려주기
        public int getdir()
        {
            file_list FL = new file_list();
            string remoteDirectory = ".";
            var files = sftp.ListDirectory(remoteDirectory);
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
                FL.Name[0] = file.FullName;
            }

            return FL.Name[];
        }
    }
}
