using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        public static Config ConfigProgram;
        
        static void Main(string[] args)
        {
            ConfigProgram = new Config();
            ConfigProgram.Serializer();
            var basicParameters = new BasicParameters();

            getPackets(basicParameters.Packets);
            calculationsPacket(basicParameters.Packets, basicParameters.Calculation);

            var command = String.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("Start...\n");

                lock (basicParameters.Calculation)
                {
                    Console.WriteLine(basicParameters.Calculation.CalculationsDataString.FirstOrDefault());
                }

                command = Console.ReadLine();
            }
            while (command == "");
            Console.WriteLine("\nEnd");
        }

        private static async void getPackets(List<string> packets)
        {
            await Task.Run(async () =>
            {
                using (var get = new Get())
                {
                    do
                    {

                        try
                        {
                            get.Main(packets);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Can not get packet");
                            continue;
                        }
                        await Task.Delay(Program.ConfigProgram.Delay);
                    }
                    while (true);
                }
            });
        }

        private static async void calculationsPacket(List<string> packets, CalculationsData calculations)
        {
            await Task.Run(async () =>
            {
                var calculationsService = new CalculationsService();

                while(packets.Count == 0)
                {
                    await Task.Delay(Program.ConfigProgram.Delay * 2);
                }

                do
                {
                    var currentPacket = new List<string>();
                    try
                    {
                        lock (packets)
                        {
                            currentPacket = packets.ToList();
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Can not make calculations");
                        continue;
                    }

                    calculations = calculationsService.Calculation(currentPacket, (CalculationsData)calculations.Clone());

                    try
                    {
                        await Task.Delay(Program.ConfigProgram.Delay);
                        lock (packets)
                        {
                            packets.RemoveRange(0, currentPacket.Count);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error to remove packet");
                    }
                }
                while (true);
            });
        }
    }
}
