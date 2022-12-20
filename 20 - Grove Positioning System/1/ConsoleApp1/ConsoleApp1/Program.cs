var numbers = ReadInput();

for (int i = 0; i < numbers.Count; i++)
{
    for (int j = 0; j < numbers.Count; j++)
    {
        if (!numbers[j].moved)
        {
            var value = numbers[j].value;
            numbers.RemoveAt(j);

            var insertPosition = 0;
            if (j + value > 0)
            {
                insertPosition = (j + value) % numbers.Count;
            }
            else
            {
                insertPosition = numbers.Count + (j + value) % numbers.Count;
            }

            numbers.Insert(insertPosition, (value, true));
            break;
        }
    }
}

var c1 = numbers[(1000 % numbers.Count + numbers.IndexOf((0, true))) % numbers.Count].value;
var c2 = numbers[(2000 % numbers.Count + numbers.IndexOf((0, true))) % numbers.Count].value;
var c3 = numbers[(3000 % numbers.Count + numbers.IndexOf((0, true))) % numbers.Count].value;

int groveCoordinatesSum = c1 + c2 + c3;

Console.WriteLine(groveCoordinatesSum);
Console.ReadLine();

List<(int value, bool moved)> ReadInput()
{
    return File.ReadAllLines(@"c:\aoc/1.txt")
        .Select(line => (int.Parse(line), false)).ToList();
}