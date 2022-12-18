var moves = ReadInput();
var figures = GetFigures();
const int blocksCount = 2022;
var map = new char[7, 10_000];
int moveIndex = 0;
int figureIndex = 0;

for (int i = 0; i < blocksCount; i++)
{
    var figure = figures[figureIndex];
    var figureX = 2;
    var figureY = GetTowerHeight(map) + 3;

    // move figure down
    while (true)
    {
        var move = moves[moveIndex];

        if (moveIndex == moves.Length - 1)
        {
            moveIndex = 0;
        }
        else
        {
            moveIndex++;
        }

        if (move == '<')
        {
            if (TryMoveLeft(map, figure, figureX, figureY))
            {
                figureX--;
            }
        }

        if (move == '>')
        {
            if (TryMoveRight(map, figure, figureX, figureY))
            {
                figureX++;
            }
        }

        if (TryMoveDown(map, figure, figureX, figureY))
        {
            figureY--;
        }
        else
        {
            PutFigureOnMap(map, figure, figureX, figureY);
            break;
        }
    }

    if (figureIndex == figures.Count - 1)
    {
        figureIndex = 0;
    }
    else
    {
        figureIndex++;
    }
}

int towerHeight = GetTowerHeight(map);

Console.WriteLine(towerHeight);
Console.ReadLine();

void PrintMap(char[,] map)
{
    for (int y = GetTowerHeight(map) + 2; y >= 0; y--)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            if (map[x,y] == '#')
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }

        }

        Console.WriteLine();
    }
}

bool TryMoveLeft(char[,] map, char[,] figure, int figureX, int figureY)
{
    for (int y = 0; y < figure.GetLength(1); y++)
    {
        for (int x = 0; x < figure.GetLength(0); x++)
        {
            if (figure[x, y] == '#')
            {
                var stoneX = x + figureX;
                var stoneY = y + figureY;

                if (stoneX == 0 || map[stoneX - 1, stoneY] == '#')
                {
                    return false;
                }
            }
        }
    }

    return true;
}

bool TryMoveRight(char[,] map, char[,] figure, int figureX, int figureY)
{
    for (int y = 0; y < figure.GetLength(1); y++)
    {
        for (int x = figure.GetLength(0) - 1; x >= 0; x--)
        {
            if (figure[x, y] == '#')
            {
                var stoneX = x + figureX;
                var stoneY = y + figureY;

                if (stoneX == map.GetLength(0) - 1 || map[stoneX + 1, stoneY] == '#')
                {
                    return false;
                }
            }
        }
    }

    return true;
}

bool TryMoveDown(char[,] map, char[,] figure, int figureX, int figureY)
{
    for (int x = 0; x < figure.GetLength(0); x++)
    {
        for (int y = figure.GetLength(1) - 1; y >= 0; y--)
        {
            if (figure[x, y] == '#')
            {
                var stoneX = x + figureX;
                var stoneY = y + figureY;

                if (stoneY == 0 || map[stoneX, stoneY - 1] == '#')
                {
                    return false;
                }
            }
        }
    }

    return true;
}

void PutFigureOnMap(char[,] map, char[,] figure, int figureX, int figureY)
{
    for (int x = 0; x < figure.GetLength(0); x++)
    {
        for (int y = 0; y < figure.GetLength(1); y++)
        {
            if (figure[x, y] == '#')
            {
                map[figureX + x, figureY + y] = '#';
            }
        }
    }
}

char[] ReadInput()
{
    var input = File.ReadAllText(@"c:\aoc/1.txt").ToCharArray();
    return input;
}

int GetTowerHeight(char[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        var lineIsEmpty = true;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            if (map[x,y] == '#')
            {
                lineIsEmpty = false;
            }
        }

        if (lineIsEmpty)
        {
            return y;
        }
    }

    return 0;
}

List<char[,]> GetFigures()
{
    var figures = new List<char[,]>
    {
        new[,]
        {
            {'#'},
            {'#'},
            {'#'},
            {'#'}
        },
        new[,]
        {
            {'.', '#', '.'},
            {'#', '#', '#'},
            {'.', '#', '.'}
        },
        new[,]
        {
            {'#', '.', '.'},
            {'#', '.', '.'},
            {'#', '#', '#'}
        },
        new[,]
        {
            {'#', '#', '#', '#'}
        },
        new[,]
        {
            {'#', '#'},
            {'#', '#'}
        }
    };

    return figures;
}