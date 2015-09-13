using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using YATA.Converter;
using System.Text;
using System.Reflection;

namespace YATA
{
    public partial class Form1 : Form
    {
        #region appSettings
        //I decided to use a config.ini file to not leave traces in the pc that runs yata (i'm a portable apps maniac :P)
        public static bool APP_ShowUI_Sim = true;
        public static bool APP_AutoGen_preview = false;
        public static string APP_photo_edtor = "";
        public static bool APP_Wait_editor = true;
        public static bool APP_Clean_On_exit = false;
        public static bool APP_Auto_Load_bgm = true;
        public static int APP_Move_buttons_colors = 10;
        public static bool APP_First_Start = true; //if true this is the first start, else it isn't
        public static bool APP_check_UPD = true;
        public static int APP_Public_version = 7; /*for the update check the application doesn't count the version, but the release number on gbatemp
                                                    1: First public yata+ version
                                                    2: Yata+ v1.1
                                                    3: Yata+ v1.2
                                                    4/5: Yata+ v1.3
                                                    7: Yata+ v1.5 (this one)
                                                    8,9,etc..: Future updates*/
        public static string APP_STRING_version = "YATA+ v1.5 LITE";
        public static int APP_SETT_SIZE_X = 678; //To remember the size
        public static int APP_SETT_SIZE_Y = 625;
        public static bool APP_export_both_screens = true;
        public static string APP_LNG = "english";
        public static bool APP_not_Optimize_Cwavs = false;
        public static int APP_opt_samples = 11025;
        #endregion
        #region strings
        List<String> messages = new List<string>() {
            "Error on reading file",
            "This file is not a theme",
            "This theme has a bgm.bcstm file, but it doesn't have the 'Use BMG' flag checked in the settings, the home menu won't load the music if you don't enable it !!!",
            "The path or the file name contains some special characters, the file won't be loaded",
            "Image size:", "Saving theme,please wait", "Error: Image is not a power of 2.",
            "Theme saved !",
            "Hey, looks like there is a new version of yata+ out there !! \r\nWhat are you waiting for ? Go now on the official thread (Credits -> Official thread) and download it !! \r\n\r\nYou can disable the auto check for updates in the preferences",
            "To use this funcion you must set your photo editor program executable in the preferences (File -> Preferences)",
            "the image has been modified, do you want to import the new one ?",
            "To convert a WAV you'll need the 'CTR_WaveConverter32' executable from the leaked sdk, this file is illegal to share, you'll have to find by yourself.\r\n When you'll have it, place it in the same directory as yata, and make sure that his name is 'CTR_WaveConverter32.exe'. \r\nIf you know another method for converting WAVs to CWAVs please contact me on gbatemp so i can implement it ",
            "To import some CWAVs you must check the 'Enable use of SFX' box in the theme settings", "If you have imported some cwavs, now you should reload the theme in YATA, do you want to (this will save the theme) ?",
            "If you don't save and reload the theme you may encounter some bugs",
            "The input file is not a valid BRSTM file" };
        #endregion

