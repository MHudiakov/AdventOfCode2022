namespace ConsoleApp1
{
    internal class Monkey
    {
        public int Id { get; set; }

        public string Operation { get; set; }

        public List<Item> Items { get; set; } = new();

        public int Divider { get; set; }

        public int TestTrueMonkeyNumber { get; set; }

        public int TestFalseMonkeyNumber { get; set; }

        public int InspectionsCount { get; set; } = 0;
    }
}
