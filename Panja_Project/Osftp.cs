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
            string remoteDirectory = ".";
            var files = sftp.ListDirectory(remoteDirectory);
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }

            return 0;
        }
    }
}
