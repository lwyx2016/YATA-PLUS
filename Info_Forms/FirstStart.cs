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
    public partial class FirstStart : Form
    {
        public FirstStart()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/relase-yet-another-theme-application-plus-yata-3ds-theme-editor.393355/");
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            Prefs frm = new Prefs();
            frm.build_settings();
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/tutorial-creating-a-3ds-custom-theme-from-scratch-with-yata-v1-1.393602/");
        }

        private void FirstStart_Load(object sender, EventArgs e)
        {
            label3.Text = Form1.APP_STRING_version;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
