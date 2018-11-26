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
        //target data folder 
        private string dir = " ";
        //current username
        private string USER;


        //Constructor 
        public AccessAuthority(string dir)
         {
            //dir ,USER mapping
            this.dir = dir;
            USER = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        }
 

        //test 01 Modify , Write , ReadPermission OFF
        public void folderSecu_Test1()
        {
            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                USER,
                FileSystemRights.Modify
                | FileSystemRights.Write
                | FileSystemRights.ReadPermissions,
                AccessControlType.Deny));
            Directory.SetAccessControl(dir, dSecurity);
        }

        //test 02 All Control OFF
        public void folderSecu_Test2()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                USER,
                FileSystemRights.FullControl,
                AccessControlType.Deny));
            Directory.SetAccessControl(dir, dSecurity);

        }

        //test 03 Modify OFF
        public void folderSecu_Test3()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                USER,
                FileSystemRights.Write| FileSystemRights.AppendData |FileSystemRights.ExecuteFile |FileSystemRights.Delete,
                AccessControlType.Deny));
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                USER,
                FileSystemRights.Read | FileSystemRights.ReadAndExecute,
                AccessControlType.Allow));
            Directory.SetAccessControl(dir, dSecurity);

        }

        // Recover control 
        public void folderSecu_Recover()
        {

            DirectorySecurity dSecurity = Directory.GetAccessControl(dir);
            dSecurity.ResetAccessRule((new FileSystemAccessRule(
                USER,
                FileSystemRights.FullControl,
                AccessControlType.Allow)));

            Directory.SetAccessControl(dir, dSecurity);
          

        }


        public void setDir(string dir)
        {
            this.dir = dir;
        }

        public string getDir(string dir)
        {
            return this.dir;
        }
    }
}
