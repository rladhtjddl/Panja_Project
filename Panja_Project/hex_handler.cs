using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Windows.Forms;

namespace Panja_Project
{
    class hex_handler
    {
        public string str2hex(string strData)
        {
            string resultHex = string.Empty;
            //byte[] arr_byteStr1 = Encoding.UTF8.GetBytes(strData);
            byte[] arr_byteStr = Encoding.Default.GetBytes(strData);

            foreach (byte byteStr in arr_byteStr)
                resultHex += string.Format("{0:X2}", byteStr);

            return resultHex;
        }
        public static byte[] Chars2Bytes(char[] charArray)
        {
            List<byte> outputs = new List<byte>();

            for (int i = 0; i < charArray.Length; i++)
            {
                outputs.Add((byte)(charArray[i] & 0xFF));
                outputs.Add((byte)(charArray[i] >> 8));
            }

            return outputs.ToArray();
        }

        public string hex_read(int pos) {

            FileStream mystream = new FileStream(@"C:\Temp\panja.dll", FileMode.Open, FileAccess.Read);


            //Stream mystream = File.OpenRead(@"C:\Temp\panja.dll");

            BinaryReader input = new BinaryReader(mystream);
            string read_value = null;

            input.BaseStream.Position = pos;

            try
            {

                for (int i = 0; i < mystream.Length; i++)
                {
                    //read_value += input.ReadByte().ToString("X2");
                   // read_value += input.ReadString().ToString();
                    
                    int value = Convert.ToInt32(input.ReadByte());
                    if (value != 0x00)
                        read_value += Char.ConvertFromUtf32(value);
                    else
                        break;
                    
                }
                
            }
            catch (IOException error) {
                MessageBox.Show(error.ToString());
                Application.Exit();
            }
            
            input.Close();
            return read_value;
        }

        public void hex_write(string test, int pos) {
            
            string write_value = test;
            

            BinaryWriter output = new BinaryWriter(File.OpenWrite(@"C:\Temp\panja.dll"),Encoding.UTF8);
            
            output.BaseStream.Position = pos;
            output.Write(write_value);
            
            output.Close();
        }

        public void hex_write1(string test, int pos)
        {

            string write_value = test;


            BinaryWriter output = new BinaryWriter(File.OpenWrite(@"C:\Temp\panja.dll"), Encoding.UTF8);

            output.BaseStream.Position = pos;
            output.Write(write_value);
            for(int i = 0; i<50;i++){ 
                output.Write(0x00);
            }
            output.Close();
        }


        public int hex_length() {

            BinaryReader input = new BinaryReader(File.OpenRead(@"C:\Temp\panja.dll"));
            input.BaseStream.Position = 0x4F0;
            int value = Convert.ToInt32(input.ReadByte().ToString("X2"), 16);
            input.Close();
            return value;
        }

        public void hex_length_set(int length)
        {

            BinaryWriter output = new BinaryWriter(File.OpenWrite(@"C:\Temp\panja.dll"), Encoding.UTF8);

            String write_value = length.ToString();

            output.BaseStream.Position = 0x4F0;
            output.Write(length);
            output.Close();
        }

    }
}
