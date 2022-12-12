var d = new Dictionary<string, int> { { "A X", 3 }, { "A Y", 4 }, { "A Z", 8 }, { "B X", 1 }, { "B Y", 5 }, { "B Z", 9 }, { "C X", 2 }, { "C Y", 6 }, { "C Z", 7 } };

var sum = File.ReadLines(@"c:\aoc/1.txt").Sum(line => d[line]);

Console.WriteLine(sum);
Console.ReadLine();