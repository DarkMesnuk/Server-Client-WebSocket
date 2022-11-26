using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Get : IDisposable
    {
        private UdpClient udpSocket;
        private IPEndPoint remoteIp;

        public Get()
        {
            udpSocket = new UdpClient(8001);
            remoteIp = new IPEndPoint(IPAddress.Any, 0);
        }

        public void Main(List<string> packets)
        {
            udpSocket.JoinMulticastGroup(IPAddress.Parse(Program.ConfigProgram.MulticastGroup));
            packets.Add(Encoding.UTF8.GetString(udpSocket.Receive(ref remoteIp)));
            udpSocket.DropMulticastGroup(IPAddress.Parse(Program.ConfigProgram.MulticastGroup));
        }

        public void Dispose()
        {
            udpSocket.Dispose();
        }
    }
}
 

