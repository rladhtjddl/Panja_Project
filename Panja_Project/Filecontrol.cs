using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panja_Project
{
    public struct file_list
    {
        public string Name;
        public string Id;
        public string By;
        public file_list(string name, string id, string by)
        {
            Name = name;
            Id = id;
            By = by;
        }
    }
        
    public struct file_info
    {
        public string fname;
        public string flink;
        public string fbyte;
        public string ftype;
        public string ftime;

        public file_info(string f_link)
        {
            FileInfo fileInfo = new FileInfo(f_link);
            fname = fileInfo.Name.ToString();
            flink = fileInfo.FullName.ToString();
            fbyte = fileInfo.Length.ToString();
            ftype = fileInfo.Extension.ToString();
            ftime = fileInfo.LastWriteTime.ToString();
        }
    }


    public struct folder_back
    {
        public string fname;
        public string full_link;
        public folder_back(string flink)
        {
            fname = "dd";
            full_link = "aa";
        }
    }

    class Filecontrol
    {
       

    }
}
