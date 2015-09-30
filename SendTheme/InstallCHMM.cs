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
        }

        private void InstallCHMM_Load(object sender, EventArgs e)
        {

        }

        void CreatePackage()
        {
            if (File.Exists("file.zip")) File.Delete("file.zip");
            string ZIPThemepath = @"Package\" + Path.GetFileName(theme);
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
                CreatePackage();
            }
            else { MessageBox.Show("You must enter a valid name and ip address"); return; }
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

                }
            }
        }
    }
}
