using System.Text.RegularExpressions;
using ConsoleApp1;
using DynamicExpresso;

var input = File.ReadAllLines(@"c:\aoc/1.txt").ToArray();
var monkeys = new List<Monkey>();

for (int i = 0; i < input.Length; i+=7)
{
    var monkey = new Monkey();
    monkey.Id = int.Parse(Regex.Match(input[i], @"\d+").Value);
    
    monkey.Items.AddRange(Regex.Split(input[i + 1], @"\D+").Where(item => !string.IsNullOrEmpty(item))
        .Select(item => new Item { WorryLevel = int.Parse(item) }));

    monkey.Operation = input[i + 2].Substring(input[i + 2].LastIndexOf('=') + 2);
    monkey.Divider = int.Parse(Regex.Match(input[i + 3], @"\d+").Value);
    monkey.TestTrueMonkeyNumber = int.Parse(Regex.Match(input[i + 4], @"\d+").Value);
    monkey.TestFalseMonkeyNumber = int.Parse(Regex.Match(input[i + 5], @"\d+").Value);
    monkeys.Add(monkey);
}

const int rounds = 20;
for (int round = 0; round < rounds; round++)
{
    foreach (var monkey in monkeys)
    {
        foreach (var item in monkey.Items)
        {
            monkey.InspectionsCount++;
            item.WorryLevel = CalcNewItemWorryLevel(item.WorryLevel, monkey.Operation);
            item.WorryLevel /= 3;
            var giveToMonkeyId = item.WorryLevel % monkey.Divider == 0 ? monkey.TestTrueMonkeyNumber : monkey.TestFalseMonkeyNumber;
            monkeys.Single(m => m.Id == giveToMonkeyId).Items.Add(item);
        }

        monkey.Items = new List<Item>();
    }
}

monkeys.Sort((first, second) => second.InspectionsCount.CompareTo(first.InspectionsCount));
int monkeyBusiness = monkeys[0].InspectionsCount * monkeys[1].InspectionsCount;

Console.WriteLine(monkeyBusiness);
Console.ReadLine();

int CalcNewItemWorryLevel(int oldWorryLevel, string operation)
{
    var interpreter = new Interpreter();
    var parsedExpression = interpreter.Parse(operation, new Parameter("old", typeof(int)));
    return (int)parsedExpression.Invoke(oldWorryLevel);
}