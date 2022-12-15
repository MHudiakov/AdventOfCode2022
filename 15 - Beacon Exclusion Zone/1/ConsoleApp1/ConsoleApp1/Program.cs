using System.Text.RegularExpressions;

const int checkRow = 2000000;
List<(int x, int y)> emptyPositions = new();

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    var coordinates = Regex.Matches(line, @"-?\d+").Select(m => int.Parse(m.Value)).ToList();
    var xSensor = coordinates[0];
    var ySensor = coordinates[1];
    var xBeacon = coordinates[2];
    var yBeacon = coordinates[3];

    var distanceToBeacon = Math.Abs(xSensor - xBeacon) + Math.Abs(ySensor - yBeacon);
    for (int y = ySensor - distanceToBeacon; y <= ySensor + distanceToBeacon; y++)
    {
        if (y != checkRow)
        {
            continue;
        }

        for (int x = xSensor - distanceToBeacon + Math.Abs(y - ySensor); x <= xSensor + distanceToBeacon - Math.Abs(y - ySensor); x++)
        {
            if (x != xBeacon || y != yBeacon)
            {
                emptyPositions.Add((x, y));
            }
        }        
    }
}

int result = emptyPositions.Distinct().Count(item => item.y == checkRow);

Console.WriteLine(result);
Console.ReadLine();