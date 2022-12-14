using ConsoleApp1;

var map = new State[1000, 1000];
const int startPoint = 500;

FillMap();

int totalUnits = 0;
while (true)
{
    if (ThrowSand())
    {
        totalUnits++;
    }
    else
    {
        break;
    }
}

Console.WriteLine(totalUnits);
Console.ReadLine();

bool ThrowSand()
{
    var x = startPoint;
    var y = 0;

    while (true)
    {
        if (map[x, y] == State.Sand)
        {
            return false;
        }

        switch (map[x, y+1])
        {
            case State.Air:
                y++;
                continue;
            case State.Rock:
            case State.Sand:
            {
                if (map[x - 1, y + 1] == State.Air)
                {
                    x--;
                    y++;
                    continue;
                }

                if (map[x + 1, y + 1] == State.Air)
                {
                    x++;
                    y++;
                    continue;
                }

                map[x, y] = State.Sand;
                return true;
            }
        }
    }
}

void FillMap()
{
    int maxY = 0;

    foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
    {
        var points = line.Split(" -> ");
        for (int i = 1; i < points.Length; i++)
        {
            var pointPrevious = points[i - 1].Split(',');
            var xPrevious = int.Parse(pointPrevious.First());
            var yPrevious = int.Parse(pointPrevious.Last());

            var pointCurrent = points[i].Split(',');
            var xCurrent = int.Parse(pointCurrent.First());
            var yCurrent = int.Parse(pointCurrent.Last());

            if (yCurrent > maxY)
            {
                maxY = yCurrent;
            }

            if (xPrevious == xCurrent)
            {
                for (int y = Math.Min(yPrevious, yCurrent); y <= Math.Max(yPrevious, yCurrent); y++)
                {
                    map[xPrevious, y] = State.Rock;
                }

                continue;
            }

            if (yPrevious == yCurrent)
            {
                for (int x = Math.Min(xPrevious, xCurrent); x <= Math.Max(xPrevious, xCurrent); x++)
                {
                    map[x, yPrevious] = State.Rock;
                }
            }
        }
    }

    int floorY = maxY + 2;

    for (int x = 0; x < map.GetLength(0); x++)
    {
        map[x, floorY] = State.Rock;
    }
}