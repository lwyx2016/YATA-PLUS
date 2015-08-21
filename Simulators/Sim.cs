using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace YATA
{
    public partial class Sim : Form
    {

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

        public Sim()
        {
            InitializeComponent();
            imgs = Form1.imageArray;
            Overlay_LR_TOP_img.Parent = topImage;
            Arrows_bottom.Parent = bottomImage;
            Arrows_bottom.Location = new Point(0, 0);
            Top_screen_overlay.Parent = Arrows_bottom;
            Top_screen_overlay.Location = new Point(0, 0);
            if (!Form1.APP_ShowUI_Sim)
            {
                Overlay_LR_TOP_img.Visible = false;
                Arrows_bottom.Visible = false;
                Top_screen_overlay.Visible = false;
            }
            setColors();
            loadImages();
        }

        void setColors()
        {
            byte[] tempbytes;
            Bitmap img;
            #region Top_Overlay
            //Top overlay
            if (Form1.enableSec[14] == 1) //If custom color is enabled
            {
                tempbytes = Form1.colChunks[13];
                img = new Bitmap(Properties.Resources.top_overlay_background);
                setColor(img, Color.FromArgb(215, tempbytes[0], tempbytes[1], tempbytes[2]));
                Overlay_LR_TOP_img.BackgroundImage = img;

                img = new Bitmap(Properties.Resources.top_overlay_text);
                setColor(img, Color.FromArgb(215, tempbytes[9], tempbytes[10], tempbytes[11]));
                Overlay_LR_TOP_img.Image = img;
            }
            #endregion
            #region Bot_Arrows
            if (Form1.enableSec[6] == 1) //If custom color is enabled
            {
                tempbytes = Form1.colChunks[4];
                img = new Bitmap(Properties.Resources.Bottom_arrow_fore);
                setColor(img, Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                Arrows_bottom.Image = img;
            }
            if (Form1.enableSec[7] == 1) //If custom color is enabled
            {
                tempbytes = Form1.colChunks[3];
                img = new Bitmap(Properties.Resources.Bottom_arrow_back);
                setColor(img, Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                Arrows_bottom.BackgroundImage = img;
            }
            #endregion
            #region iconResizer
            Color back = Color.White;
            Color separator = Color.Gray;
            Color icon = Color.FromArgb(255, 145, 174, 208);
            if (Form1.enableSec[13] == 1) //If custom color is enabled
            {
                tempbytes = Form1.colChunks[12];
                back = Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]);
                icon = Color.FromArgb(0xFF, tempbytes[12], tempbytes[13], tempbytes[14]);
                separator = Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]);
            }
            img = Properties.Resources.bottom_Resizer_mask;
            for (int i = 0; i < img.Width; i++)
            {
                for (int ii = 0; ii < img.Height; ii++)
                {
                    Color col = img.GetPixel(i, ii);
                    if (col.A != 0)
                    {
                        if (col.R == 255) img.SetPixel(i, ii, separator);
                        else if (col.B == 255) img.SetPixel(i, ii, icon);
                        else img.SetPixel(i, ii, back);
                    }
                }
            }
            Top_screen_overlay.Image = img;
            #endregion
            #region BottomBar
            Color Default = Color.FromArgb(255, 224, 221, 216);
            Color Shading = Color.White;
            Color TextShadow = Color.FromArgb(255, 187, 184, 179);
            Color text = Color.FromArgb(255, 72, 71, 66);
            if (Form1.enableSec[7] == 1) //If custom color is enabled
            {
                tempbytes = Form1.colChunks[5];
                Default = Color.FromArgb(0xFF, tempbytes[7], tempbytes[8], tempbytes[9]);
                Shading = Color.FromArgb(0xFF, tempbytes[4], tempbytes[5], tempbytes[6]);
                TextShadow = Color.FromArgb(0xFF, tempbytes[20], tempbytes[21], tempbytes[22]);
                text = Color.FromArgb(0xFF, tempbytes[23], tempbytes[24], tempbytes[25]);
            }
            img = Properties.Resources.sim_bottom_mask;
            for (int i = 0; i < img.Width; i++)
            {
                for (int ii = 0; ii < img.Height; ii++)
                {
                    Color col = img.GetPixel(i, ii);
                    if (col.A != 0)
                    {
                        if (col.R == 255) img.SetPixel(i, ii, text);
                        else if (col.G == 255) img.SetPixel(i, ii, Shading);
                        else if (col.B == 255) img.SetPixel(i, ii, TextShadow);
                        else img.SetPixel(i, ii, Default);
                    }
                }
            }
            #endregion
            #region cursor
            Bitmap Cursor_img = Properties.Resources.Cursor;
            tempbytes = Form1.colChunks[0];
            Color Cursor_main = Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]);
            Color Cursor_glow = Color.FromArgb(0xFF, tempbytes[9], tempbytes[10], tempbytes[11]);
            for (int i = 0; i < Cursor_img.Width; i++)
            {
                for (int ii = 0; ii < Cursor_img.Height; ii++)
                {
                    Color col = Cursor_img.GetPixel(i, ii);                    
                    if (col.B == 255 && col.A == 255)
                    {
                        Cursor_img.SetPixel(i, ii, Cursor_main);
                    }
                    else if (col.A != 0)
                    {
                        Cursor_img.SetPixel(i, ii, Cursor_glow);
                    }
                }
            }
            Graphics g = Graphics.FromImage(img);
            g.DrawImage(Cursor_img,new Rectangle(24,44, Cursor_img.Width, Cursor_img.Height));
            Top_screen_overlay.BackgroundImage = img;
            #endregion
        }

        private Bitmap SaveForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            Refresh();
            Bitmap screen = new Bitmap(Width, Height);
            this.DrawToBitmap(screen, new Rectangle(0, 0, this.Width, this.Height));
            FormBorderStyle = FormBorderStyle.FixedSingle;
            return screen;
        }

        private Bitmap setColor(Bitmap img, Color col)
        {
            Bitmap result;
            result = img;
            for (int i = 0; i < result.Width; i++)
            {
                for (int ii = 0; ii < result.Height; ii++)
                {
                    if (result.GetPixel(i, ii).A != 0) result.SetPixel(i, ii, col);
                }
            }
            return result;
        }

        private void loadImages()
        {
            if (Form1.bottomDraw != 3) { bottomImage.Image = Properties.Resources.prev_unsupported; }
            else
            {
                bottomImg = new Bitmap(imgs[1]);
                Rectangle f1 = new Rectangle(0, 0, 320, 240);
                Rectangle f2 = new Rectangle(320, 0, 320, 240);
                Rectangle f3 = new Rectangle(640, 0, 320, 240);
                frame1 = new Bitmap(f1.Width, f1.Height);
                frame2 = new Bitmap(f2.Width, f2.Height);
                frame3 = new Bitmap(f3.Width, f3.Height);
                using (Graphics g = Graphics.FromImage(frame1)) { g.DrawImage(bottomImg, new Rectangle(0, 0, frame1.Width, frame1.Height), f1, GraphicsUnit.Pixel); }
                using (Graphics g = Graphics.FromImage(frame2)) { g.DrawImage(bottomImg, new Rectangle(0, 0, frame2.Width, frame2.Height), f2, GraphicsUnit.Pixel); }
                using (Graphics g = Graphics.FromImage(frame3)) { g.DrawImage(bottomImg, new Rectangle(0, 0, frame3.Width, frame3.Height), f3, GraphicsUnit.Pixel); }
                bottomImage.Image = bottomImg;
            }
            if (Form1.topDraw != 3) { topImage.Image = Properties.Resources.prev_unsupported; topImage.SizeMode = PictureBoxSizeMode.StretchImage; }
            else
            {
                topImg = new Bitmap(imgs[0]);
                topImage.Image = topImg;
            }
            updateGUI();
        }

        private void Sim_KeyDown(object sender, KeyEventArgs e)
        {
            using (Graphics gr = Graphics.FromImage(bottomImage.Image))
            {
                int frame = 0;
                if (Form1.bottomDraw == 3 && Form1.bottomFrame == 2)
                {
                    if (e.KeyCode == Keys.Right)
                    {
                        frameCnt++;
                        if (frameCnt > 2) frameCnt = 0;
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        frameCnt--;
                        if (frameCnt < 0) frameCnt = 2;
                    }
                    frame = frameCnt;
                }
                else if (Form1.bottomDraw == 3 && Form1.bottomFrame == 4)
                {
                    int[] pattern = { 0, 1, 2, 1 };
                    if (e.KeyCode == Keys.Right)
                    {
                        frameCnt++;
                        if (frameCnt == 4) frameCnt = 0;
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        frameCnt--;
                        if (frameCnt < 0) frameCnt = 3;
                    }
                    if (frameCnt > 3) frameCnt = 3;
                    frame = pattern[frameCnt];
                }
                switch (frame)
                {
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
            else if (Form1.APP_ShowUI_Sim & Form1.generating_preview)
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
                    if (Form1.APP_export_both_screens)
                    {
                        SaveForm().Save(save.FileName);
                    }
                    else
                    {
                        Image preview = new Bitmap(400, 240);
                        Graphics g = Graphics.FromImage(preview);
                        if (Form1.APP_ShowUI_Sim) { g.DrawImage(topImg, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); } else { g.DrawImage(topImage.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(Overlay_LR_TOP_img.BackgroundImage, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(Overlay_LR_TOP_img.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); }
                        preview.Save(save.FileName);
                    }
                    this.Close();
                }
                else if (Form1.Preview_PATH != null)
                {
                    if (Form1.APP_export_both_screens)
                    {
                        SaveForm().Save(Form1.Preview_PATH);
                    }
                    else
                    {
                        Image preview = new Bitmap(400, 240);
                        Graphics g = Graphics.FromImage(preview);
                        if (Form1.APP_ShowUI_Sim) { g.DrawImage(topImg, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); } else { g.DrawImage(topImage.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(Overlay_LR_TOP_img.BackgroundImage, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); g.DrawImage(Overlay_LR_TOP_img.Image, 0, 0, new Rectangle(new Point(0, 0), new Size(400, 240)), GraphicsUnit.Pixel); }
                        preview.Save(Form1.Preview_PATH);
                    }
                    this.Close();
                }
                else this.Close();
            }
        }

    }
}
