using ConsoleApp1;

var input = File.ReadAllLines(@"c:\aoc/1.txt");
var map = new Cell[input.Length, input.First().Length];
var startCells = new List<Cell>();

// Read map
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        var name = input[i][j];

        var cell = new Cell();
        cell.Name = name;
        cell.X = i;
        cell.Y = j;
        cell.Height = name switch
        {
            'S' => 0,
            'E' => 25,
            _ => name - 97
        };

        if (name is 'S' or 'a')
        {
            startCells.Add(cell);
        }

        map[i,j] = cell;
    }
}

// Process
int shortestRouteLength = int.MaxValue;

foreach (var startCell in startCells)
{
    var routeLength = 0;
    var checkQueue = new Queue<Cell>();
    checkQueue.Enqueue(startCell);

    while (checkQueue.Any())
    {
        var cell = checkQueue.Dequeue();
        if (cell.Name == 'E')
        {
            routeLength = cell.VisitStep;
            break;
        }

        if (cell.X > 0)
        {
            var upCell = map[cell.X - 1, cell.Y];
            ProcessCell(upCell, cell, checkQueue);
        }

        if (cell.X < map.GetLength(0) - 1)
        {
            var downCell = map[cell.X + 1, cell.Y];
            ProcessCell(downCell, cell, checkQueue);
        }

        if (cell.Y > 0)
        {
            var leftCell = map[cell.X, cell.Y - 1];
            ProcessCell(leftCell, cell, checkQueue);
        }

        if (cell.Y < map.GetLength(1) - 1)
        {
            var rightCell = map[cell.X, cell.Y + 1];
            ProcessCell(rightCell, cell, checkQueue);
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

void ProcessCell(Cell processing, Cell current, Queue<Cell> checkQueue)
{
    if (!processing.Visited && processing.Height - current.Height <= 1)
    {
        processing.VisitStep = current.VisitStep + 1;
        processing.Visited = true;
        checkQueue.Enqueue(processing);
    }
}

void CleanMap(Cell[,] map)
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