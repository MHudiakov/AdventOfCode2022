var cubes = ReadInput();
int area = 0;
const int mapSize = 25;
var map = new int[mapSize, mapSize, mapSize];

foreach (var cube in cubes)
{
    map[cube.x, cube.y, cube.z] = 1;
}

for (int x = 0; x < mapSize; x++)
{
    for (int y = 0; y < mapSize; y++)
    {
        for (int z = 0; z < mapSize; z++)
        {
            if (map[x, y, z] == 1)
            {
                continue;
            }

            CleanMap();
            ProcessCell(x, y, z);

            if (map[mapSize - 1, mapSize - 1, mapSize - 1] != 2)
            {
                cubes.Add((x, y, z));
            }
        }
    }
}

void ProcessCell(int x, int y, int z)
{
    var stack = new Stack<(int x, int y, int z)>();
    map[x, y, z] = 2;
    stack.Push((x ,y, z));

    while (stack.Any())
    {
        var cell = stack.Pop();

        if (cell.x != 0)
        {
            var leftCell = map[cell.x - 1, cell.y, cell.z];

            if (leftCell == 0)
            {
                map[cell.x - 1, cell.y, cell.z] = 2;
                stack.Push((cell.x - 1, cell.y, cell.z));
            }
        }

        if (cell.x != mapSize - 1)
        {
            var rightCell = map[cell.x + 1, cell.y, cell.z];

            if (rightCell == 0)
            {
                map[cell.x + 1, cell.y, cell.z] = 2;
                stack.Push((cell.x + 1, cell.y, cell.z));
            }
        }

        if (cell.y != 0)
        {
            var upCell = map[cell.x, cell.y - 1, cell.z];

            if (upCell == 0)
            {
                map[cell.x, cell.y - 1, cell.z] = 2;
                stack.Push((cell.x, cell.y - 1, cell.z));
            }
        }

        if (cell.y != mapSize - 1)
        {
            var downCell = map[cell.x, cell.y + 1, cell.z];

            if (downCell == 0)
            {
                map[cell.x, cell.y + 1, cell.z] = 2;
                stack.Push((cell.x, cell.y + 1, cell.z));
            }
        }

        if (cell.z != 0)
        {
            var backCell = map[cell.x, cell.y, cell.z - 1];

            if (backCell == 0)
            {
                map[cell.x, cell.y, cell.z - 1] = 2;
                stack.Push((cell.x, cell.y, cell.z - 1));
            }
        }

        if (cell.z != mapSize - 1)
        {
            var forwardCell = map[cell.x, cell.y, cell.z + 1];

            if (forwardCell == 0)
            {
                map[cell.x, cell.y, cell.z + 1] = 2;
                stack.Push((cell.x, cell.y, cell.z + 1));
            }

        }
    }
}

void CleanMap()
{
    for (int x = 0; x < mapSize; x++)
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                if (map[x,y,z] == 2)
                {
                    map[x, y, z] = 0;
                }
            }
        }
    }
}

foreach (var cube in cubes)
{
    int cubeArea = 6;

    if (cubes.Contains((cube.x - 1, cube.y, cube.z)))
    {
        cubeArea--;
    }

    if (cubes.Contains((cube.x + 1, cube.y, cube.z)))
    {
        cubeArea--;
    }

    if (cubes.Contains((cube.x, cube.y - 1, cube.z)))
    {
        cubeArea--;
    }

    if (cubes.Contains((cube.x, cube.y + 1, cube.z)))
    {
        cubeArea--;
    }

    if (cubes.Contains((cube.x, cube.y, cube.z - 1)))
    {
        cubeArea--;
    }

    if (cubes.Contains((cube.x, cube.y, cube.z + 1)))
    {
        cubeArea--;
    }

    area += cubeArea;
}

Console.WriteLine(area);
Console.ReadLine();

List<(int x, int y, int z)> ReadInput()
{
    return File.ReadAllLines(@"c:\aoc/1.txt")
        .Select(line => line.Split(',')
            .Select(int.Parse).ToList())
        .Select(coordinates => (coordinates[0], coordinates[1], coordinates[2])).ToList();
}