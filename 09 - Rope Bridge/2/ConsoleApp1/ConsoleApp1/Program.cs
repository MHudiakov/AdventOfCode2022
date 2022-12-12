using ConsoleApp1;

var map = new bool[10000, 10000];
const int initPosition = 5000;
var knots = Enumerable.Range(1, 10).Select(_ => new Position(initPosition, initPosition)).ToList();
map[initPosition, initPosition] = true;

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    var input = line.Split(" ");
    var command = input[0];
    var steps = int.Parse(input[1]);

    var commandsDictionary = new Dictionary<string, (int x, int y)>
    {
        { "R", (1, 0) }, { "L", (-1, 0) }, { "U", (0, 1) }, { "D", (0, -1) }
    };

    for (int i = 0; i < steps; i++)
    {
        knots[0].X += commandsDictionary[command].x;
        knots[0].Y += commandsDictionary[command].y;
        
        for (int knotIndex = 1; knotIndex < knots.Count; knotIndex++)
        {
            UpdateTailPosition(knots[knotIndex-1], knots[knotIndex]);
        }        
        
        map[knots[^1].X, knots[^1].Y] = true;
    }
}

int visitedPositionsCount = map.Cast<bool>().Count(p => p);

Console.WriteLine(visitedPositionsCount);
Console.ReadLine();

void UpdateTailPosition(Position h, Position t)
{
    var x = (h.X - t.X) / -2;
    var y = (h.Y - t.Y) / -2;
    
    if (x != 0 || y != 0)
    {
        t.X = h.X + x;
        t.Y = h.Y + y;
    }
}