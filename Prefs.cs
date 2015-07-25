using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YATA {
    public partial class Prefs : Form {
        public Prefs() {
            InitializeComponent();
        }

        private void Prefs_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Form1.APP_ShowUI_preview;
            checkBox2.Checked = Form1.APP_ShowUI_Sim;
            checkBox3.Checked = Form1.APP_AutoGen_preview;
            checkBox4.Checked = Form1.APP_Wait_editor;
            checkBox5.Checked = Form1.APP_Clean_On_exit;
            checkBox6.Checked = Form1.APP_Auto_Load_bgm;
            textBox1.Text = Form1.APP_photo_edtor;
            numericUpDown1.Value = Form1.APP_Move_buttons_colors;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = false;
            checkBox4.Checked = true;
            checkBox5.Checked = false;
            checkBox6.Checked = true;
            numericUpDown1.Value = 10;
            Form1.APP_photo_edtor = "";
            save_settings();
            Form1.load_prefs();
            this.Close();
        }

        void save_settings()
        {
            string[] lines = new string[9];
            lines[0] = "ui_prev=" + checkBox1.Checked.ToString();
            lines[1] = "ui_sim=" + checkBox2.Checked.ToString();
            lines[2] = "gen_prev=" + checkBox3.Checked.ToString();
            lines[3] = "photo_edit=" + textBox1.Text;
            lines[4] = "wait_editor=" + checkBox4.Checked.ToString();
            lines[5] = "clean_on_exit=" + checkBox5.Checked.ToString();
            lines[6] = "load_bgm=" + checkBox6.Checked.ToString();
            lines[7] = "first_start=false";
            lines[8] = "shift_btns=" + numericUpDown1.Value.ToString();
            System.IO.File.Delete("Settings.ini");
            System.IO.File.WriteAllLines("Settings.ini", lines);
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save_settings();
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
    }
}
