using ConsoleApp1;

var monkeys = ReadInput();

var monkeysStack = new Stack<Monkey>();
var rootMonkey = monkeys.First(m => m.Name.Equals("root"));
monkeysStack.Push(rootMonkey);

while (monkeysStack.Any())
{
    var monkey = monkeysStack.Peek();
    var values = monkey.Operation.Split(' ');
    var leftMonkeyName = values[0];
    var rightMonkeyName = values[2];
    var operation = values[1];
    var leftMonkey = monkeys.First(m => m.Name.Equals(leftMonkeyName));
    var rightMonkey = monkeys.First(m => m.Name.Equals(rightMonkeyName));

    if (leftMonkey.Number.HasValue && rightMonkey.Number.HasValue)
    {
        monkeysStack.Pop();

        monkey.Number = operation switch
        {
            "+" => leftMonkey.Number + rightMonkey.Number,
            "-" => leftMonkey.Number - rightMonkey.Number,
            "*" => leftMonkey.Number * rightMonkey.Number,
            "/" => leftMonkey.Number / rightMonkey.Number,
            _ => monkey.Number
        };
    }
    else
    {
        if (!leftMonkey.Number.HasValue)
        {
            monkeysStack.Push(leftMonkey);
        }

        if (!rightMonkey.Number.HasValue)
        {
            monkeysStack.Push(rightMonkey);
        }
    }
}


Console.WriteLine(rootMonkey.Number);

List<Monkey> ReadInput()
{
    var monkeys = new List<Monkey>();

    foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
    {
        var monkey = new Monkey();
        var values = line.Split(": ");

        monkey.Name = values[0];

        if (int.TryParse(values[1], out int number))
        {
            monkey.Number = number;
        }
        else
        {
            monkey.Operation = values[1];
        }

        monkeys.Add(monkey);
    }

    return monkeys;
}