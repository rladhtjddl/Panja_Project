using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
            // (1) IP 주소와 포트를 지정하고 TCP 연결 
            TcpClient tc = new TcpClient("54.185.231.100", 4000);
            //TcpClient tc = new TcpClient("localhost", 7000);
            string endpoint = "Lend";
            string msg = "login_user" + endpoint;

            //정보입력
            string info_ID, info_PW;


            info_ID = txtID.Text.ToString() + endpoint;
            info_PW = txtPW.Text.ToString() + endpoint;
            

            msg = msg + info_ID + info_PW;

            Console.WriteLine("인포오오 : " + msg);

            byte[] buff = Encoding.ASCII.GetBytes(msg);

            // (2) NetworkStream을 얻어옴 
            NetworkStream stream = tc.GetStream();

            // (3) 스트림에 바이트 데이타 전송
            stream.Write(buff, 0, buff.Length);

            // (4) 서버가 Connection을 닫을 때가지 읽는 경우
            byte[] outbuf = new byte[1024];
            int nbytes;
            MemoryStream mem = new MemoryStream();
            while ((nbytes = stream.Read(outbuf, 0, outbuf.Length)) > 0)
            {
                mem.Write(outbuf, 0, nbytes);
            }
            byte[] outbytes = mem.ToArray();
            mem.Close();

            //(4-1) 서버가 ok를 보냈을시
            if (Encoding.ASCII.GetString(outbytes) == "ok")
            {
                MessageBox.Show("회원정보 확인");

                // (5) 스트림과 TcpClient 객체 닫기
                stream.Close();
                tc.Close();

                Console.WriteLine(Encoding.ASCII.GetString(outbytes));
                
                MainUI main = new MainUI(txtID.Text);
                main.Show();
            }
            else if(Encoding.ASCII.GetString(outbytes) == "no")
            {
                MessageBox.Show("회원정보 불일치\n 아이디와 비밀번호를 확인하세요");
                // (5) 스트림과 TcpClient 객체 닫기
                stream.Close();
                tc.Close();

                Console.WriteLine(Encoding.ASCII.GetString(outbytes));




            }



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
