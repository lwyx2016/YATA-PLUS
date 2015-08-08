//STILL WORKING ON THIS, PROBABLY IT WON'T BE USED, TOO MANY UNKNOWN THINGS IN THE BRSTM STRUCTURE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YATA.Converter
{
    public static class BRSTM_BCSTM_converter
    {

        public static byte[] Create_BCSTM(byte[] BRSTM)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(BRSTM, 0, BRSTM.Length);
            int DATA_SECTION = SearchBytePattern(new byte[4] { 0x44, 0x41, 0x54, 0x41 }, stream);
            byte[] bcstm;
            List<byte> Data = new List<byte>();
            Data.AddRange(Encoding.ASCII.GetBytes("CSTM")); //MAGIC
            Data.AddRange(new byte[] { 0xFF, 0xFE,0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }); // Big endian + Empty data
            stream.Position = 10;
            Data.AddRange(new byte[4] { 0x00,(byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>()); //Size
            Data.AddRange(getEmptyBytes(48, 0x00)); //empty
            Data.AddRange(Encoding.ASCII.GetBytes("INFO")); //MAGIC
            stream.Position = 14;
            Data.AddRange(new byte[4] { 0x00, (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte() }.Reverse<byte>()); //Size
            Data.AddRange(getEmptyBytes(24, 0x00)); //empty
            Data.Add(0x02); //DSPADPCM
            bcstm = Data.ToArray();
            return bcstm;
        }

        private static byte[] getEmptyBytes(int count, byte _byte)
        {
            byte[] res = new byte[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = _byte;
            }
            return res;
        }

        private static int SearchBytePattern(byte[] pattern, MemoryStream bytes)
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
                        return Convert.ToInt32(bytes.Position) - 4;
                    }
                }
                if ((totalLength - bytes.Position) <= patternLength)
                    break;
            }
            bytes.Position = 0;
            return 0;
        }

        public static byte Create_BRSTM(byte[] BCSTM)
        {
            throw new NotImplementedException();
        }

    }
}
