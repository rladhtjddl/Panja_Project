using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.IO;

namespace Panja_Project
{
    class AccessAuthority
    {
        public string dir = " ";

        public AccessAuthority(string dir)
        {
            this.dir = dir;
        }

        public void fileSecu_Test1()
        {

            FileSecurity fSecurity = File.GetAccessControl(dir);
            fSecurity.AddAccessRule(new FileSystemAccessRule("SYSTEM",
               FileSystemRights.Modify, AccessControlType.Deny));

        }

        public void folderSecu_Test1()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                "SYSTEM",
                FileSystemRights.Modify
                | FileSystemRights.Write
                | FileSystemRights.ReadPermissions,
                AccessControlType.Deny));
            Directory.SetAccessControl(dir, dSecurity);
        }
        
        public void folderSecu_Test2()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                "SYSTEM",
                FileSystemRights.FullControl,
                AccessControlType.Deny));
            Directory.SetAccessControl(dir, dSecurity);

        }

        public void folderSecu_Test3()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                "SYSTEM",
                FileSystemRights.Modify,
                AccessControlType.Deny));
            Directory.SetAccessControl(dir, dSecurity);

        }
        public void folderSecu_Recover()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                "SYSTEM",
                FileSystemRights.FullControl,
                AccessControlType.Allow));
            Directory.SetAccessControl(dir, dSecurity);

        }


        public void setDir (string dir)
        {
            this.dir = dir;
        }

        public string getDir (string dir)
        {
            return this.dir;
        }
    }
}
