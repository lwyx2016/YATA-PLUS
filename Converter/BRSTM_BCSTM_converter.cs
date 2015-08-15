//STILL WORKING ON THIS, PROBABLY IT WON'T BE USED, TOO MANY UNKNOWN THINGS IN THE BRSTM STRUCTURE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YATA.Converter
{
    public static class BRSTM_BCSTM_converter
    {

        #region original GML code
        /*
        //BRSTM to BCSTM by froggestspirit
        //infile is brstm, outfile is bcstm. both start at position 0
        chr aaaa; //temporary variables
        chr bbbb;
        chr cccc;
        chr dddd;
        file_bin_write_long($4353544D);//CSTM
        file_bin_write_long($FFFE4000);
        file_bin_write_long($00000002);
        file_bin_seek(infile,8);
        file_bin_convert_long();//size Convert long/short swaps the endian-ness so $4312 becomes $1243
        file_bin_write_long($03000000);//0x10
        file_bin_write_long($00400000);
        file_bin_write_long($40000000);
        file_bin_write_long($00010000);
        file_bin_write_long($01400000);//0x20
        file_bin_write_long($40010000);
        file_bin_seek(infile,$1C);
        file_bin_convert_long();//loop?
        file_bin_write_long($02400000);
        dataSection=file_bin_convert_long();//0x30
        file_bin_convert_long();
        file_bin_write_long($00000000);
        file_bin_write_long($00000000);
        file_bin_write_long($494E464F);
        file_bin_write_long($00010000);
        file_bin_write_long($00410000);
        file_bin_write_long($18000000);
        file_bin_write_long($01010000);
        file_bin_write_long($50000000);
        file_bin_write_long($01010000);
        file_bin_write_long($5C000000);
        file_bin_write_long($02010200);
        file_bin_seek(infile,$64);
        file_bin_convert_short();//frequency?
        file_bin_write_byte(outfile,0);
        file_bin_write_byte(outfile,0);
        file_bin_seek(infile,$68);
        file_bin_convert_long();
        file_bin_convert_long();
        file_bin_seek(infile,$74);
        file_bin_convert_long();
        file_bin_write_long($00200000);
        file_bin_write_long($00380000);
        file_bin_seek(infile,$80);
        file_bin_convert_long();
        file_bin_convert_long();
        file_bin_convert_long();
        file_bin_write_long($04000000);
        file_bin_write_long($00380000);
        file_bin_write_long($001F0000);
        file_bin_write_long($18000000);
        file_bin_write_long($01000000);
        file_bin_write_long($01410000);
        file_bin_write_long($20000000);
        file_bin_write_long($02000000);
        file_bin_write_long($02410000);
        file_bin_write_long($28000000);
        file_bin_write_long($02410000);
        file_bin_write_long($30000000);
        file_bin_write_long($7F400000);
        file_bin_write_long($00010000);
        file_bin_write_long($0C000000);
        file_bin_write_long($02000000);
        file_bin_write_long($00010000);
        file_bin_write_long($00030000);
        file_bin_write_long($10000000);
        file_bin_write_long($00030000);
        file_bin_write_long($36000000);
        file_bin_seek(infile,$C0);
        while(file_bin_position(outfile)<$FB) file_bin_convert_short();
        file_bin_convert_long();
        file_bin_seek(infile,$E6);
        while(file_bin_position(outfile)<$109) file_bin_convert_short();
        file_bin_seek(infile,$F8);
        while(file_bin_position(outfile)<$129) file_bin_convert_short();
        file_bin_seek(infile,$11A);
        file_bin_convert_short();
        file_bin_write_long($00000000);
        file_bin_seek(infile,$120);
        while(file_bin_position(outfile)<$13F) file_bin_convert_short();
        file_bin_write_long($5345454B);
        file_bin_seek(infile,$144);
        file_bin_convert_long();
        file_bin_write_long($00000000);
        file_bin_write_long($00000000);
        file_bin_seek(infile,$150);
        while(file_bin_position(outfile)<dataSection){
                aaaa=file_bin_read_byte(infile);
                bbbb=file_bin_read_byte(infile);
                cccc=file_bin_read_byte(infile);
                dddd=file_bin_read_byte(infile);
               
                file_bin_write_byte(outfile,bbbb);
                file_bin_write_byte(outfile,aaaa);
                file_bin_write_byte(outfile,dddd);
                file_bin_write_byte(outfile,cccc);
        }
        file_bin_write_long($44415441);//DATA
        file_bin_seek(infile,file_bin_position(outfile));
        file_bin_convert_long();
        file_bin_convert_long();
        file_bin_convert_long();
 
        file_bin_close(infile);
        file_bin_close(outfile);
        game_end();
        */
        #endregion

        public static byte[] Create_BCSTM(byte[] BRSTM)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(BRSTM, 0, BRSTM.Length);
            int DATA_SECTION = FINDBytePattern(Encoding.ASCII.GetBytes("DATA"), stream);
            byte[] bcstm;
            List<byte> Data = new List<byte>();
            Data.AddRange(Encoding.ASCII.GetBytes("CSTM")); //MAGIC
            Data.AddRange(new byte[] { 0xFF, 0xFE,0x40, 0x00, 0x00, 0x00, 0x00, 0x02 }); // Big endian + Something
            stream.Position = 8;
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>()); //Size
            Data.AddRange(new byte[4] { 0x03, 0x00, 0x00, 0x00 }); //0x10
            Data.AddRange(new byte[4] { 0x00, 0x40, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x40, 0x00, 0x00, 0x00 }); 
            Data.AddRange(new byte[4] { 0x00, 0x01, 0x00, 0x00 }); 
            Data.AddRange(new byte[4] { 0x01, 0x40, 0x00, 0x00 }); //0x20
            Data.AddRange(new byte[4] { 0x40, 0x01, 0x00, 0x00 });
            stream.Position = 0x1C;
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>()); //Loop ?
            Data.AddRange(new byte[4] { 0x02, 0x40, 0x00, 0x00 });
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>()); //0x30
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            Data.AddRange(Encoding.ASCII.GetBytes("INFO")); //MAGIC
            Data.AddRange(new byte[4] { 0x00, 0x01, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x41, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x18, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x01, 0x01, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x50, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x01, 0x01, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x5C, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x02, 0x01, 0x02, 0x00 });
            stream.Position = 0x64;
            Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte()}.Reverse<byte>());//frequency ?
            Data.AddRange(new byte[2] { 0x00, 0x00 });
            stream.Position = 0x68;
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            stream.Position = 0x74;
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { 0x00, 0x20, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x38, 0x00, 0x00 });
            stream.Position = 0x80;
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { 0x04, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x38, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x1F, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x18, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x01, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x01, 0x41, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x20, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x02, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x02, 0x41, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x28, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x02, 0x41, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x30, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x7F, 0x40, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x01, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x0C, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x02, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x01, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x03, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x10, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x03, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x36, 0x00, 0x00, 0x00 });
            stream.Position = 0xC0;
            while (Data.Count < 0xFB) Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte()}.Reverse<byte>());
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            stream.Position = 0xE6;
            while (Data.Count < 0x109) Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            stream.Position = 0xF8;
            while (Data.Count < 0x129) Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            stream.Position = 0x11A;
            Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            stream.Position = 0x120;
            while (Data.Count < 0x13F) Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { 0x53, 0x45, 0x45, 0x4B });
            stream.Position = 0x144;
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
/*          Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });
            Data.AddRange(new byte[4] { 0x00, 0x00, 0x00, 0x00 });*/
            stream.Position = 0x150;
            while (Data.Count < DATA_SECTION)
            {
                Data.AddRange(new byte[2] { (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            }
            stream.Position = DATA_SECTION + 4;
            Data.AddRange(Encoding.ASCII.GetBytes("DATA")); //MAGIC
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            Data.AddRange(new byte[4] { (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>());
            while (Data.Count < stream.Length)
            {
                Data.Add((byte)stream.ReadByte());
            }

            bcstm = Data.ToArray();
            return bcstm;
        }


       private static int FINDBytePattern(byte[] pattern, MemoryStream bytes)
        {
            bool found = false;
            bytes.Position = 0;
            while (found == false)
            {
                if (bytes.Position + 4 >= bytes.Length) return 0;
                if ((byte)bytes.ReadByte() == pattern[0])
                {
                    if ((byte)bytes.ReadByte() == pattern[1])
                    {
                        if ((byte)bytes.ReadByte() == pattern[2])
                        {
                            if ((byte)bytes.ReadByte() == pattern[3])
                            {
                                return Convert.ToInt32(bytes.Position) - 4;
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
