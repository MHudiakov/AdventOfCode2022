using ConsoleApp1;

var map = new bool[10000, 10000];
var h = new Position (5000, 5000);
var t = new Position(5000, 5000);
map[t.X, t.Y] = true;

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    var input = line.Split(" ");
    var command = input[0];
    var steps = int.Parse(input[1]);

    int x = 0;
    int y = 0;

    switch (command)
    {
        case "R":
            x = 1;
            y = 0;
            break;
        case "L":
            x = -1;
            y = 0;
            break;
        case "U":
            x = 0;
            y = 1;
            break;
        case "D":
            x = 0;
            y = -1;
            break;
    }


    for (int i = 0; i < steps; i++)
    {
        h = new Position(h.X + x, h.Y + y);
        t = CalcNewTailPosition(h, t);
        map[t.X, t.Y] = true;
    }
}

int visitedPositionsCount = map.Cast<bool>().Count(position => position);

Console.WriteLine(visitedPositionsCount);
Console.ReadLine();


Position CalcNewTailPosition(Position h, Position t)
{
    return h switch
    {
        { } k when k.X - t.X == 2 && k.Y - t.Y == 0 => new Position(k.X - 1, k.Y),
        { } k when k.X - t.X == -2 && k.Y - t.Y == 0 => new Position(k.X + 1, k.Y),
        { } k when k.X - t.X == 0 && k.Y - t.Y == 2 => new Position(k.X, k.Y - 1),
        { } k when k.X - t.X == 0 && k.Y - t.Y == -2 => new Position(k.X, k.Y + 1),

        { } k when k.X - t.X == 1 && k.Y - t.Y == 2 => new Position(k.X, k.Y-1),
        { } k when k.X - t.X == -1 && k.Y - t.Y == 2 => new Position(k.X, k.Y - 1),
        { } k when k.X - t.X == 1 && k.Y - t.Y == -2 => new Position(k.X, k.Y + 1),
        { } k when k.X - t.X == -1 && k.Y - t.Y == -2 => new Position(k.X, k.Y + 1),

        { } k when k.X - t.X == 2 && k.Y - t.Y == 1 => new Position(k.X-1, k.Y),
        { } k when k.X - t.X == 2 && k.Y - t.Y == -1 => new Position(k.X-1, k.Y),
        { } k when k.X - t.X == -2 && k.Y - t.Y == 1 => new Position(k.X+1, k.Y),
        { } k when k.X - t.X == -2 && k.Y - t.Y == -1 => new Position(k.X+1, k.Y),

        _ => t
    };
}