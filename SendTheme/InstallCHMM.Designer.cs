namespace YATA.SendTheme
{
    partial class InstallCHMM
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
            this.lbl_1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.chb_pngprev = new System.Windows.Forms.CheckBox();
            this.chb_bmgprev = new System.Windows.Forms.CheckBox();
            this.chb_smdhinfo = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.Location = new System.Drawing.Point(12, 9);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(295, 41);
            this.lbl_1.TabIndex = 6;
            this.lbl_1.Text = "If you have a 3ds with a firmware equal or lower than 9.2 with ninjhax1 you can r" +
    "ecive themes with CHMM2.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 20);
            this.textBox1.TabIndex = 5;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(125, 172);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 20);
            this.btn_send.TabIndex = 4;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // chb_pngprev
            // 
            this.chb_pngprev.AutoSize = true;
            this.chb_pngprev.Location = new System.Drawing.Point(12, 108);
            this.chb_pngprev.Name = "chb_pngprev";
            this.chb_pngprev.Size = new System.Drawing.Size(131, 17);
            this.chb_pngprev.TabIndex = 7;
            this.chb_pngprev.Text = "Generate png preview";
            this.chb_pngprev.UseVisualStyleBackColor = true;
            // 
            // chb_bmgprev
            // 
            this.chb_bmgprev.AutoSize = true;
            this.chb_bmgprev.Location = new System.Drawing.Point(12, 131);
            this.chb_bmgprev.Name = "chb_bmgprev";
            this.chb_bmgprev.Size = new System.Drawing.Size(108, 17);
            this.chb_bmgprev.TabIndex = 8;
            this.chb_bmgprev.Text = "Add bmg preview";
            this.chb_bmgprev.UseVisualStyleBackColor = true;
            // 
            // chb_smdhinfo
            // 
            this.chb_smdhinfo.AutoSize = true;
            this.chb_smdhinfo.Location = new System.Drawing.Point(12, 154);
            this.chb_smdhinfo.Name = "chb_smdhinfo";
            this.chb_smdhinfo.Size = new System.Drawing.Size(93, 17);
            this.chb_smdhinfo.TabIndex = 9;
            this.chb_smdhinfo.Text = "Add smdh info";
            this.chb_smdhinfo.UseVisualStyleBackColor = true;
            this.chb_smdhinfo.CheckedChanged += new System.EventHandler(this.chb_smdhinfo_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(210, 20);
            this.textBox2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Theme name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "3DS ip address:";
            // 
            // InstallCHMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 204);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.chb_smdhinfo);
            this.Controls.Add(this.chb_bmgprev);
            this.Controls.Add(this.chb_pngprev);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_send);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallCHMM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install theme";
            this.Load += new System.EventHandler(this.InstallCHMM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.CheckBox chb_pngprev;
        private System.Windows.Forms.CheckBox chb_bmgprev;
        private System.Windows.Forms.CheckBox chb_smdhinfo;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}