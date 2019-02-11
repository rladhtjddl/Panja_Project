namespace Panja_Project
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
            this.cloud_list.DoubleClick += new System.EventHandler(this.cloud_list_DoubleClick);
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
            // Cloud_Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1139, 756);
            this.Controls.Add(this.path_now);
            this.Controls.Add(this.cloud_list);
            this.Name = "Cloud_Explorer";
            this.Text = "Cloud_Explorer";
            this.Load += new System.EventHandler(this.Cloud_Explorer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView cloud_list;
        private System.Windows.Forms.TextBox path_now;
        private System.Windows.Forms.ImageList imageList1;
    }
}