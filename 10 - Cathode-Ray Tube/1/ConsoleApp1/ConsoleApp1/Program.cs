var cyclesToCount = new List<int> { 20, 60, 100, 140, 180, 220 };
int xRegister = 1;
int totalStrengths = 0;
int cycle = 0;

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    if (cyclesToCount.Contains(cycle))
    {
        totalStrengths += xRegister * cycle;
    }

    switch (line)
    {
        case "noop":
            cycle++;
            break;
        case { } s when s.StartsWith("addx"):
            cycle++;
            int value = int.Parse(line.Split(' ').Last());
            
            if (cyclesToCount.Contains(cycle))
            {
                totalStrengths += xRegister * cycle;
            }

            xRegister += value;
            cycle++;

            break;
    }
}

Console.WriteLine(totalStrengths);
Console.ReadLine();