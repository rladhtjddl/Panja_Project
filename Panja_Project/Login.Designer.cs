namespace Panja_Project
{
    partial class Login
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPW = new System.Windows.Forms.TextBox();
            this.lnkLostID = new System.Windows.Forms.LinkLabel();
            this.lnkLostPW = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(307, 79);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnSignUp
            // 
            this.btnSignUp.Location = new System.Drawing.Point(307, 159);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(75, 23);
            this.btnSignUp.TabIndex = 1;
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.UseVisualStyleBackColor = true;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "PW";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(91, 81);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(172, 25);
            this.txtID.TabIndex = 4;
            // 
            // txtPW
            // 
            this.txtPW.Location = new System.Drawing.Point(91, 157);
            this.txtPW.Name = "txtPW";
            this.txtPW.Size = new System.Drawing.Size(172, 25);
            this.txtPW.TabIndex = 5;
            this.txtPW.UseSystemPasswordChar = true;
            // 
            // lnkLostID
            // 
            this.lnkLostID.AutoSize = true;
            this.lnkLostID.Location = new System.Drawing.Point(206, 217);
            this.lnkLostID.Name = "lnkLostID";
            this.lnkLostID.Size = new System.Drawing.Size(20, 15);
            this.lnkLostID.TabIndex = 6;
            this.lnkLostID.TabStop = true;
            this.lnkLostID.Text = "ID";
            this.lnkLostID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLostID_LinkClicked);
            // 
            // lnkLostPW
            // 
            this.lnkLostPW.AutoSize = true;
            this.lnkLostPW.Location = new System.Drawing.Point(232, 217);
            this.lnkLostPW.Name = "lnkLostPW";
            this.lnkLostPW.Size = new System.Drawing.Size(31, 15);
            this.lnkLostPW.TabIndex = 7;
            this.lnkLostPW.TabStop = true;
            this.lnkLostPW.Text = "PW";
            this.lnkLostPW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLostPW_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Forgot Account ?";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 289);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lnkLostPW);
            this.Controls.Add(this.lnkLostID);
            this.Controls.Add(this.txtPW);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.btnLogin);
            this.Name = "Login";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtPW;
        private System.Windows.Forms.LinkLabel lnkLostID;
        private System.Windows.Forms.LinkLabel lnkLostPW;
        private System.Windows.Forms.Label label3;
    }
}