using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace YATA {
    public partial class Prefs : Form {
        public Prefs() {
            InitializeComponent();
            checkBox2.Checked = Form1.APP_ShowUI_Sim;
            checkBox3.Checked = Form1.APP_AutoGen_preview;
            checkBox4.Checked = Form1.APP_Wait_editor;
            checkBox5.Checked = Form1.APP_Clean_On_exit;
            checkBox6.Checked = Form1.APP_Auto_Load_bgm;
            checkBox7.Checked = Form1.APP_check_UPD;
            checkBox8.Checked = Form1.APP_export_both_screens;
            textBox1.Text = Form1.APP_photo_edtor;
            numericUpDown1.Value = Form1.APP_Move_buttons_colors;
            numericUpDown2.Value = Form1.APP_SETT_SIZE_X;
            numericUpDown3.Value = Form1.APP_SETT_SIZE_Y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            checkBox3.Checked = false;
            checkBox4.Checked = true;
            checkBox5.Checked = false;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            numericUpDown1.Value = 10;
            Form1.APP_photo_edtor = "";
            build_settings();
            Form1.load_prefs();
            this.Close();
        }

        public void build_settings()
        {
            string[] lines = new string[12];
            lines[0] = "ui_sim=" + checkBox2.Checked.ToString();
            lines[1] = "gen_prev=" + checkBox3.Checked.ToString();
            lines[2] = "photo_edit=" + textBox1.Text;
            lines[3] = "wait_editor=" + checkBox4.Checked.ToString();
            lines[4] = "clean_on_exit=" + checkBox5.Checked.ToString();
            lines[5] = "load_bgm=" + checkBox6.Checked.ToString();
            lines[6] = "first_start_v4=false";
            lines[7] = "shift_btns=" + numericUpDown1.Value.ToString();
            lines[8] = "check_updates=" + checkBox7.Checked.ToString();
            lines[9] = "happy_easter=false";
            lines[10] = "sett_size=" + numericUpDown2.Value.ToString() + numericUpDown3.Value.ToString();
            lines[11] = "exp_both_screens=" + checkBox8.Checked.ToString();
            System.IO.File.Delete("Settings.ini");
            System.IO.File.WriteAllLines("Settings.ini", lines);
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            build_settings();
            Form1.load_prefs();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox7.Checked) MessageBox.Show("If you don't update YATA+, you may miss some important new features in the next updates....");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://youtu.be/JilVtq_Wd6U");
        }
    }
}
