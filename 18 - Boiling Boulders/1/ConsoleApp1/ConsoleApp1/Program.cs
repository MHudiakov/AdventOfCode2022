var cubes = ReadInput();
int area = 0;

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