        public Form1(string arg)
        {            
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            load_prefs();
            if (!File.Exists("System.Net.FtpClient.dll")) MessageBox.Show("System.Net.FtpClient.dll was not found, please re-download YATA+ from the official thread and extract the file here, without this DLL you can't install themes via FTP", "MISSING DLL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (!File.Exists("NAudio.dll")) MessageBox.Show("NAudio.dll was not found, please re-download YATA+ from the official thread and extract the file here, without this DLL the conversion WAV->CWAV won't work","MISSING DLL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              try
            {
                InitializeComponent();
                if (APP_LNG != "english" && File.Exists(@"languages\" + APP_LNG + @"\main.txt"))
                {
                    messages.Clear();
                    string[] lng = File.ReadAllLines(@"languages\" + APP_LNG + @"\main.txt");
                    foreach (string line in lng)
                    {
                        if (!line.StartsWith(";"))
                        {
                            string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                            if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                            else if (line.StartsWith("lbl")) { ((Label)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                            else if (line.StartsWith("drpdwn")) { (toolStrip1.Items[tmp[0]]).Text = tmp[1]; }
                            else if (line.StartsWith("new")) { (file_newFile.DropDownItems[tmp[0]]).Text = tmp[1]; }
                            else if (line.StartsWith("file") || line.StartsWith("File")) { (drpdwn_file.DropDownItems[tmp[0]]).Text = tmp[1]; }
                            else if (line.StartsWith("edit")) { (drpdwn_edit.DropDownItems[tmp[0]]).Text = tmp[1]; }
                            else if (line.StartsWith("@")) { messages.Add(line.Replace(@"\r\n", Environment.NewLine).Remove(0, 1)); }
                        }
                    }
                }
                if (File.Exists(arg))
                {
                    loadFromDragAndDrop(new string[1] { arg });
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show("There was an error in this application","YATA PLUS ---- FATAL ERROR !!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                MessageBox.Show("This may be related to the language file, try to delete the languages folder and then restart YATA+");
                MessageBox.Show("A log file will be generated, if you have every required dll,please send me the content of this file.");
                string[] LOG = new string[14];
                LOG[0] = "OSVersion: " + Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString();
                LOG[1] = "YATA version: " + APP_STRING_version;
                LOG[2] = "Is64BitOperatingSystem: " + Environment.Is64BitOperatingSystem.ToString();
                LOG[3] = "-------------------------------------------------";
                LOG[4] = "msg: " + ex.Message;
                LOG[5] = "-------------------------------------------------";
                LOG[6] = "InnerException" + ex.InnerException;
                LOG[7] = "-------------------------------------------------";
                LOG[8] = "Source:" + ex.Source;
                LOG[9] = "-------------------------------------------------";
                LOG[10] = "StackTrace:" + ex.StackTrace;
                LOG[11] = "-------------------------------------------------";
                LOG[12] = "StackTrace:" + ex.TargetSite;
                LOG[13] = "-------------------------------------------------";
                SaveFileDialog sv = new SaveFileDialog();
                sv.Filter = "txt file|*.txt";
                sv.Title = "Save debug file";
                if (sv.ShowDialog() == DialogResult.OK) { System.IO.File.WriteAllLines(sv.FileName, LOG); }
                MessageBox.Show("You can find more information in the windows' event viewer");
                InitializeComponent();
            }
        }
        //Constants
        private const int RGB565 = 0;
        private const int BGR888 = 1;
        private const int A8 = 2;
        private readonly string[] imgEnum = { 
                                         "Top",
                                         "Bottom",
                                         "Folder Closed",
                                         "Folder Open",
                                         "Border-48px",
                                         "Border-24px",
                                         "Top screen 'SIMPLE' Background"
                                         };

        //Flags
        public static uint useBGM = 0; //0x5
        public static uint topDraw = 0;  //0xC
        public static uint topFrame = 0;  //0x10
        public static uint bottomDraw = 0;  //0x20
        public static uint bottomFrame = 0;   //0x24
        public static bool UseSecondTOPIMG = false;
        public static uint[] enableSec;

        //Other
        private bool imgListBoxLoaded = false;
        private string path = null;
        private string filename = null;
        public static uint[] imgOffs;
        private uint[] imgLens;
        private uint[] colorOffs;
        public static byte[][] colChunks;
        public static uint topColorOff = 0;

        public static byte[][] topcol;

        public static Bitmap[] imageArray;
        private static List<uint> RGBOffs = new List<uint>();
        private uint unk = 0;
        private uint cwavOff = 0;
        private uint cwavLen = 0;
        public static byte[] cwav; //For importing from CwavReplace
        public static byte magicByte;
        public static byte[] outFile;

        void OPEN_FILE() 
        {
            lbl_DragBgm.Visible = false;
            if (APP_Clean_On_exit && System.IO.File.Exists(path + "dec_" + filename)) { System.IO.File.Delete(path + "dec_" + filename); }
            UseSecondTOPIMG = false;
            imgOffs = null;
            imgLens = null;
            colorOffs = null;
            imageArray = null;
            RGBOffs.Clear();
            colChunks = null;
            imgListBoxLoaded = false;
            pictureBox1.Image = null;

            File_installTheme.Enabled = false;
            file_saveAS.Enabled = false;
            file_save.Enabled = false;
            file_reload.Enabled = false;
            file_saveAS.Enabled = false;
            file_prev.Enabled = false;
            edit_importIMG.Enabled = false;
            edit_saveIMG.Enabled = false;
            edit_impCWAV.Enabled = false;
            edit_saveCWAV.Enabled = false;
            edit_createCWAV.Enabled = false;
            edit_CWAVdump.Enabled = false;
            drpdwn_settings.Enabled = false;
            drpdwn_sim.Enabled = false;
            try { if (File.Exists(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav")) File.Delete(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav"); } catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR ON DELETING TMP_BGM"); }
            if (APP_Clean_On_exit && System.IO.File.Exists(path + "dec_" + filename))
            {
                System.IO.File.Delete(path + "dec_" + filename);
            }
            this.Refresh();
            path = openFileLZ.FileName.Substring(0, openFileLZ.FileName.LastIndexOf("\\") + 1);
            filename = openFileLZ.FileName.Substring(path.Length, openFileLZ.FileName.Length - path.Length);
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(openFileLZ.FileName, FileMode.Open));
                if ((reader.ReadBytes(4)).ToU32() != 0x1) //if the user try to load a theme arleady uncompressed (so frist 4 bytes = 0x1)
                {
                    reader.Close();
                    //the theme is compressed
                    dsdecmp.Decompress(openFileLZ.FileName, path + "dec_" + filename);
                }
                else
                {
                    reader.Close();
                    //the theme is not uncompressed
                    System.IO.File.Copy(openFileLZ.FileName, path + "dec_" + filename);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), messages[0]);
                return;
            }

            try
            {
                BinaryReader br = new BinaryReader(File.Open(path + "dec_" + filename, FileMode.Open));
                if ((br.ReadBytes(4)).ToU32() != 0x1) { MessageBox.Show(messages[1]); return; }
                List<uint> offs = new List<uint>();
                br.BaseStream.Position = 0x18;  //top
                offs.Add((br.ReadBytes(4)).ToU32());
                br.BaseStream.Position = 0x28;  //bot
                offs.Add((br.ReadBytes(4)).ToU32());
                br.BaseStream.Position = 0x30;  //unk
                unk = (br.ReadBytes(4)).ToU32();
                br.BaseStream.Position = 0x40;  //f1
                offs.Add((br.ReadBytes(4)).ToU32());
                br.BaseStream.Position = 0x44;  //f2
                offs.Add((br.ReadBytes(4)).ToU32());
                br.BaseStream.Position = 0x54;  //b1
                offs.Add((br.ReadBytes(4)).ToU32());
                br.BaseStream.Position = 0x58;  //b2
                offs.Add((br.ReadBytes(4)).ToU32());
                br.BaseStream.Position = 0xC0;  //cwav
                cwavOff = (br.ReadBytes(4)).ToU32();
                br.BaseStream.Position = 0x1C;
                offs.Add(br.ReadBytes(4).ToU32());//top screen add tex
                br.Close();
                imgOffs = offs.ToArray();
            }
            catch (IOException)
            {
            }
            loadFlags();
            loadList();
            loadColors();
            drpdwn_sim.Enabled = true;
            drpdwn_settings.Enabled = true;
            file_save.Enabled = true;
            file_saveAS.Enabled = true;
            file_reload.Enabled = true;
            edit_saveIMG.Enabled = true;
            file_prev.Enabled = true;
            edit_importIMG.Enabled = true;
            edit_saveCWAV.Enabled = true;
            edit_impCWAV.Enabled = true;
            edit_CWAVdump.Enabled = true;
            edit_createCWAV.Enabled = true;
            File_installTheme.Enabled = true;
            if (APP_Auto_Load_bgm && File.Exists(path + "bgm.bcstm")) 
            {
                if (useBGM != 1) MessageBox.Show(messages[2]);
                LoadBGM(path + "bgm.bcstm");
            }
        }

        void LoadBGM(string filepath)
        {
            if (!File.Exists("vgmstream.exe")) File.WriteAllBytes("vgmstream.exe", Properties.Resources.test);
            if (!File.Exists("libg7221_decode.dll")) File.WriteAllBytes("libg7221_decode.dll", Properties.Resources.libg7221_decode);
            if (!File.Exists("libmpg123-0.dll")) File.WriteAllBytes("libmpg123-0.dll", Properties.Resources.libmpg123_0);
            if (!File.Exists("libvorbis.dll")) File.WriteAllBytes("libvorbis.dll", Properties.Resources.libvorbis);

            if (File.Exists(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav")) File.Delete(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav");
            this.Refresh();
            Process proc = new Process();
            proc.StartInfo.FileName = "vgmstream.exe";
            if (filepath.Contains("♪")) MessageBox.Show(messages[3]);
            proc.StartInfo.Arguments = "-o " + "\"" + Path.GetDirectoryName(openFileLZ.FileName) + "\\tmp_bgm.wav\" "  + " \"" + filepath + "\"";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
            System.Diagnostics.Process.Start(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav");
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileLZ.ShowDialog() == DialogResult.OK)
            {
                OPEN_FILE();
            }
        }

        private void loadList()
        {
            List<string> strList = new List<string>();
            int e = 0;
            foreach (uint i in imgOffs)
            {
                strList.Add(imgEnum[e] + " (" + i.ToString("X08") + ")");
                e++;
            }
            imgListBox.DataSource = strList.ToArray();
            List<uint> lens = new List<uint>();
            List<Bitmap> images = new List<Bitmap>();
            if (topDraw > 2) lens.Add((uint)(topFrame == 1 ? 0x40000 : 0x80000)); else if (topDraw == 2 && imgOffs[0] != imgOffs[6]) { lens.Add((uint)(0x8000)); UseSecondTOPIMG = true; } else lens.Add(0);
            if (bottomDraw == 3) lens.Add((uint)(bottomFrame == 1 ? 0x40000 : 0x80000)); else lens.Add(0);
            if (enableSec[2] > 0) lens.Add(0x10000); else lens.Add(0);
            if (enableSec[2] > 0) lens.Add(0x10000); else lens.Add(0);
            if (enableSec[4] > 0) lens.Add(0x10000); else lens.Add(0);
            if (enableSec[4] > 0) lens.Add(0x4000); else lens.Add(0);
            if (topDraw == 2 && imgOffs[6] != 0x0) lens.Add(0x8000); else lens.Add(0);
            imgLens = lens.ToArray();
            for (int i = 0; i < imgOffs.Length; i++)
            {
                if (imgLens[i] == 0x8000 || i == 6) images.Add(getImage(imgOffs[i], imgLens[i], A8)); else images.Add(getImage(imgOffs[i], imgLens[i], i > 1 ? BGR888 : RGB565));
            }
            if (cwavOff > 0) cwav = getCWAV();
            imageArray = images.ToArray();
            imgListBoxLoaded = true;
            updatePicBox(0);
        }

        private void loadFlags()
        {
            BinaryReader dec_br = new BinaryReader(File.Open(path + "dec_" + filename, FileMode.Open));
            List<uint> enables = new List<uint>();
            dec_br.BaseStream.Position = 0x5;
            useBGM = dec_br.ReadByte();
            dec_br.BaseStream.Position = 0xC;
            topDraw = dec_br.ReadBytes(4).ToU32();
            dec_br.BaseStream.Position = 0x10;
            topFrame = dec_br.ReadBytes(4).ToU32();
            dec_br.BaseStream.Position = 0x20;
            bottomDraw = dec_br.ReadBytes(4).ToU32();
            dec_br.BaseStream.Position = 0x24;
            bottomFrame = dec_br.ReadBytes(4).ToU32();
            dec_br.BaseStream.Position = 0x2C;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //0 - Cursor
            dec_br.BaseStream.Position = 0x30;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x34;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //1 - 3D Folder
            dec_br.BaseStream.Position = 0x38;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x3C;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //2 - Folder tex'
            dec_br.BaseStream.Position = 0x48;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //3 - file graphic
            dec_br.BaseStream.Position = 0x4C;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x50;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //4 - Border tex'
            dec_br.BaseStream.Position = 0x5C;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //5 - Arrow But
            dec_br.BaseStream.Position = 0x60;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x64;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //6 - Arrow
            dec_br.BaseStream.Position = 0x68;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x6C;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //7 - Bottom/Close But
            dec_br.BaseStream.Position = 0x70;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x74;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x78;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //8 - Game text
            dec_br.BaseStream.Position = 0x7C;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x80;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //9 - Bottom Solid
            dec_br.BaseStream.Position = 0x84;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x88;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //10 - Bottom Outer
            dec_br.BaseStream.Position = 0x8C;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x90;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //11 - Folder BG
            dec_br.BaseStream.Position = 0x94;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x98;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //12 - Folder Arrow
            dec_br.BaseStream.Position = 0x9C;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xA0;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //13 - Icon-resize
            dec_br.BaseStream.Position = 0xA4;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xA8;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //14 - Top Overlay
            dec_br.BaseStream.Position = 0xAC;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xB0;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //15 - Demo Msg
            dec_br.BaseStream.Position = 0xB4;
            RGBOffs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xB8;
            enables.Add(dec_br.ReadBytes(4).ToU32());   //16 - SFX
            dec_br.BaseStream.Position = 0xBC;
            cwavLen = dec_br.ReadBytes(4).ToU32();
            dec_br.Close();
            enableSec = enables.ToArray();
        }

        private void loadColors()
        {
            BinaryReader dec_br = new BinaryReader(File.Open(path + "dec_" + filename, FileMode.Open));
            List<uint> offs = new List<uint>();
            dec_br.BaseStream.Position = 0x14;
            topColorOff = dec_br.ReadBytes(4).ToU32();
            dec_br.BaseStream.Position = 0x1C;
           // addTopTEXTUREOff = dec_br.ReadBytes(4).ToU32();

            dec_br.BaseStream.Position = 0x30;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x38;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x4C;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x60;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x68;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x70;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x74;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x7C;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x84;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x8C;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x94;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0x9C;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xA4;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xAC;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            dec_br.BaseStream.Position = 0xB4;
            offs.Add(dec_br.ReadBytes(4).ToU32());
            colorOffs = offs.ToArray();
            List<byte[]> cols = new List<byte[]>();
            int cnt = 0;
            foreach (uint i in colorOffs)
            {
                dec_br.BaseStream.Position = i;
                switch (cnt)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 13:
                    case 14:
                        cols.Add(dec_br.ReadBytes(0x10));
                        break;
                    case 5:
                    case 6:
                    case 11:
                    case 12:
                        cols.Add(dec_br.ReadBytes(0x20));
                        break;
                }
                cnt++;
            }
            List<byte[]> TopColor = new List<byte[]>();
            dec_br.BaseStream.Position = topColorOff;
            if (topDraw == 2) { TopColor.Add(dec_br.ReadBytes(0x7)); } else TopColor.Add(dec_br.ReadBytes(0x5));
            topcol = TopColor.ToArray();
            dec_br.Close();
            colChunks = cols.ToArray();
        }

        private void updatePicBox(int i)
        {
            try
            {
                pictureBox1.Image = null;
                Debug.Print(imgOffs[i].ToString());
                if (imgOffs[i] == 0x1 || imgOffs[i] == 0x0) throw new Exception();
                pictureBox1.Image = imageArray[i];                
                label4.Text = messages[4]+" " + imageArray[i].Width.ToString() + "x" + imageArray[i].Height.ToString();
                label4.Visible = true;
                btn_photoedit.Enabled = true;
                lbl_ImgNotInc.Visible = false;
            }
            catch (Exception)
            {
                lbl_ImgNotInc.Visible = true;
                btn_photoedit.Enabled = false;
                label4.Visible = false;
            }
        }

        private void imgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imgListBoxLoaded == true)
            {
                updatePicBox(imgListBox.SelectedIndex);                
            }
        }

        private byte[] getCWAV()
        {
            BinaryReader dec_br = new BinaryReader(File.Open(path + "dec_" + filename, FileMode.Open));
            long cLen = dec_br.BaseStream.Length - cwavOff;
            byte[] wav;
            dec_br.BaseStream.Position = cwavOff;
            wav = dec_br.ReadBytes((int)cLen);
            dec_br.Close();
            return wav;
        }

        private Bitmap getImage(uint offset, uint length, int type)
        {
            if (length == 0) { Debug.Print("Get image|| Off:" + offset.ToString() + "  Lenght: 0||"); return null; }
            int red = 0, green = 0, blue = 0;
            int imgWidth = 0, imgHeight = 0;
            if (offset == imgOffs[4] && length == 0x10000)
            {
                imgWidth = 64;
                imgHeight = 128;
            }
            else
            {
                switch (length)
                {
                    case 0x40000:
                        imgWidth = 512;
                        imgHeight = 256;
                        break;
                    case 0x80000:
                        imgWidth = 1024;
                        imgHeight = 256;
                        break;
                    case 0x10000:
                        imgWidth = 128;
                        imgHeight = 64;
                        break;
                    case 0x4000:
                        imgWidth = 32;
                        imgHeight = 64;
                        break;
                    default:
                        imgWidth = 64;
                        imgHeight = 64;
                        break;
                }
            }
            Debug.Print("Get image|| Off:" + offset.ToString() + "  Lenght:" + length.ToString() + " Type:" + type.ToString() + "  Size:"+ imgWidth.ToString() + "x" + imgHeight.ToString() +  " ||");
            Bitmap img = new Bitmap(imgWidth, imgHeight);
            BinaryReader dec_br = new BinaryReader(File.Open(path + "dec_" + filename, FileMode.Open));
            dec_br.BaseStream.Position = offset;
            try
            {
                uint x = 0, y = 0;
                int p = gcm(img.Width, 8) / 8;
                if (p == 0) p = 1;

                if (type == RGB565)
                {
                    uint i = 0;
                    int[] u16s = new int[length / 2];
                    for (int j = 0; j <= (length / 2) - 1; j++) { u16s[j] = dec_br.ReadInt16(); }
                    foreach (int pix in u16s)
                    {
                        d2xy(i % 64, out x, out y);
                        uint tile = i / 64;
                        // Shift Tile Coordinate into Tilemap
                        x += (uint)(tile % p) * 8;
                        y += (uint)(tile / p) * 8;
                        red = (byte)((pix >> 11) & 0x1f) * 8;
                        green = (byte)(((pix >> 5) & 0x3f) * 4);
                        blue = (byte)((pix) & 0x1f) * 8;
                        img.SetPixel((int)x, (int)y, Color.FromArgb(0xFF, red, green, blue));
                        i++;
                    }
                }
                else if (type == BGR888)
                {
                    for (uint i = 0; i < length / 8; i++)
                    {
                        d2xy(i % 64, out x, out y);
                        uint tile = i / 64;
                        // Shift Tile Coordinate into Tilemap
                        x += (uint)(tile % p) * 8;
                        y += (uint)(tile / p) * 8;
                        byte[] data = dec_br.ReadBytes(3);
                        img.SetPixel((int)x, (int)y, Color.FromArgb(0xFF, data[2], data[1], data[0])); 
                    }
                }
                else if (type == A8)
                {                    
                    for (uint i = 0; i < length / 8; i++)
                    {
                        d2xy(i % 64, out x, out y);
                        uint tile = i / 64;
                        // Shift Tile Coordinate into Tilemap
                        x += (uint)(tile % p) * 8;
                        y += (uint)(tile / p) * 8;
                        byte data = dec_br.ReadByte();
                        img.SetPixel((int)x, (int)y, Color.FromArgb(0xFF, data, data, data));
                        if (i == 0) Debug.Print(data.ToString());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            dec_br.Close();
            return img;
        }

        private byte[] bitmapToRawImg(Bitmap img, int format)
        {
            List<byte> array = new List<byte>();
            int w = img.Width;
            int h = img.Height;
            w = h = Math.Max(nlpo2(w), nlpo2(h));
            uint x = 0, y = 0;
            uint val = 0;
            Color c;
            int p = gcm(w, 8) / 8;
            if (p == 0) p = 1;
            for (uint i = 0; i < w * h; i++)
            {
                d2xy(i % 64, out x, out y);
                uint tile = i / 64;
                x += (uint)(tile % p) * 8;
                y += (uint)(tile / p) * 8;
                if (!(x >= img.Width || y >= img.Height))
                {
                    c = img.GetPixel((int)x, (int)y);
                    if (c.A == 0) c = Color.FromArgb(0, 86, 86, 86);
                    if (format == RGB565)
                    {
                        val = (uint)((c.R / 8) & 0x1f) << 11;
                        val += (uint)(((c.G / 4) & 0x3f) << 5);
                        val += (uint)((c.B / 8) & 0x1f);
                        array.Add((byte)(val & 0xFF));
                        array.Add((byte)((val >> 8) & 0xFF));
                    }
                    else if (format == BGR888)
                    {
                        array.Add((byte)c.B);
                        array.Add((byte)c.G);
                        array.Add((byte)c.R);
                    }
                    else if (format == A8)
                    {
                        array.Add((byte)c.R);
                    }
                }

            }

            return array.ToArray();
        }

        /// <summary>
        /// Greatest common multiple (to round up)
        /// </summary>
        /// <param name="n">Number to round-up.</param>
        /// <param name="m">Multiple to round-up to.</param>
        /// <returns>Rounded up number.</returns>
        private static int gcm(int n, int m)
        {
            return ((n + m - 1) / m) * m;
        }
        /// <summary>
        /// Next Largest Power of 2
        /// </summary>
        /// <param name="x">Input to round up to next 2^n</param>
        /// <returns>2^n > x && x > 2^(n-1) </returns>
        private static int nlpo2(int x)
        {
            x--; // comment out to always take the next biggest power of two, even if x is already a power of two
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return (x + 1);
        }
        // Morton Translation
        /// <summary>
        /// Combines X/Y Coordinates to a decimal ordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private uint xy2d(uint x, uint y)
        {
            x &= 0x0000ffff;
            y &= 0x0000ffff;
            x |= (x << 8);
            y |= (y << 8);
            x &= 0x00ff00ff;
            y &= 0x00ff00ff;
            x |= (x << 4);
            y |= (y << 4);
            x &= 0x0f0f0f0f;
            y &= 0x0f0f0f0f;
            x |= (x << 2);
            y |= (y << 2);
            x &= 0x33333333;
            y &= 0x33333333;
            x |= (x << 1);
            y |= (y << 1);
            x &= 0x55555555;
            y &= 0x55555555;
            return x | (y << 1);
        }
        /// <summary>
        /// Decimal Ordinate In to X / Y Coordinate Out
        /// </summary>
        /// <param name="d">Loop integer which will be decoded to X/Y</param>
        /// <param name="x">Output X coordinate</param>
        /// <param name="y">Output Y coordinate</param>
        private void d2xy(uint d, out uint x, out uint y)
        {
            x = d;
            y = (x >> 1);
            x &= 0x55555555;
            y &= 0x55555555;
            x |= (x >> 1);
            y |= (y >> 1);
            x &= 0x33333333;
            y &= 0x33333333;
            x |= (x >> 2);
            y |= (y >> 2);
            x &= 0x0f0f0f0f;
            y &= 0x0f0f0f0f;
            x |= (x >> 4);
            y |= (y >> 4);
            x &= 0x00ff00ff;
            y &= 0x00ff00ff;
            x |= (x >> 8);
            y |= (y >> 8);
            x &= 0x0000ffff;
            y &= 0x0000ffff;
        }

        private void makeTheme(string file)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                StatusLabel.Visible = true;
                StatusLabel.Text = messages[5]+".....0%";
                this.Refresh();
                //I had to edit the method to save the theme, 
                //If the user by editing the theme changed the offset for a texture the program would save the new theme with the older offset, so the theme will get corrupted
                //I solved by doing so:
                //temporarily write the old offsets
                bw.Write(1);
                bw.Write((byte)0);
                bw.Write((byte)useBGM);
                bw.Write((byte)0);
                bw.Write((byte)0);
                bw.Write(0);
                bw.Write(topDraw);
                bw.Write(topFrame);
                bw.Write(topColorOff);
                bw.Write(imgOffs[0]); //imgOffs[0]
                bw.Write(imgOffs[6]);
                bw.Write(bottomDraw);
                bw.Write(bottomFrame);
                bw.Write(imgOffs[1]); //imgOffs[1]
                bw.Write(enableSec[0]);
                bw.Write(colorOffs[0]);
                bw.Write(enableSec[1]);
                bw.Write(colorOffs[1]);
                bw.Write(enableSec[2]);
                bw.Write(imgOffs[2]); //imgOffs[2]
                bw.Write(imgOffs[3]); //imgOffs[3]
                bw.Write(enableSec[3]);
                bw.Write(colorOffs[2]);//
                bw.Write(enableSec[4]);
                bw.Write(imgOffs[4]); //imgOffs[4]
                bw.Write(imgOffs[5]);//imgOffs[5]
                bw.Write(enableSec[5]);
                bw.Write(colorOffs[3]);
                bw.Write(enableSec[6]);
                bw.Write(colorOffs[4]);
                bw.Write(enableSec[7]);
                bw.Write(colorOffs[5]);
                bw.Write(colorOffs[6]);
                bw.Write(enableSec[8]);
                bw.Write(colorOffs[7]);
                bw.Write(enableSec[9]);
                bw.Write(colorOffs[8]);
                bw.Write(enableSec[10]);
                bw.Write(colorOffs[9]);
                bw.Write(enableSec[11]);
                bw.Write(colorOffs[10]);
                bw.Write(enableSec[12]);
                bw.Write(colorOffs[11]);
                bw.Write(enableSec[13]);
                bw.Write(colorOffs[12]);
                bw.Write(enableSec[14]);
                bw.Write(colorOffs[13]);
                bw.Write(enableSec[15]);
                bw.Write(colorOffs[14]);
                bw.Write(enableSec[16]);
                bw.Write(cwavLen);
                bw.Write(cwavOff);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                StatusLabel.Text = messages[5] + ".....11%";
                this.Refresh();
                //Then when writing the new data goes back and writes the new offsets

                Debug.Print("STARTING DATA SECTION AT " + bw.BaseStream.Position.ToString());
                //top screen COLORS
                uint oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x14;
                bw.Write(oldOFFS); //imgOffs[0]
                topColorOff = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Top screen colors: " + bw.BaseStream.Position.ToString());
                bw.Write(topcol[0]);
                StatusLabel.Text = messages[5] + ".....13%";
                this.Refresh();

                //top image
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x18;
                bw.Write(oldOFFS); //imgOffs[0]
                imgOffs[0] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Top image " + bw.BaseStream.Position.ToString());
                if (topDraw == 3) bw.Write(bitmapToRawImg(imageArray[0], RGB565));
                else if (topDraw == 2 && UseSecondTOPIMG) bw.Write(bitmapToRawImg(imageArray[0], A8));
                StatusLabel.Text = messages[5] + ".....15%";
                this.Refresh();

                //top ALT image
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x1C;
                if (imgOffs[6] != 0x0) { bw.Write(oldOFFS); imgOffs[6] = oldOFFS; } else bw.Write(0);
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Top alt image " + bw.BaseStream.Position.ToString());
                if (topDraw == 2 && imgOffs[6] != 0x0) bw.Write(bitmapToRawImg(imageArray[6], A8));
                StatusLabel.Text = messages[5] + ".....17%";
                this.Refresh();

                //bottom image
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x28;
                bw.Write(oldOFFS); //imgOffs[1]
                imgOffs[1] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Bottom image " + bw.BaseStream.Position.ToString());
                if (bottomDraw == 3) bw.Write(bitmapToRawImg(imageArray[1], RGB565));
                StatusLabel.Text = messages[5] + ".....19%";
                this.Refresh();

                //col 0
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x30;
                bw.Write(oldOFFS); //colorOffs[0]
                colorOffs[0] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Color 0 " + bw.BaseStream.Position.ToString());
                /*if (enableSec[0] == 1)*/
                bw.Write(colChunks[0]);
                StatusLabel.Text = messages[5] + ".....23%";
                this.Refresh();

                //col 1
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x38;
                bw.Write(oldOFFS); //colorOffs[1]
                colorOffs[1] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Color1 " + bw.BaseStream.Position.ToString());
                /*if (enableSec[1] == 1)*/
                bw.Write(colChunks[1]);
                StatusLabel.Text = messages[5] + ".....27%";
                this.Refresh();

                //image 2
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x40;
                bw.Write(oldOFFS); //imgOffs[2]
                imgOffs[2] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Image 2 " + bw.BaseStream.Position.ToString());
                if (enableSec[2] == 1) bw.Write(bitmapToRawImg(imageArray[2], BGR888));
                StatusLabel.Text = messages[5] + ".....31%";
                this.Refresh();

                //image 3
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x44;
                bw.Write(oldOFFS); //imgOffs[3]
                imgOffs[3] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Image 3 " + bw.BaseStream.Position.ToString());
                if (enableSec[2] == 1) bw.Write(bitmapToRawImg(imageArray[3], BGR888));
                StatusLabel.Text = messages[5] + ".....35%";
                this.Refresh();

                //col 2
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x4C;
                bw.Write(oldOFFS); //colorOffs[2]
                colorOffs[2] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Color 2 " + bw.BaseStream.Position.ToString());
                /*if (enableSec[3] == 1)*/
                bw.Write(colChunks[2]);
                StatusLabel.Text = messages[5] + ".....39%";
                this.Refresh();

                //image 4
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x54;
                bw.Write(oldOFFS); //imgOffs[4]
                imgOffs[4] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Image 4 " + bw.BaseStream.Position.ToString());
                if (enableSec[4] == 1) bw.Write(bitmapToRawImg(imageArray[4], BGR888));
                StatusLabel.Text = messages[5] + ".....43%";
                this.Refresh();

                //image 5
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x58;
                bw.Write(oldOFFS); //imgOffs[5]
                imgOffs[5] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Image 5 " + bw.BaseStream.Position.ToString());
                if (enableSec[4] == 1) bw.Write(bitmapToRawImg(imageArray[5], BGR888));
                StatusLabel.Text = messages[5] + ".....47%";
                this.Refresh();

                //col 3
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x60;
                bw.Write(oldOFFS); //colorOffs[3]
                colorOffs[3] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Color 3+ " + bw.BaseStream.Position.ToString());
                /*if (enableSec[5] == 1) */
                bw.Write(colChunks[3]);
                StatusLabel.Text = messages[5] + ".....51%";
                this.Refresh();

                //col 4
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x68;
                bw.Write(oldOFFS); //colorOffs[4]
                colorOffs[4] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[6] == 1)*/ bw.Write(colChunks[4]);
                StatusLabel.Text = messages[5] + ".....55%";
                this.Refresh();

                //col 5
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x70;
                bw.Write(oldOFFS); //colorOffs[5]
                colorOffs[5] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[7] == 1) */ bw.Write(colChunks[5]);
                StatusLabel.Text = messages[5] + ".....59%";
                this.Refresh();

                //col 6
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x74;
                bw.Write(oldOFFS); //colorOffs[6]
                colorOffs[6] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[7] == 1)*/ bw.Write(colChunks[6]);
                StatusLabel.Text = messages[5] + ".....63%";
                this.Refresh();

                //col 7
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x7C;
                bw.Write(oldOFFS); //colorOffs[7]
                colorOffs[7] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[8] == 1)*/ bw.Write(colChunks[7]);
                StatusLabel.Text = messages[5] + ".....67%";
                this.Refresh();

                //col 8
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x84;
                bw.Write(oldOFFS); //colorOffs[8]
                colorOffs[8] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[9] == 1)*/ bw.Write(colChunks[8]);
                StatusLabel.Text = messages[5] + ".....71%";
                this.Refresh();

                //col 9
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x8C;
                bw.Write(oldOFFS); //colorOffs[9]
                colorOffs[9] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
               /* if (enableSec[10] == 1)*/ bw.Write(colChunks[9]);
               StatusLabel.Text = messages[5] + ".....75%";
               this.Refresh();

                //col 10
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x94;
                bw.Write(oldOFFS); //colorOffs[10]
                colorOffs[10] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[11] == 1)*/ bw.Write(colChunks[10]);
                StatusLabel.Text = messages[5] + ".....79%";
                this.Refresh();

                //col 11
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0x9C;
                bw.Write(oldOFFS);
                colorOffs[11] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[12] == 1)*/ bw.Write(colChunks[11]);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                StatusLabel.Text = messages[5] + ".....83%";
                this.Refresh();

