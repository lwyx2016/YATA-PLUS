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
        string[] FileList = new string[8] { "", "", "", "", "", "", "", "" };
        bool[] EnabledList = new bool[8] { false, false, false ,false, false ,false, false, false };
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
        }

        private void CwavReplace_Load(object sender, EventArgs e)
        {
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
                    label4.Text = "This CWAV is enabled";
                    label4.ForeColor = Color.Green;
                }
                else { MessageBox.Show("This cwav file is invalid !"); }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != null && listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex <= 7)
            {
                button2.Enabled = true;
                button3.Enabled = true;
                label4.Visible = true;
                if (EnabledList[listBox1.SelectedIndex])
                {
                    label4.Text = "This CWAV is enabled";
                    label4.ForeColor = Color.Green;
                }
                else
                {
                    label4.Text = "This CWAV is not enabled";
                    label4.ForeColor = Color.Red;
                }
            }
            else 
            {
                button2.Enabled = false;
                button3.Enabled = false;
                label4.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileList[listBox1.SelectedIndex] = "";
            EnabledList[listBox1.SelectedIndex] = false;
            label4.ForeColor = Color.Red;
            label4.Text = "This CWAV is not enabled";
        }

        void Generate(bool import, string filename = "") //filename is used only if import = false
        {
            //Backup old frame data
            //
            if (import)
            {
                if (System.IO.File.Exists(Path.GetTempPath() + "cwav_tmp.bin")) System.IO.File.Delete(Path.GetTempPath() + "cwav_tmp.bin");
                filename = Path.GetTempPath() + "cwav_tmp.bin"; 
            }
            //Loads the selected cwavs in memory
            #region loadCWAVs
            BinaryReader read;
            if (EnabledList[0])
            {
                read = new BinaryReader(File.Open(FileList[0], FileMode.Open));
                LaunchApp = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else LaunchApp = null;
            if (EnabledList[1])
            {
                read = new BinaryReader(File.Open(FileList[1], FileMode.Open));
                cursor = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else cursor = null;
            if (EnabledList[2])
            {
                read = new BinaryReader(File.Open(FileList[2], FileMode.Open));
                CloseApp = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else CloseApp = null;
            if (EnabledList[3])
            {
                read = new BinaryReader(File.Open(FileList[3], FileMode.Open));
                Folder = read.ReadBytes((int)read.BaseStream.Length);
                read.Close();
            }
            else Folder = null;
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
           FileStream writer = new FileStream(filename, FileMode.Create);
           BinaryWriter binWRITER = new BinaryWriter(writer);
           binWRITER.Write(2);
           binWRITER.Write(UInt32.Parse("64", System.Globalization.NumberStyles.HexNumber));
           if (EnabledList[0])
           {
               binWRITER.Write(CwavCheck(FileList[0]));
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
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
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
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
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
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
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
               binWRITER.Write(CloseApp);
           }
           else
           {
               binWRITER.Write(0);
               binWRITER.Write(1);
           }
           if (EnabledList[5])
           {
               binWRITER.Write(0);
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(0);
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(0);
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(0);
               binWRITER.Write(0);
               binWRITER.Write(0);
               binWRITER.Write(CwavCheck(FileList[5]));
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
               binWRITER.Write(frame1);
           }
           else
           {
               binWRITER.Write(0);
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(0);
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(0);
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(UInt32.Parse("64", NumberStyles.HexNumber));
               binWRITER.Write(0);
               binWRITER.Write(0);
               binWRITER.Write(0);
               binWRITER.Write(0);
               binWRITER.Write(1);
           }
           if (EnabledList[6])
           {
               binWRITER.Write(CwavCheck(FileList[6]));
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
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
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
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
               binWRITER.Write(UInt32.Parse("50", NumberStyles.HexNumber));
               binWRITER.Write(Open3DS);
           }
           if (!binWRITER.BaseStream.Position.ToString("X").EndsWith("0"))
           {
               while (!binWRITER.BaseStream.Position.ToString("X").EndsWith("0"))
               {
                   binWRITER.Write(0);
               }

           }
           binWRITER.Close();
           binWRITER.Close();
           if (import) 
           {
               Form1.cwav = File.ReadAllBytes(Path.GetTempPath() + "cwav_tmp.bin");
               System.IO.File.Delete(Path.GetTempPath() + "cwav_tmp.bin");
           }
           MessageBox.Show("Completed");
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
            Generate(true, "");
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
