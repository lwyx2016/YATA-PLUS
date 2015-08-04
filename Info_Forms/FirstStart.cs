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
            string[] lines = new string[8];
            lines[0] = "ui_prev=" + Form1.APP_ShowUI_preview;
            lines[1] = "ui_sim=" + Form1.APP_ShowUI_Sim;
            lines[2] = "gen_prev=" + Form1.APP_AutoGen_preview;
            lines[3] = "photo_edit=" + Form1.APP_photo_edtor;
            lines[4] = "wait_editor=" + Form1.APP_Wait_editor;
            lines[5] = "clean_on_exit=" + Form1.APP_Clean_On_exit;
            lines[6] = "load_bgm=" + Form1.APP_Auto_Load_bgm;
            lines[6] = "first_start=false";
            System.IO.File.Delete("Settings.ini");
            System.IO.File.WriteAllLines("Settings.ini", lines);
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/tutorial-creating-a-3ds-custom-theme-from-scratch-with-yata-v1-1.393602/");
        }
    }
}
