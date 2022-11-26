using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class CalculationsData : ICloneable
    {

        /// <summary>
        /// Середнє арифметичне
        /// </summary>
        public double AM { get; set; }

        /// <summary>
        /// Стандартне відхилення
        /// </summary>
        public double SD { get; set; }

        /// <summary>
        /// Мода
        /// </summary>
        public ulong Fashion { get; set; }

        /// <summary>
        /// Медіана
        /// </summary>
        public ulong Median { get; set; }

        /// <summary>
        /// Кількість втрачених пакетів
        /// </summary>
        public ulong LosingPacketCount { get; set; }

        /// <summary>
        /// Кількість прорахованих пакетів
        /// </summary>
        public ulong CalculationsPacketCount { get; set; }

        public List<string> CalculationsDataString { get; private set; }

        #region Розрахункові параметри для прискорення розрахунків
        public ulong LastPacketNumber { get; private set; }
        public double SumNumber { get; private set; }
        public List<ulong> Numbers { get; private set; }
        public Dictionary<ulong, ulong> RepetitionsNumbersCount { get; private set; }
        public double SumOfSquaresOfDifferences { get; set; }
        #endregion

        public CalculationsData()
        {
            AM = 0;
            SD = 0;
            Fashion = 0;
            Median = 0;
            LosingPacketCount = 0;
            CalculationsPacketCount = 0;
            CalculationsDataString = new List<string>();
            CalculationsDataString.Add(String.Empty);

            LastPacketNumber = 0;
            SumNumber = 0;
            Numbers = new List<ulong>();
            RepetitionsNumbersCount = new Dictionary<ulong, ulong>();
        }

        public object Clone()
        {
            var clone = new CalculationsData();

            clone.AM = AM;
            clone.SD = SD;
            clone.Fashion = Fashion;
            clone.Median = Median;
            clone.LosingPacketCount = LosingPacketCount;
            clone.CalculationsPacketCount = CalculationsPacketCount;
            clone.CalculationsDataString = CalculationsDataString;

            clone.LastPacketNumber = LastPacketNumber;
            clone.SumNumber = SumNumber;
            clone.Numbers = Numbers;
            clone.RepetitionsNumbersCount = RepetitionsNumbersCount;

            return clone;
        }

        public void SetCalculationParameters(ulong lastPacketNumber, double sumNumber, List<ulong> numbers, Dictionary<ulong, ulong> repetitionsNumbersCount, double sumOfSquaresOfDifferences)
        {
            LastPacketNumber = lastPacketNumber;
            SumNumber = sumNumber;
            Numbers = numbers;
            RepetitionsNumbersCount = repetitionsNumbersCount;
            SumOfSquaresOfDifferences = sumOfSquaresOfDifferences;
        }

        public void CreateString()
        {
            StringBuilder calculationsDataString = new StringBuilder();
            calculationsDataString.AppendLine("НА МОМЕНТ ВИВЕДЕННЯ\n");
            calculationsDataString.AppendLine($"Кiлькiсть оброблених пакетiв: {CalculationsPacketCount}");
            calculationsDataString.AppendLine($"Кiлькiсть втрачених пакетiв: {LosingPacketCount}");
            calculationsDataString.AppendLine("\n");
            calculationsDataString.AppendLine($"Середнє арифметичне: {AM}");
            calculationsDataString.AppendLine($"Стандартне вiдхилення: {SD}");
            calculationsDataString.AppendLine($"Мода: {Fashion}");
            calculationsDataString.AppendLine($"Медiана: {Median}");

            CalculationsDataString[0] = calculationsDataString.ToString();
        }
    }
}
