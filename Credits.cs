﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YATA
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Main changes from the previous version of YATA+:\r\n -Conversions now support spaces\r\n -Conversions now support multiple files at once\r\n -Drag and drop support\r\n -New exception handling\r\n -Other minor fixes");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/release-yet-another-theme-application-yata.379209/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text); 
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.Text); 
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel4.Text); 
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
                    MessageBox.Show("Hey, looks like there is a new version of yata+ out there !! \r\n What are you waiting for ? Go now on the official thread (Credits -> Official thread) and download it !! \r\n\r\n You can disable the auto check for updates in the preferences");
                }
                else MessageBox.Show("You're on the latest version of YATA+ !!");
            }
            catch { MessageBox.Show("Error while searching for updates"); }
        }
    }
}
