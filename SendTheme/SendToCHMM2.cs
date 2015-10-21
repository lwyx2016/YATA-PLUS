using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//This script isn't tested and will be used later
//Debug info will be removed
namespace YATA.SendTheme
{
    class SendToCHMM2
    {
        List<String> LOG = new List<string>();

        public int send(string Address, string file, int port, bool SaveLog)
        {
            try
            {                
                LOG.Add("Starting sender....");
                IPEndPoint ipEndPoint = CreateIPEndPoint(Address + ":" + port.ToString());
                LOG.Add("IpEndPoint created");
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                LOG.Add("client created");
                client.Connect(ipEndPoint);
                LOG.Add("Connected");
                byte[] data = new byte[11];
                LOG.Add("data created");
                client.Receive(data);
                LOG.Add("Recieved data:");
                string RecivedData = "";
                for (int i = 0; i < data.Length; i++) RecivedData = RecivedData + data[i].ToString();             
                LOG.Add(RecivedData);
                string ShouldRecive = "";
                for (int i = 0; i < Encoding.UTF8.GetBytes("YATA SENDER").Length; i++) ShouldRecive = ShouldRecive + Encoding.UTF8.GetBytes("YATA SENDER")[i].ToString();
                if (!RecivedData.Contains(ShouldRecive)) { if (SaveLog) File.WriteAllLines("SENDLOG.txt", LOG); return 1;}
                LOG.Add("data matches YATA SENDER");
                if (!File.Exists(file)) { if (SaveLog) File.WriteAllLines("SENDLOG.txt", LOG); return 2; }
                LOG.Add("File exists");
                client.SendFile(file);
                ShouldRecive = "";
                for (int i = 0; i < Encoding.UTF8.GetBytes("YATA TERM").Length; i++) ShouldRecive = ShouldRecive + Encoding.UTF8.GetBytes("YATA TERM")[i].ToString();
                data = new byte[9];
                client.Receive(data);
                RecivedData = "";
                for (int i = 0; i < data.Length; i++) RecivedData = RecivedData + data[i].ToString();
                LOG.Add(RecivedData);
                if (!RecivedData.Contains(ShouldRecive)) { LOG.Add("YATA TERM not recived"); return 2; }
                client.Shutdown(SocketShutdown.Both);
                LOG.Add("Client shutdown");
                client.Close();
                LOG.Add("Client close");
                if (SaveLog) File.WriteAllLines("SENDLOG.txt", LOG);
                return 0;
            }
            catch (Exception ex)
            {
                LOG.Add("Exception:");
                LOG.Add("Message" + ex.Message);
                LOG.Add("Inner exception" + ex.InnerException);
                LOG.Add(ex.ToString());
                // MessageBox.Show("There was an error: " + ex.Message + "\r\n" + ex.InnerException);
                if (SaveLog) File.WriteAllLines("SENDLOG.txt", LOG);
                return -1;
            }
           
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }
    }

}