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
            Cloud_Explorer cld = new Cloud_Explorer();
            cld.ShowDialog();
            

        }

        private void settings_Click(object sender, EventArgs e)
        {

        }
        
    }
}
