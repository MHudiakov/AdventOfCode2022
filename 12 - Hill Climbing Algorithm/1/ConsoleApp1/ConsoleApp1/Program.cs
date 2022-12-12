using ConsoleApp1;

var input = File.ReadAllLines(@"c:\aoc/1.txt");
var map = new Ceil[input.Length, input.First().Length];
var checkQueue = new Queue<Ceil>();

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

        if (name == 'S')
        {
            checkQueue.Enqueue(ceil);
        }

        map[i,j] = ceil;
    }
}

// Process
int result = 0;
while (checkQueue.Any())
{
    var ceil = checkQueue.Dequeue();
    if (ceil.Name == 'E')
    {
        result = ceil.VisitStep;
        break;
    }

    if (ceil.Visited)
    {
        continue;
    }

    ceil.Visited = true;

    if (ceil.X > 0)
    {
        var upCeil = map[ceil.X - 1, ceil.Y];
        ProcessCeil(upCeil, ceil);
    }

    if (ceil.X < map.GetLength(0) - 1)
    {
        var downCeil = map[ceil.X + 1, ceil.Y];
        ProcessCeil(downCeil, ceil);
    }

    if (ceil.Y > 0)
    {
        var leftCeil = map[ceil.X, ceil.Y - 1];
        ProcessCeil(leftCeil, ceil);
    }

    if (ceil.Y < map.GetLength(1) - 1)
    {
        var rightCeil = map[ceil.X, ceil.Y + 1];
        ProcessCeil(rightCeil, ceil);
    }
}

Console.WriteLine(result);
Console.ReadLine();

void ProcessCeil(Ceil processed, Ceil current)
{
    if (!processed.Visited && processed.Height - current.Height <= 1)
    {
        processed.VisitStep = current.VisitStep + 1;
        checkQueue.Enqueue(processed);
    }
}