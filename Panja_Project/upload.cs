using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Panja_Project
{
    public partial class upload : Form
    {
        public upload()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cmd창
            System.Diagnostics.ProcessStartInfo proinfo = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process pro = new System.Diagnostics.Process();

            proinfo.FileName = @"cmd";
            proinfo.CreateNoWindow = false; //띄우기 안띄우기
            proinfo.UseShellExecute = false;
            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;

            pro.StartInfo = proinfo;
            pro.Start();

            pro.StandardInput.Write("psftp -pw ubuntu ubuntu@54.218.123.84" + Environment.NewLine); //우분투 접속
            pro.StandardInput.Write("put C:\\Users\\KOS\\Desktop\\2018수업\\exam\\kkk.txt ~/panja/imsi" + Environment.NewLine); //파일 전송 (경로 나중에 바꿀것)
            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            Console.WriteLine(resultValue);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Parse("192.168.154.1"), 13000);
                server.Start();

                byte[] buffer = new byte[1024];

                while (true)
                {
                    Console.WriteLine("Waiting for a connection.....");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("\nConnected!!");

                    NetworkStream stream = client.GetStream();

                    while (stream.Read(buffer, 0, buffer.Length) != 0)
                    {
                        // deserializing;
                        DataPacket packet = new DataPacket();
                        packet.Deserialize(ref buffer);

                        string Name = packet.Name;
                        string Subject = packet.Subject;
                        Int32 Grade = packet.Grade;
                        string Memo = packet.Memo;

                        Console.WriteLine("이 름 : {0}", Name);
                        Console.WriteLine("과 목 : {0}", Subject);
                        Console.WriteLine("점 수 : {0}", Grade);
                        Console.WriteLine("메 모 : {0}", Memo);
                        Console.WriteLine("");
                        Console.WriteLine("===========================================");
                        Console.WriteLine("");
                    }

                    stream.Close();
                    client.Close();
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            Console.ReadLine();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = string.Empty;
                string Subject = string.Empty;
                Int32 Grade = 0;
                string Memo = string.Empty;

                do
                {
                    // 1. 데이타 입력
                    Console.Write("이름 : ");
                    Name = Console.ReadLine();

                    Console.Write("과목 : ");
                    Subject = Console.ReadLine();

                    Console.Write("점수 : ");
                    string tmpGrage = Console.ReadLine();
                    if (tmpGrage != "")
                    {
                        int outGrade = 0;
                        if (Int32.TryParse(tmpGrage, out outGrade))
                            Grade = Convert.ToInt32(tmpGrage);
                        else
                            Grade = 0;
                    }
                    else
                        Grade = 0;

                    Console.Write("메모 : ");
                    Memo = Console.ReadLine();

                    if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Subject))
                        break;

                    // 2. 구조체 데이타를 바이트 배열로 변환
                    DataPacket packet = new DataPacket();
                    packet.Name = Name;
                    packet.Subject = Subject;
                    packet.Grade = Grade;
                    packet.Memo = Memo;

                    byte[] buffer = packet.Serialize();

                    // 3. 서버에 접속
                    TcpClient client = new TcpClient();
                    client.Connect("192.168.154.1", 13000);
                    Console.WriteLine("Connected...");

                    NetworkStream stream = client.GetStream();

                    // 4. 데이타 전송
                    stream.Write(buffer, 0, buffer.Length);
                    Console.WriteLine("{0} data sent", buffer.Length);
                    Console.WriteLine("===============================\n");

                    // 5. 스트림&소켓 닫기
                    stream.Close();
                    client.Close();

                } while (Name != "" && Subject != "");
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0} ", se.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : {0} ", ex.Message.ToString());
            }
            Console.ReadLine();
        }

       
    }
            



}


