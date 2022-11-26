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
            var numbers = calculationData.Numbers;
            Dictionary<ulong, ulong> repetitionsNumbersCount = calculationData.RepetitionsNumbersCount;
            var sumOfSquaresOfDifferences = calculationData.SumOfSquaresOfDifferences;

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

            numbers.AddRange(newNumbers);

            #region Медіана
            numbers.Sort();
            calculationData.Median = numbers[numbers.Count / 2];
            #endregion

            #region Мода
            ulong maxCount = 0;
            ulong mode = 0;

            foreach (ulong numberKey in repetitionsNumbersCount.Keys)
            {
                if (repetitionsNumbersCount[numberKey] > maxCount)
                {
                    maxCount = repetitionsNumbersCount[numberKey];
                    mode = numberKey;
                }
            }

            calculationData.Fashion = mode;
            #endregion

            #region Стандартне відхилення
            sumOfSquaresOfDifferences += newNumbers.Select(val => (val - average) * (val - average)).Sum();
            calculationData.SD = Math.Sqrt(sumOfSquaresOfDifferences / calculationsPacketCount);
            #endregion

            calculationData.SetCalculationParameters(lastPacketNumber, sumNumber, numbers, repetitionsNumbersCount, sumOfSquaresOfDifferences);

            calculationData.CreateString();

            return calculationData;
        }
    }
}
