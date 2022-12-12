// Read stacks
var listOfStacks = new List<Stack<char>>();
var linesOfStack = File.ReadLines(@"c:\aoc/stack.txt").ToList();

for (int lineIndex = linesOfStack.Count - 1; lineIndex >= 0; lineIndex--)
{
    var line = linesOfStack[lineIndex];
    var stackIndex = 0;
    for (int character = 1; character < line.Length; character += 4)
    {
        if (listOfStacks.Count <= stackIndex)
        {
            listOfStacks.Add(new Stack<char>());
        }

        var symbol = line[character];
        if (symbol != ' ')
        {
            listOfStacks[stackIndex].Push(symbol);
        }

        stackIndex++;
    }
}

//Read moves
var moves = new List<int[]>();
foreach (var line in File.ReadLines(@"c:\aoc/1.txt"))
{
    var move = line.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    moves.Add(move);
}

// Process moves
foreach (var move in moves)
{
    for (int i = 0; i < move[0]; i++)
    {
        var value = listOfStacks[move[1] - 1].Pop();
        listOfStacks[move[2] - 1].Push(value);
    }
}

var result = listOfStacks.Aggregate(string.Empty, (current, stack) => current + stack.Peek());

Console.WriteLine(result);
Console.ReadLine();