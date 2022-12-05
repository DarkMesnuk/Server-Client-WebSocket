using System;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        public static Config ConfigProgram;
        static void Main(string[] args)
        {
            ConfigProgram = new Config();
            ConfigProgram.Serializer();
            Console.WriteLine("Start sending...");
            Send();
            Console.ReadLine();
        }

        private static async void Send()
        {
            await Task.Run(() =>
            {
                using (var send = new Send())
                {
                    do
                    {
                        send.Main();
                    }
                    while (true);
                }
            });
        }
    }
}
