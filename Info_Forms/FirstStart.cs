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
            if (checkBox1.Checked)
            {
                MessageBox.Show("The application must be restarted, you must reopen it manually");
                if (!File.Exists("NoWMP.txt")) File.WriteAllText("NoWMP.txt", "If this file exists YATA won't load the windows media player");
                Application.Exit();
            }
            else
            {
                if (File.Exists("NoWMP.txt")) File.Delete("NoWMP.txt");
            }
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/tutorial-creating-a-3ds-custom-theme-from-scratch-with-yata-v1-1.393602/");
        }

        private void FirstStart_Load(object sender, EventArgs e)
        {
            label3.Text = Form1.APP_STRING_version;
            checkBox1.Checked = File.Exists("NoWMP.txt");
        }
    }
}
