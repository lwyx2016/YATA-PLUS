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
            MessageBox.Show("Main changes from the original version of YATA: \r\n -Support for every image editor \r\n -Fixed bug that swaps Close button colors with Bottom Btns ones in Theme settings\r\n -Support for user preferences\r\n -Loadin uncompressed themes (like the ones from everey file explorer)\r\n -CWAVs to WAVs converter \r\n -WAVs to CWAVs converter(you'll need the 3ds sdk for this)\r\n -Fixed some bugs that corrupt the theme when saving\r\n -other minor fixes and functions");
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
