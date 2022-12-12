int overlaps = 0;

foreach (var line in File.ReadLines(@"c:\aoc/1.txt"))
{
    var ranges = line.Split(',', '-').Select(int.Parse).ToArray();
    
    if (ranges[2] >= ranges[0] && ranges[3] <= ranges[1] || ranges[0] >= ranges[2] && ranges[1] <= ranges[3])
        overlaps++;
}

Console.WriteLine(overlaps);
Console.ReadLine();