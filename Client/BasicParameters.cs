using System.Collections.Generic;

namespace Client
{
    public class BasicParameters
    {
        public List<string> Packets { get; init; }
        public CalculationsData Calculation { get; init; }

        public BasicParameters()
        {
            Packets = new List<string>();
            Calculation = new CalculationsData();
        }
    }
}