                //col 12
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0xA4;
                bw.Write(oldOFFS);
                colorOffs[12] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[13] == 1)*/ bw.Write(colChunks[12]);
                StatusLabel.Text = messages[5] + ".....87%";
                this.Refresh();

                //col 13
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0xAC;
                bw.Write(oldOFFS);
                colorOffs[13] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                /*if (enableSec[14] == 1)*/ bw.Write(colChunks[13]);
                StatusLabel.Text = messages[5] + ".....91%";
                this.Refresh();

                //col 14
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0xB4;
                bw.Write(oldOFFS);
                colorOffs[14] = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("...Color 14 " + bw.BaseStream.Position.ToString());
                /* if (enableSec[15] == 1)*/
                bw.Write(colChunks[14]);
               StatusLabel.Text = messages[5] + ".....95%";
               this.Refresh();

                //wav
                oldOFFS = (uint)bw.BaseStream.Position;
                bw.BaseStream.Position = 0xC0;
                bw.Write(oldOFFS); //cwavOff
                cwavOff = oldOFFS;
                bw.BaseStream.Position = oldOFFS;
                Debug.Print("Wav offset " + bw.BaseStream.Position.ToString());
                if (enableSec[16] == 1) bw.Write(cwav);
                StatusLabel.Text = messages[5] + ".....99%";
                this.Refresh();

