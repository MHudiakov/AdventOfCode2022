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
        Move();
    }
}

var password = (positionY + 1) * 1000 + (positionX + 1) * 4 + DirectionToNumber(direction);
Console.WriteLine(password);

void Move()
{
    switch (direction)
    {
        case 'L':
            if (positionX == 0 || map[positionY][positionX - 1] == ' ')
            {
                int indexX = 0;
                int indexY = 0;
                char newDirection = direction;

                if (positionY is >= 0 and < 50)
                {
                    indexX = 0;
                    indexY = 149 - positionY;
                    newDirection = 'R';
                }

                if (positionY is >= 50 and < 100)
                {
                    indexX = positionY - 50;
                    indexY = 100;
                    newDirection = 'D';
                }

                if (positionY is >= 100 and < 150)
                {
                    indexX = 50;
                    indexY = 149 - positionY;
                    newDirection = 'R';
                }

                if (positionY is >= 150 and < 200)
                {
                    indexX = 50 + positionY - 150;
                    indexY = 0;
                    newDirection = 'D';
                }

                if (map[indexY][indexX] == '#')
                {
                    return;
                }

                positionX = indexX;
                positionY = indexY;
                direction = newDirection;
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
                int indexX = 0;
                int indexY = 0;
                char newDirection = direction;

                if (positionX is >= 0 and < 50)
                {
                    indexX = 50;
                    indexY = 50 + positionX;
                    newDirection = 'R';
                }

                if (positionX is >= 50 and < 100)
                {
                    indexX = 0;
                    indexY = 150 + positionX - 50;
                    newDirection = 'R';
                }

                if (positionX is >= 100 and < 150)
                {
                    indexX = positionX - 100;
                    indexY = 199;
                    newDirection = 'U';
                }

                if (map[indexY][indexX] == '#')
                {
                    return;
                }

                positionX = indexX;
                positionY = indexY;
                direction = newDirection;
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
                int indexX = 0;
                int indexY = 0;
                char newDirection = direction;

                if (positionY is >= 0 and < 50)
                {
                    indexX = 99;
                    indexY = 149 - positionY;
                    newDirection = 'L';
                }

                if (positionY is >= 50 and < 100)
                {
                    indexX = 100 + positionY - 50;
                    indexY = 49;
                    newDirection = 'U';
                }

                if (positionY is >= 100 and < 150)
                {
                    indexX = 149;
                    indexY = 149 - positionY;
                    newDirection = 'L';
                }

                if (positionY is >= 150 and < 200)
                {
                    indexX = 50 + positionY - 150;
                    indexY = 149;
                    newDirection = 'U';
                }

                if (map[indexY][indexX] == '#')
                {
                    return;
                }

                positionX = indexX;
                positionY = indexY;
                direction = newDirection;
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
                int indexX = 0;
                int indexY = 0;
                char newDirection = direction;

                if (positionX is >= 0 and < 50)
                {
                    indexX = 100 + positionX;
                    indexY = 0;
                    newDirection = 'D';
                }

                if (positionX is >= 50 and < 100)
                {
                    indexX = 49;
                    indexY = 150 + positionX - 50;
                    newDirection = 'L';
                }

                if (positionX is >= 100 and < 150)
                {
                    indexX = 99;
                    indexY = 50 + positionX - 100;
                    newDirection = 'L';
                }

                if (map[indexY][indexX] == '#')
                {
                    return;
                }

                positionX = indexX;
                positionY = indexY;
                direction = newDirection;
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