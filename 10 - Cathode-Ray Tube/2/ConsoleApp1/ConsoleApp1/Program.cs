int xRegister = 1;
const int width = 39;
int currentWidth = 0;

void WritePixel(ref int currentWidth, int registerValue)
{
    Console.Write(Math.Abs(currentWidth - registerValue) <= 1 ? '#' : '.');

    if (currentWidth == width)
    {
        currentWidth = 0;
        Console.WriteLine();
    }
    else
    {
        currentWidth++;
    }
}

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    WritePixel(ref currentWidth, xRegister);

    switch (line)
    {
        case "noop":
            break;
        case { } s when s.StartsWith("addx"):
            WritePixel(ref currentWidth, xRegister);
            int value = int.Parse(line.Split(' ').Last());
            xRegister += value;
            break;
    }
}

Console.ReadLine();