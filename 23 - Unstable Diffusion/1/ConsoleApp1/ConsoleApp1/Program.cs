using ConsoleApp1;

const int roundsNumber = 10;
var elves = ReadInput();
var directions = new List<char> { 'N', 'S', 'W', 'E' };

for (int i = 0; i < roundsNumber; i++)
{
    ConsiderNewPosition();
    Move();
    UpdateDirections();
}

int emptyGroundCount =
    (elves.Max(e => e.PositionY) - elves.Min(e => e.PositionY) + 1) *
    (elves.Max(e => e.PositionX) - elves.Min(e => e.PositionX) + 1) - elves.Count;

Console.WriteLine(emptyGroundCount);

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

void Move()
{
    foreach (var elf in elves)
    {
        if (elves.Count(e => e.ProposedPositionX == elf.ProposedPositionX && e.ProposedPositionY == elf.ProposedPositionY) == 1)
        {
            elf.PositionX = elf.ProposedPositionX;
            elf.PositionY = elf.ProposedPositionY;
        }
    }

    foreach (var elf in elves)
    {
        elf.ProposedPositionX = elf.PositionX;
        elf.ProposedPositionY = elf.PositionY;
    }
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