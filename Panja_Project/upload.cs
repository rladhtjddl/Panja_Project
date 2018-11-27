using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;



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
            proinfo.CreateNoWindow = true; //띄우기 안띄우기
            proinfo.UseShellExecute = false;
            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;
            

            pro.StartInfo = proinfo;
            pro.Start();

            pro.StandardInput.Write("cd ../../Properties" + Environment.NewLine);
            pro.StandardInput.Write("psftp -pw ubuntu ubuntu@34.216.228.162" + Environment.NewLine); //우분투 접속
            pro.StandardInput.Write("put C:\\Users\\KOS\\Desktop\\2018수업\\exam\\kkk.txt ~/panja/imsi" + Environment.NewLine); //파일 전송 (경로 나중에 바꿀것)
            pro.StandardInput.Close();

            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            Console.WriteLine(resultValue);



        }

        private void button2_Click(object sender, EventArgs e)
        {

            //-----------------------------------------------------------------------------
            JObject json = new JObject();
            JArray jjson = new JArray();
          


            string[] file_list = new string[4];  //0 : name , 1 : byte, 2 : format

            //파일정보 가져오기

            file_list a = new file_list { Id = "Locu", Name = "dlfma", By = "32048" };
            json = JObject.FromObject(a);
            jjson.Add(json);
            file_list b = new file_list { Id = "Tim", Name = "one", By = "20448" };
            //json.Add(JObject.FromObject(b));
            json = JObject.FromObject(b);
            jjson.Add(json);
            file_list c = new file_list { Id = "Kim", Name = "three", By = "20648" };
            //json.Add(JObject.FromObject(c));
            json = JObject.FromObject(c);
            jjson.Add(json);
            
            json.Add("link", jjson);



            //리스트로 저장
            IList<file_list> person = jjson.ToObject<IList<file_list>>();
            
            //출력
            //Console.WriteLine(person[0].Id);
            //Console.WriteLine(person[1].Name);
            Console.WriteLine(json.ToString());

            //-----------------------------------------------------------------------------
            
            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(@"c:\Temp\file_list.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                json.WriteTo(writer);
            }
            

            //-----------------------------------------------------------------------------

            /*
            file_info temp = new file_info("dd");

            Console.WriteLine(temp.fname);
            Console.WriteLine(temp.fbyte);
            Console.WriteLine(temp.ftime);
            Console.WriteLine(temp.ftype);
            */













        }

        private void Method(ref string[] reff)
        {
            reff[0] = reff[0] + " bbb";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a;
            DirectoryInfo dir = new DirectoryInfo("C:\\Temp");
            Console.WriteLine(dir.Name);
            Console.WriteLine(dir.FullName);
            Console.WriteLine(dir.GetFiles().Length);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            //스트링
            string dirPath = @"C:\Temp";
       

            string[] files = Directory.GetFiles(dirPath ,"*.*",SearchOption.AllDirectories);
            foreach(string s in files)
            {
                Console.WriteLine(s);
            }
            */






        }

        private void button5_Click(object sender, EventArgs e)
        {

            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Parse("54.187.238.235"), 13000);
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

        private void button6_Click(object sender, EventArgs e)
        {
            TcpListener server = null;

            String myCompleteMessage = "";
            JObject recieved_data;
            string command;


            JObject sample_jso = new JObject();
            


            sign_up user = new sign_up();
            JObject json = new JObject();
            JArray jjson = new JArray();


            user.id = "Locu";
            user.pw = "12345";
            user.email = "rladhtjddl";
            user.name = "오성";
            user.birth = "1995/01/05";
            user.sex = "man";
            json = JObject.FromObject(user);
            jjson.Add(json);
            sample_jso.Add("user", jjson);

            sample_jso.Add("command", "user_plus");





            try
            {
                Console.WriteLine("ㅇㅇ");
                string Name = string.Empty;
                string Subject = string.Empty;
                Int32 Grade = 0;
                string Memo = string.Empty;

                do
                {
                    // 1. 데이타 입력
                    Console.Write("이름 : ");
                    Name = "ousung";
                    
                    Console.Write("과목 : ");
                    Subject = "math";

                    Console.Write("점수 : ");
                    string tmpGrage = "92";
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
                    Memo = "메모인가요";

                    if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Subject))
                        break;

                    // 2. 구조체 데이타를 바이트 배열로 변환
                    string target_buffer = sample_jso.ToString();
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(target_buffer);


                    Console.WriteLine("----------- Json to String -------------");
                    Console.WriteLine("DATA : " + target_buffer);
                    Console.WriteLine("----------- String to btye -------------");
                    Console.WriteLine("DATA : " + buffer);
                   
                    // 3. 서버에 접속
                    TcpClient client = new TcpClient();
                    client.Connect("54.187.238.235", 10050);
                    Console.WriteLine("Connected...");

                    NetworkStream stream = client.GetStream();

                    // 4. 데이타 전송
                    stream.Write(buffer, 0, buffer.Length);
                    Console.WriteLine("{0} data sent", buffer.Length);
                    Console.WriteLine("===============================\n");


                    if (stream.CanRead)
                    {

                        byte[] myReadBuffer = new byte[1024];

                        int numberOfBytesRead = 0;

                        // Incoming message may be larger than the buffer size.
                        do
                        {
                            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                            myCompleteMessage =
                                String.Concat(myCompleteMessage, Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                        }
                        while (stream.DataAvailable);

                        Console.WriteLine("Received message : " + myCompleteMessage);

                        recieved_data = new JObject();
                        recieved_data = (JObject)JsonConvert.DeserializeObject(myCompleteMessage);
                        command = recieved_data["command"].ToString();
                    }
                    else
                    {
                        Console.WriteLine("Receiving Fail");
                    }



                    if (stream.CanRead)
                    {

                        byte[] myReadBuffer = new byte[1024];

                        int numberOfBytesRead = 0;

                        // Incoming message may be larger than the buffer size.
                        do
                        {
                            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                            myCompleteMessage =
                                String.Concat(myCompleteMessage, Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                        }
                        while (stream.DataAvailable);

                        Console.WriteLine("Received message : " + myCompleteMessage);

                        recieved_data = new JObject();
                        recieved_data = (JObject)JsonConvert.DeserializeObject(myCompleteMessage);
                        command = recieved_data["command"].ToString();
                    }
                    else
                    {
                        Console.WriteLine("Receiving Fail");
                    }



                    
                    // 5. 스트림&소켓 닫기
                    stream.Close();
                    client.Close();

                } while (Name != "ousung" && Subject != "math");
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

        private void upload_Load(object sender, EventArgs e)
        {

        }
    }


}

