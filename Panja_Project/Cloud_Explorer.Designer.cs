﻿namespace Panja_Project
{
    partial class Cloud_Explorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cloud_Explorer));
            this.cloud_list = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.path_now = new System.Windows.Forms.TextBox();
            this.btn_upload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cloud_list
            // 
            this.cloud_list.Dock = System.Windows.Forms.DockStyle.Top;
            this.cloud_list.LargeImageList = this.imageList1;
            this.cloud_list.Location = new System.Drawing.Point(0, 0);
            this.cloud_list.Name = "cloud_list";
            this.cloud_list.Size = new System.Drawing.Size(1139, 756);
            this.cloud_list.SmallImageList = this.imageList1;
            this.cloud_list.TabIndex = 0;
            this.cloud_list.UseCompatibleStateImageBehavior = false;
            this.cloud_list.Click += new System.EventHandler(this.cloud_list_Click);
            this.cloud_list.DoubleClick += new System.EventHandler(this.cloud_list_DoubleClick);
            this.cloud_list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cloud_list_MouseClick);
            this.cloud_list.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cloud_list_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1");
            this.imageList1.Images.SetKeyName(1, "2");
            // 
            // path_now
            // 
            this.path_now.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.path_now.Location = new System.Drawing.Point(0, 728);
            this.path_now.Name = "path_now";
            this.path_now.ReadOnly = true;
            this.path_now.Size = new System.Drawing.Size(1139, 28);
            this.path_now.TabIndex = 1;
            // 
            // btn_upload
            // 
            this.btn_upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_upload.BackColor = System.Drawing.Color.White;
            this.btn_upload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_upload.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_upload.Image = ((System.Drawing.Image)(resources.GetObject("btn_upload.Image")));
            this.btn_upload.Location = new System.Drawing.Point(976, 525);
            this.btn_upload.Margin = new System.Windows.Forms.Padding(0);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_upload.Size = new System.Drawing.Size(89, 88);
            this.btn_upload.TabIndex = 3;
            this.btn_upload.UseVisualStyleBackColor = false;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // Cloud_Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1139, 756);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.path_now);
            this.Controls.Add(this.cloud_list);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cloud_Explorer";
            this.Text = "Panja Cloud Explorer";
            this.Load += new System.EventHandler(this.Cloud_Explorer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView cloud_list;
        private System.Windows.Forms.TextBox path_now;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btn_upload;
    }
}