namespace YATA
{
    partial class CwavReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CwavReplace));
            this.btn_import = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.men_gen = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_import = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.lbl_sdk = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbl_enabled = new System.Windows.Forms.Label();
            this.btn_remove = new System.Windows.Forms.Button();
            this.lbl_top = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_import
            // 
            this.btn_import.ContextMenuStrip = this.contextMenuStrip1;
            this.btn_import.Location = new System.Drawing.Point(269, 310);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(117, 39);
            this.btn_import.TabIndex = 0;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.men_gen});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(213, 26);
            // 
            // men_gen
            // 
            this.men_gen.Name = "men_gen";
            this.men_gen.Size = new System.Drawing.Size(212, 22);
            this.men_gen.Text = "Generate but don\'t import";
            this.men_gen.Click += new System.EventHandler(this.generateButDontImportToolStripMenuItem_Click);
            // 
            // lbl_import
            // 
            this.lbl_import.Location = new System.Drawing.Point(8, 295);
            this.lbl_import.Name = "lbl_import";
            this.lbl_import.Size = new System.Drawing.Size(256, 57);
            this.lbl_import.TabIndex = 1;
            this.lbl_import.Text = "You must save the theme before you can see the\r\nimported cwavs in the CWAV Dumper" +
    "";
            this.lbl_import.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "CWAVs:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(10, 121);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(240, 160);
            this.listBox1.TabIndex = 4;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btn_select
            // 
            this.btn_select.Enabled = false;
            this.btn_select.Location = new System.Drawing.Point(281, 176);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(105, 23);
            this.btn_select.TabIndex = 5;
            this.btn_select.Text = "Select CWAV";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbl_sdk
            // 
            this.lbl_sdk.Location = new System.Drawing.Point(266, 121);
            this.lbl_sdk.Name = "lbl_sdk";
            this.lbl_sdk.Size = new System.Drawing.Size(127, 52);
            this.lbl_sdk.TabIndex = 6;
            this.lbl_sdk.Text = "To create a cwav file you need a tool from the Nintendo SDK";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "*.bcwav|*.bcwav|*.cwav|*.cwav|*.*|*.*";
            this.openFileDialog1.Title = "Select a CWAV";
            // 
            // lbl_enabled
            // 
            this.lbl_enabled.Location = new System.Drawing.Point(270, 219);
            this.lbl_enabled.Name = "lbl_enabled";
            this.lbl_enabled.Size = new System.Drawing.Size(123, 13);
            this.lbl_enabled.TabIndex = 7;
            this.lbl_enabled.Text = "This cwav is not enabed";
            this.lbl_enabled.Visible = false;
            // 
            // btn_remove
            // 
            this.btn_remove.Enabled = false;
            this.btn_remove.Location = new System.Drawing.Point(281, 246);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(105, 35);
            this.btn_remove.TabIndex = 8;
            this.btn_remove.Text = "Remove selected CWAV";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbl_top
            // 
            this.lbl_top.Location = new System.Drawing.Point(8, 9);
            this.lbl_top.Name = "lbl_top";
            this.lbl_top.Size = new System.Drawing.Size(391, 112);
            this.lbl_top.TabIndex = 9;
            this.lbl_top.Text = resources.GetString("lbl_top.Text");
            this.lbl_top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(-6, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 5);
            this.panel1.TabIndex = 10;
            // 
            // CwavReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 361);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_top);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.lbl_enabled);
            this.Controls.Add(this.lbl_sdk);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_import);
            this.Controls.Add(this.btn_import);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(396, 340);
            this.Name = "CwavReplace";
            this.Text = "CWAV replace";
            this.Load += new System.EventHandler(this.CwavReplace_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Label lbl_import;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Label lbl_sdk;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lbl_enabled;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label lbl_top;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem men_gen;
    }
}