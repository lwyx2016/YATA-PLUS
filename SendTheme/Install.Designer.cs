namespace YATA
{
    partial class Install
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
            this.lbl_wait = new System.Windows.Forms.Label();
            this.lbl_ip = new System.Windows.Forms.Label();
            this.lbl_themename = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.chb_smdhinfo = new System.Windows.Forms.CheckBox();
            this.chb_bmgprev = new System.Windows.Forms.CheckBox();
            this.chb_pngprev = new System.Windows.Forms.CheckBox();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.lbl_folder = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_wait
            // 
            this.lbl_wait.AutoSize = true;
            this.lbl_wait.Location = new System.Drawing.Point(94, 228);
            this.lbl_wait.Name = "lbl_wait";
            this.lbl_wait.Size = new System.Drawing.Size(145, 13);
            this.lbl_wait.TabIndex = 25;
            this.lbl_wait.Text = "Wait, this may take a while....";
            this.lbl_wait.Visible = false;
            // 
            // lbl_ip
            // 
            this.lbl_ip.AutoSize = true;
            this.lbl_ip.Location = new System.Drawing.Point(9, 84);
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(82, 13);
            this.lbl_ip.TabIndex = 24;
            this.lbl_ip.Text = "3DS ip address:";
            // 
            // lbl_themename
            // 
            this.lbl_themename.AutoSize = true;
            this.lbl_themename.Location = new System.Drawing.Point(9, 58);
            this.lbl_themename.Name = "lbl_themename";
            this.lbl_themename.Size = new System.Drawing.Size(72, 13);
            this.lbl_themename.TabIndex = 23;
            this.lbl_themename.Text = "Theme name:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(125, 81);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 20);
            this.textBox2.TabIndex = 22;
            this.textBox2.Text = "192.168.";
            // 
            // chb_smdhinfo
            // 
            this.chb_smdhinfo.AutoSize = true;
            this.chb_smdhinfo.Location = new System.Drawing.Point(12, 180);
            this.chb_smdhinfo.Name = "chb_smdhinfo";
            this.chb_smdhinfo.Size = new System.Drawing.Size(100, 17);
            this.chb_smdhinfo.TabIndex = 21;
            this.chb_smdhinfo.Text = "Add SMDH info";
            this.chb_smdhinfo.UseVisualStyleBackColor = true;
            this.chb_smdhinfo.CheckedChanged += new System.EventHandler(this.chb_smdhinfo_CheckedChanged);
            // 
            // chb_bmgprev
            // 
            this.chb_bmgprev.AutoSize = true;
            this.chb_bmgprev.Location = new System.Drawing.Point(12, 157);
            this.chb_bmgprev.Name = "chb_bmgprev";
            this.chb_bmgprev.Size = new System.Drawing.Size(108, 17);
            this.chb_bmgprev.TabIndex = 20;
            this.chb_bmgprev.Text = "Add bmg preview";
            this.chb_bmgprev.UseVisualStyleBackColor = true;
            this.chb_bmgprev.CheckedChanged += new System.EventHandler(this.chb_bmgprev_CheckedChanged);
            // 
            // chb_pngprev
            // 
            this.chb_pngprev.AutoSize = true;
            this.chb_pngprev.Location = new System.Drawing.Point(12, 134);
            this.chb_pngprev.Name = "chb_pngprev";
            this.chb_pngprev.Size = new System.Drawing.Size(136, 17);
            this.chb_pngprev.TabIndex = 19;
            this.chb_pngprev.Text = "Generate PNG preview";
            this.chb_pngprev.UseVisualStyleBackColor = true;
            // 
            // lbl_1
            // 
            this.lbl_1.Location = new System.Drawing.Point(12, 9);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(295, 41);
            this.lbl_1.TabIndex = 18;
            this.lbl_1.Text = "Open any FTP client and click send to put the theme on your 3ds, then you can ins" +
    "tall the theme with CHMM2 or you favorite theme installer";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 17;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(123, 199);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 20);
            this.btn_send.TabIndex = 16;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_folder
            // 
            this.lbl_folder.AutoSize = true;
            this.lbl_folder.Location = new System.Drawing.Point(9, 110);
            this.lbl_folder.Name = "lbl_folder";
            this.lbl_folder.Size = new System.Drawing.Size(91, 13);
            this.lbl_folder.TabIndex = 27;
            this.lbl_folder.Text = "SD themes folder:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(125, 107);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 20);
            this.textBox3.TabIndex = 26;
            this.textBox3.Text = "/Themes/";
            // 
            // Install
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 258);
            this.Controls.Add(this.lbl_folder);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.lbl_wait);
            this.Controls.Add(this.lbl_ip);
            this.Controls.Add(this.lbl_themename);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.chb_smdhinfo);
            this.Controls.Add(this.chb_bmgprev);
            this.Controls.Add(this.chb_pngprev);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_send);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Install";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install theme";
            this.Load += new System.EventHandler(this.Install_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_wait;
        private System.Windows.Forms.Label lbl_ip;
        private System.Windows.Forms.Label lbl_themename;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox chb_smdhinfo;
        private System.Windows.Forms.CheckBox chb_bmgprev;
        private System.Windows.Forms.CheckBox chb_pngprev;
        private System.Windows.Forms.Label lbl_folder;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbl_1;
    }
}