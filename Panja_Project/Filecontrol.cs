﻿using System;
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
        public string fbyte;
        public string ftype;
        public string ftime;

        public file_info(string f_link)
        {
            FileInfo fileInfo = new FileInfo("C:\\Temp\\file_list.json");
            fname = fileInfo.FullName.ToString();
            fbyte = fileInfo.Length.ToString();
            ftype = fileInfo.Extension.ToString();
            ftime = fileInfo.LastWriteTime.ToString();
        }
    }

    class Filecontrol
    {
       

    }
}