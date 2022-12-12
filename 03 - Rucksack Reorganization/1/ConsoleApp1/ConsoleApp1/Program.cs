var repeatedSymbols = (from line in File.ReadLines(@"c:\aoc/1.txt")
    let firstHalf = line.ToCharArray(0, line.Length / 2)
    let secondHalf = line.ToCharArray(line.Length / 2, line.Length / 2)
    select firstHalf.Intersect(secondHalf).First()).ToList();

int sum = repeatedSymbols.Sum(symbol => symbol - (char.IsUpper(symbol) ? 38 : 96));

Console.WriteLine(sum);
Console.ReadLine();