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
    public partial class ImgSIZES : Form
    {
        public ImgSIZES()
        {
            InitializeComponent();
            if (Form1.APP_LNG.Trim().ToLower() != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\imgSIZES.txt"))
            {
                label1.Text = File.ReadAllText(@"languages\" + Form1.APP_LNG + @"\imgSIZES.txt");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/threads/tutorial-creating-a-3ds-custom-theme-from-scratch-with-yata-v1-1.393602/");
        }

        private void ImgSIZES_Load(object sender, EventArgs e)
        {

        }
    }
}
