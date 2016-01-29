namespace YATA.Converter
{
    partial class ConvertSETTINGS
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_WavtoCWAV = new System.Windows.Forms.Button();
            this.btn_convert = new System.Windows.Forms.Button();
            this.btn_WAVbrstm = new System.Windows.Forms.Button();
            this.btn_BRSTMbcstm = new System.Windows.Forms.Button();
            this.btn_WAVbcstm = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(309, 242);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_WavtoCWAV
            // 
            this.btn_WavtoCWAV.Location = new System.Drawing.Point(24, 12);
            this.btn_WavtoCWAV.Name = "btn_WavtoCWAV";
            this.btn_WavtoCWAV.Size = new System.Drawing.Size(327, 41);
            this.btn_WavtoCWAV.TabIndex = 1;
            this.btn_WavtoCWAV.Text = "Convert from WAV to CWAV";
            this.btn_WavtoCWAV.UseVisualStyleBackColor = true;
            this.btn_WavtoCWAV.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_convert
            // 
            this.btn_convert.Location = new System.Drawing.Point(24, 59);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(327, 41);
            this.btn_convert.TabIndex = 2;
            this.btn_convert.Text = "Convert from CWAV/BCSTM/BRSTM (Vgmstream) to WAV";
            this.btn_convert.UseVisualStyleBackColor = true;
            this.btn_convert.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_WAVbrstm
            // 
            this.btn_WAVbrstm.Location = new System.Drawing.Point(27, 106);
            this.btn_WAVbrstm.Name = "btn_WAVbrstm";
            this.btn_WAVbrstm.Size = new System.Drawing.Size(151, 41);
            this.btn_WAVbrstm.TabIndex = 4;
            this.btn_WAVbrstm.Text = "Convert from WAV to BRSTM";
            this.btn_WAVbrstm.UseVisualStyleBackColor = true;
            this.btn_WAVbrstm.Click += new System.EventHandler(this.button5_Click);
            // 
            // btn_BRSTMbcstm
            // 
            this.btn_BRSTMbcstm.Location = new System.Drawing.Point(197, 106);
            this.btn_BRSTMbcstm.Name = "btn_BRSTMbcstm";
            this.btn_BRSTMbcstm.Size = new System.Drawing.Size(151, 41);
            this.btn_BRSTMbcstm.TabIndex = 5;
            this.btn_BRSTMbcstm.Text = "Convert from BRSTM to BCSTM";
            this.btn_BRSTMbcstm.UseVisualStyleBackColor = true;
            this.btn_BRSTMbcstm.Click += new System.EventHandler(this.button6_Click);
            // 
            // btn_WAVbcstm
            // 
            this.btn_WAVbcstm.Location = new System.Drawing.Point(24, 153);
            this.btn_WAVbcstm.Name = "btn_WAVbcstm";
            this.btn_WAVbcstm.Size = new System.Drawing.Size(327, 41);
            this.btn_WAVbcstm.TabIndex = 6;
            this.btn_WAVbcstm.Text = "Convert from WAV to BCSTM";
            this.btn_WAVbcstm.UseVisualStyleBackColor = true;
            this.btn_WAVbcstm.Click += new System.EventHandler(this.button7_Click);
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(24, 200);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(327, 23);
            this.btn_play.TabIndex = 7;
            this.btn_play.Text = "Play the file";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.button8_Click);
            // 
            // ConvertSETTINGS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 266);
            this.ControlBox = false;
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.btn_WAVbcstm);
            this.Controls.Add(this.btn_BRSTMbcstm);
            this.Controls.Add(this.btn_WAVbrstm);
            this.Controls.Add(this.btn_convert);
            this.Controls.Add(this.btn_WavtoCWAV);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConvertSETTINGS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConvertSETTINGS_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.Button btn_WavtoCWAV;
        public System.Windows.Forms.Button btn_convert;
        public System.Windows.Forms.Button btn_WAVbrstm;
        public System.Windows.Forms.Button btn_BRSTMbcstm;
        public System.Windows.Forms.Button btn_WAVbcstm;
        public System.Windows.Forms.Button btn_play;
    }
}