var max = 0;
var sum = 0;

foreach (var line in File.ReadLines(@"c:\aoc/1.txt"))
{
    if (string.IsNullOrEmpty(line))
    {
        if (sum > max)
            max = sum;
        
        sum = 0;
    }
    else
    {
        sum += int.Parse(line);
    }
}

Console.WriteLine(max);
Console.ReadLine();