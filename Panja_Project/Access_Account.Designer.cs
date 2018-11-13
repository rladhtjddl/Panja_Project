namespace Panja_Project
{
    partial class Access_Account
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_SignUp = new System.Windows.Forms.Button();
            this.tb_ID = new System.Windows.Forms.TextBox();
            this.tb_PW = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.find_ID = new System.Windows.Forms.LinkLabel();
            this.find_PW = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(290, 44);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 0;
            this.btn_Login.Text = "Sign in";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_SignUp
            // 
            this.btn_SignUp.Location = new System.Drawing.Point(290, 99);
            this.btn_SignUp.Name = "btn_SignUp";
            this.btn_SignUp.Size = new System.Drawing.Size(75, 23);
            this.btn_SignUp.TabIndex = 1;
            this.btn_SignUp.Text = "Sign Up";
            this.btn_SignUp.UseVisualStyleBackColor = true;
            this.btn_SignUp.Click += new System.EventHandler(this.btn_SignUp_Click);
            // 
            // tb_ID
            // 
            this.tb_ID.Location = new System.Drawing.Point(129, 45);
            this.tb_ID.Name = "tb_ID";
            this.tb_ID.Size = new System.Drawing.Size(129, 25);
            this.tb_ID.TabIndex = 3;
            this.tb_ID.WordWrap = false;
            // 
            // tb_PW
            // 
            this.tb_PW.Location = new System.Drawing.Point(129, 100);
            this.tb_PW.Name = "tb_PW";
            this.tb_PW.PasswordChar = '*';
            this.tb_PW.Size = new System.Drawing.Size(129, 25);
            this.tb_PW.TabIndex = 4;
            this.tb_PW.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "PW";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Did you forget ID/PW ?";
            // 
            // find_ID
            // 
            this.find_ID.AutoSize = true;
            this.find_ID.Location = new System.Drawing.Point(223, 156);
            this.find_ID.Name = "find_ID";
            this.find_ID.Size = new System.Drawing.Size(52, 15);
            this.find_ID.TabIndex = 9;
            this.find_ID.TabStop = true;
            this.find_ID.Text = "Find ID";
            this.find_ID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.find_ID_LinkClicked);
            // 
            // find_PW
            // 
            this.find_PW.AutoSize = true;
            this.find_PW.Location = new System.Drawing.Point(281, 156);
            this.find_PW.Name = "find_PW";
            this.find_PW.Size = new System.Drawing.Size(63, 15);
            this.find_PW.TabIndex = 10;
            this.find_PW.TabStop = true;
            this.find_PW.Text = "Find PW";
            // 
            // Access_Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 212);
            this.Controls.Add(this.find_PW);
            this.Controls.Add(this.find_ID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_PW);
            this.Controls.Add(this.tb_ID);
            this.Controls.Add(this.btn_SignUp);
            this.Controls.Add(this.btn_Login);
            this.Name = "Access_Account";
            this.Text = "Sign";
            this.Load += new System.EventHandler(this.Access_Account_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_SignUp;
        private System.Windows.Forms.TextBox tb_ID;
        private System.Windows.Forms.TextBox tb_PW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel find_ID;
        private System.Windows.Forms.LinkLabel find_PW;
    }
}