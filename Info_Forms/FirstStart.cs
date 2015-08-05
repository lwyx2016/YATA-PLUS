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
            string[] settings = System.IO.File.ReadAllLines("Settings.ini");
            List<string> NEWsettings = new List<string>();
            NEWsettings.AddRange(settings);
            NEWsettings.Add("first_start_v3=false");
            System.IO.File.Delete("Settings.ini");
            System.IO.File.WriteAllLines("Settings.ini", NEWsettings.ToArray());
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/tutorial-creating-a-3ds-custom-theme-from-scratch-with-yata-v1-1.393602/");
        }

        private void FirstStart_Load(object sender, EventArgs e)
        {
            label3.Text = Form1.APP_STRING_version;
            if (DateTime.Today.Day <= 15 && DateTime.Today.Month == 8) { label4.Text = "Also, in the same thread there is a opened poll, where you can vote if you want me to make some videos on how to use yata + ,hurry up because you can only vote until August 15"; }
        }
    }
}
