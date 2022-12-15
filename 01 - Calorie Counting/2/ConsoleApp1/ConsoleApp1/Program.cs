var sum = 0;
var caloriesList = new List<int>();

foreach (var line in File.ReadLines(@"c:\aoc/1.txt"))
{
    if (string.IsNullOrEmpty(line))
    {
        caloriesList.Add(sum);        
        sum = 0;
    }
    else
    {
        sum += int.Parse(line);
    }
}

var result = caloriesList.OrderByDescending(i => i).Take(3).Sum();

Console.WriteLine(result);
Console.ReadLine();