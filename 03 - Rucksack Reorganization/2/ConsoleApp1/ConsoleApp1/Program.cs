var group = new List<string>();
var groupLength = 0;
var sum = 0;

foreach (var line in File.ReadLines(@"c:\aoc/1.txt"))
{
    group.Add(line);
    groupLength++;

    if (groupLength % 3 == 0)
    {
        var repeatedSymbol = group[0].Intersect(group[1]).Intersect(group[2]).First();
        sum += repeatedSymbol - (char.IsUpper(repeatedSymbol) ? 38 : 96);

        groupLength = 0;
        group = new List<string>();
    }
}

Console.WriteLine(sum);
Console.ReadLine();