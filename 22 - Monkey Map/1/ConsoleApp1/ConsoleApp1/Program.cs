var map = ReadMap();
var moves = ReadMoves();
int positionY = 0;
int positionX = map[0].FindIndex(item => item == '.');
var direction = 'R';

foreach (var move in moves)
{
    if (move is "R" or "L")
    {
        direction = GetNewDirection(direction, move[0]);
        continue;
    }

    var movesCount = int.Parse(move);

    for (int i = 0; i < movesCount; i++)
    {
        Move(direction);
    }
    
}

var password = (positionY + 1) * 1000 + (positionX + 1) * 4 + DirectionToNumber(direction);
Console.WriteLine(password);

void Move(char direction)
{
    switch (direction)
    {
        case 'L':
            if (positionX == 0 || map[positionY][positionX - 1] == ' ')
            {
                var index = map[positionY].FindLastIndex(i => i != ' ');

                if (map[positionY][index] == '#')
                {
                    return;
                }

                positionX = index;
                return;
            }

            if (map[positionY][positionX-1] == '#')
            {
                return;
            }

            positionX--;
            break;
        case 'U':
            if (positionY == 0 || map[positionY - 1][positionX] == ' ')
            {
                var index = 0;

                for (int i = map.Count - 1; i >= 0; i--)
                {
                    if (map[i].Count - 1 < positionX)
                    {
                        continue;
                    }

                    if (map[i][positionX] is '#' or '.')
                    {
                        index = i;
                        break;
                    }
                }

                if (map[index][positionX] == '#')
                {
                    return;
                }

                positionY = index;
                return;
            }

            if (map[positionY-1][positionX] == '#')
            {
                return;
            }

            positionY--;
            break;
        case 'R':
            if (positionX == map[positionY].Count - 1 || map[positionY][positionX + 1] == ' ')
            {
                var index = map[positionY].FindIndex(i => i != ' ');

                if (map[positionY][index] == '#')
                {
                    return;
                }

                positionX = index;
                return;
            }

            if (map[positionY][positionX + 1] == '#')
            {
                return;
            }

            positionX++;

            break;
        case 'D':
            if (positionY == map.Count - 1 || map[positionY + 1].Count < positionX + 1 ||  map[positionY + 1][positionX] == ' ')
            {
                var index = 0;

                for (int i = 0; i < map.Count; i++)
                {
                    if (map[i].Count - 1 < positionX)
                    {
                        continue;
                    }

                    if (map[i][positionX] is '#' or '.')
                    {
                        index = i;
                        break;
                    }
                }

                if (map[index][positionX] == '#')
                {
                    return;
                }

                positionY = index;
                return;
            }

            if (map[positionY + 1][positionX] == '#')
            {
                return;
            }

            positionY++;
            break;
    }
}

int DirectionToNumber(char position)
{
    return position switch
    {
        'R' => 0,
        'D' => 1,
        'L' => 2,
        'U' => 3
    };
}

char GetNewDirection(char currentDirection, char turn)
{
    var positions = new List<char>{'L', 'U', 'R', 'D'};

    if (turn == 'R')
    {
        return positions[(positions.IndexOf(currentDirection) + 1) % positions.Count];
    }
    else
    {
        var index = positions.IndexOf(currentDirection) - 1;
        if (index < 0)
        {
            index = 3;
        }

        return positions[index];
    }
}

List<List<char>> ReadMap()
{
    return File.ReadAllLines(@"c:\aoc/1.txt").Select(line => line.ToList()).ToList();
}

List<string> ReadMoves()
{
    var moves = new List<string>();
    var movesStr = File.ReadAllText(@"c:\aoc/moves.txt");

    var move = string.Empty;
    foreach (var symbol in movesStr)
    {
        if (symbol is 'L' or 'R')
        {
            moves.Add(move);
            move = string.Empty;
            moves.Add(symbol.ToString());
        }
        else
        {
            move += symbol;
        }
    }

    if (!string.IsNullOrEmpty(move))
    {
        moves.Add(move);
    }

    return moves;
}