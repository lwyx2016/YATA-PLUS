using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Text;
using System.Windows.Forms;

namespace YATA
{
    public partial class Install : Form
    {
        string SMDH = "";
        string ogg = "";
        FtpClient conn = new FtpClient();
        string file = "";
        byte[][] DataToSend;
        string[] DataNames;
        public Install(string fl)
        {
            file = fl;
            InitializeComponent();
            try { 
            #region language
            if (Form1.APP_LNG != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\install.txt"))
            {
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\install.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("lbl")) { ((Label)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                    }
                }
            }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error initializing the language data for this window, try to set the language to english, if you can't because the settings windows crashes too, delete the languages folder");
                MessageBox.Show("for translators: 'Lbl_something' is diffrent from 'lbl_something', follow the template");
                MessageBox.Show("Exception details: " + ex.Message);
                this.Close();
            }
        }

        void CreatePackage()
        {
            List<byte[]> Data = new List<byte[]>();
            List<string> Names = new List<string>();
            Data.Add(File.ReadAllBytes(file));
            Names.Add("body_LZ.bin");
            if (chb_pngprev.Checked)
            {
                Sim frm = new Sim();
                frm.Show();
                Data.Add(frm.GeneratePreview());
                Names.Add("Preview.png");
            }
            if (File.Exists(Path.GetDirectoryName(file) + @"\bgm.bcstm"))
            {
                Data.Add(File.ReadAllBytes(Path.GetDirectoryName(file) + @"\bgm.bcstm"));
                Names.Add("bgm.bcstm");
                if (chb_bmgprev.Checked && File.Exists(ogg))
                {
                    Data.Add(File.ReadAllBytes(ogg));
                    Names.Add("BGM.ogg");
                }
            }
            if (chb_smdhinfo.Checked && File.Exists(SMDH))
            {
                Data.Add(File.ReadAllBytes(SMDH));
                Names.Add("info.smdh");
            }
            DataToSend = Data.ToArray();
            DataNames = Names.ToArray();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_send.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            lbl_wait.Visible = true;
            try
            {
                CreatePackage();                
                conn.Host = textBox2.Text;
                conn.ConnectTimeout = 40000;
                conn.Port = 5000;
                conn.Credentials = new NetworkCredential("Anonymus", "");
                conn.Connect();               
                if (!conn.DirectoryExists(textBox3.Text)) conn.CreateDirectory(textBox3.Text);
                if (!conn.DirectoryExists(textBox3.Text + textBox1.Text)) conn.CreateDirectory(textBox3.Text + textBox1.Text);
                for (int i = 0; i < DataToSend.Length; i++) { 
                    Stream ostream = conn.OpenWrite(textBox3.Text + textBox1.Text + @"/" + DataNames[i]);
                    ostream.Write(DataToSend[i], 0, DataToSend[i].Length);
                    ostream.Flush();
                    ostream.Dispose();
                    ostream.Close();
                }
            }
            catch (Exception ex)
            {
                btn_send.Enabled = true;
                textBox1.Enabled = true;
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            try
            {
                conn.Dispose();                
            }
            catch { }
            MessageBox.Show(Form1.messages[19]);
            lbl_wait.Visible = false;
        }

        private void Install_Load(object sender, EventArgs e)
        {
            textBox1.Text = Path.GetFileNameWithoutExtension(file);
        }

        private void chb_bmgprev_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_bmgprev.Checked)
            {
                OpenFileDialog opn = new OpenFileDialog();
                opn.Filter = "Ogg file |*.ogg";
                opn.Title = "Select a ogg file";
                if (opn.ShowDialog() == DialogResult.OK)
                {
                    ogg = opn.FileName;
                }
                else
                {
                    chb_bmgprev.Checked = false;
                    ogg = "";
                }
            }
        }

        private void chb_smdhinfo_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_smdhinfo.Checked)
            {
                OpenFileDialog opn = new OpenFileDialog();
                opn.Filter = "Smdh file |*.smdh";
                opn.Title = "Select a smdh file";
                if (opn.ShowDialog() == DialogResult.OK)
                {
                    SMDH = opn.FileName;
                }
                else
                {
                    chb_smdhinfo.Checked = false;
                    SMDH = "";
                }
            }
        }
    }
}
