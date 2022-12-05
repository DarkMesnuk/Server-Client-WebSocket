using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public class CalculationsService
    {
        public CalculationsData Calculation(List<string> packets, CalculationsData calculationData)
        {
            ulong lastPacketNumber = calculationData.LastPacketNumber;
            ulong calculationsPacketCount = calculationData.CalculationsPacketCount;
            double sumNumber = calculationData.SumNumber;
            Dictionary<ulong, ulong> repetitionsNumbersCount = calculationData.RepetitionsNumbersCount;
            double sumOfSquaresOfDifferences = calculationData.SumOfSquaresOfDifferences;

            var newNumbers = new List<ulong>();

            foreach (var packet in packets)
            {
                ulong numberPacket;
                ulong number;

                try
                {
                    var packetElement = packet.Split('_');
                    numberPacket = Convert.ToUInt64(packetElement[0]);
                    number = Convert.ToUInt64(packetElement[1]);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Bad packet. Can not convert : {packet}");
                    continue;
                }

                // Номер останього пакета
                lastPacketNumber = numberPacket;

                // Кількість прорахованих пакетів
                calculationsPacketCount++;

                // Середнє арифметичне
                sumNumber += number;

                // Медіана
                newNumbers.Add(number);

                // Мода
                if (repetitionsNumbersCount.ContainsKey(number))
                    repetitionsNumbersCount[number]++;
                else
                    repetitionsNumbersCount[number] = 1;

            }

            // Кількість прорахованих пакетів
            calculationData.CalculationsPacketCount = calculationsPacketCount;

            // Кількість втрачених пакетів
            calculationData.LosingPacketCount = lastPacketNumber - calculationsPacketCount + 1;
            
            #region Середнє арифметичне
            double average = sumNumber / (double)calculationsPacketCount;
            calculationData.AM = average;
            #endregion

            #region Мода
            ulong maxCount = repetitionsNumbersCount.Values.Max();
            calculationData.Fashion = repetitionsNumbersCount.Where(x => x.Value == maxCount).FirstOrDefault().Key;
            #endregion

            #region Медіана
            var numbers = new List<ulong>();

            for(ulong i = 1; i <= maxCount; i++)
            {
                numbers.AddRange(repetitionsNumbersCount.Where(x => x.Value == i).Select(x => x.Key));
            }

            numbers.Sort();
            calculationData.Median = numbers[numbers.Count / 2];
            #endregion

            #region Стандартне відхилення
            sumOfSquaresOfDifferences += newNumbers.Select(val => (val - average) * (val - average)).Sum();
            calculationData.SD = Math.Sqrt(sumOfSquaresOfDifferences / calculationsPacketCount);
            #endregion

            calculationData.SetCalculationParameters(lastPacketNumber, sumNumber, repetitionsNumbersCount, sumOfSquaresOfDifferences);

            calculationData.CreateString();

            return calculationData;
        }
    }
}
