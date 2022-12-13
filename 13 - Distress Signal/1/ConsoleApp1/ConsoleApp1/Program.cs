var input = (from line in File.ReadAllLines(@"c:\aoc/1.txt")
    where !string.IsNullOrEmpty(line)
    select ParseLine(line[1..]).list).ToList();

int indicesSum = 0;
for (int i = 0; i < input.Count; i+=2)
{
    var left = input[i];
    var right = input[i + 1];
    var rightOrder = CheckOrder(left, right);

    if (rightOrder.result)
    {
        indicesSum += i / 2 + 1;
    }
}

Console.WriteLine(indicesSum);
Console.ReadLine();

(bool result, bool goFurther) CheckOrder(List<object> left, List<object> right)
{
    for (int i = 0; i < left.Count; i++)
    {
        if (right.Count < i + 1)
        {
            return (false, false);
        }

        var lItem = left[i];
        var rItem = right[i];

        if (lItem is int && rItem is int)
        {
            if ((int)lItem < (int)rItem)
            {
                return (true, false);
            }

            if ((int)lItem > (int)rItem)
            {
                return (false, false);
            }
        }
        else
        {
            var l = lItem is int ? new List<object> { (int)lItem } : (List<object>)lItem;
            var r = rItem is int ? new List<object> { (int)rItem } : (List<object>)rItem;
            var check = CheckOrder(l, r);

            if (!check.goFurther)
            {
                return check;
            }
        }
    }

    if (right.Count > left.Count)
    {
        return (true, false);
    }

    return (false, true);
}

(List<object> list, int offset) ParseLine(string line)
{
    var result = new List<object>();
    string digit = string.Empty;

    for (int i = 0; i < line.Length; i++)
    {
        var symbol = line[i];

        switch (line[i])
        {
            case var c when char.IsDigit(c):
                digit += symbol;

                if (!char.IsDigit(line[i + 1]))
                {
                    result.Add(int.Parse(digit));
                    digit = string.Empty;
                }

                break;
            case '[':
                var parsedLine = ParseLine(line[(i + 1)..]);
                result.Add(parsedLine.list);
                i += parsedLine.offset + 1;
                break;

            case ']':
                return (result, i);
        }
    }

    return (result, 0);
}