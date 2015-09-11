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
        FtpClient conn = new FtpClient();
        string file = "";
        public Install(string fl)
        {
            file = fl;
            InitializeComponent();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_send.Enabled = false;
            textBox1.Enabled = false;
            try
            {
                if (File.Exists(Path.GetTempPath() + "install_theme_tmp.zip")) File.Delete(Path.GetTempPath() + "install_theme_tmp.zip");
                if (Directory.Exists(Path.GetTempPath() + "theme_tmp")) Directory.Delete(Path.GetTempPath() + "theme_tmp", true);
                Directory.CreateDirectory(Path.GetTempPath() + "theme_tmp");
                System.IO.File.Copy(file, Path.GetTempPath() + @"theme_tmp\install_body_LZ.bin");
                if (File.Exists(Path.GetDirectoryName(file) + @"\bgm.bcstm") && Form1.useBGM == 1) System.IO.File.Copy(Path.GetDirectoryName(file) + @"\bgm.bcstm", Path.GetTempPath() + @"theme_tmp\install_bgm.bcstm");
                ZipFile.CreateFromDirectory(Path.GetTempPath() + "theme_tmp", Path.GetTempPath() + "install_theme_tmp.zip");
                conn.Host = textBox1.Text;
                conn.ConnectTimeout = 20000;
                conn.Port = 5000;
                conn.Credentials = new NetworkCredential("Anonymus","");
                conn.Connect();
                Stream ostream = conn.OpenWrite(@"/install_theme_tmp.zip");
                Byte[] data = System.IO.File.ReadAllBytes(Path.GetTempPath() + "install_theme_tmp.zip");
                ostream.Write(data, 0, data.Length);                
                ostream.Flush();
                ostream.Dispose();
                ostream.Close();
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
            if (File.Exists(Path.GetTempPath() + "install_theme_tmp.zip")) File.Delete(Path.GetTempPath() + "install_theme_tmp.zip");
            if (Directory.Exists(Path.GetTempPath() + "theme_tmp")) Directory.Delete(Path.GetTempPath() + "theme_tmp", true);
            lbl_2.Visible = true;
        }

        private void Install_Load(object sender, EventArgs e)
        {

        }
    }
}
