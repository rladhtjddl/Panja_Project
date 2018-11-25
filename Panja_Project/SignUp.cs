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
            if(!txtID.Text.Equals("") && !txtPW.Equals("") && !txtPWconfirm.Equals("")
                && !txtEmail.Equals("") && !txtCode.Equals("") && !txtName.Equals("")
                &&(rbtnMan.Checked || rbtnWoman.Checked)&&idchecker)
            {
                //signup OK
                txtCode.Text.ToString();
                txtPW.Text.ToString();
                txtName.Text.ToString();
                txtEmail.Text.ToString();
                string birth = cboxYear.Text.ToString() + @"/" + cboxMonth.Text.ToString() + @"/" + cboxDay.Text.ToString();
                string sex;
                if (rbtnMan.Enabled)
                {
                    sex = "Man";
                }
                else
                {
                    sex = "Woman";
                }

                System.Console.WriteLine(txtCode.Text.ToString() + "\n"
                    + txtEmail.Text.ToString() + "\n"
                    + txtEmail.Text.ToString() + "\n"
                    + txtEmail.Text.ToString() + "\n"
                    + txtEmail.Text.ToString() + "\n");

            }
            else if (!txtPW.Equals(txtPWconfirm.Text))
            {
                //signup fail
                lblWarn.Visible = true;
            }
            else
            {
                //signup fail
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //중복 확인

            //확인시  중복이 없으면 
            idchecker = true; 
        }
    }
}
