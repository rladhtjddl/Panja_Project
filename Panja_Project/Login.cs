using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panja_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp form = new SignUp();
            form.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MainUI main = new MainUI();
            this.Hide();//아이디 비밀번호 체크 
            
            main.Show();
        }

        private void lnkLostID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //아이디 찾기 
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void lnkLostPW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //패스워드 찾기
        }
    }
}
