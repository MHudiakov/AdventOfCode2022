var numbers = ReadInput();

for (int i = 0; i < numbers.Count; i++)
{
    var number = numbers.First(n => n.position == i);
    var currentPosition = numbers.IndexOf(number);
    numbers.RemoveAt(currentPosition);

    var insertPosition = (currentPosition + number.value > 0)
        ? (currentPosition + number.value) % numbers.Count
        : numbers.Count + (currentPosition + number.value) % numbers.Count;

    numbers.Insert((int)insertPosition, number);
}

int indexOfZero = numbers.FindIndex(n => n.value == 0);
long groveCoordinatesSum = new[] { 1000, 2000, 3000 }.Select(i => numbers[(i % numbers.Count + indexOfZero) % numbers.Count].value).Sum();

Console.WriteLine(groveCoordinatesSum);

List<(long value, int position)> ReadInput()
{
    return File.ReadAllLines(@"c:\aoc/1.txt").Select((line, index) => (long.Parse(line), index)).ToList();
}