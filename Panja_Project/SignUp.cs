using Newtonsoft.Json.Linq;
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
    public partial class SignUp : Form
    {

        private Boolean idchecker = false; 

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            
            int current_year = date.Year;

            string[] array_year= new string[current_year - 1950+1];
            string[] array_month = new string[12];
            string[] array_day = new string[31];

            //1950 ~ Current Year
            for(int i = 1950; i <= current_year; i++)
            {
                array_year[i-1950] = i.ToString();
            }

            //Month
            for(int i = 1; i <=12; i++)
            {
                array_month[i - 1] = i.ToString();
            }

            //Year 
            for(int i = 1; i <=31; i++)
            {
                array_day[i - 1] = i.ToString();
            }

            // 각 콤보박스에 데이타를 초기화
            cboxYear.Items.AddRange(array_year);
            cboxMonth.Items.AddRange(array_month);
            cboxDay.Items.AddRange(array_day);


            // 처음 선택값 지정. 첫째 아이템 선택
            cboxYear.SelectedIndex = 0;
            cboxMonth.SelectedIndex = 0;
            cboxDay.SelectedIndex = 0;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //빈칸체크
            if (!txtID.Text.Equals("") && !txtPW.Equals("") && !txtPWconfirm.Equals("") 
                && !txtEmail.Equals("") && !txtCode.Equals("") && !txtName.Equals("")
                && (rbtnMan.Checked || rbtnWoman.Checked) && idchecker)
            {
                if(txtPW.Text.ToString().Equals(txtPWconfirm.Text.ToString()))
                {
                    
                    // (1) IP 주소와 포트를 지정하고 TCP 연결 
                    TcpClient tc = new TcpClient("54.185.231.100", 4000);
                    //TcpClient tc = new TcpClient("localhost", 7000);
                    string endpoint = "Lend";
                    string msg = "signup" + endpoint;

                    //정보입력
                    string info_ID, info_PW, info_Name, info_Email, info_Birth, info_Sex;


                    info_ID = txtID.Text.ToString() + endpoint;
                    info_PW = txtPW.Text.ToString() + endpoint;
                    info_Name = txtName.Text.ToString() + endpoint;
                    info_Birth = cboxYear.Text.ToString() + @"/" + cboxMonth.Text.ToString() + @"/" + cboxDay.Text.ToString() + endpoint;
                    info_Email = txtEmail.Text.ToString() + endpoint;
                    if (rbtnMan.Enabled)   //성별한테는 endpoint 안붙힘.
                    {
                        info_Sex = "M" + endpoint;
                    }
                    else
                    {
                        info_Sex = "W" + endpoint;
                    }

                    msg = msg + info_ID + info_PW + info_Name + info_Birth + info_Email + info_Sex;

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
                        MessageBox.Show("회원가입 완료");
                        Close();
                    }
                    // (5) 스트림과 TcpClient 객체 닫기
                    stream.Close();
                    tc.Close();

                    Console.WriteLine(Encoding.ASCII.GetString(outbytes));



                }
                else
                {
                    MessageBox.Show("비밀번호가 일치하지않습니다.");
                }
            }
            else
            {
                MessageBox.Show("빈칸을 모두 채우거나, 아이디 체크를 하세요");
            }

            
            



           
            

            


            //if(!txtID.Text.Equals("") && !txtPW.Equals("") && !txtPWconfirm.Equals("")
            //    && !txtEmail.Equals("") && !txtCode.Equals("") && !txtName.Equals("")
            //    &&(rbtnMan.Checked || rbtnWoman.Checked)&&idchecker)
            //{
            //    //signup OK
            //    txtCode.Text.ToString();
            //    txtPW.Text.ToString();
            //    txtName.Text.ToString();
            //    txtEmail.Text.ToString();
            //    string birth = cboxYear.Text.ToString() + @"/" + cboxMonth.Text.ToString() + @"/" + cboxDay.Text.ToString();
            //    string sex;
            //    if (rbtnMan.Enabled)
            //    {
            //        sex = "Man";
            //    }
            //    else
            //    {
            //        sex = "Woman";
            //    }


            //    //오성의코드
            //    //JOSON으로 파싱
            //    sign_up user = new sign_up();
            //    JObject json = new JObject();
            //    JArray jjson = new JArray();


            //    user.id = txtID.Text.ToString();
            //    user.pw = txtPW.Text.ToString();
            //    user.email = txtEmail.Text.ToString();
            //    user.name = txtName.Text.ToString();
            //    user.birth = birth;
            //    user.sex = sex;
            //    json = JObject.FromObject(user);
            //    jjson.Add(json);
            //    json.Add("user", jjson);
            //    json.Add("command", "user_plus");
            //    Console.WriteLine(json.ToString());


            //    //서버연결
            //    try
            //    {
            //        Console.WriteLine("ㅇㅇ");





            //            // 2. 구조체 데이타를 바이트 배열로 변환
            //            string target_buffer = json.ToString();
            //            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(target_buffer);


            //            Console.WriteLine("----------- Json to String -------------");
            //            Console.WriteLine("DATA : " + target_buffer);
            //            Console.WriteLine("----------- String to btye -------------");
            //            Console.WriteLine("DATA : " + buffer);

            //            // 3. 서버에 접속
            //            TcpClient client = new TcpClient();
            //            client.Connect("54.187.238.235", 10050);
            //            Console.WriteLine("Connected...");

            //            NetworkStream stream = client.GetStream();

            //            // 4. 데이타 전송
            //            stream.Write(buffer, 0, buffer.Length);
            //            Console.WriteLine("{0} data sent", buffer.Length);
            //            Console.WriteLine("===============================\n");

            //            // 5. 스트림&소켓 닫기
            //            stream.Close();
            //            client.Close();


            //    }
            //    catch (SocketException se)
            //    {
            //        Console.WriteLine("SocketException : {0} ", se.Message.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Exception : {0} ", ex.Message.ToString());
            //    }
            //    Console.ReadLine();









            //}
            //else if (!txtPW.Equals(txtPWconfirm.Text))
            //{
            //    //signup fail
            //    lblWarn.Visible = true;
            //}
            //else
            //{
            //    //signup fail
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //중복 확인


            // (1) IP 주소와 포트를 지정하고 TCP 연결 
            TcpClient tc = new TcpClient("54.185.231.100", 4000);
            //TcpClient tc = new TcpClient("localhost", 7000);
            string endpoint = "Lend";
            string msg = "chk_id" + endpoint;

            //정보입력
            string info_ID, info_PW, info_Name, info_Email, info_Birth, info_Sex;


            info_ID = txtID.Text.ToString() + endpoint;


            msg = msg + info_ID;

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
                MessageBox.Show("사용 가능한 아이디 입니다.");//확인시  중복이 없으면 
                idchecker = true;
            }
            if (Encoding.ASCII.GetString(outbytes) == "no")
            {
                MessageBox.Show("아이디가 이미 존재합니다.");
                idchecker = false;
            }


                // (5) 스트림과 TcpClient 객체 닫기
                stream.Close();
            tc.Close();

            Console.WriteLine(Encoding.ASCII.GetString(outbytes));














            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
