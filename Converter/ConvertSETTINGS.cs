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
        public ConvertType RET { get; set; }
        public ConvertSETTINGS()
        {
            InitializeComponent();
            try { 
            #region language
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
                #endregion
            }
            catch (Exception ex)
            {
                RET = ConvertType.nothing;
                MessageBox.Show("There was an error initializing the language data for this window, try to set the language to english, if you can't because the settings windows crashes too, delete the languages folder");
                MessageBox.Show("for translators: 'Lbl_something' is diffrent from 'lbl_something', follow the template");
                MessageBox.Show("Exception details: " + ex.Message);
                this.Close();
            }
        }

        public enum ConvertType
        {
            brstmTOwav,
            wavTObrstm,
            wavTObcstm,
            brstmTObcstm,
            wavTOcwav,
            cwavTowav,
            wavTOcwav_No_opt,
            nothing,
            play_file
        }


        private void button2_Click(object sender, EventArgs e)
        {
            RET = ConvertType.wavTOcwav;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RET = ConvertType.brstmTOwav;
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RET = ConvertType.wavTObrstm;
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RET = ConvertType.wavTObcstm;
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RET = ConvertType.brstmTObcstm;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RET = ConvertType.wavTOcwav_No_opt;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RET = ConvertType.nothing;
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RET = ConvertType.play_file;
            Close();
        }

        private void ConvertSETTINGS_Load(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }
    }
}
