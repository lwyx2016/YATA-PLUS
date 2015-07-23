using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Main changes from the previous version of YATA+:\r\n -Support for 'Simple' 3ds themes\r\n -Theme settings now describes every color\r\n -Adding images to themes that doesn't have\r\n -Now vgmstream and is DLLs isn't deleted anymore\r\n -Support for playing bgm.bcstm files\r\n -Built-in player for Cwavs\r\n -Other minor bugs fixed");
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
    }
}
