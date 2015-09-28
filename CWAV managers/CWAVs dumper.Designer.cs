namespace YATA
{
    partial class CWAVs_dumper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CWAVs_dumper));
            this.btn_dump = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_exportCWAV = new System.Windows.Forms.Button();
            this.btn_exportWAV = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.play = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dump
            // 
            this.btn_dump.Location = new System.Drawing.Point(12, 230);
            this.btn_dump.Name = "btn_dump";
            this.btn_dump.Size = new System.Drawing.Size(164, 42);
            this.btn_dump.TabIndex = 0;
            this.btn_dump.Text = "Dump from the theme";
            this.btn_dump.UseVisualStyleBackColor = true;
            this.btn_dump.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(289, 212);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btn_exportCWAV
            // 
            this.btn_exportCWAV.Enabled = false;
            this.btn_exportCWAV.Location = new System.Drawing.Point(307, 134);
            this.btn_exportCWAV.Name = "btn_exportCWAV";
            this.btn_exportCWAV.Size = new System.Drawing.Size(75, 42);
            this.btn_exportCWAV.TabIndex = 3;
            this.btn_exportCWAV.Text = "Export all CWAVs\r\n";
            this.btn_exportCWAV.UseVisualStyleBackColor = true;
            this.btn_exportCWAV.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_exportWAV
            // 
            this.btn_exportWAV.Enabled = false;
            this.btn_exportWAV.Location = new System.Drawing.Point(307, 182);
            this.btn_exportWAV.Name = "btn_exportWAV";
            this.btn_exportWAV.Size = new System.Drawing.Size(75, 42);
            this.btn_exportWAV.TabIndex = 4;
            this.btn_exportWAV.Text = "Export all WAVs\r\n";
            this.btn_exportWAV.UseVisualStyleBackColor = true;
            this.btn_exportWAV.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "No files found";
            this.label1.Visible = false;
            // 
            // play
            // 
            this.play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.play.Image = global::YATA.Properties.Resources.play;
            this.play.Location = new System.Drawing.Point(307, 12);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(75, 38);
            this.play.TabIndex = 0;
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // CWAVs_dumper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 284);
            this.Controls.Add(this.play);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_exportWAV);
            this.Controls.Add(this.btn_exportCWAV);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_dump);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(406, 321);
            this.Name = "CWAVs_dumper";
            this.Text = "CWAVs dumper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_closing);
            this.Load += new System.EventHandler(this.CWAVs_dumper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_dump;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_exportCWAV;
        private System.Windows.Forms.Button btn_exportWAV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button play;
    }
}