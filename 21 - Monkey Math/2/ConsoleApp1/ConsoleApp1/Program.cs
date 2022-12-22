using ConsoleApp1;

var monkeys = ReadInput();
const string humanName = "humn";

var rootMonkey = monkeys.First(m => m.Name.Equals("root"));
var human = monkeys.First(m => m.Name.Equals(humanName));
human.Operation = "x";
human.Number = null;
rootMonkey.Operation = rootMonkey.Operation.Replace("+", "=");




foreach (var monkey in monkeys)
{
    if (int.TryParse(monkey.Operation, out int number))
    {
        foreach (var m in monkeys.Where(m => m.Operation.Contains(monkey.Name)))
        {
            m.Operation = m.Operation.Replace(monkey.Name, monkey.Operation);
        }
    }
}




var monkeyNames = monkeys.Select(m => m.Name).ToList();
while (monkeyNames.Any(rootMonkey.Operation.Contains))
{
    foreach (var monkey in monkeys)
    {
        if (monkey == rootMonkey)
        {
            continue;
        }

        if (rootMonkey.Operation.Contains(monkey.Name))
        {
            rootMonkey.Operation = rootMonkey.Operation.Replace(monkey.Name, int.TryParse(monkey.Operation, out _)? monkey.Operation : $"({monkey.Operation})");
        }
    }
}


















Console.WriteLine(rootMonkey.Operation);

List<Monkey> ReadInput()
{
    var monkeys = new List<Monkey>();

    foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
    {
        var monkey = new Monkey();
        var values = line.Split(": ");

        monkey.Name = values[0];
        monkey.Operation = values[1];
        monkeys.Add(monkey);
    }

    return monkeys;
}