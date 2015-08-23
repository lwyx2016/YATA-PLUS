using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YATA.Converter
{
    public partial class ConvertSETTINGS : Form
    {
        public FileConverter.ConvertType RET { get; set; }
        public ConvertSETTINGS()
        {
            InitializeComponent();
            if (Form1.APP_LNG.Trim().ToLower() != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\CONVERTsettings.txt"))
            {
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\CONVERTsettings.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.wavTOcwav;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.brstmTOwav;
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.wavTObrstm;
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.wavTObcstm;
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.brstmTObcstm;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.wavTOcwav_No_opt;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.nothing;
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RET = FileConverter.ConvertType.play_file;
            Close();
        }

        private void ConvertSETTINGS_Load(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }
    }
}
