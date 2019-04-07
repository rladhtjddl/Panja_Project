namespace Panja_Project
{
    partial class MainUI
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.local_btn = new System.Windows.Forms.Button();
            this.Cloud_btn = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Button();
            this.display_id = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // local_btn
            // 
            this.local_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.local_btn.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.local_btn.ForeColor = System.Drawing.Color.White;
            this.local_btn.Location = new System.Drawing.Point(12, 49);
            this.local_btn.Name = "local_btn";
            this.local_btn.Size = new System.Drawing.Size(383, 401);
            this.local_btn.TabIndex = 0;
            this.local_btn.Text = " Local  Explorer";
            this.local_btn.UseVisualStyleBackColor = false;
            this.local_btn.Click += new System.EventHandler(this.local_btn_Click);
            // 
            // Cloud_btn
            // 
            this.Cloud_btn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Cloud_btn.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Cloud_btn.ForeColor = System.Drawing.Color.White;
            this.Cloud_btn.Location = new System.Drawing.Point(401, 49);
            this.Cloud_btn.Name = "Cloud_btn";
            this.Cloud_btn.Size = new System.Drawing.Size(383, 401);
            this.Cloud_btn.TabIndex = 1;
            this.Cloud_btn.Text = "Cloud Explorer";
            this.Cloud_btn.UseVisualStyleBackColor = false;
            this.Cloud_btn.Click += new System.EventHandler(this.Cloud_btn_Click);
            // 
            // settings
            // 
            this.settings.BackColor = System.Drawing.Color.Silver;
            this.settings.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.settings.ForeColor = System.Drawing.Color.White;
            this.settings.Location = new System.Drawing.Point(690, 8);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(94, 40);
            this.settings.TabIndex = 3;
            this.settings.Text = "설정";
            this.settings.UseVisualStyleBackColor = false;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // display_id
            // 
            this.display_id.AutoSize = true;
            this.display_id.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.display_id.Location = new System.Drawing.Point(12, 11);
            this.display_id.Name = "display_id";
            this.display_id.Size = new System.Drawing.Size(672, 32);
            this.display_id.TabIndex = 4;
            this.display_id.Text = " ID : J3N_JAN6 ( 프리미엄 이용기간 : 18.10.01 ~ 19.02.01 )";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.display_id);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.Cloud_btn);
            this.Controls.Add(this.local_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PANJA Security";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button local_btn;
        private System.Windows.Forms.Button Cloud_btn;
        private System.Windows.Forms.Button settings;
        private System.Windows.Forms.Label display_id;
    }
}

