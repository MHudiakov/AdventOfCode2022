using ConsoleApp1;

var map = ReadMap();
var blizzards = GetBlizzards(map);

var startPositionX = map[0].IndexOf('.');
var startPositionY = 0;
var targetPositionX = map[^1].IndexOf('.');
var targetPositionY = map.Count - 1;

var mapWidth = map.First().Count;
var mapHeight = map.Count;

var sum = MakeTrip(true) + MakeTrip(false) + MakeTrip(true);

Console.WriteLine(sum);

int MakeTrip(bool forward)
{
    var minutes = 0;
    var states = new List<(int x, int y)> { forward ? (startPositionX, startPositionY) : (targetPositionX, targetPositionY) };

    while (true)
    {
        MoveBlizzards();
        minutes++;
        states = CalcNewStates(states);

        if (forward && states.Contains((targetPositionX, targetPositionY)) ||
            !forward && states.Contains((startPositionX, startPositionY)))
        {
            return minutes;
        }
    }
}

List<(int x, int y)> CalcNewStates(List<(int x, int y)> previousStates)
{
    var newStates = new List<(int x, int y)>();

    foreach (var state in previousStates)
    {
        if (!blizzards.Any(b => b.PositionX == state.x && b.PositionY == state.y) &&
            !newStates.Contains((state.x, state.y)))
        {
            newStates.Add((state.x, state.y));
        }

        if (state.x > 0 && 
            map[state.y][state.x - 1] != '#' && 
            !blizzards.Any(b => b.PositionX == state.x - 1 && b.PositionY == state.y) &&
            !newStates.Contains((state.x - 1, state.y)))
        {
            newStates.Add((state.x - 1, state.y));
        }

        if (state.x < mapWidth - 1 &&
            map[state.y][state.x + 1] != '#' &&
            !blizzards.Any(b => b.PositionX == state.x + 1 && b.PositionY == state.y) &&
            !newStates.Contains((state.x + 1, state.y)))
        {
            newStates.Add((state.x + 1, state.y));
        }

        if (state.y > 0 &&
            map[state.y - 1][state.x] != '#' &&
            !blizzards.Any(b => b.PositionX == state.x && b.PositionY == state.y - 1) &&
            !newStates.Contains((state.x, state.y - 1)))
        {
            newStates.Add((state.x, state.y - 1));
        }

        if (state.y < mapHeight - 1 &&
            map[state.y + 1][state.x] != '#' &&
            !blizzards.Any(b => b.PositionX == state.x && b.PositionY == state.y + 1) &&
            !newStates.Contains((state.x, state.y + 1)))
        {
            newStates.Add((state.x, state.y + 1));
        }
    }

    return newStates;
}

void MoveBlizzards()
{
    foreach (var blizzard in blizzards)
    {
        switch (blizzard.Direction)
        {
            case '<':
                blizzard.PositionX = blizzard.PositionX == 1 ? mapWidth - 2 : blizzard.PositionX - 1;
                break;
            case '>':
                blizzard.PositionX = blizzard.PositionX == mapWidth - 2 ? 1 : blizzard.PositionX + 1;
                break;
            case '^':
                blizzard.PositionY = blizzard.PositionY == 1 ? mapHeight - 2 : blizzard.PositionY - 1;
                break;
            case 'v':
                blizzard.PositionY = blizzard.PositionY == mapHeight - 2 ? 1 : blizzard.PositionY + 1;
                break;
        }
    }
}

List<Blizzard> GetBlizzards(List<List<char>> map)
{
    var blizzards = new List<Blizzard>();
    for (var i = 0; i < map.Count; i++)
    {
        var mapLine = map[i];
        for (var j = 0; j < mapLine.Count; j++)
        {
            var symbol = mapLine[j];

            if (symbol != '.' && symbol != '#')
            {
                blizzards.Add(new Blizzard
                {
                    Direction = symbol,
                    PositionX = j,
                    PositionY = i
                });
            }
        }
    }

    return blizzards;
}

List<List<char>> ReadMap()
{
    return File.ReadAllLines(@"c:\aoc/1.txt").Select(line => line.ToList()).ToList();
}