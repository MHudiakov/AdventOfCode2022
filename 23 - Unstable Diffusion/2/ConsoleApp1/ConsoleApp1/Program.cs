using ConsoleApp1;

int roundsNumber = 0;
var elves = ReadInput();
var directions = new List<char> { 'N', 'S', 'W', 'E' };

while (true)
{
    ConsiderNewPosition();
    var positionUpdated = Move();
    UpdateDirections();

    if (positionUpdated)
    {
        roundsNumber++;
    }
    else
    {
        break;
    }
}

Console.WriteLine(roundsNumber + 1);

void UpdateDirections()
{
    char first = directions[0];
    directions.RemoveAt(0);
    directions.Add(first);
}

void ConsiderNewPosition()
{
    foreach (var elf in elves)
    {
        if (!elves.Any(e => 
                e.PositionX == elf.PositionX && e.PositionY == elf.PositionY + 1 ||
                e.PositionX == elf.PositionX && e.PositionY == elf.PositionY - 1 ||
                e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY ||
                e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY ||
                e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY + 1 ||
                e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY - 1 ||
                e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY + 1 ||
                e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY - 1))
        {
            elf.ProposedPositionX = elf.PositionX;
            elf.ProposedPositionY = elf.PositionY;
            continue;
        }

        foreach (var direction in directions)
        {
            if (direction == 'N')
            {
                if (!elves.Any(e =>
                        e.PositionX == elf.PositionX && e.PositionY == elf.PositionY - 1 ||
                        e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY - 1 ||
                        e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY - 1))
                {
                    elf.ProposedPositionX = elf.PositionX;
                    elf.ProposedPositionY = elf.PositionY - 1;
                    break;
                }
            }
            
            if (direction == 'S')
            {
                if (!elves.Any(e =>
                        e.PositionX == elf.PositionX && e.PositionY == elf.PositionY + 1 ||
                        e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY + 1 ||
                        e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY + 1))
                {
                    elf.ProposedPositionX = elf.PositionX;
                    elf.ProposedPositionY = elf.PositionY + 1;
                    break;
                }
            }
            
            if (direction == 'W')
            {
                if (!elves.Any(e =>
                        e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY ||
                        e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY + 1 ||
                        e.PositionX == elf.PositionX - 1 && e.PositionY == elf.PositionY - 1))
                {
                    elf.ProposedPositionX = elf.PositionX - 1;
                    elf.ProposedPositionY = elf.PositionY;
                    break;
                }
            }
            
            if (direction == 'E')
            {
                if (!elves.Any(e =>
                        e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY ||
                        e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY + 1 ||
                        e.PositionX == elf.PositionX + 1 && e.PositionY == elf.PositionY - 1))
                {
                    elf.ProposedPositionX = elf.PositionX + 1;
                    elf.ProposedPositionY = elf.PositionY;
                    break;
                }
            }
        }
    }
}

bool Move()
{
    var positionUpdated = false;

    foreach (var elf in elves)
    {
        if (elves.Count(e => e.ProposedPositionX == elf.ProposedPositionX && e.ProposedPositionY == elf.ProposedPositionY) == 1)
        {
            if (elf.PositionX != elf.ProposedPositionX || elf.PositionY != elf.ProposedPositionY)
            {
                positionUpdated = true;
            }

            elf.PositionX = elf.ProposedPositionX;
            elf.PositionY = elf.ProposedPositionY;
        }
    }

    foreach (var elf in elves)
    {
        elf.ProposedPositionX = elf.PositionX;
        elf.ProposedPositionY = elf.PositionY;
    }

    return positionUpdated;
}

List<Elf> ReadInput()
{
    var elves = new List<Elf>();

    var lines = File.ReadAllLines(@"c:\aoc/1.txt").ToList();
    for (var lineIndex = 0; lineIndex < lines.Count; lineIndex++)
    {
        var line = lines[lineIndex];
        for (var symbolIndex = 0; symbolIndex < line.Length; symbolIndex++)
        {
            var symbol = line[symbolIndex];
            if (symbol == '#')
            {
                elves.Add(new Elf
                {
                    PositionY = lineIndex,
                    PositionX = symbolIndex,
                    ProposedPositionX = symbolIndex,
                    ProposedPositionY = lineIndex,
                });
            }
        }
    }

    return elves;
}