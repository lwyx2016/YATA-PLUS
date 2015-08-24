using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace YATA {
    public partial class Sett : Form {

        RealTimeSim sim = null;
        private byte[][] cols = Form1.colChunks;
        private uint[] flags;
        public Color[] colCursor;
        private Color[] col3DFolder;
        private Color[] colFiles;
        public Color[] colArrowBut;
        public Color[] colArrow;
        public Color[] colBotBut;
        public Color[] colClose;
        private Color[] colGameTxt;
        private Color[] colBotSolid;
        private Color[] colBotOuter;
        private Color[] colFolderBG;
        private Color[] colFolderArrow;
        public Color[] colIconResize;
        public Color[] colTopOverlay;
        private Color[] colDemoMsg;
        private Color[] ColTopScreen;
        List<String> messages = new List<string>() {"1: Shading\r\n2: main color\r\n3: unk\r\n4: expanded glow colour", 
                                                        "1: Shading\r\n2: main color",
                                                        "1: bottom shadow\r\n2: main color\r\n3: highlight",
                                                        "1: highlight\r\n2: main colour\r\n3: shadow",
                                                        "1: edge color\r\n2: unpressed color\r\n3: pressed color",
                                                        "1: Shading\r\n2: Default\r\n3: Highlight\r\n4: Text shadow\r\n5: Text color\r\n6: Pressed text",
                                                        "1: Shading\r\n2: Default\r\n3: Highlight\r\n4: Unk (Text shadow ?)\r\n5: Text color\r\n6: Pressed text",
                                                        "1: Background color\r\n2: Text color",
                                                        "1: Upper empty slot shading\r\n2: Main Color\r\n3: Bottom empty slot shading\r\n4: Unknown",
                                                        "1: Horizontal stripes behind bottom buttons\r\n2: Main Color\r\n3: Edges shadowing",
                                                        "1: Top shadow\r\n2: Default\r\n3: Bottom shadow\r\n4: Unk ",
                                                        "1: Button Shading and Outline, Button Pressed Color\r\n2: Button Main Color, Button Pressed Outline\r\n3: Button Highlight\r\n4: Glow/Shadow Color\r\n5: UNK\r\n6: Arrow Shadow\r\n7: Arrow unpressed color\r\n8: Arrow pressed color\r\nShadow v offset: Shadow vertical offset" ,
                                                        "1: Larger/Smaller divider\r\n2: Default\r\n3: Highlight\r\n4: Shading\r\n5: Icon\r\n6: Icon shding and pressed color\r\n7:Lower edges highlight",
                                                        "1: Overlay background\r\n2/3:Unknown\r\n4: Text color",
                                                        "1: Background color\r\n2: Text color",
                                                        "None:Static image (both top and bottom screen) \r\nBOTTOM SCREEN: \r\nFlipbook tile:the order of frames will be 1-2-3-2-1-2-3-2 etc..\r\nFlipbook Cyclic:the order of frames will be 1-2-3-1-2-3 etc.." };

        public Sett() {
            InitializeComponent();
            #region language
            if (Form1.APP_LNG.Trim().ToLower() != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\sett.txt"))
            {
                messages.Clear();
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\sett.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("label")) { ((Label)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("CHK")) { ((CheckBox)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("grp")) { ((GroupBox)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("@")) { messages.Add(line.Remove(0, 1)); }
                    }
                }
            }
            #endregion
            getColors();
            Button[] col1 = AddButtons(4, 0 , colCursor, Form1.enableSec[0] == 1 ? true : false,0,-Form1.APP_Move_buttons_colors);
            Button[] col2 = AddButtons(2, 1, col3DFolder, Form1.enableSec[1] == 1 ? true : false,1, -Form1.APP_Move_buttons_colors);
            Button[] col3 = AddButtons(3, 2, colFiles, Form1.enableSec[3] == 1 ? true : false,2, -Form1.APP_Move_buttons_colors);
            Button[] col4 = AddButtons(3, 3, colArrowBut, Form1.enableSec[5] == 1 ? true : false,3, -Form1.APP_Move_buttons_colors);
            Button[] col5 = AddButtons(3, 4, colArrow, Form1.enableSec[6] == 1 ? true : false,4, -Form1.APP_Move_buttons_colors);
            Button[] col6 = AddButtons(6, 5, colBotBut, Form1.enableSec[7] == 1 ? true : false,5, -Form1.APP_Move_buttons_colors);
            Button[] col7 = AddButtons(6, 6, colClose, Form1.enableSec[7] == 1 ? true : false,6, -Form1.APP_Move_buttons_colors);
            Button[] col8 = AddButtons(2, 7, colGameTxt, Form1.enableSec[8] == 1 ? true : false,7, -Form1.APP_Move_buttons_colors);
            Button[] col9 = AddButtons(4, 8, colBotSolid, Form1.enableSec[9] == 1 ? true : false,8, -Form1.APP_Move_buttons_colors);
            Button[] col10 = AddButtons(3, 9, colBotOuter, Form1.enableSec[10] == 1 ? true : false,9, -Form1.APP_Move_buttons_colors);
            Button[] col11 = AddButtons(4, 10, colFolderBG, Form1.enableSec[11] == 1 ? true : false,10, -Form1.APP_Move_buttons_colors);
            Button[] col12 = AddButtons(8, 11, colFolderArrow, Form1.enableSec[12] == 1 ? true : false,11, -Form1.APP_Move_buttons_colors);
            Button[] col13 = AddButtons(7, 13, colIconResize, Form1.enableSec[13] == 1 ? true : false,12, -Form1.APP_Move_buttons_colors);
            Button[] col14 = AddButtons(4, 14, colTopOverlay, Form1.enableSec[14] == 1 ? true : false,13, -Form1.APP_Move_buttons_colors);
            Button[] col15 = AddButtons(2, 15, colDemoMsg, Form1.enableSec[15] == 1 ? true : false,14, -Form1.APP_Move_buttons_colors);
            Button[] col16 = AddButtons(1, 0, ColTopScreen,true,15,15 -Form1.APP_Move_buttons_colors); //the last two parametrers are for spoofing the group and for remove tot from the x value
            ArrowNum1.Enabled = Form1.enableSec[12] == 1 ? true : false;
            ArrowNum2.Enabled = Form1.enableSec[12] == 1 ? true : false;
            ArrowNum3.Enabled = Form1.enableSec[12] == 1 ? true : false;
            foreach (Button b in col1) this.grp_colors.Controls.Add(b);
            foreach (Button b in col2) this.grp_colors.Controls.Add(b);
            foreach (Button b in col3) this.grp_colors.Controls.Add(b);
            foreach (Button b in col4) this.grp_colors.Controls.Add(b);
            foreach (Button b in col5) this.grp_colors.Controls.Add(b);
            foreach (Button b in col6) this.grp_colors.Controls.Add(b);
            foreach (Button b in col7) this.grp_colors.Controls.Add(b);
            foreach (Button b in col8) this.grp_colors.Controls.Add(b);
            foreach (Button b in col9) this.grp_colors.Controls.Add(b);
            foreach (Button b in col10) this.grp_colors.Controls.Add(b);
            foreach (Button b in col11) this.grp_colors.Controls.Add(b);
            foreach (Button b in col12) this.grp_colors.Controls.Add(b);
            foreach (Button b in col13) this.grp_colors.Controls.Add(b);
            foreach (Button b in col14) this.grp_colors.Controls.Add(b);
            foreach (Button b in col15) this.grp_colors.Controls.Add(b);
            this.grp_topSIMPLE.Controls.Add(col16[0]);
            flags = Form1.enableSec.ToArray();
            CB_topDraw.SelectedIndex = (int)Form1.topDraw;
            CB_topFrame.SelectedIndex = (int)Form1.topFrame;
            CB_botDraw.SelectedIndex = (int)Form1.bottomDraw;
            CB_botFrame.SelectedIndex = (int)Form1.bottomFrame;
            CHK0.Checked = flags[0] == 1 ? true : false;
            CHK1.Checked = flags[1] == 1 ? true : false;
            CHK2.Checked = flags[2] == 1 ? true : false;
            CHK3.Checked = flags[3] == 1 ? true : false;
            CHK4.Checked = flags[4] == 1 ? true : false;
            CHK5.Checked = flags[5] == 1 ? true : false;
            CHK6.Checked = flags[6] == 1 ? true : false;
            CHK7.Checked = flags[7] == 1 ? true : false;
            CHK8.Checked = flags[8] == 1 ? true : false;
            CHK9.Checked = flags[9] == 1 ? true : false;
            CHK10.Checked = flags[10] == 1 ? true : false;
            CHK11.Checked = flags[11] == 1 ? true : false;
            CHK12.Checked = flags[12] == 1 ? true : false;
            CHK13.Checked = flags[13] == 1 ? true : false;
            CHK14.Checked = flags[14] == 1 ? true : false;
            CHK15.Checked = flags[15] == 1 ? true : false;
            CHK16.Checked = flags[16] == 1 ? true : false;
            CHK17.Checked = Form1.useBGM == 1 ? true : false;
            if (Form1.topDraw == 1 || Form1.topDraw == 2) grp_topSIMPLE.Enabled = true; else grp_topSIMPLE.Enabled = false;
            if (Form1.topDraw == 2) CHK_bg.Enabled = true;
            CHK_scndtex.Checked = Form1.UseSecondTOPIMG;
        }

        private void getColors() {
            byte[] tempbytes;
            List <Color> tempcolors;
            #region colCursor
            tempbytes = cols[0];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[9], tempbytes[10], tempbytes[11]));
                colCursor = tempcolors.ToArray();
            #endregion
            #region col3DFolder
            tempbytes = cols[1];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                col3DFolder = tempcolors.ToArray();
            #endregion
            #region colFiles
            tempbytes = cols[2];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                colFiles = tempcolors.ToArray();
            #endregion
            #region colArrowBut
            tempbytes = cols[3];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                colArrowBut = tempcolors.ToArray();
            #endregion
            #region colArrow
            tempbytes = cols[4];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                colArrow = tempcolors.ToArray();
            #endregion
            #region colBotBut
            tempbytes = cols[5];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[4], tempbytes[5], tempbytes[6]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[7], tempbytes[8], tempbytes[9]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[10], tempbytes[11], tempbytes[12]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[20], tempbytes[21], tempbytes[22]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[23], tempbytes[24], tempbytes[25]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[26], tempbytes[27], tempbytes[28]));
                colBotBut = tempcolors.ToArray(); //In his version rei swapped this line with..
            #endregion
            #region colClose
            tempbytes = cols[6];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[4], tempbytes[5], tempbytes[6]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[7], tempbytes[8], tempbytes[9]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[10], tempbytes[11], tempbytes[12]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[20], tempbytes[21], tempbytes[22]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[23], tempbytes[24], tempbytes[25]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[26], tempbytes[27], tempbytes[28]));
                colClose = tempcolors.ToArray();// .. this one
            #endregion
            #region colGameTxt
            tempbytes = cols[7];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[10], tempbytes[11], tempbytes[12]));
                colGameTxt = tempcolors.ToArray();
            #endregion
            #region colBotSolid
            tempbytes = cols[8];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                tempcolors.Add(Color.FromArgb(tempbytes[9], tempbytes[10], tempbytes[11], tempbytes[12]));
                colBotSolid = tempcolors.ToArray();
            #endregion
            #region colBotOuter
            tempbytes = cols[9];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                colBotOuter = tempcolors.ToArray();
            #endregion
            #region colFolderBG
            tempbytes = cols[10];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                tempcolors.Add(Color.FromArgb(tempbytes[9], tempbytes[10], tempbytes[11], tempbytes[12]));
                colFolderBG = tempcolors.ToArray();
            #endregion
            #region colFolderArrow
                tempbytes = cols[11];
                tempcolors = new List<Color>();
                ArrowNum1.Value = Convert.ToInt32(tempbytes[2]);
                ArrowNum2.Value = Convert.ToInt32(tempbytes[3]);
                ArrowNum3.Value = Convert.ToInt32(tempbytes[16]);
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[4], tempbytes[5], tempbytes[6]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[7], tempbytes[8], tempbytes[9]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[10], tempbytes[11], tempbytes[12]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[13], tempbytes[14], tempbytes[15]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[17], tempbytes[18], tempbytes[19]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[20], tempbytes[21], tempbytes[22]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[23], tempbytes[24], tempbytes[25]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[26], tempbytes[27], tempbytes[28]));
                string a = "";
                for (int i = 0; i < cols[11].Length; i++)
                {
                     a = a + " " + cols[11][i];
                }
                textBox1.Text = a;
                colFolderArrow = tempcolors.ToArray();
            #endregion
            #region colIconResize
            tempbytes = cols[12];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[9], tempbytes[10], tempbytes[11]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[12], tempbytes[13], tempbytes[14]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[15], tempbytes[16], tempbytes[17]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[18], tempbytes[19], tempbytes[20]));
                colIconResize = tempcolors.ToArray();
            #endregion
            #region colTopOverlay
            tempbytes = cols[13];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[6], tempbytes[7], tempbytes[8]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[9], tempbytes[10], tempbytes[11]));
                colTopOverlay = tempcolors.ToArray();
            #endregion
            #region colDemoMsg
            tempbytes = cols[14];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[3], tempbytes[4], tempbytes[5]));
                colDemoMsg = tempcolors.ToArray();
            #endregion
            #region ColTopScreen
            tempbytes = Form1.topcol[0];
                tempcolors = new List<Color>();
                tempcolors.Add(Color.FromArgb(0xFF, tempbytes[0], tempbytes[1], tempbytes[2]));
                numericUpDown2.Value = tempbytes[3];
                numericUpDown1.Value = tempbytes[4];
            if (Form1.topDraw == 2)
            {
                if (Form1.imgOffs[6] == 0x0) CHK_bg.Checked = true; else CHK_bg.Checked = false;
            }
                ColTopScreen = tempcolors.ToArray();
            #endregion
        }

        private void setColors() {
            #region col3DFolder
            cols[0][0] = colCursor[0].R; cols[0][1] = colCursor[0].G; cols[0][2] = colCursor[0].B;
            cols[0][3] = colCursor[1].R; cols[0][4] = colCursor[1].G; cols[0][5] = colCursor[1].B;
            cols[0][6] = colCursor[2].R; cols[0][7] = colCursor[2].G; cols[0][8] = colCursor[2].B;
            cols[0][9] = colCursor[3].R; cols[0][10] = colCursor[3].G; cols[0][11] = colCursor[3].B;
            #endregion
            #region colFiles
            cols[1][0] = col3DFolder[0].R; cols[1][1] = col3DFolder[0].G; cols[1][2] = col3DFolder[0].B;
            cols[1][3] = col3DFolder[1].R; cols[1][4] = col3DFolder[1].G; cols[1][5] = col3DFolder[1].B;
            #endregion
            #region colArrowBut
            cols[2][0] = colFiles[0].R; cols[2][1] = colFiles[0].G; cols[2][2] = colFiles[0].B;
            cols[2][3] = colFiles[1].R; cols[2][4] = colFiles[1].G; cols[2][5] = colFiles[1].B;
            cols[2][6] = colFiles[2].R; cols[2][7] = colFiles[2].G; cols[2][8] = colFiles[2].B;
            #endregion
            #region colArrow
            cols[3][0] = colArrowBut[0].R; cols[3][1] = colArrowBut[0].G; cols[3][2] = colArrowBut[0].B;
            cols[3][3] = colArrowBut[1].R; cols[3][4] = colArrowBut[1].G; cols[3][5] = colArrowBut[1].B;
            cols[3][6] = colArrowBut[2].R; cols[3][7] = colArrowBut[2].G; cols[3][8] = colArrowBut[2].B;
            #endregion
            #region colBotBut
            cols[4][0] = colArrow[0].R; cols[4][1] = colArrow[0].G; cols[4][2] = colArrow[0].B;
            cols[4][3] = colArrow[1].R; cols[4][4] = colArrow[1].G; cols[4][5] = colArrow[1].B;
            cols[4][6] = colArrow[2].R; cols[4][7] = colArrow[2].G; cols[4][8] = colArrow[2].B;
            #endregion
            #region colClose
            cols[5][4] = colBotBut[0].R; cols[5][5] = colBotBut[0].G; cols[5][6] = colBotBut[0].B;
            cols[5][7] = colBotBut[1].R; cols[5][8] = colBotBut[1].G; cols[5][9] = colBotBut[1].B;
            cols[5][10] = colBotBut[2].R; cols[5][11] = colBotBut[2].G; cols[5][12] = colBotBut[2].B;
            cols[5][20] = colBotBut[3].R; cols[5][21] = colBotBut[3].G; cols[5][22] = colBotBut[3].B;
            cols[5][23] = colBotBut[4].R; cols[5][24] = colBotBut[4].G; cols[5][25] = colBotBut[4].B;
            cols[5][26] = colBotBut[5].R; cols[5][27] = colBotBut[5].G; cols[5][28] = colBotBut[5].B;
            cols[6][4] = colClose[0].R; cols[6][5] = colClose[0].G; cols[6][6] = colClose[0].B;
            cols[6][7] = colClose[1].R; cols[6][8] = colClose[1].G; cols[6][9] = colClose[1].B;
            cols[6][10] = colClose[2].R; cols[6][11] = colClose[2].G; cols[6][12] = colClose[2].B;
            cols[6][20] = colClose[3].R; cols[6][21] = colClose[3].G; cols[6][22] = colClose[3].B;
            cols[6][23] = colClose[4].R; cols[6][24] = colClose[4].G; cols[6][25] = colClose[4].B;
            cols[6][26] = colClose[5].R; cols[6][27] = colClose[5].G; cols[6][28] = colClose[5].B;
            #endregion
            #region colGameTxt
            cols[7][0] = colGameTxt[0].R; cols[7][1] = colGameTxt[0].G; cols[7][2] = colGameTxt[0].B;
            cols[7][10] = colGameTxt[1].R; cols[7][11] = colGameTxt[1].G; cols[7][12] = colGameTxt[1].B;
            #endregion
            #region colBotSolid
            cols[8][0] = colBotSolid[0].R; cols[8][1] = colBotSolid[0].G; cols[8][2] = colBotSolid[0].B;
            cols[8][3] = colBotSolid[1].R; cols[8][4] = colBotSolid[1].G; cols[8][5] = colBotSolid[1].B;
            cols[8][6] = colBotSolid[2].R; cols[8][7] = colBotSolid[2].G; cols[8][8] = colBotSolid[2].B;
            cols[8][9] = colBotSolid[3].A; cols[8][10] = colBotSolid[3].R; cols[8][11] = colBotSolid[3].G; cols[8][12] = colBotSolid[3].B;
            #endregion
            #region colBotOuter
            cols[9][0] = colBotOuter[0].R; cols[9][1] = colBotOuter[0].G; cols[9][2] = colBotOuter[0].B;
            cols[9][3] = colBotOuter[1].R; cols[9][4] = colBotOuter[1].G; cols[9][5] = colBotOuter[1].B;
            cols[9][6] = colBotOuter[2].R; cols[9][7] = colBotOuter[2].G; cols[9][8] = colBotOuter[2].B;
            #endregion
            #region colFolderBG
            cols[10][0] = colFolderBG[0].R; cols[10][1] = colFolderBG[0].G; cols[10][2] = colFolderBG[0].B;
            cols[10][3] = colFolderBG[1].R; cols[10][4] = colFolderBG[1].G; cols[10][5] = colFolderBG[1].B;
            cols[10][6] = colFolderBG[2].R; cols[10][7] = colFolderBG[2].G; cols[10][8] = colFolderBG[2].B;
            cols[10][9] = colFolderBG[3].A; cols[10][10] = colFolderBG[3].R; cols[10][11] = colFolderBG[3].G; cols[10][12] = colFolderBG[3].B;
            #endregion
            #region colFolderArrow
            cols[11][2] = (byte)ArrowNum1.Value;
            cols[11][3] = (byte)ArrowNum2.Value;
            cols[11][16] = (byte)ArrowNum3.Value;
            cols[11][4] = colFolderArrow[0].R; cols[11][5] = colFolderArrow[0].G; cols[11][6] = colFolderArrow[0].B;
            cols[11][7] = colFolderArrow[1].R; cols[11][8] = colFolderArrow[1].G; cols[11][9] = colFolderArrow[1].B;
            cols[11][10] = colFolderArrow[2].R; cols[11][11] = colFolderArrow[2].G; cols[11][12] = colFolderArrow[2].B;
            cols[11][13] = colFolderArrow[3].R; cols[11][14] = colFolderArrow[3].G; cols[11][15] = colFolderArrow[3].B;
            cols[11][17] = colFolderArrow[4].R; cols[11][18] = colFolderArrow[4].G; cols[11][19] = colFolderArrow[4].B;
            cols[11][20] = colFolderArrow[5].R; cols[11][21] = colFolderArrow[5].G; cols[11][22] = colFolderArrow[5].B;
            cols[11][23] = colFolderArrow[6].R; cols[11][24] = colFolderArrow[6].G; cols[11][25] = colFolderArrow[6].B;
            cols[11][26] = colFolderArrow[7].R; cols[11][27] = colFolderArrow[7].G; cols[11][28] = colFolderArrow[7].B;
            /* textBox1.Text = textBox1.Text.Trim();
             string[] a = textBox1.Text.Split(Convert.ToChar(" "));
             MessageBox.Show(a.Length.ToString());
             if (a.Length != 32) { MessageBox.Show("The raw textbox must contain 0x20 bytes (32 bytes in dec)"); }
             else
             {
                 for (int i = 0; i < 32; i++)
                 {
                     cols[11][i] = (byte)Convert.ToInt32(a[i]);
                 }
             }*/
            #endregion
            #region colIconResize
            cols[12][0] = colIconResize[0].R; cols[12][1] = colIconResize[0].G; cols[12][2] = colIconResize[0].B;
            cols[12][3] = colIconResize[1].R; cols[12][4] = colIconResize[1].G; cols[12][5] = colIconResize[1].B;
            cols[12][6] = colIconResize[2].R; cols[12][7] = colIconResize[2].G; cols[12][8] = colIconResize[2].B;
            cols[12][9] = colIconResize[3].R; cols[12][10] = colIconResize[3].G; cols[12][11] = colIconResize[3].B;
            cols[12][12] = colIconResize[4].R; cols[12][13] = colIconResize[4].G; cols[12][14] = colIconResize[4].B;
            cols[12][15] = colIconResize[5].R; cols[12][16] = colIconResize[5].G; cols[12][17] = colIconResize[5].B;
            cols[12][18] = colIconResize[6].R; cols[12][19] = colIconResize[6].G; cols[12][20] = colIconResize[6].B;
            #endregion
            #region colTopOverlay
            cols[13][0] = colTopOverlay[0].R; cols[13][1] = colTopOverlay[0].G; cols[13][2] = colTopOverlay[0].B;
            cols[13][3] = colTopOverlay[1].R; cols[13][4] = colTopOverlay[1].G; cols[13][5] = colTopOverlay[1].B;
            cols[13][6] = colTopOverlay[2].R; cols[13][7] = colTopOverlay[2].G; cols[13][8] = colTopOverlay[2].B;
            cols[13][9] = colTopOverlay[3].R; cols[13][10] = colTopOverlay[3].G; cols[13][11] = colTopOverlay[3].B;
            #endregion
            #region colDemoMsg
            cols[14][0] = colDemoMsg[0].R; cols[14][1] = colDemoMsg[0].G; cols[14][2] = colDemoMsg[0].B;
            cols[14][3] = colDemoMsg[1].R; cols[14][4] = colDemoMsg[1].G; cols[14][5] = colDemoMsg[1].B;
            #endregion
            #region ColTopScreen
            Form1.topcol[0][0] = ColTopScreen[0].R; Form1.topcol[0][1] = ColTopScreen[0].G; Form1.topcol[0][2] = ColTopScreen[0].B;
            Form1.topcol[0][3] = Convert.ToByte(numericUpDown2.Value);
            Form1.topcol[0][4] = Convert.ToByte(numericUpDown1.Value);
            if (Form1.topDraw == 2)
            {
                if (CHK_bg.Checked)
                {
                    Form1.topcol[0][5] = 0x00;
                    Form1.topcol[0][6] = 0x64;
                    Form1.imgOffs[6] = 0x0;
                }
                else
                {
                    Form1.topcol[0][5] = 0x56;
                    Form1.topcol[0][6] = 0x56;
                }
            }
            #endregion
        }

        private void buttonSaveSett_Click(object sender, EventArgs e) {
            flags[0] = (uint)(CHK0.Checked ? 1 : 0);
            flags[1] = (uint)(CHK1.Checked ? 1 : 0);
            flags[2] = (uint)(CHK2.Checked ? 1 : 0);
            flags[3] = (uint)(CHK3.Checked ? 1 : 0);
            flags[4] = (uint)(CHK4.Checked ? 1 : 0);
            flags[5] = (uint)(CHK5.Checked ? 1 : 0);
            flags[6] = (uint)(CHK6.Checked ? 1 : 0);
            flags[7] = (uint)(CHK7.Checked ? 1 : 0);
            flags[8] = (uint)(CHK8.Checked ? 1 : 0);
            flags[9] = (uint)(CHK9.Checked ? 1 : 0);
            flags[10] = (uint)(CHK10.Checked ? 1 : 0);
            flags[11] = (uint)(CHK11.Checked ? 1 : 0);
            flags[12] = (uint)(CHK12.Checked ? 1 : 0);
            flags[13] = (uint)(CHK13.Checked ? 1 : 0);
            flags[14] = (uint)(CHK14.Checked ? 1 : 0);
            flags[15] = (uint)(CHK15.Checked ? 1 : 0);
            flags[16] = (uint)(CHK16.Checked ? 1 : 0);
            setColors();
            Form1.colChunks = cols;
            Form1.useBGM = (uint)(CHK17.Checked ? 1 : 0);
            Form1.topDraw = (uint)CB_topDraw.SelectedIndex;
            Form1.bottomDraw = (uint)CB_botDraw.SelectedIndex;
            Form1.topFrame = (uint)CB_topFrame.SelectedIndex;
            Form1.bottomFrame = (uint)CB_botFrame.SelectedIndex;
            Form1.enableSec = flags;
            Form1.UseSecondTOPIMG = CHK_scndtex.Checked;
            closeForm();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            closeForm();
        }

        private void closeForm() {
            this.Close();
        }

        private void HelpPressed(object sender, EventArgs e) 
        {
            int button =  Convert.ToInt32((((Button)sender).Name.Substring(4)));
            MessageBox.Show(messages[button - 1]);
        }

        private void colorSelect(object sender, EventArgs e) {
            if(colDialog.ShowDialog() == DialogResult.OK){
                ((Button)sender).BackColor = colDialog.Color;
                int buttonSet = Convert.ToInt32((((Button)sender).Name.Split('-'))[0]);
                int button = Convert.ToInt32((((Button)sender).Name.Split('-'))[1]);
                switch(buttonSet){
                    case 0:
                        colCursor[button] = colDialog.Color;
                        break;
                    case 1:
                        col3DFolder[button] = colDialog.Color;
                        break;
                    case 2:
                        colFiles[button] = colDialog.Color;
                        break;
                    case 3:
                        colArrowBut[button] = colDialog.Color;
                        break;
                    case 4:
                        colArrow[button] = colDialog.Color;
                        break;
                    case 5:
                        colBotBut[button] = colDialog.Color;
                        break;
                    case 6:
                        colClose[button] = colDialog.Color;
                        break;
                    case 7:
                        colGameTxt[button] = colDialog.Color;
                        break;
                    case 8:
                        colBotSolid[button] = colDialog.Color;
                        break;
                    case 9:
                        colBotOuter[button] = colDialog.Color;
                        break;
                    case 10:
                        colFolderBG[button] = colDialog.Color;
                        break;
                    case 11:
                        colFolderArrow[button] = colDialog.Color;
                        break;
                    case 12:
                        colIconResize[button] = colDialog.Color;
                        break;
                    case 13:
                        colTopOverlay[button] = colDialog.Color;
                        break;
                    case 14:
                        colDemoMsg[button] = colDialog.Color;
                        break;
                    case 15:
                        ColTopScreen[0] = colDialog.Color;
                        break;
                 }
                if (sim!= null && sim.Visible) { sim.Show(); sim.setColors(); }
            }
        }

        private Button[] AddButtons(int amount, int yPos, Color[] cols, bool enab ,int yPosNAME, int RemoveFromX = 0) {
            Button[] btnArray = new Button[amount + 1];
            for (int i = 0; i < amount; i++) { 
                btnArray[i] = new Button();
                btnArray[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnArray[i].Size = new System.Drawing.Size(20, 20);
                btnArray[i].Location = new System.Drawing.Point(110 + (i * 22) - RemoveFromX, 20 + (25 * yPos));
                btnArray[i].Name = yPosNAME + "-" + i;
                btnArray[i].Click += new System.EventHandler(colorSelect);
                btnArray[i].Enabled = enab;
                btnArray[i].ForeColor = enab == true ? Color.Black : Color.Gray;
                if (enab) btnArray[i].BackColor = cols[i];
            }
            return btnArray;
        }

        private void CB_topDraw_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.topDraw == 1 || Form1.topDraw == 2) grp_topSIMPLE.Enabled = true; else grp_topSIMPLE.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(messages[15]);
        }

        private void FORM_Closing(object sender, FormClosingEventArgs e)
        {
            sim.Close();
            Form1.APP_SETT_SIZE_X = this.Size.Width;
            Form1.APP_SETT_SIZE_Y = this.Size.Height;
        }

        private void Sett_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sim == null || !sim.Visible) { sim = new RealTimeSim(this); sim.Show(); } else if (sim.Visible) { sim.Focus(); }
            sim.setColors();
        }
    }
}
