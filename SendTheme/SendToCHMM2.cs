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

//This script isn't tested and will be used later
//Debug info will be removed
namespace YATA.SendTheme
{
    class SendToCHMM2
    {
        public int send(string Address, string file, int port)
        {
            try
            {
                Debug.Print("Starting send....");
                IPEndPoint ipEndPoint = CreateIPEndPoint(Address + ":" + port.ToString());
                Debug.Print("IpEndPoint created");
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Debug.Print("client created");
                client.Connect(ipEndPoint);
                Debug.Print("Connected");
                byte[] data = new byte[11];
                Debug.Print("data created");
                client.Receive(data);
                Debug.Print("Recieved data");
                if (data != GetBytes("YATA SENDER")) return 1;
                Debug.Print("data matches YATA SENDER");
                if (!File.Exists(file)) return 2;
                Debug.Print("File exists");
                client.SendFile(file);
                Debug.Print("Sent file");
                client.Shutdown(SocketShutdown.Both);
                Debug.Print("Shutdown OK");
                client.Close();
                Debug.Print("Connection closed");
                return 0;
            }
            catch (Exception ex)
            {
                Debug.Print("Exception: " + ex.Message);
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