using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace YATA
{
    public partial class CWAVs_dumper : Form
    {
        List<String> messages = new List<string>(){"No files found",
        "Done !",
        "Cleaning up...",
        "This theme doesn't support CWAVs to add them check the 'Enable use of SFX' box in the theme settings and go to 'Create CWAVs chunk'"};
        public CWAVs_dumper()
        {
            InitializeComponent();
            try { 
            #region language
            if (Form1.APP_LNG.Trim().ToLower() != "english" && File.Exists(@"languages\" + Form1.APP_LNG + @"\CwavsDumper.txt"))
            {
                messages.Clear();
                string[] lng = File.ReadAllLines(@"languages\" + Form1.APP_LNG + @"\CwavsDumper.txt");
                foreach (string line in lng)
                {
                    if (!line.StartsWith(";"))
                    {
                        string[] tmp = line.Replace(@"\r\n", Environment.NewLine).Split(Convert.ToChar("="));
                        if (line.StartsWith("btn")) { ((Button)this.Controls.Find(tmp[0], true)[0]).Text = tmp[1]; }
                        else if (line.StartsWith("@")) { messages.Add(line.Replace(@"\r\n", Environment.NewLine).Remove(0, 1)); }
                    }
                }
                label1.Text = messages[0];
            }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error initializing the language data for this window, try to set the language to english, if you can't because the settings windows crashes too, delete the languages folder");
                MessageBox.Show("for translators: 'Lbl_something' is diffrent from 'lbl_something', follow the template");
                MessageBox.Show("Exception details: " + ex.Message);
            }
        }

        List<long> source = new List<long>();
        string fileName;
        string fileExtension;
        byte filesizelocation;
        int filesizebuffer = 0;
        string filesizeString;

        private void button1_Click(object sender, EventArgs e)
        {
            clean();
            listBox1.Items.Clear();
            listBox1.Enabled = false;
            btn_dump.Enabled = false;
            btn_play.Enabled = false;
            btn_exportCWAV.Enabled = false;
            btn_exportWAV.Enabled = false;
            label1.Visible = false;
            string[] filesize = new string[4];
            byte[] magic = new byte[4];
            byte[] CWAVBytes_Little_end = new byte[] { 0x43, 0x57, 0x41, 0x56 };
            byte[] CWAVBytes_BIG_end = new byte[] { 0x43, 0x57, 0x41, 0x56 };
            FileStream fs = new FileStream(Path.GetTempPath() + "snd_dump.bin", FileMode.Open, FileAccess.ReadWrite);
            SearchBytePattern(CWAVBytes_Little_end, fs);
            SearchBytePattern(CWAVBytes_BIG_end, fs);
            if (source.Count == 0)
            {
                btn_dump.Enabled = false;
                label1.Visible = true;
                return;
            }
            foreach (long s in source)
            {
                fileName = "0x" + s.ToString("X");
                fs.Seek(s, SeekOrigin.Begin);
                fs.Read(magic, 0, 4);
                string HexMagic = magic[0].ToString("X2") + magic[1].ToString("X2") + magic[2].ToString("X2") + magic[3].ToString("X2");
                if ((string.Compare(Encoding.ASCII.GetString(magic, 0, 4).ToString(), "CWAV") == 0))
                {
                    fileExtension = "BCWAV";
                    filesizelocation = 0x0C;
                    if (Directory.Exists(Path.GetTempPath() + "DUMP\\" ) == false) 
                    {
                        Directory.CreateDirectory(Path.GetTempPath() + "DUMP\\");
                    }
                }
                fs.Seek(s, SeekOrigin.Begin);
                fs.Seek(filesizelocation, SeekOrigin.Current);//advances from magic to get filesize.
                int i3 = 0;//i3 is to ensure that you're only reading the four needed bytes of file size. This will be ammended as new formats become available.
                for (int time = 0; time <= 3; time++)
                {
                    filesize[i3] = fs.ReadByte().ToString("X2");//X2 is used so that if any bytes get their leading zeroes truncated, it'll add them right back before adding it to the string array.
                    i3++;
                }
                filesizeString = filesize[3] + filesize[2] + filesize[1] + filesize[0];//get the filesize string.
                byte[] bytes = new byte[Convert.ToInt32(filesizeString, 16)];//convert this to read.
                filesizebuffer = int.Parse(filesizeString, System.Globalization.NumberStyles.HexNumber);//sets filesize to byte array size for loading and saving.
                fs.Seek(s, SeekOrigin.Begin);//seek back to read all the data
                fs.Read(bytes, 0, Convert.ToInt32(filesizeString, 16));//read the ripped music data into the buffer
                BinaryWriter binWriter = new BinaryWriter(File.Open(Path.GetTempPath() + "DUMP\\"  + fileName + "." + fileExtension, FileMode.Create));//create the new file with the parameters set earlier.
                binWriter.BaseStream.Write(bytes, 0, bytes.Length);//write the data.
                binWriter.Close();//close the data stream
                Array.Clear(bytes, 0, bytes.Length);//clear the array so no garbage data remains
                bytes = null;//forcefully destroy any data left and make the array become invalid until it is recreated.
            }
            fs.Close();
            fs.Dispose();
            if (File.Exists(Path.GetTempPath() + "snd_dump.bin")) File.Delete(Path.GetTempPath() + "snd_dump.bin");
            string[] files = Directory.GetFiles(Path.GetTempPath() + "DUMP\\" );
            foreach (string file in files)
            {
                listBox1.Items.Add(System.IO.Path.GetFileName(file.ToString()));
            }
            if (!File.Exists("vgmstream.exe")) File.WriteAllBytes("vgmstream.exe", Properties.Resources.test);
            if (!File.Exists("libg7221_decode.dll")) File.WriteAllBytes("libg7221_decode.dll", Properties.Resources.libg7221_decode);
            if (!File.Exists("libmpg123-0.dll")) File.WriteAllBytes("libmpg123-0.dll", Properties.Resources.libmpg123_0);
            if (!File.Exists("libvorbis.dll")) File.WriteAllBytes("libvorbis.dll", Properties.Resources.libvorbis);
            listBox1.Enabled = true;
            btn_exportCWAV.Enabled = true;
            btn_exportWAV.Enabled = true;
        }

        public List<int> SearchBytePattern(byte[] pattern, Stream bytes)
        {

            int patternLength = pattern.Length;
            long totalLength = bytes.Length;
            byte firstMatchByte = pattern[0];
            for (long i = 0; i < totalLength; i++)
            {
                if (firstMatchByte == bytes.ReadByte())
                {
                    bytes.Position--;
                    byte[] match = new byte[patternLength];
                    bytes.Read(match, 0, patternLength);
                    if (match.SequenceEqual<byte>(pattern))
                    {
                        source.Add(bytes.Position - patternLength);
                        i += patternLength - 1;
                    }
                }
                if ((totalLength - bytes.Position) <= patternLength)
                    break;
            }
            bytes.Position = 0;
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if ( folderBrowserDialog1.SelectedPath != "")
            {
                string[] files = Directory.GetFiles(Path.GetTempPath() + "DUMP\\");
                foreach (string file in files)
                {
                    System.IO.File.Copy(file, folderBrowserDialog1.SelectedPath +"\\"+ System.IO.Path.GetFileName(file), true);
                }
                MessageBox.Show(messages[1]);
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("vgmstream.exe")) File.WriteAllBytes("vgmstream.exe", Properties.Resources.test);
            if (!File.Exists("libg7221_decode.dll")) File.WriteAllBytes("libg7221_decode.dll", Properties.Resources.libg7221_decode);
            if (!File.Exists("libmpg123-0.dll")) File.WriteAllBytes("libmpg123-0.dll", Properties.Resources.libmpg123_0);
            if (!File.Exists("libvorbis.dll")) File.WriteAllBytes("libvorbis.dll", Properties.Resources.libvorbis);
            try
            {
            if (!Directory.Exists(Path.GetTempPath() + @"DUMP\tmpWAVS\")) Directory.CreateDirectory(Path.GetTempPath() + @"DUMP\tmpWAVS\");
            string[] files = Directory.GetFiles(Path.GetTempPath() + "DUMP\\");
            if (File.Exists(Path.GetTempPath() + @"DUMP\tmpWAVS\" + Path.GetFileName(files[listBox1.SelectedIndex]) + ".wav")) File.Delete(Path.GetTempPath() + @"DUMP\tmpWAVS\" + Path.GetFileName(files[listBox1.SelectedIndex]) + ".wav");
            Process proc = new Process();
            proc.StartInfo.FileName = "vgmstream.exe";
            proc.StartInfo.Arguments = "-o \"" + Path.GetTempPath() + @"DUMP\tmpWAVS\" + Path.GetFileName(files[listBox1.SelectedIndex]) + ".wav\" "+ "\"" + files[listBox1.SelectedIndex] + "\"";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
            System.Diagnostics.Process.Start(Path.GetTempPath() + @"DUMP\tmpWAVS\" + Path.GetFileName(files[listBox1.SelectedIndex]) + ".wav");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_play.Enabled = true;
        }

        void clean() 
        {
            //if (File.Exists(Path.GetTempPath() + "snd_dump.bin")) File.Delete(Path.GetTempPath() + "snd_dump.bin");
            if (Directory.Exists(Path.GetTempPath() + "DUMP")) Directory.Delete(Path.GetTempPath() + "DUMP", true);
            return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("vgmstream.exe")) File.WriteAllBytes("vgmstream.exe", Properties.Resources.test);
            if (!File.Exists("libg7221_decode.dll")) File.WriteAllBytes("libg7221_decode.dll", Properties.Resources.libg7221_decode);
            if (!File.Exists("libmpg123-0.dll")) File.WriteAllBytes("libmpg123-0.dll", Properties.Resources.libmpg123_0);
            if (!File.Exists("libvorbis.dll")) File.WriteAllBytes("libvorbis.dll", Properties.Resources.libvorbis);
             folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath == "") return;
            if (!Directory.Exists(Path.GetTempPath() + "DUMP\\Wav"))
            { Directory.CreateDirectory(Path.GetTempPath() + "DUMP\\Wav"); }
            else
            {
                string[] Wavs = Directory.GetFiles(Path.GetTempPath() + "DUMP\\Wav");
                foreach (string Wav in Wavs)
                {
                    System.IO.File.Delete(Wav);
                }
            }
            string[] files = Directory.GetFiles(Path.GetTempPath() + "DUMP\\");
            foreach (string file in files)
            {
                  Process proc = new Process();
                  proc.StartInfo.FileName = "vgmstream.exe";
                  proc.StartInfo.Arguments = "-o " + Path.GetTempPath() + "DUMP\\Wav/" + System.IO.Path.GetFileName(file) + ".wav " + file;
                  proc.StartInfo.UseShellExecute = false;
                  proc.StartInfo.CreateNoWindow = true;
                  proc.Start();
                  proc.WaitForExit();
            }
            string[] ConvertedFiles = Directory.GetFiles(Path.GetTempPath() + "DUMP\\Wav");
            foreach (string ConvertedFile in ConvertedFiles)
            {
                System.IO.File.Copy(ConvertedFile, folderBrowserDialog1.SelectedPath + "\\" + System.IO.Path.GetFileName(ConvertedFile), true);
            }
            MessageBox.Show(messages[1]);
        }

        private void Frm_closing(object sender, FormClosingEventArgs e)
        {
            label1.Visible = true;
            label1.Text = messages[2];
            this.Refresh();
            clean();
          /*  if (File.Exists("vgmstream.exe")) File.Delete("vgmstream.exe");
            if (File.Exists("libg7221_decode.dll")) File.Delete("libg7221_decode.dll");
            if (File.Exists("libmpg123-0.dll")) File.Delete("libmpg123-0.dll");
            if (File.Exists("libvorbis.dll")) File.Delete("libvorbis.dll");*/
        }

        private void CWAVs_dumper_Load(object sender, EventArgs e)
        {
            if (Form1.enableSec[16] == 0) { MessageBox.Show(messages[3]); this.Close(); }
        }
    }
}
