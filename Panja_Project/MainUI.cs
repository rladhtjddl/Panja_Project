using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Panja_Project
{
    public partial class MainUI : Form
    {
        private JToken json_s;

        public MainUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void local_btn_Click(object sender, EventArgs e)
        {
            Local_Explorer exp = new Local_Explorer();
            exp.ShowDialog();
        }

        private void Cloud_btn_Click(object sender, EventArgs e)
        {
            JObject json = new JObject();
            JArray jjson = new JArray();
            long count;
            string json_t;
            Filecontrol fi = new Filecontrol();
            fi.Get_json();

            using (StreamReader r = new StreamReader(@"../../Properties/file_list.Json"))
            {
                json_s = r.ReadToEnd();
                json_t = json_s.ToString();
            }

            json = JObject.Parse(json_t);
            count = json["link"].LongCount();
            string[] sum_link = new string[10000]; 
            
            for(int q=0; q< count; q++)
            {
                sum_link[q] = json["link"][q]["flink"].ToString();
                Console.WriteLine(sum_link[q]);
            }
            //동근 sum_link[] -> 0~count 까지가 경로 저장해둔거


        }

        private void settings_Click(object sender, EventArgs e)
        {

        }
        
    }
}
