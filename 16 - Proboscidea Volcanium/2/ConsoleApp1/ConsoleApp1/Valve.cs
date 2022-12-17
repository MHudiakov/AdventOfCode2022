using static System.Int32;

namespace ConsoleApp1
{
    internal class Valve
    {
        public string Name { get; set; }

        public int FlowRate { get; set; }

        public List<string> Neighbours { get; set; }

        public bool Visited { get; set; }

        public int Distance { get; set; } = MaxValue;
    }
}
