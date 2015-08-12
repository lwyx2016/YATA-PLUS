using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YATA
{
    public partial class FileConverter : Form
    {
        public FileConverter()
        {
            InitializeComponent();
        }

        private void FileConverter_Load(object sender, EventArgs e)
        {

        }

       public enum ConvertType
        {
            brstmTOwav,
            wavTObrstm,
            wavTObcstm,
            brstmTObcstm,
            wavTOcwav,
            cwavTowav,
            wavTOcwav_No_opt,
            nothing,
            play_file
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
