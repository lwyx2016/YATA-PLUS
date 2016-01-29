using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace YATA
{
    public partial class CwavReplace : Form
    {
        List<string> messages = new List<string> { "Move cursor",
"Launch App",
"Create/Delete Folder",
"Close App",
"Open 3DS",
"Bottom screen - frame 1",
"Bottom screen - frame 2",
"Bottom screen - frame 3",
        "To import some CWAVs you must check the 'Enable use of SFX' box in the theme settings",
        "This CWAV is enabled",
        "This cwav file is invalid !",
        "This CWAV is not enabled",
        "Completed"};
        string[] FileList = new string[8] { "", "", "", "", "", "", "", "" };
        bool[] EnabledList = new bool[8] { false, false, false, false, false, false, false, false };
        byte[] LaunchApp;
        byte[] cursor;
        byte[] CloseApp;
        byte[] Folder;
        byte[] Open3DS;
        byte[] frame1;
        byte[] frame2;
        byte[] frame3;


        public CwavReplace()
        {
            InitializeComponent();
            try { 
            #region language
            if (Form1.APP_LNG.Trim().ToLower() != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\CwavReplace.txt"))
            {
                messages.Clear();
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\CwavReplace.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("lbl")) { ((Label)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("men_gen")) { men_gen.Text = tmp[1]; }
                        else if (line.StartsWith("@")) { messages.Add(line.Replace(@"\r\n", Environment.NewLine).Remove(0, 1)); }
                    }
                }
            }
            listBox1.Items.Add(messages[0]);
            listBox1.Items.Add(messages[1]);
            listBox1.Items.Add(messages[2]);
            listBox1.Items.Add(messages[3]);
            listBox1.Items.Add(messages[4]);
            listBox1.Items.Add(messages[5]);
            listBox1.Items.Add(messages[6]);
            listBox1.Items.Add(messages[7]);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error initializing the language data for this window, try to set the language to english, if you can't because the settings windows crashes too, delete the languages folder");
                MessageBox.Show("for translators: 'Lbl_something' is diffrent from 'lbl_something', follow the template");
                MessageBox.Show("Exception details: " + ex.Message);
                this.Close();
            }
        }

        private void CwavReplace_Load(object sender, EventArgs e)
        {
            if (Form1.enableSec[16] == 0) { MessageBox.Show(messages[8]); this.Close(); }
            FileList = new string[8] { "", "", "", "", "", "", "", "" };
            EnabledList = new bool[8] { false, false, false, false, false, false, false, false };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (CwavCheck(openFileDialog1.FileName) != null)
                {
                    FileList[listBox1.SelectedIndex] = openFileDialog1.FileName;
                    EnabledList[listBox1.SelectedIndex] = true;
                    lbl_enabled.Text = messages[9];
                    lbl_enabled.ForeColor = Color.Green;
                }
                else { MessageBox.Show(messages[10]); }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != null && listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex <= 7)
            {
                btn_select.Enabled = true;
                btn_remove.Enabled = true;
                lbl_enabled.Visible = true;
                if (EnabledList[listBox1.SelectedIndex])
                {
                    lbl_enabled.Text = messages[9];
                    lbl_enabled.ForeColor = Color.Green;
                }
                else
                {
                    lbl_enabled.Text = messages[11];
                    lbl_enabled.ForeColor = Color.Red;
                }
            }
            else
            {
                btn_select.Enabled = false;
                btn_remove.Enabled = false;
                lbl_enabled.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileList[listBox1.SelectedIndex] = "";
            EnabledList[listBox1.SelectedIndex] = false;
            lbl_enabled.ForeColor = Color.Red;
            lbl_enabled.Text = messages[11];
        }

        bool importFrames;

        void Generate(bool import, string filename = "nothing.n") //filename is used only if import = false
        {
            FileStream writer = new FileStream(filename, FileMode.Create);
            BinaryWriter binWRITER = new BinaryWriter(writer);
            MemoryStream mem = new MemoryStream();
            if (import)
            {
                binWRITER = new BinaryWriter(mem);
            }
            //Loads the selected cwavs in memory
            #region loadCWAVs
            BinaryReader read;
            if (EnabledList[0])
            {
                read = new BinaryReader(File.Open(FileList[0], FileMode.Open));
                cursor = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else cursor = null;
            if (EnabledList[1])
            {
                read = new BinaryReader(File.Open(FileList[1], FileMode.Open));
                LaunchApp = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else LaunchApp = null;
            if (EnabledList[2])
            {
                read = new BinaryReader(File.Open(FileList[2], FileMode.Open));
                Folder = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else Folder = null;
            if (EnabledList[3])
            {
                read = new BinaryReader(File.Open(FileList[3], FileMode.Open));
                CloseApp = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else CloseApp = null;
            if (EnabledList[4])
            {
                read = new BinaryReader(File.Open(FileList[4], FileMode.Open));
                Open3DS = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else Open3DS = null;
            if (EnabledList[5])
            {
                read = new BinaryReader(File.Open(FileList[5], FileMode.Open));
                frame1 = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else frame1 = null;
            if (EnabledList[6])
            {
                read = new BinaryReader(File.Open(FileList[6], FileMode.Open));
                frame2 = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else frame2 = null;
            if (EnabledList[7])
            {
                read = new BinaryReader(File.Open(FileList[7], FileMode.Open));
                frame3 = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else frame3 = null;
            #endregion
            //Thanks to Custom themes cwavs for this part :)           
            binWRITER.Write(2);
            binWRITER.Write(0x64);
            if (EnabledList[0])
            {
                binWRITER.Write(CwavCheck(FileList[0]));
                binWRITER.Write(0x50);
                binWRITER.Write(cursor);
            }
            else
            {
                binWRITER.Write(0);
                binWRITER.Write(1);
            }
            if (EnabledList[1])
            {
                binWRITER.Write(CwavCheck(FileList[1]));
                binWRITER.Write(0x50);
                binWRITER.Write(LaunchApp);
            }
            else
            {
                binWRITER.Write(0);
                binWRITER.Write(1);
            }
            if (EnabledList[2])
            {
                binWRITER.Write(CwavCheck(FileList[2]));
                binWRITER.Write(0x50);
                binWRITER.Write(Folder);
            }
            else
            {
                binWRITER.Write(0);
                binWRITER.Write(1);
            }
            if (EnabledList[3])
            {
                binWRITER.Write(CwavCheck(FileList[3]));
                binWRITER.Write(0x50);
                binWRITER.Write(CloseApp);
            }
            else
            {
                binWRITER.Write(0);
                binWRITER.Write(1);
            }
            if (importFrames)
            {
                if (EnabledList[5])
                {
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(CwavCheck(FileList[5]));
                    binWRITER.Write(0x50);
                    binWRITER.Write(frame1);
                }
                else
                {
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(1);
                }
                if (EnabledList[6])
                {
                    binWRITER.Write(CwavCheck(FileList[6]));
                    binWRITER.Write(0x50);
                    binWRITER.Write(frame2);
                }
                else
                {
                    binWRITER.Write(0);
                    binWRITER.Write(1);
                }
                if (EnabledList[7])
                {
                    binWRITER.Write(CwavCheck(FileList[7]));
                    binWRITER.Write(0x50);
                    binWRITER.Write(frame3);
                }
                else
                {
                    binWRITER.Write(0);
                    binWRITER.Write(1);
                }
                if (EnabledList[4])
                {
                    binWRITER.Write(CwavCheck(FileList[4]));
                    binWRITER.Write(0x50);
                    binWRITER.Write(Open3DS);
                }
            }
            else
            {
                if (EnabledList[5])
                {
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(CwavCheck(FileList[5]));
                    binWRITER.Write(0x50);
                    binWRITER.Write(frame1);
                }
                else
                {
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0x64);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(0);
                    binWRITER.Write(1);
                    binWRITER.Write(0);
                    binWRITER.Write(1);
                    binWRITER.Write(0);
                    binWRITER.Write(1);
                }
                if (EnabledList[4])
                {
                    binWRITER.Write(CwavCheck(FileList[4]));
                    binWRITER.Write(0x50);
                    binWRITER.Write(Open3DS);
                }
            }

            if (!binWRITER.BaseStream.Position.ToString("X").EndsWith("0"))
            {
                while (!binWRITER.BaseStream.Position.ToString("X").EndsWith("0"))
                {
                    binWRITER.Write(0);
                }

            }
            if (import)
            {
                if (mem.Length < 0x2DC00)
                {
                    Form1.cwav = mem.ToArray();
                    Form1.cwavLen = (uint)mem.Length;
                }
                else
                {
                    MessageBox.Show("The size of the generated cwav chunk is too big, remove some cwav or lower the quality of them !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    binWRITER.Close();
                    return;
                }
            }
            binWRITER.Close();
            MessageBox.Show(messages[12]);
        }

        private void generateButDontImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save";
            save.Filter = "Bin file|*.bin";
            save.FileName = "";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Generate(false, save.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.bottomDraw != 3)
            {
                if (Form1.bottomFrame != 2 || Form1.bottomFrame != 3)
                {
                    importFrames = false;
                    if (EnabledList[6] || EnabledList[7]) MessageBox.Show("This theme is not set to include an animated bottom screen, the sounds for the frames won't be imported");
                }
            }
            else { if (EnabledList[5] && EnabledList[6] && EnabledList[7]) importFrames = true; }
            Generate(true);
        }

        private byte[] CwavCheck(String cwav)
        {
            try
            {
                BinaryReader binary = new BinaryReader(File.Open(cwav, FileMode.Open));
                int binlength = (int)binary.BaseStream.Length;

                //Header
                byte[] b = binary.ReadBytes(8);
                String cwavhead = null;
                cwavhead = Convert.ToString(String.Format("{0:X}", b[0]));
                cwavhead += Convert.ToString(String.Format("{0:X}", b[1]));
                cwavhead += Convert.ToString(String.Format("{0:X}", b[2]));
                cwavhead += Convert.ToString(String.Format("{0:X}", b[3]));

                //Length
                binary.BaseStream.Position = 0xC;
                b = binary.ReadBytes(4);
                binary.Close();
                int cwavlength = BitConverter.ToInt32(b, 0);

                if (cwavhead.Equals("43574156") && cwavlength == binlength)
                {
                    return b;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
