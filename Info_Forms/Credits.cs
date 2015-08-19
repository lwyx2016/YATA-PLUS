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

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Main changes from the previous version of YATA+:\r\n -Fixed a bug in the theme simulator\r\n -Now the no background flag is supported for the mood-matrix type themes");
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
            MessageBox.Show("Updates are disabled in this version of YATA");
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
    }
}
