using System.Numerics;
using ConsoleApp1;

var monkeys = ReadInput();
const string humanName = "humn";

var rootMonkey = monkeys.First(m => m.Name.Equals("root"));
var human = monkeys.First(m => m.Name.Equals(humanName));
human.Operation = "x";
human.Number = null;
rootMonkey.Operation = rootMonkey.Operation.Replace("+", "=");

for (int i = 0; i < 25; i++)
{
    foreach (var monkey in monkeys)
    {
        if (BigInteger.TryParse(monkey.Operation, out _))
        {
            foreach (var m in monkeys.Where(m => m.Operation.Contains(monkey.Name)))
            {
                m.Operation = m.Operation.Replace(monkey.Name, monkey.Operation);
            }
        }
    }

    foreach (var monkey in monkeys)
    {
        if (BigInteger.TryParse(monkey.Operation, out _) || monkey.Operation == "x")
        {
            continue;
        }

        var values = monkey.Operation.Split(' ');
        var leftOperandStr = values[0];
        var rightOperandStr = values[2];
        var operation = values[1];

        if (BigInteger.TryParse(leftOperandStr, out BigInteger leftOperand) && BigInteger.TryParse(rightOperandStr, out BigInteger rightOperand))
        {
            monkey.Operation = operation switch
            {
                "+" => BigInteger.Add(leftOperand, rightOperand).ToString(),
                "-" => BigInteger.Subtract(leftOperand, rightOperand).ToString(),
                "*" => BigInteger.Multiply(leftOperand, rightOperand).ToString(),
                "/" => BigInteger.Divide(leftOperand, rightOperand).ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
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
            rootMonkey.Operation = rootMonkey.Operation.Replace(monkey.Name,
                BigInteger.TryParse(monkey.Operation, out _) || monkey.Operation == "x"
                    ? monkey.Operation
                    : $"({monkey.Operation})");
        }
    }
}

Console.WriteLine("Please solve this equation somehow, for example here: https://www.dcode.fr/equation-solver and you'll get the answer.");
Console.WriteLine();
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