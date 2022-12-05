using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Send : IDisposable
    {
        private UdpClient udpSocket;
        private IPEndPoint remotePoint;
        private Random random;
        private ulong numberPacket;

        public Send()
        {
            udpSocket = new UdpClient();
            remotePoint = new IPEndPoint(IPAddress.Parse("235.5.5.11"), 8001);
            random = new Random();
            numberPacket = 0;
        }

        public async void Main() =>
            await udpSocket.SendAsync(Encoding.UTF8.GetBytes($"{numberPacket++}_{random.Next(Program.ConfigProgram.RangeFrom, Program.ConfigProgram.RangeTo)}"), remotePoint);

        public void Dispose()
        {
            udpSocket.Dispose();
        }
    }
}