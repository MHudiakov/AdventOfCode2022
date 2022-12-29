var numbers = File.ReadAllLines(@"c:\aoc/1.txt").ToList();
var sum = ToSnafu(numbers.Select(FromSnafu).Sum());
Console.WriteLine(sum);

long FromSnafu(string number)
{
    return (long)number.Select(n => n switch
        {
            '-' => -1,
            '=' => -2,
            _ => long.Parse(n.ToString())
        })
        .Select((n, i) => n * Math.Pow(5, number.Length - i - 1)).Sum();
}

string ToSnafu(long value)
{
    var buffer = new List<char>();
    int targetBase = 5;
    int shift = 0;

    do
    {
        var symbol = value % targetBase + shift;
        shift = (int)symbol / 3;

        buffer.Add(symbol switch
        {
            5 => '0',
            4 => '-',
            3 => '=',
            _ => symbol.ToString().First()
        });

        value /= targetBase;
    }
    while (value > 0 || shift > 0);

    buffer.Reverse();
    return string.Join(string.Empty, buffer);
}