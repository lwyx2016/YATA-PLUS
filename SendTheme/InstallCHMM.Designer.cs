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
            this.components = new System.ComponentModel.Container();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.chb_pngprev = new System.Windows.Forms.CheckBox();
            this.chb_bmgprev = new System.Windows.Forms.CheckBox();
            this.chb_smdhinfo = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lbl_themename = new System.Windows.Forms.Label();
            this.lbl_ip = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendAndSaveDebugInformationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_wait = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
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
            this.textBox1.Location = new System.Drawing.Point(125, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 5;
            // 
            // btn_send
            // 
            this.btn_send.ContextMenuStrip = this.contextMenuStrip1;
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
            this.chb_pngprev.Size = new System.Drawing.Size(136, 17);
            this.chb_pngprev.TabIndex = 7;
            this.chb_pngprev.Text = "Generate PNG preview";
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
            this.chb_bmgprev.CheckedChanged += new System.EventHandler(this.chb_bmgprev_CheckedChanged);
            // 
            // chb_smdhinfo
            // 
            this.chb_smdhinfo.AutoSize = true;
            this.chb_smdhinfo.Location = new System.Drawing.Point(12, 154);
            this.chb_smdhinfo.Name = "chb_smdhinfo";
            this.chb_smdhinfo.Size = new System.Drawing.Size(100, 17);
            this.chb_smdhinfo.TabIndex = 9;
            this.chb_smdhinfo.Text = "Add SMDH info";
            this.chb_smdhinfo.UseVisualStyleBackColor = true;
            this.chb_smdhinfo.CheckedChanged += new System.EventHandler(this.chb_smdhinfo_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(125, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "192.168.";
            // 
            // lbl_themename
            // 
            this.lbl_themename.AutoSize = true;
            this.lbl_themename.Location = new System.Drawing.Point(9, 56);
            this.lbl_themename.Name = "lbl_themename";
            this.lbl_themename.Size = new System.Drawing.Size(72, 13);
            this.lbl_themename.TabIndex = 11;
            this.lbl_themename.Text = "Theme name:";
            // 
            // lbl_ip
            // 
            this.lbl_ip.AutoSize = true;
            this.lbl_ip.Location = new System.Drawing.Point(9, 82);
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(82, 13);
            this.lbl_ip.TabIndex = 12;
            this.lbl_ip.Text = "3DS ip address:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendAndSaveDebugInformationsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(258, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // sendAndSaveDebugInformationsToolStripMenuItem
            // 
            this.sendAndSaveDebugInformationsToolStripMenuItem.Name = "sendAndSaveDebugInformationsToolStripMenuItem";
            this.sendAndSaveDebugInformationsToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.sendAndSaveDebugInformationsToolStripMenuItem.Text = "Send and save debug informations";
            // 
            // lbl_wait
            // 
            this.lbl_wait.AutoSize = true;
            this.lbl_wait.Location = new System.Drawing.Point(97, 195);
            this.lbl_wait.Name = "lbl_wait";
            this.lbl_wait.Size = new System.Drawing.Size(145, 13);
            this.lbl_wait.TabIndex = 15;
            this.lbl_wait.Text = "Wait, this may take a while....";
            this.lbl_wait.Visible = false;
            // 
            // InstallCHMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 212);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallCHMM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install theme";
            this.Load += new System.EventHandler(this.InstallCHMM_Load);
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Label lbl_themename;
        private System.Windows.Forms.Label lbl_ip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sendAndSaveDebugInformationsToolStripMenuItem;
        private System.Windows.Forms.Label lbl_wait;
    }
}