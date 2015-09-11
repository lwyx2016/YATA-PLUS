using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YATA
{

    public partial class Credits : Form
    {
        List<String> messages = new List<string>() {
            "Hey, looks like there is a new version of yata+ out there !! \r\n What are you waiting for ? Go now on the official thread (Credits -> Official thread) and download it !! \r\n\r\n You can disable the auto check for updates in the preferences",
            "You're on the latest version of YATA+ !!",
            "Error while searching for updates",
        };
        public Credits()
        {
            InitializeComponent();
            try { 
            #region language
            if (Form1.APP_LNG.Trim().ToLower() != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\credits.txt"))
            {
                messages.Clear();
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\credits.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("lbl")) { ((Label)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("@")) { messages.Add(line.Replace(@"\r\n", Environment.NewLine).Remove(0, 1)); }
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

        private void button2_Click(object sender, EventArgs e)
        {
            FirstStart dlg = new FirstStart();
            dlg.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/release-yet-another-theme-application-yata.379209/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/relase-yet-another-theme-application-plus-yata-3ds-theme-editor.393355/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                    System.Net.WebClient d = new System.Net.WebClient();
                if (Convert.ToInt32(d.DownloadString("https://raw.githubusercontent.com/exelix11/YATA-PLUS/master/PublicVersion.txt")) > Form1.APP_Public_version)
                {
                    MessageBox.Show(messages[0]);
                }
                else MessageBox.Show(messages[1]);
            }
            catch { MessageBox.Show(messages[2]); }
        }


        private void Link_CLICKED(object sender, EventArgs e)
        {
            LinkLabel snd = (LinkLabel)sender;
            System.Diagnostics.Process.Start(snd.Text);
        }

        private void Credits_Load(object sender, EventArgs e)
        {
            label3.Text = Form1.APP_STRING_version;
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
