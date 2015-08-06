using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace YATA {
    public partial class Sim : Form {

        Bitmap[] imgs;
        Image bottomImg;
        Image topImg;
        Image frame1, frame2, frame3;
        
        Image bat = Properties.Resources.battery;
        Image inter = Properties.Resources.net;
        Image note = Properties.Resources.notes;
        Image friend = Properties.Resources.friends;
        Image news = Properties.Resources.news;
        Image web = Properties.Resources.web;
        Image mii = Properties.Resources.miiverse;

        int frameCnt = 0;

        public Sim() {
            InitializeComponent();
            imgs = Form1.imageArray;
            Overlay_back_img.Parent = topImage;
            overlay_text_img.Parent = Overlay_back_img;
            setColors();
            loadImages();
        }

        void setColors()
        {
            byte[] tempbytes;
            Bitmap img;
            #region TopOverlay
            //Top overlay
            if (Form1.enableSec[14] == 1) //If custom color is enabled
            {
                tempbytes = Form1.colChunks[13];
                img = new Bitmap(Overlay_back_img.Image);
                Color back = Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]);
                for (int i = 0; i < img.Width; i++)
                {
                    for (int ii = 0; ii < img.Height; ii++)
                    {
                        if (img.GetPixel(i, ii).A != 0) img.SetPixel(i, ii, back);
                    }
                }
                Overlay_back_img.Image = img;
                Color text = Color.FromArgb(0xFF, tempbytes[9], tempbytes[10], tempbytes[11]);
                img = new Bitmap(overlay_text_img.Image);
                for (int i = 0; i < img.Width; i++)
                {
                    for (int ii = 0; ii < img.Height; ii++)
                    {
                        if (img.GetPixel(i, ii).A != 0) img.SetPixel(i, ii, text);
                    }
                }
                overlay_text_img.Image = img;
            }
            #endregion
        }

        private void loadImages() {
            bottomImg = new Bitmap(imgs[1]);
            Rectangle f1 = new Rectangle(0, 0, 320, 240);
            Rectangle f2 = new Rectangle(320, 0, 320, 240);
            Rectangle f3 = new Rectangle(640, 0, 320, 240);
            frame1 = new Bitmap(f1.Width, f1.Height);
            frame2 = new Bitmap(f2.Width, f2.Height);
            frame3 = new Bitmap(f3.Width, f3.Height);
            using (Graphics g = Graphics.FromImage(frame1)) { g.DrawImage(bottomImg, new Rectangle(0, 0, frame1.Width, frame1.Height), f1, GraphicsUnit.Pixel);}
            using (Graphics g = Graphics.FromImage(frame2)) { g.DrawImage(bottomImg, new Rectangle(0, 0, frame2.Width, frame2.Height), f2, GraphicsUnit.Pixel); }
            using (Graphics g = Graphics.FromImage(frame3)) { g.DrawImage(bottomImg, new Rectangle(0, 0, frame3.Width, frame3.Height), f3, GraphicsUnit.Pixel); }
            topImg = new Bitmap(imgs[0]);
            topImage.Image = topImg;
            bottomImage.Image = bottomImg;
            updateGUI();
        }

        private void Sim_KeyDown(object sender, KeyEventArgs e) {
            using (Graphics gr = Graphics.FromImage(bottomImage.Image)) {
                int frame = 0;
                if(Form1.bottomDraw == 3 && Form1.bottomFrame == 2){
                    if (e.KeyCode == Keys.Right) {
                        frameCnt++;
                        if (frameCnt > 2) frameCnt = 0;
                    }
                    else if (e.KeyCode == Keys.Left) {
                        frameCnt--;
                        if (frameCnt < 0) frameCnt = 2;
                    }
                    frame = frameCnt;
                }
                else if (Form1.bottomDraw == 3 && Form1.bottomFrame == 4) {
                    int[] pattern = { 0, 1, 2, 1};
                    if (e.KeyCode == Keys.Right) {
                        frameCnt++;
                        if (frameCnt == 3) frameCnt = 0;
                    }
                    else if (e.KeyCode == Keys.Left) {
                        frameCnt--;
                        if (frameCnt < 0) frameCnt = 3;
                    }
                    frame = pattern[frameCnt];
                }
                switch (frame) {
                    case 0:
                        bottomImage.Image = frame1;
                        break;
                    case 1:
                        bottomImage.Image = frame2;
                        break;
                    case 2:
                        bottomImage.Image = frame3;
                        break;
                }
                updateGUI();
                gr.Flush();
                gr.Dispose();
            }
        }

        private void updateGUI()
        {
            if (Form1.APP_ShowUI_Sim & !Form1.generating_preview)
            {
                using (Graphics gr = Graphics.FromImage(topImage.Image))
                {
                    gr.DrawImage(bat, new Point(368, -6));
                    gr.DrawImage(inter, new Point(1, 0));
                    gr.Flush();
                    gr.Dispose();
                }
                using (Graphics gr = Graphics.FromImage(bottomImage.Image))
                {

                    gr.DrawImage(note, new Point(63, 3));
                    gr.DrawImage(friend, new Point(104, 3));
                    gr.DrawImage(news, new Point(146, 3));
                    gr.DrawImage(web, new Point(188, 3));
                    gr.DrawImage(mii, new Point(228, 3));
                    gr.Flush();
                    gr.Dispose();

                }
            }
            else if (Form1.APP_ShowUI_preview & Form1.generating_preview)
            {
                using (Graphics gr = Graphics.FromImage(topImage.Image))
                {
                    gr.DrawImage(bat, new Point(368, -6));
                    gr.DrawImage(inter, new Point(1, 0));
                    gr.Flush();
                    gr.Dispose();
                }
                using (Graphics gr = Graphics.FromImage(bottomImage.Image))
                {

                    gr.DrawImage(note, new Point(63, 3));
                    gr.DrawImage(friend, new Point(104, 3));
                    gr.DrawImage(news, new Point(146, 3));
                    gr.DrawImage(web, new Point(188, 3));
                    gr.DrawImage(mii, new Point(228, 3));
                    gr.Flush();
                    gr.Dispose();

                }
            }
        }

        private void Sim_Load(object sender, EventArgs e)
        {
            if (Form1.generating_preview)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "png file|*.png";
                save.Title = "save preview";
                if (Form1.Preview_PATH == null && save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Image preview = new Bitmap(400, 240);
                    Graphics g = Graphics.FromImage(preview);
                    if (!Form1.APP_ShowUI_Sim) { g.DrawImage(topImg, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); } else { g.DrawImage(topImage.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(Overlay_back_img.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(overlay_text_img.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); }
                        preview.Save(save.FileName);
                    this.Close();
                }
                else if (Form1.Preview_PATH != null)
                {
                    Image preview = new Bitmap(400, 240);
                    Graphics g = Graphics.FromImage(preview);
                    if (!Form1.APP_ShowUI_Sim) { g.DrawImage(topImg, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); } else { g.DrawImage(topImage.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(Overlay_back_img.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(overlay_text_img.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); }
                        preview.Save(Form1.Preview_PATH);
                    this.Close();
                }
                else this.Close(); 
            }
        }

    }
}
