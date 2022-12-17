namespace ConsoleApp1
{
    internal class Node
    {
        public string Name { get; set; }

        public int TotalPressure { get; set; }

        public int RemainingTime { get; set; }

        public List<string> ValvesToVisit { get; set; }
    }
}
