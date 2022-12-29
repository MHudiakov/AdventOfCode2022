var moves = ReadInput();
var figures = GetFigures();
const long blocksCount = 1000000000000;
var map = new char[7, 1_000_000];
int moveIndex = 0;
int figureIndex = 0;
var figureNumber = 0;
var heightsDictionary = new Dictionary<int, int>();
const int minPatternHeight = 2700;

while(true)
{
    figureNumber++;
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

    var height = GetTowerHeight(map);
    heightsDictionary[figureNumber] = height;

    // Looking for a pattern
    if (height > minPatternHeight * 2)
    {
        var patternStartHeight = TryFindPattern(map);

        if (patternStartHeight.HasValue)
        {
            var patternHeight = height - patternStartHeight.Value - 1;
            var patternStartIndex = heightsDictionary.Values.ToList().IndexOf(patternStartHeight.Value - patternHeight);
            var patternFiguresNumber = (figureNumber - patternStartIndex) / 2;
            long numberOfPatternRepeats = (blocksCount - patternStartIndex) / patternFiguresNumber;
            long diff = blocksCount - patternStartIndex - numberOfPatternRepeats * patternFiguresNumber;

            var totalLength = numberOfPatternRepeats * patternHeight +
                              heightsDictionary[patternStartIndex + (int)diff];

            Console.WriteLine(totalLength);
            break;
        }
    }
}

int? TryFindPattern(char[,] map)
{
    var height = GetTowerHeight(map);

    for (int y = height - 1 - minPatternHeight; y >= minPatternHeight; y--)
    {
        bool patternFound = true;
        for (int i = 0; i < minPatternHeight; i++)
        {
            var l1Index = height - 1 - i;
            var l2Index = y - i;

            if (!CompareLines(l1Index, l2Index, map))
            {
                patternFound = false;
                break;
            }
        }

        if (patternFound)
        {
            return y;
        }
    }

    return null;
}

bool CompareLines(int y1, int y2, char[,] map)
{
    var line1 = Enumerable.Range(0, map.GetLength(0))
        .Select(x => map[x, y1])
        .ToArray();

    var line2 = Enumerable.Range(0, map.GetLength(0))
        .Select(x => map[x, y2])
        .ToArray();

    for (int i = 0; i < line1.Length; i++)
    {
        if (line1[i] != line2[i])
        {
            return false;
        }
    }

    return true;
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
    return File.ReadAllText(@"c:\aoc/1.txt").ToCharArray();
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