                bw.Close();
            }
        }

        private void SimToolStrip_Click(object sender, EventArgs e)
        {
                Sim sim = new Sim();
                sim.ShowDialog();
        }

        private void toolStripSettings_Click(object sender, EventArgs e)
        {
            Sett settings = new Sett();
            settings.Size = new System.Drawing.Size(APP_SETT_SIZE_X, APP_SETT_SIZE_Y);
            settings.ShowDialog();
        }

        private void prefToolStrip_Click(object sender, EventArgs e)
        {
            Prefs pref = new Prefs();
            pref.ShowDialog();
        }

        private void importToolstrip_Click(object sender, EventArgs e)
        {
            openNewImg.FileName = imgEnum[imgListBox.SelectedIndex].ToString();
            if (openNewImg.ShowDialog() == DialogResult.OK)
            {
                import_image(openNewImg.FileName);
            }
        }

        void import_image(string name)
        {
            byte[] png = File.ReadAllBytes(name);
            using (Stream BitmapStream = new MemoryStream(png))
            {
                Image img = Image.FromStream(BitmapStream);
                Bitmap mBitmap = new Bitmap(img);
                if (mBitmap.Size.Height.isPower2() && mBitmap.Size.Width.isPower2())
                {
                    imageArray[imgListBox.SelectedIndex] = mBitmap;
                    imgOffs[imgListBox.SelectedIndex] = 0x2; //TO BE DIFFRENT FROM 0
                    updatePicBox(imgListBox.SelectedIndex);
                    if (imgListBox.SelectedIndex == 6 && topDraw != 2) MessageBox.Show("This image will be used only if the top screen draw type is set to 'Solid w/ Texture squares'");
                }
                else
                {
                    MessageBox.Show(messages[6]);
                    return;
                }
                BitmapStream.Close();
            }
            return;
        }

        public static bool generating_preview = false; //This is the best i could come up with...
        public static string Preview_PATH = null;

        private void saveFile_Click(object sender, EventArgs e)
        {
            makeTheme(path + "new_dec_" + filename);
            dsdecmp.Compress(path + "new_dec_" + filename, path + filename);
            File.Delete(path + "new_dec_" + filename);
            if (APP_AutoGen_preview)
            {
                Sim frm = new Sim();
                Preview_PATH = path + filename + ".png" ;
                generating_preview = true;
                frm.ShowDialog();
                generating_preview = false;
                Preview_PATH = null;
            }
            StatusLabel.Visible = false;
            this.Refresh();
            MessageBox.Show(messages[7]);
        }

        private void saveAsFile_Click(object sender, EventArgs e)
        {
            saveTheme.FileName = "body_LZ.bin";
            if (saveTheme.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string newpath = saveTheme.FileName.Substring(0, saveTheme.FileName.LastIndexOf("\\") + 1);
                makeTheme(newpath + "new_dec_" + filename);
                dsdecmp.Compress(newpath + "new_dec_" + filename, saveTheme.FileName);
                File.Delete(newpath + "new_dec_" + filename);
                if (APP_AutoGen_preview)
                {
                    Sim frm = new Sim();
                    Preview_PATH = path + filename + ".png";
                    generating_preview = true;
                    frm.ShowDialog();
                    generating_preview = false;
                    Preview_PATH = null;
                }
                StatusLabel.Visible = false;
                this.Refresh();
                MessageBox.Show(messages[7]);
            }
        }

        private void saveImage_Click(object sender, EventArgs e)
        {
            savePng.FileName = imgEnum[imgListBox.SelectedIndex].ToString();
            if (savePng.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageArray[imgListBox.SelectedIndex].Save(savePng.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void saveCWAVButton_Click(object sender, EventArgs e)
        {
            saveCWAVDialog.FileName = "Cwavs.bin";
            if (saveCWAVDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BinaryWriter br = new BinaryWriter(File.Create(saveCWAVDialog.FileName));
                br.Write(getCWAV());
                br.Close();
            }
        }

        private void importCWAVButton_Click(object sender, EventArgs e)
        {
            openCWAVDialog.FileName = "Cwavs.bin";
            if (openCWAVDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cwav = File.ReadAllBytes(openCWAVDialog.FileName);
            }
        }

        public static void load_prefs()
        {
            if (!System.IO.File.Exists("Settings.ini"))
            {
                string[] baseSettings = { "ui_sim=true", "gen_prev=false", "photo_edit=", "wait_editor=true", "clean_on_exit=true", "load_bgm=true", "first_start_v6=true","shift_btns=10", "check_updates=true","exp_both_screens=true", "happy_easter=false","lng=english","n_opt_cwavs=false", "opt_samples=11025" };
                System.IO.File.WriteAllLines("Settings.ini", baseSettings);
            }
            string[] lines = System.IO.File.ReadAllLines("Settings.ini");
            foreach (string line in lines)
            {
                if (line.ToLower().StartsWith("ui_sim="))
                {
                    APP_ShowUI_Sim = Convert.ToBoolean(line.ToLower().Substring(7));
                }
                else if (line.ToLower().StartsWith("gen_prev="))
                {
                    APP_AutoGen_preview = Convert.ToBoolean(line.ToLower().Substring(9));
                }
                else if (line.ToLower().StartsWith("photo_edit="))
                {
                    APP_photo_edtor = line.ToLower().Substring(11);
                }
                else if (line.ToLower().StartsWith("wait_editor="))
                {
                    APP_Wait_editor = Convert.ToBoolean(line.ToLower().Substring(12));
                }
                else if (line.ToLower().StartsWith("clean_on_exit="))
                {
                    APP_Clean_On_exit = Convert.ToBoolean(line.ToLower().Substring(14));
                }
                else if (line.ToLower().StartsWith("load_bgm="))
                {
                    APP_Auto_Load_bgm = Convert.ToBoolean(line.ToLower().Substring(9));
                }
                else if (line.ToLower().StartsWith("first_start_v6="))
                {
                    APP_First_Start = Convert.ToBoolean(line.ToLower().Substring(15));
                }
                else if (line.ToLower().StartsWith("shift_btns="))
                {
                    APP_Move_buttons_colors = Convert.ToInt32(line.ToLower().Substring(11));
                }
                else if (line.ToLower().StartsWith("check_updates="))
                {
                    APP_check_UPD = Convert.ToBoolean(line.ToLower().Substring(14));
                }
                else if (line.ToLower().StartsWith("happy_easter="))
                {
                    if (Convert.ToBoolean(line.ToLower().Substring(13))) {
                        Info_Forms.Easter es = new Info_Forms.Easter();
                        es.Show();
                    }
                }
                else if (line.ToLower().StartsWith("sett_size="))
                {
                    APP_SETT_SIZE_X = Convert.ToInt32(line.ToLower().Substring(10, 3));
                    APP_SETT_SIZE_Y = Convert.ToInt32(line.ToLower().Substring(13, 3));
                }
                else if (line.ToLower().StartsWith("exp_both_screens="))
                {
                    APP_export_both_screens = Convert.ToBoolean(line.ToLower().Substring(17));
                }
                else if (line.ToLower().StartsWith("lng="))
                {
                    APP_LNG = (line.Substring(4));
                }
                else if (line.ToLower().StartsWith("n_opt_cwavs="))
                {
                    APP_not_Optimize_Cwavs = Convert.ToBoolean(line.ToLower().Substring(12));
                }
                else if (line.ToLower().StartsWith("opt_samples="))
                {
                    APP_opt_samples = Convert.ToInt32(line.ToLower().Substring(12));
                }   
            }
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug_menu.Visible = Debugger.IsAttached;
            if(APP_First_Start){
                FirstStart dlg = new FirstStart();
                dlg.ShowDialog();
            }
            try
            {
                if (APP_check_UPD)
                {
                    System.Net.WebClient d = new System.Net.WebClient();
                    if (Convert.ToInt32(d.DownloadString("https://raw.githubusercontent.com/exelix11/YATA-PLUS/master/PublicVersion.txt")) > APP_Public_version)
                    {
                        MessageBox.Show(messages[8]);
                    }
                }
            }
            catch {/*Do nothing*/}
        }

        private void generatePreviewForCHMMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim frm = new Sim();
            Preview_PATH = null;
            generating_preview = true;
            frm.ShowDialog();
            generating_preview = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (APP_photo_edtor == "")
            {
                MessageBox.Show(messages[9]);
                return;
            }
            if (System.IO.File.Exists(Path.GetTempPath() + "THEMEIMG_" + imgListBox.SelectedIndex.ToString() + ".png")) System.IO.File.Delete(Path.GetTempPath() + "THEMEIMG_" + imgListBox.SelectedIndex.ToString() + ".png");
            imageArray[imgListBox.SelectedIndex].Save(Path.GetTempPath() + "THEMEIMG_" + imgListBox.SelectedIndex.ToString() + ".png");
            Process prc = new Process();
            prc.StartInfo.FileName = APP_photo_edtor;
            prc.StartInfo.Arguments = Path.GetTempPath() + "THEMEIMG_" + imgListBox.SelectedIndex.ToString() + ".png";
            prc.Start();
            if (APP_Wait_editor)
            {
                lbl_notCrashed.Visible = true;
                prc.WaitForExit();
                lbl_notCrashed.Visible = false;
                Image a = Image.FromFile(Path.GetTempPath() + "THEMEIMG_" + imgListBox.SelectedIndex.ToString() + ".png");
                if (a != imageArray[imgListBox.SelectedIndex])
                {
                    DialogResult result = MessageBox.Show(messages[10], "YATA", MessageBoxButtons.YesNo);
                    if (result == System.Windows.Forms.DialogResult.Yes) { import_image(Path.GetTempPath() + "THEMEIMG_" + imgListBox.SelectedIndex.ToString() + ".png"); }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Credits frm = new Credits();
            frm.ShowDialog();
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            try { if (File.Exists(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav")) File.Delete(Path.GetDirectoryName(openFileLZ.FileName) + @"\tmp_bgm.wav"); } catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR ON DELETING TMP_BGM"); }
            if (APP_Clean_On_exit && System.IO.File.Exists(path + "dec_" + filename))
            {
                System.IO.File.Delete(path + "dec_" + filename);
            }
        }

        private void cWAVsWavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.GetTempPath() + "snd_dump.bin")) File.Delete(Path.GetTempPath() + "snd_dump.bin");
            BinaryWriter br = new BinaryWriter(File.Create(Path.GetTempPath() + "snd_dump.bin"));
            br.Write(getCWAV());
            br.Close();
            CWAVs_dumper frm = new CWAVs_dumper();
            frm.ShowDialog();
        }

        private void wAVsCWAVsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("CTR_WaveConverter32.exe"))
            {
                MessageBox.Show(messages[11],"Please understand",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            try
            {
                OpenFileDialog opn = new OpenFileDialog();
                opn.Filter = "WAV file|*.wav|Every file|*.*";
                opn.Title = "Select a WAV file";
                opn.Multiselect = true;
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (opn.FileNames.Length == 1)
                    {
                        Wav2CWAV(opn.FileName);
                    }
                    else
                    {
                        for (int i = 0; i < opn.FileNames.Length; i++)
                        {
                            if (!APP_not_Optimize_Cwavs)
                            {
                                Debug.Print("Optimizing CWAV: " + Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav");
                                WaveFormat New = new WaveFormat(APP_opt_samples, 8, 1);
                                WaveStream Original = new WaveFileReader(opn.FileNames[i]);
                                WaveFormatConversionStream stream = new WaveFormatConversionStream(New, Original);
                                if (System.IO.File.Exists(Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav")) File.Delete(Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav");
                                WaveFileWriter.CreateWaveFile(Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav", stream);
                                stream.Dispose();
                                Original.Dispose();
                            }
                            Process prc = new Process();
                            prc.StartInfo.FileName = "CTR_WaveConverter32.exe";
                            if (!APP_not_Optimize_Cwavs) prc.StartInfo.Arguments = "-o \"" + opn.FileNames[i] + ".bcwav\" \"" + Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav\""; else prc.StartInfo.Arguments = "-o \"" + opn.FileNames[i] + ".bcwav\" \"" + opn.FileNames[i] + "\"";
                            Debug.Print("Converting CWAV: " + Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav");
                            prc.Start();
                            prc.WaitForExit();
                            if (System.IO.File.Exists(Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav")) File.Delete(Path.GetTempPath() + Path.GetFileName(opn.FileNames[i]) + ".tmp.wav");
                        }
                        MessageBox.Show("Done !");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void editCWAVsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enableSec[16] == 0) { MessageBox.Show(messages[12]); return; }
            CwavReplace frm = new CwavReplace();
            frm.ShowDialog();
            if (MessageBox.Show(messages[13], "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                StatusLabel.Visible = true;
                makeTheme(path + "new_dec_" + filename);
                dsdecmp.Compress(path + "new_dec_" + filename, path + filename);
                File.Delete(path + "new_dec_" + filename);
                StatusLabel.Visible = false;
                OPEN_FILE();
            }
            else MessageBox.Show(messages[14]);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ImgSIZES dlg = new ImgSIZES();
            dlg.ShowDialog();
        }

        private void printColorDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Print(colChunks[11][0].ToString() + " " + colChunks[11][1].ToString() + " " + colChunks[11][2].ToString() + " " + colChunks[11][3].ToString() + " " + colChunks[11][4].ToString());
        }

        private void printColorOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Print(colChunks[12][0].ToString() + " " + colChunks[12][1].ToString() + " " + colChunks[12][2].ToString() + " " + colChunks[12][3].ToString() + " " + colChunks[12][4].ToString());
        }

        private void loadBgmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Title = "Load BGM";
            opn.Filter = "Bcstm files| *.bcstm";
            if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK) LoadBGM(opn.FileName);
        }

        private void cWAVWAVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("vgmstream.exe")) File.WriteAllBytes("vgmstream.exe", Properties.Resources.test);
            if (!File.Exists("libg7221_decode.dll")) File.WriteAllBytes("libg7221_decode.dll", Properties.Resources.libg7221_decode);
            if (!File.Exists("libmpg123-0.dll")) File.WriteAllBytes("libmpg123-0.dll", Properties.Resources.libmpg123_0);
            if (!File.Exists("libvorbis.dll")) File.WriteAllBytes("libvorbis.dll", Properties.Resources.libvorbis);
            try
            {
                OpenFileDialog opn = new OpenFileDialog();
                opn.Filter = "Every supported file|*.*";
                opn.Title = "Open file";
                opn.Multiselect = true;
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (opn.FileNames.Length == 1)
                    {
                        AudioTOWav(opn.FileName);
                    }
                    else
                    {
                        for (int i = 0; i < opn.FileNames.Length; i++)
                        {
                            Process prc = new Process();
                            prc.StartInfo.FileName = "vgmstream.exe";
                            prc.StartInfo.Arguments = "-o \"" + opn.FileNames[i] +  ".wav\" " + "\"" + opn.FileNames[i] + "\"";
                            prc.StartInfo.CreateNoWindow = true;
                            prc.StartInfo.UseShellExecute = false;
                            prc.Start();
                            prc.WaitForExit();
                        }
                        MessageBox.Show("Done !");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            loadFromDragAndDrop(files);
        }

        void loadFromDragAndDrop(string[] files)
        {
            if (File.Exists(files[0]))
            {
                BinaryReader reader = new BinaryReader(File.Open(files[0], FileMode.Open));
                string[] MAGIC = new string[4] { reader.ReadByte().ToString(), reader.ReadByte().ToString(), reader.ReadByte().ToString(), reader.ReadByte().ToString() };
                reader.Close();
                if (MAGIC[0] == "67" && MAGIC[1] == "83" && MAGIC[2] == "84" && MAGIC[3] == "77")//DEC: 67 83 84 77 = HEX: 43 53 54 4D = STRING: CSTM
                {
                    ConvertSETTINGS dlg = new ConvertSETTINGS();
                    dlg.Text = "Convert settings TYPE:BCSTM";
                    dlg.btn_WavtoCWAV.Enabled = false;
                    dlg.btn_WAVbrstm.Enabled = false;
                    dlg.btn_WAVbcstm.Enabled = false;
                    dlg.btn_BRSTMbcstm.Enabled = false;
                    dlg.ShowDialog();
                    if (dlg.RET == ConvertSETTINGS.ConvertType.play_file) { LoadBGM(files[0]); }
                    else if (dlg.RET == ConvertSETTINGS.ConvertType.brstmTOwav) { AudioTOWav(files[0]); }
                }
                else if (MAGIC[0] == "82" && MAGIC[1] == "83" && MAGIC[2] == "84" && MAGIC[3] == "77")//RSTM
                {
                    ConvertSETTINGS dlg = new ConvertSETTINGS();
                    dlg.Text = "Convert settings TYPE:BRSTM";
                    dlg.btn_WavtoCWAV.Enabled = false;
                    dlg.btn_WAVbrstm.Enabled = false;
                    dlg.btn_WAVbcstm.Enabled = false;
                    dlg.ShowDialog();
                    if (dlg.RET == ConvertSETTINGS.ConvertType.play_file) { LoadBGM(files[0]); }
                    else if (dlg.RET == ConvertSETTINGS.ConvertType.brstmTObcstm) { Brstm2BCSTM(files[0]); }
                    else if (dlg.RET == ConvertSETTINGS.ConvertType.brstmTOwav) { AudioTOWav(files[0]); }
                }
                else if (MAGIC[0] == "82" && MAGIC[1] == "73" && MAGIC[2] == "70" && MAGIC[3] == "70")//WAV (RIFF)
                {
                    ConvertSETTINGS dlg = new ConvertSETTINGS();
                    dlg.Text = "Convert settings TYPE:WAV";
                    dlg.btn_convert.Enabled = false;
                    dlg.btn_BRSTMbcstm.Enabled = false;
                    if (!File.Exists("CTR_WaveConverter32.exe")) { dlg.btn_WavtoCWAV.Enabled = false; }
                    dlg.ShowDialog();
                    if (dlg.RET == ConvertSETTINGS.ConvertType.play_file)
                    {
                        System.Diagnostics.Process.Start(files[0]);
                    }
                    else if (dlg.RET == ConvertSETTINGS.ConvertType.wavTOcwav) { Wav2CWAV(files[0]); }
                    else if (dlg.RET == ConvertSETTINGS.ConvertType.wavTObrstm) { wav2BRSTM(files[0]); }
                    else if (dlg.RET == ConvertSETTINGS.ConvertType.wavTObcstm) { Wav2BCSTM(files[0]); }
                }
                else if (MAGIC[0] == "67" && MAGIC[1] == "87" && MAGIC[2] == "65" && MAGIC[3] == "86")//CWAV
                {
                    ConvertSETTINGS dlg = new ConvertSETTINGS();
                    dlg.Text = "Convert settings TYPE:CWAV";
                    dlg.btn_BRSTMbcstm.Enabled = false;
                    dlg.btn_WavtoCWAV.Enabled = false;
                    dlg.btn_WAVbrstm.Enabled = false;
                    dlg.btn_WAVbcstm.Enabled = false;
                    dlg.btn_play.Enabled = false;
                    if (!File.Exists("CTR_WaveConverter32.exe")) { dlg.btn_WavtoCWAV.Enabled = false; }
                    dlg.ShowDialog();
                    if (dlg.RET == ConvertSETTINGS.ConvertType.brstmTOwav) { AudioTOWav(files[0]); }
                }
                else if (MAGIC[0] == "137" && MAGIC[1] == "80" && MAGIC[2] == "78" && MAGIC[3] == "71")//PNG
                {
                    if (imgListBox.Items.Count > 0 && imgListBox.SelectedIndex >= 0)
                    {
                        import_image(files[0]);
                    }
                    else MessageBox.Show("To import an image you must open a theme first");
                }
                else
                {
                    openFileLZ.FileName = files[0];
                    OPEN_FILE();
                }
            }
        }

        private void openTheFileConverterFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The form has been removed in the stable release");
        }

        private void wAVBRSTMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("BrstmConv.exe")) File.WriteAllBytes("BrstmConv.exe", Properties.Resources.BrstmConv);
            if (!File.Exists("BrawlLib.dll")) File.WriteAllBytes("BrawlLib.dll", Properties.Resources.BrawlLib);
            try
            {
                OpenFileDialog opn = new OpenFileDialog();
                opn.Filter = "Wav file|*.wav";
                opn.Title = "Open file";
                opn.Multiselect = false;
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    wav2BRSTM(opn.FileName);
                }
             
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void bRSTMBCSTMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Title = "Open a brstm file";
            opn.Filter = "BRSTM file|*.brstm";
            if (opn.ShowDialog() == DialogResult.OK)
            {
                Brstm2BCSTM(opn.FileName);
            }
        }

        private void printBackArrowColorDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string a = "";
            for (int i = 0; i < colChunks[11].Length; i++)
            {
                a = a + " " + colChunks[11][i];
            }
            Debug.Print(a);
        }

        private void wAVBCSTMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("BrstmConv.exe")) File.WriteAllBytes("BrstmConv.exe", Properties.Resources.BrstmConv);
            if (!File.Exists("BrawlLib.dll")) File.WriteAllBytes("BrawlLib.dll", Properties.Resources.BrawlLib);
            if (File.Exists(Path.GetTempPath() + "tmp.bcstm")) File.Delete(Path.GetTempPath() + "tmp.bcstm");
            try
            {
                OpenFileDialog opn = new OpenFileDialog();
                opn.Filter = "Wav file|*.wav";
                opn.Title = "Open file";
                opn.Multiselect = false;
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Wav2BCSTM(opn.FileName);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        #region Converters
        void Brstm2BCSTM(string input)
        {
            if (!File.Exists("BrstmConv.exe")) File.WriteAllBytes("BrstmConv.exe", Properties.Resources.BrstmConv);
            if (!File.Exists("BrawlLib.dll")) File.WriteAllBytes("BrawlLib.dll", Properties.Resources.BrawlLib);
            if (File.Exists(Path.GetTempPath() + "tmp.bcstm")) File.Delete(Path.GetTempPath() + "tmp.bcstm");
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "Save the BCSTM file";
            sav.Filter = "BCSTM file|*.bcstm";
            if (sav.ShowDialog() == DialogResult.OK)
            {
                BinaryReader bin = new BinaryReader(File.Open(input, FileMode.Open));
                bin.BaseStream.Position = 0;
                string data = bin.ReadByte().ToString() + bin.ReadByte().ToString() + bin.ReadByte().ToString() + bin.ReadByte().ToString();
                string MAGIC = Encoding.ASCII.GetBytes("R")[0].ToString() + Encoding.ASCII.GetBytes("S")[0].ToString() + Encoding.ASCII.GetBytes("T")[0].ToString() + Encoding.ASCII.GetBytes("M")[0].ToString();
                if (data == MAGIC)
                {
                    bin.Close();
                    System.IO.File.WriteAllBytes(sav.FileName, BRSTM_BCSTM_converter.Create_BCSTM(File.ReadAllBytes(input)));
                    MessageBox.Show("done !");
                }
                else
                {
                    bin.Close();
                    MessageBox.Show(messages[15]);
                }
            }
        }
        void Wav2BCSTM(string input)
        {
            if (!File.Exists("BrstmConv.exe")) File.WriteAllBytes("BrstmConv.exe", Properties.Resources.BrstmConv);
            if (!File.Exists("BrawlLib.dll")) File.WriteAllBytes("BrawlLib.dll", Properties.Resources.BrawlLib);
            if (File.Exists(Path.GetTempPath() + "tmp.bcstm")) File.Delete(Path.GetTempPath() + "tmp.bcstm");
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Bcstm file|*.bcstm";
            sv.Title = "Save file";
            if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Process prc = new Process();
                prc.StartInfo.FileName = "BrstmConv.exe";
                prc.StartInfo.Arguments = "\"" + input + "\" " + "\"" + Path.GetTempPath() + "tmp.bcstm\"";
                prc.StartInfo.CreateNoWindow = true;
                prc.StartInfo.UseShellExecute = false;
                prc.Start();
                prc.WaitForExit();
                if (!File.Exists(Path.GetTempPath() + "tmp.bcstm")) return;
                File.WriteAllBytes(sv.FileName, BRSTM_BCSTM_converter.Create_BCSTM(File.ReadAllBytes(Path.GetTempPath() + "tmp.bcstm")));
                File.Delete(Path.GetTempPath() + "tmp.bcstm");
                if (File.Exists(sv.FileName)) MessageBox.Show("Done !"); else MessageBox.Show("Error while converting the file, run the command in the cmd to check the output");
            }
        }
        void wav2BRSTM(string input)
        {
            if (!File.Exists("BrstmConv.exe")) File.WriteAllBytes("BrstmConv.exe", Properties.Resources.BrstmConv);
            if (!File.Exists("BrawlLib.dll")) File.WriteAllBytes("BrawlLib.dll", Properties.Resources.BrawlLib);
            if (File.Exists(Path.GetTempPath() + "tmp.bcstm")) File.Delete(Path.GetTempPath() + "tmp.bcstm");
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Brstm file|*.brstm";
            sv.Title = "Save file";
            if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Process prc = new Process();
                prc.StartInfo.FileName = "BrstmConv.exe";
                prc.StartInfo.Arguments = "\"" + input + "\" " + "\"" + sv.FileName + "\"";
                prc.StartInfo.CreateNoWindow = true;
                prc.StartInfo.UseShellExecute = false;
                prc.Start();
                prc.WaitForExit();
                if (File.Exists(sv.FileName)) MessageBox.Show("Done !"); else MessageBox.Show("Error while converting the file, run the command in the cmd to check the output");
            }
        }
        void AudioTOWav(string input)
        {
            if (!File.Exists("vgmstream.exe")) File.WriteAllBytes("vgmstream.exe", Properties.Resources.test);
            if (!File.Exists("libg7221_decode.dll")) File.WriteAllBytes("libg7221_decode.dll", Properties.Resources.libg7221_decode);
            if (!File.Exists("libmpg123-0.dll")) File.WriteAllBytes("libmpg123-0.dll", Properties.Resources.libmpg123_0);
            if (!File.Exists("libvorbis.dll")) File.WriteAllBytes("libvorbis.dll", Properties.Resources.libvorbis);
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "WAV file|*.wav|Every file|*.*";
            sv.Title = "Save file";
            if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Process prc = new Process();
                prc.StartInfo.FileName = "vgmstream.exe";
                prc.StartInfo.Arguments = "-o \"" + sv.FileName + "\" " + "\"" + input + "\"";
                prc.StartInfo.CreateNoWindow = true;
                prc.StartInfo.UseShellExecute = false;
                prc.Start();
                prc.WaitForExit();
                if (File.Exists(sv.FileName)) MessageBox.Show("Done !"); else MessageBox.Show("Error while converting the file, run the command in the cmd to check the output");
            }
        }
        void Wav2CWAV(string input)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "CWAVs|*.bcwav|Every file|*.*";
            sv.Title = "Save the CWAV file";
            if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!APP_not_Optimize_Cwavs)
                {
                    Debug.Print("Optimizing CWAV: " + Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav");
                    WaveFormat New = new WaveFormat(APP_opt_samples, 8, 1);
                    WaveStream Original = new WaveFileReader(input);
                    WaveFormatConversionStream stream = new WaveFormatConversionStream(New, Original);
                    if (System.IO.File.Exists(Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav")) File.Delete(Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav");
                    WaveFileWriter.CreateWaveFile(Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav", stream);
                    stream.Dispose();
                    Original.Dispose();
                }
                Process prc = new Process();
                prc.StartInfo.FileName = "CTR_WaveConverter32.exe";
                if (!APP_not_Optimize_Cwavs) prc.StartInfo.Arguments = "-o \"" + sv.FileName + "\" \"" + Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav\""; else prc.StartInfo.Arguments = "-o \"" + sv.FileName + "\" \"" +input+ "\"";
                prc.Start();
                prc.WaitForExit();
                if (System.IO.File.Exists(Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav")) File.Delete(Path.GetTempPath() + Path.GetFileName(input) + ".tmp.wav");
                if (File.Exists(sv.FileName)) MessageBox.Show("Done !"); else MessageBox.Show("Error while converting the file, run the command in the cmd to check the output");
            }
        }
        #endregion

        private void lZCOMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.ShowDialog();
            dsdecmp.Compress(opn.FileName, opn.FileName + ".cmp");
        }

        private void lZUNCOMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.ShowDialog();
            dsdecmp.Decompress(opn.FileName, opn.FileName + ".dcmp");
        }

        private void basicThemeTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveTheme.FileName = "body_LZ.bin";
            if (saveTheme.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(saveTheme.FileName, Properties.Resources.StaticThemeTemplate);
                openFileLZ.FileName = saveTheme.FileName;
                OPEN_FILE();
            }
        }

        private void panoramicThemeTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveTheme.FileName = "body_LZ.bin";
            if (saveTheme.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(saveTheme.FileName, Properties.Resources.PanoramicTemplate);
                openFileLZ.FileName = saveTheme.FileName;
                OPEN_FILE();
            }
        }

        private void bottomScreenAnimatedTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveTheme.FileName = "body_LZ.bin";
            if (saveTheme.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(saveTheme.FileName, Properties.Resources.AnimatedBotScreenTemplate);
                openFileLZ.FileName = saveTheme.FileName;
                OPEN_FILE();
            }
        }

        private void file_reload_Click(object sender, EventArgs e)
        {
            StatusLabel.Visible = true;
            makeTheme(path + "new_dec_" + filename);
            dsdecmp.Compress(path + "new_dec_" + filename, path + filename);
            File.Delete(path + "new_dec_" + filename);
            StatusLabel.Visible = false;
            OPEN_FILE();
        }

        private void File_installTheme_Click(object sender, EventArgs e)
        {
            makeTheme(path + "new_dec_" + filename);
            dsdecmp.Compress(path + "new_dec_" + filename, path + filename);
            File.Delete(path + "new_dec_" + filename);
            StatusLabel.Visible = false;
            this.Refresh();
            Install dlg = new Install(openFileLZ.FileName);
            dlg.ShowDialog();
        }
    }
}


    public static class exten {
        public static uint ToU32(this byte[] b) {
            return (uint)BitConverter.ToInt32(b, 0);
        }
        public static bool isPower2(this int x) {
            return (x != 0) && ((x & (x - 1)) == 0);
        }
    }