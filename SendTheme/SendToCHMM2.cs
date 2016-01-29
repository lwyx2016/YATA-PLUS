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
        public int send(string Address, string file, int port, bool SaveLog)
        {
            try
            {                
                Debug.WriteLine("Starting sender....");
                IPEndPoint ipEndPoint = CreateIPEndPoint(Address + ":" + port.ToString());
                Debug.WriteLine("IpEndPoint created");
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Debug.WriteLine("client created");
                client.Connect(ipEndPoint);
                Debug.WriteLine("Connected");
                byte[] data = new byte[11];
                Debug.WriteLine("data created");
                client.Receive(data);
                Debug.WriteLine("Recieved data:");
                string RecivedData = "";
                for (int i = 0; i < data.Length; i++) RecivedData = RecivedData + data[i].ToString();             
                Debug.WriteLine(RecivedData);
                string ShouldRecive = "";
                for (int i = 0; i < Encoding.UTF8.GetBytes("YATA SENDER").Length; i++) ShouldRecive = ShouldRecive + Encoding.UTF8.GetBytes("YATA SENDER")[i].ToString();
                if (!RecivedData.Contains(ShouldRecive)) { return 1;}
                Debug.WriteLine("data matches YATA SENDER");
                if (!File.Exists(file)) { return 2; }
                Debug.WriteLine("File exists");
                client.SendFile(file);
                ShouldRecive = "";
                for (int i = 0; i < Encoding.UTF8.GetBytes("YATA TERM").Length; i++) ShouldRecive = ShouldRecive + Encoding.UTF8.GetBytes("YATA TERM")[i].ToString();
                data = new byte[9];
                client.Receive(data);
                RecivedData = "";
                for (int i = 0; i < data.Length; i++) RecivedData = RecivedData + data[i].ToString();
                Debug.WriteLine(RecivedData);
                if (!RecivedData.Contains(ShouldRecive)) { Debug.WriteLine("YATA TERM not recived"); return 2; }
                client.Shutdown(SocketShutdown.Both);
                Debug.WriteLine("Client shutdown");
                client.Close();
                Debug.WriteLine("Client close");
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception:");
                Debug.WriteLine("Message" + ex.Message);
                Debug.WriteLine("Inner exception" + ex.InnerException);
                Debug.WriteLine(ex.ToString());
                // MessageBox.Show("There was an error: " + ex.Message + "\r\n" + ex.InnerException);
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