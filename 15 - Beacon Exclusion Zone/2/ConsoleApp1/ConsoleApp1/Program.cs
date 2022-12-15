using System.Numerics;
using System.Text.RegularExpressions;

const int beaconMaxPosition = 4000000;
var points = new List<(int x, int y, int distance)>();

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    var coordinates = Regex.Matches(line, @"-?\d+").Select(m => int.Parse(m.Value)).ToList();
    var xSensor = coordinates[0];
    var ySensor = coordinates[1];
    var xBeacon = coordinates[2];
    var yBeacon = coordinates[3];
    var distanceToBeacon = ManhattanDistance(xSensor, ySensor, xBeacon, yBeacon);
    points.Add((xSensor, ySensor, distanceToBeacon));
}

foreach (var point in points)
{
    var result = ProcessPoint(point);
    if (result.HasValue)
    {
        var tuningFrequency = BigInteger.Multiply(result.Value.x, 4000000) + result.Value.y;
        Console.WriteLine(tuningFrequency);
        break;
    }
}

Console.ReadLine();

(int x, int y)? ProcessPoint((int x, int y, int distance) point)
{
    for (int i = -(point.distance + 1); i <= point.distance + 1; i++)
    {
        var x = point.x + i;
        var y1 = point.y + point.distance + 1 - Math.Abs(i);
        var y2 = point.y + point.distance + 1 + Math.Abs(i);

        if (x is >= 0 and <= beaconMaxPosition && y1 is >= 0 and <= beaconMaxPosition)
        {
            if (IsPointDetected(x, y1, points))
            {
                return (x, y1);
            }
        }

        if (x is >= 0 and <= beaconMaxPosition && y2 is >= 0 and <= beaconMaxPosition)
        {
            if (IsPointDetected(x, y2, points))
            {
                return (x, y2);
            }
        }
    }

    return null;
}

bool IsPointDetected(int x, int y, List<(int x, int y, int distance)> sensors)
{
    return sensors.All(sensor => ManhattanDistance(x, y, sensor.x, sensor.y) > sensor.distance);
}

int ManhattanDistance(int x1, int y1, int x2, int y2)
{
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}