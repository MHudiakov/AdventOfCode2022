﻿var numbers = ReadInput();

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
    int position = 0;
    var input = new List<(long value, int position)>();
    foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
    {
        input.Add((long.Parse(line), position));
        position++;
    }

    return input;
}