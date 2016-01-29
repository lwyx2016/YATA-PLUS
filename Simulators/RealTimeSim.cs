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
    public partial class RealTimeSim : Form
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
        Sett own;

        public RealTimeSim(Sett owner)
        {
            own = owner;
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
            loadImages();
        }

         public void setColors()
        {
            Overlay_LR_TOP_img.BackgroundImage = null;
            Overlay_LR_TOP_img.Image = null;
            Arrows_bottom.BackgroundImage = null;
            Arrows_bottom.Image = null;
            Top_screen_overlay.BackgroundImage = null;
            Top_screen_overlay.Image = null;

            Color[] tempbytes;
            Bitmap img;
            #region Top_Overlay
            //Top overlay
            if (Form1.enableSec[14] == 1) //If custom color is enabled
            {
                tempbytes = own.colTopOverlay;
                img = new Bitmap(Properties.Resources.top_overlay_background);
                setColor(img, tempbytes[0]);
                Overlay_LR_TOP_img.BackgroundImage = img;

                img = new Bitmap(Properties.Resources.top_overlay_text);
                setColor(img, tempbytes[3]);
                Overlay_LR_TOP_img.Image = img;
            }
            else
            {
                img = new Bitmap(Properties.Resources.top_overlay_background);
                setColor(img, Color.FromArgb(0xFF, 226, 228, 227));
                Overlay_LR_TOP_img.BackgroundImage = img;

                img = new Bitmap(Properties.Resources.top_overlay_text);
                setColor(img, Color.FromArgb(0xFF, 72, 73, 78));
                Overlay_LR_TOP_img.Image = img;
            }
            #endregion
            #region Bot_Arrows
            if (Form1.enableSec[6] == 1) //If custom color is enabled
            {
                tempbytes = own.colArrow;
                img = new Bitmap(Properties.Resources.Bottom_arrow_fore);
                setColor(img, tempbytes[1]);
                Arrows_bottom.Image = img;

                tempbytes = own.colArrowBut;
                img = new Bitmap(Properties.Resources.Bottom_arrow_back);
                setColor(img, tempbytes[1]);
                Arrows_bottom.BackgroundImage = img;
            }
            else
            {
                img = new Bitmap(Properties.Resources.Bottom_arrow_fore);
                setColor(img, Color.FromArgb(0xFF, 159, 191, 187));
                Arrows_bottom.Image = img;

                img = new Bitmap(Properties.Resources.Bottom_arrow_back);
                setColor(img, Color.FromArgb(0xFF, 219, 217, 218));
                Arrows_bottom.BackgroundImage = img;
            }
            #endregion
            #region iconResizer
            Color back = Color.FromArgb(255, 220, 224, 225);
            Color separator = Color.Gray;
            Color icon = Color.FromArgb(255, 145, 174, 208);
            if (Form1.enableSec[13] == 1) //If custom color is enabled
            {
                tempbytes = own.colIconResize;
                back = tempbytes[1];
                icon = tempbytes[4];
                separator = tempbytes[0];
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
                tempbytes = own.colBotBut;
                Default = tempbytes[1];
                Shading = tempbytes[0];
                TextShadow = tempbytes[3];
                text = tempbytes[4];
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
            Color Cursor_main = Color.FromArgb(0xFF, 45, 233, 156);
            Color Cursor_glow = Color.FromArgb(0xFF, 66, 243, 175);
            if (Form1.enableSec[0] == 1) //If custom color is enabled
            {
                tempbytes = own.colCursor;
                Cursor_main = tempbytes[1];
                Cursor_glow = tempbytes[0];
            }
            for (int i = 0; i < Cursor_img.Width; i++)
            {
                for (int ii = 0; ii < Cursor_img.Height; ii++)
                {
                    Color col = Cursor_img.GetPixel(i, ii);
                    if (col.B > 10 && col.A != 0)
                    {
                        Color final = Color.FromArgb(col.A, Cursor_main.R, Cursor_main.G, Cursor_main.B);
                        Cursor_img.SetPixel(i, ii, final);
                    }
                    else if (col.R > 10 && col.A != 0)
                    {
                        Color final = Color.FromArgb(col.A, Cursor_glow.R, Cursor_glow.G, Cursor_glow.B);
                        Cursor_img.SetPixel(i, ii, final);
                    }
                }
            }
            Graphics g = Graphics.FromImage(img);
            g.DrawImage(Cursor_img, new Rectangle(24, 44, Cursor_img.Width, Cursor_img.Height));
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

        private void Sim_Load(object sender, EventArgs e)
        {
           
        }

    }
}
