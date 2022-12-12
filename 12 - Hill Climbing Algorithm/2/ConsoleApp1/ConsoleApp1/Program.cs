using ConsoleApp1;

var input = File.ReadAllLines(@"c:\aoc/1.txt");
var map = new Ceil[input.Length, input.First().Length];
var startCells = new List<Ceil>();

// Read map
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        var name = input[i][j];

        var ceil = new Ceil();
        ceil.Name = name;
        ceil.X = i;
        ceil.Y = j;
        ceil.Height = name switch
        {
            'S' => 0,
            'E' => 25,
            _ => name - 97
        };

        if (name is 'S' or 'a')
        {
            startCells.Add(ceil);
        }

        map[i,j] = ceil;
    }
}

// Process
int shortestRouteLength = int.MaxValue;

foreach (var startCeil in startCells)
{
    var routeLength = 0;
    var checkQueue = new Queue<Ceil>();
    checkQueue.Enqueue(startCeil);

    while (checkQueue.Any())
    {
        var ceil = checkQueue.Dequeue();
        if (ceil.Name == 'E')
        {
            routeLength = ceil.VisitStep;
            break;
        }

        if (ceil.X > 0)
        {
            var upCeil = map[ceil.X - 1, ceil.Y];
            ProcessCeil(upCeil, ceil, checkQueue);
        }

        if (ceil.X < map.GetLength(0) - 1)
        {
            var downCeil = map[ceil.X + 1, ceil.Y];
            ProcessCeil(downCeil, ceil, checkQueue);
        }

        if (ceil.Y > 0)
        {
            var leftCeil = map[ceil.X, ceil.Y - 1];
            ProcessCeil(leftCeil, ceil, checkQueue);
        }

        if (ceil.Y < map.GetLength(1) - 1)
        {
            var rightCeil = map[ceil.X, ceil.Y + 1];
            ProcessCeil(rightCeil, ceil, checkQueue);
        }
    }

    if (routeLength < shortestRouteLength && routeLength > 0)
    {
        shortestRouteLength = routeLength;
    }

    CleanMap(map);
}

Console.WriteLine(shortestRouteLength);
Console.ReadLine();

void ProcessCeil(Ceil processing, Ceil current, Queue<Ceil> checkQueue)
{
    if (!processing.Visited && processing.Height - current.Height <= 1)
    {
        processing.VisitStep = current.VisitStep + 1;
        processing.Visited = true;
        checkQueue.Enqueue(processing);
    }
}

void CleanMap(Ceil[,] map)
{
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            map[i, j].Visited = false;
            map[i, j].VisitStep = 0;
        }
    }
}