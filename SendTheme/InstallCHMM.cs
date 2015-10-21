using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using System.Diagnostics;
using System.IO.Compression;

namespace YATA.SendTheme
{
    public partial class InstallCHMM : Form
    {
        string theme;
        string SMDH = "";
        string ogg = "";

        public InstallCHMM(string themepath)
        {
            InitializeComponent();
            theme = themepath;
            #region language
            if (Form1.APP_LNG != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\installTOchmm.txt"))
            {
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\installTOchmm.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("lbl")) { ((Label)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("chb")) { ((CheckBox)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                    }
                }
            }
            #endregion
        }

        private void InstallCHMM_Load(object sender, EventArgs e)
        {
            textBox1.Text = Path.GetFileNameWithoutExtension(theme);
        }

        void CreatePackage()
        {
            if (File.Exists("file.zip")) File.Delete("file.zip");
            string ZIPThemepath = @"Package\" + textBox1.Text.Trim();
            Directory.CreateDirectory("Package");
            Directory.CreateDirectory(ZIPThemepath);
            File.Copy(theme, ZIPThemepath + @"\body_LZ.bin");            
            if (chb_pngprev.Checked)
            {
                Sim frm = new Sim();
                Form1.Preview_PATH = ZIPThemepath + @"\Preview.png";
                Form1.generating_preview = true;
                frm.ShowDialog();
                Form1.generating_preview = false;
            }
            if (File.Exists(Path.GetDirectoryName(theme) + @"\bgm.bcstm"))
            {
                File.Copy(Path.GetDirectoryName(theme) + @"\bgm.bcstm", ZIPThemepath + @"\bgm.bcstm");
                if (chb_bmgprev.Checked && File.Exists(ogg))
                {
                    File.Copy(ogg, ZIPThemepath + @"\BGM.ogg");
                }
            }
            if (chb_smdhinfo.Checked && File.Exists(SMDH))
            {
                File.Copy(SMDH, ZIPThemepath + @"\info.smdh");
            }
            ZipFile.CreateFromDirectory("Package", "file.zip");
            Directory.Delete("Package", true);
            return;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                btn_send.Enabled = false;
                lbl_wait.Visible = true;
                this.Refresh();
                CreatePackage();
                SendToCHMM2 Sender = new SendToCHMM2();
                int ret = Sender.send(textBox2.Text, "file.zip", 5000,false);
                if (ret == 0) MessageBox.Show("Done");
                else /*if (ret != -1)*/ MessageBox.Show("There was an error, the theme was not sent");
                lbl_wait.Visible = false;
            }
            else { MessageBox.Show("You must enter a valid name and ip address"); }
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                btn_send.Enabled = false;
                lbl_wait.Visible = true;
                this.Refresh();
                CreatePackage();
                SendToCHMM2 Sender = new SendToCHMM2();
                int ret = Sender.send(textBox2.Text, "file.zip", 5000,true);
                if (ret == 0) MessageBox.Show("Done");
                else /*if (ret != -1)*/ MessageBox.Show("There was an error, the theme was not sent");
                try
                {
                    System.Diagnostics.Process.Start("SENDLOG.txt");
                }
                catch
                { //not opened 
                }
                lbl_wait.Visible = false;
            }
            else { MessageBox.Show("You must enter a valid name and ip address"); return; }
        }
    }
}
