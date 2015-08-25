namespace YATA {
    partial class Prefs {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prefs));
            this.chb_UISim = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_default = new System.Windows.Forms.Button();
            this.chb_SavePrev = new System.Windows.Forms.CheckBox();
            this.lbl_photoedit = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chb_wait = new System.Windows.Forms.CheckBox();
            this.chb_delTempFile = new System.Windows.Forms.CheckBox();
            this.chb_loadBGM = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lbl_shift = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chb_updates = new System.Windows.Forms.CheckBox();
            this.lbl_size = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.chb_ExportBot = new System.Windows.Forms.CheckBox();
            this.btn_setASdef = new System.Windows.Forms.Button();
            this.lbl_lang = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // chb_UISim
            // 
            this.chb_UISim.AutoSize = true;
            this.chb_UISim.Location = new System.Drawing.Point(15, 26);
            this.chb_UISim.Name = "chb_UISim";
            this.chb_UISim.Size = new System.Drawing.Size(201, 17);
            this.chb_UISim.TabIndex = 1;
            this.chb_UISim.Text = "Show Home Menu UI in the simulator\r\n";
            this.chb_UISim.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(31, 333);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_default
            // 
            this.btn_default.Location = new System.Drawing.Point(217, 333);
            this.btn_default.Name = "btn_default";
            this.btn_default.Size = new System.Drawing.Size(119, 23);
            this.btn_default.TabIndex = 3;
            this.btn_default.Text = "Revert to default";
            this.btn_default.UseVisualStyleBackColor = true;
            this.btn_default.Click += new System.EventHandler(this.button2_Click);
            // 
            // chb_SavePrev
            // 
            this.chb_SavePrev.AutoSize = true;
            this.chb_SavePrev.Location = new System.Drawing.Point(15, 49);
            this.chb_SavePrev.Name = "chb_SavePrev";
            this.chb_SavePrev.Size = new System.Drawing.Size(277, 17);
            this.chb_SavePrev.TabIndex = 4;
            this.chb_SavePrev.Text = "Automatically generate preview when saving a theme";
            this.chb_SavePrev.UseVisualStyleBackColor = true;
            // 
            // lbl_photoedit
            // 
            this.lbl_photoedit.AutoSize = true;
            this.lbl_photoedit.Location = new System.Drawing.Point(12, 149);
            this.lbl_photoedit.Name = "lbl_photoedit";
            this.lbl_photoedit.Size = new System.Drawing.Size(192, 13);
            this.lbl_photoedit.TabIndex = 5;
            this.lbl_photoedit.Text = "Your photo editing program executable:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 167);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(266, 20);
            this.textBox1.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(284, 165);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "exe files|*.exe";
            this.openFileDialog1.Title = "Open your phot editor";
            // 
            // chb_wait
            // 
            this.chb_wait.AutoSize = true;
            this.chb_wait.Location = new System.Drawing.Point(12, 193);
            this.chb_wait.Name = "chb_wait";
            this.chb_wait.Size = new System.Drawing.Size(227, 17);
            this.chb_wait.TabIndex = 8;
            this.chb_wait.Text = "Wait the editor before resuming application";
            this.chb_wait.UseVisualStyleBackColor = true;
            // 
            // chb_delTempFile
            // 
            this.chb_delTempFile.AutoSize = true;
            this.chb_delTempFile.Location = new System.Drawing.Point(15, 95);
            this.chb_delTempFile.Name = "chb_delTempFile";
            this.chb_delTempFile.Size = new System.Drawing.Size(240, 17);
            this.chb_delTempFile.TabIndex = 9;
            this.chb_delTempFile.Text = "Delete Body_LZ_dec.bin when closing YATA";
            this.chb_delTempFile.UseVisualStyleBackColor = true;
            // 
            // chb_loadBGM
            // 
            this.chb_loadBGM.AutoSize = true;
            this.chb_loadBGM.Location = new System.Drawing.Point(15, 116);
            this.chb_loadBGM.Name = "chb_loadBGM";
            this.chb_loadBGM.Size = new System.Drawing.Size(276, 30);
            this.chb_loadBGM.TabIndex = 10;
            this.chb_loadBGM.Text = "Automatically load bgm.bcstm when opening a theme\r\n        (This may slow down th" +
    "e loading of themes)";
            this.chb_loadBGM.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(252, 218);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown1.TabIndex = 11;
            // 
            // lbl_shift
            // 
            this.lbl_shift.Location = new System.Drawing.Point(12, 218);
            this.lbl_shift.Name = "lbl_shift";
            this.lbl_shift.Size = new System.Drawing.Size(234, 48);
            this.lbl_shift.TabIndex = 12;
            this.lbl_shift.Text = "Shift color buttons on the right of:\r\n(For computers where the system font isn\'t " +
    "the english default one)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "px";
            // 
            // chb_updates
            // 
            this.chb_updates.AutoSize = true;
            this.chb_updates.Location = new System.Drawing.Point(15, 3);
            this.chb_updates.Name = "chb_updates";
            this.chb_updates.Size = new System.Drawing.Size(230, 17);
            this.chb_updates.TabIndex = 14;
            this.chb_updates.Text = "Automatically check for updates on start up";
            this.chb_updates.UseVisualStyleBackColor = true;
            this.chb_updates.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // lbl_size
            // 
            this.lbl_size.Location = new System.Drawing.Point(12, 266);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.Size = new System.Drawing.Size(192, 32);
            this.lbl_size.TabIndex = 15;
            this.lbl_size.Text = "Set the size for the settings window: ";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(204, 274);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            656,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown2.TabIndex = 16;
            this.numericUpDown2.Value = new decimal(new int[] {
            656,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(276, 274);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            625,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown3.TabIndex = 17;
            this.numericUpDown3.Value = new decimal(new int[] {
            625,
            0,
            0,
            0});
            // 
            // chb_ExportBot
            // 
            this.chb_ExportBot.AutoSize = true;
            this.chb_ExportBot.Location = new System.Drawing.Point(15, 72);
            this.chb_ExportBot.Name = "chb_ExportBot";
            this.chb_ExportBot.Size = new System.Drawing.Size(329, 17);
            this.chb_ExportBot.TabIndex = 18;
            this.chb_ExportBot.Text = "When exporting preview include both the top and bottom screen";
            this.chb_ExportBot.UseVisualStyleBackColor = true;
            // 
            // btn_setASdef
            // 
            this.btn_setASdef.Location = new System.Drawing.Point(15, 362);
            this.btn_setASdef.Name = "btn_setASdef";
            this.btn_setASdef.Size = new System.Drawing.Size(332, 23);
            this.btn_setASdef.TabIndex = 19;
            this.btn_setASdef.Text = "How to set YATA as the default app for opening themes";
            this.btn_setASdef.UseVisualStyleBackColor = true;
            this.btn_setASdef.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbl_lang
            // 
            this.lbl_lang.AutoSize = true;
            this.lbl_lang.Location = new System.Drawing.Point(52, 304);
            this.lbl_lang.Name = "lbl_lang";
            this.lbl_lang.Size = new System.Drawing.Size(54, 13);
            this.lbl_lang.TabIndex = 20;
            this.lbl_lang.Text = "language:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(121, 301);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(157, 21);
            this.comboBox1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "X                     Y";
            // 
            // Prefs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 394);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbl_lang);
            this.Controls.Add(this.btn_setASdef);
            this.Controls.Add(this.chb_ExportBot);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.chb_updates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.chb_loadBGM);
            this.Controls.Add(this.chb_delTempFile);
            this.Controls.Add(this.chb_wait);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbl_photoedit);
            this.Controls.Add(this.chb_SavePrev);
            this.Controls.Add(this.btn_default);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.chb_UISim);
            this.Controls.Add(this.lbl_shift);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_size);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(347, 293);
            this.Name = "Prefs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.Prefs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chb_UISim;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_default;
        private System.Windows.Forms.CheckBox chb_SavePrev;
        private System.Windows.Forms.Label lbl_photoedit;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chb_wait;
        private System.Windows.Forms.CheckBox chb_delTempFile;
        private System.Windows.Forms.CheckBox chb_loadBGM;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lbl_shift;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chb_updates;
        private System.Windows.Forms.Label lbl_size;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.CheckBox chb_ExportBot;
        private System.Windows.Forms.Button btn_setASdef;
        private System.Windows.Forms.Label lbl_lang;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}