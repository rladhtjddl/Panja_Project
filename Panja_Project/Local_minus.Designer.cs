namespace Panja_Project
{
    partial class Local_Minus
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


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Local_Minus));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_minus = new System.Windows.Forms.Button();
            this.btn_plus = new System.Windows.Forms.Button();
            this.List_own = new System.Windows.Forms.ListView();
            this.List_go = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("궁서", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(94, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("궁서", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(454, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 36);
            this.label2.TabIndex = 5;
            this.label2.Text = "Selected";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(737, 88);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 48);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(737, 146);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(123, 48);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btn_minus
            // 
            this.btn_minus.Image = global::Panja_Project.Properties.Resources.arrow_left;
            this.btn_minus.Location = new System.Drawing.Point(313, 304);
            this.btn_minus.Margin = new System.Windows.Forms.Padding(4);
            this.btn_minus.Name = "btn_minus";
            this.btn_minus.Size = new System.Drawing.Size(84, 82);
            this.btn_minus.TabIndex = 2;
            this.btn_minus.UseVisualStyleBackColor = true;
            // 
            // btn_plus
            // 
            this.btn_plus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_plus.Image = global::Panja_Project.Properties.Resources.arrow_right;
            this.btn_plus.Location = new System.Drawing.Point(313, 189);
            this.btn_plus.Margin = new System.Windows.Forms.Padding(4);
            this.btn_plus.Name = "btn_plus";
            this.btn_plus.Size = new System.Drawing.Size(84, 88);
            this.btn_plus.TabIndex = 1;
            this.btn_plus.UseVisualStyleBackColor = false;
            // 
            // List_own
            // 
            this.List_own.Location = new System.Drawing.Point(17, 72);
            this.List_own.Margin = new System.Windows.Forms.Padding(4);
            this.List_own.Name = "List_own";
            this.List_own.Size = new System.Drawing.Size(285, 454);
            this.List_own.TabIndex = 8;
            this.List_own.UseCompatibleStateImageBehavior = false;
            this.List_own.View = System.Windows.Forms.View.Details;
            // 
            // List_go
            // 
            this.List_go.Location = new System.Drawing.Point(406, 72);
            this.List_go.Margin = new System.Windows.Forms.Padding(4);
            this.List_go.Name = "List_go";
            this.List_go.Size = new System.Drawing.Size(285, 454);
            this.List_go.TabIndex = 9;
            this.List_go.UseCompatibleStateImageBehavior = false;
            // 
            // Local_Minus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 546);
            this.Controls.Add(this.List_go);
            this.Controls.Add(this.List_own);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_minus);
            this.Controls.Add(this.btn_plus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Local_Minus";
            this.Text = "폴더 보호 해제";
            this.Load += new System.EventHandler(this.Local_Minus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.Button btn_minus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView List_own;
        private System.Windows.Forms.ListView List_go;
    }
}