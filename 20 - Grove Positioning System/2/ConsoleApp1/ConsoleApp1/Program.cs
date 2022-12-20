var numbers = ReadInput();
const long decryptionKey = 811589153;
const int numberOfMixes = 10;

for (int k = 0; k < numberOfMixes; k++)
{
    for (int j = 0; j < numbers.Count; j++)
    {
        var number = numbers.First(n => n.position == j);
        var currentPosition = numbers.IndexOf(number);
        numbers.RemoveAt(currentPosition);

        var insertPosition = (currentPosition + number.value > 0)
            ? (currentPosition + number.value) % numbers.Count
            : numbers.Count + (currentPosition + number.value) % numbers.Count;

        numbers.Insert((int)insertPosition, number);
    }
}

int indexOfZero = numbers.FindIndex(n => n.value == 0);
long groveCoordinatesSum = new[] { 1000, 2000, 3000 }.Select(i => numbers[(i % numbers.Count + indexOfZero) % numbers.Count].value).Sum();

Console.WriteLine(groveCoordinatesSum);

List<(long value, int position)> ReadInput()
{
    return File.ReadAllLines(@"c:\aoc/1.txt").Select((line, index) => (decryptionKey * long.Parse(line), index)).ToList();
}