using ConsoleApp1;

var input = File.ReadAllLines(@"c:\aoc/1.txt");
var map = new Cell[input.Length, input.First().Length];
var checkQueue = new Queue<Cell>();

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

        if (name == 'S')
        {
            checkQueue.Enqueue(cell);
        }

        map[i,j] = cell;
    }
}

// Process
int result = 0;
while (checkQueue.Any())
{
    var cell = checkQueue.Dequeue();
    if (cell.Name == 'E')
    {
        result = cell.VisitStep;
        break;
    }

    if (cell.Visited)
    {
        continue;
    }

    cell.Visited = true;

    if (cell.X > 0)
    {
        var upCell = map[cell.X - 1, cell.Y];
        ProcessCell(upCell, cell);
    }

    if (cell.X < map.GetLength(0) - 1)
    {
        var downCell = map[cell.X + 1, cell.Y];
        ProcessCell(downCell, cell);
    }

    if (cell.Y > 0)
    {
        var leftCell = map[cell.X, cell.Y - 1];
        ProcessCell(leftCell, cell);
    }

    if (cell.Y < map.GetLength(1) - 1)
    {
        var rightCell = map[cell.X, cell.Y + 1];
        ProcessCell(rightCell, cell);
    }
}

Console.WriteLine(result);
Console.ReadLine();

void ProcessCell(Cell processed, Cell current)
{
    if (!processed.Visited && processed.Height - current.Height <= 1)
    {
        processed.VisitStep = current.VisitStep + 1;
        checkQueue.Enqueue(processed);
    }
}