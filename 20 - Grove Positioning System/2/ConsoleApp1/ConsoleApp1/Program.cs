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
var c1 = numbers[(1000 % numbers.Count + indexOfZero) % numbers.Count].value;
var c2 = numbers[(2000 % numbers.Count + indexOfZero) % numbers.Count].value;
var c3 = numbers[(3000 % numbers.Count + indexOfZero) % numbers.Count].value;

long groveCoordinatesSum = c1 + c2 + c3;

Console.WriteLine(groveCoordinatesSum);

List<(long value, int position)> ReadInput()
{
    int position = 0;
    var input = new List<(long value, int position)>();
    foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
    {
        input.Add((long.Parse(line) * decryptionKey, position));
        position++;
    }

    return input;
}