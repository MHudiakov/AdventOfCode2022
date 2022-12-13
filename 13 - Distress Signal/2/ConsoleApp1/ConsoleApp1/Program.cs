var input = (from line in File.ReadAllLines(@"c:\aoc/1.txt")
    where !string.IsNullOrEmpty(line)
    select ParseLine(line[1..]).list).ToList();

var dividerPacket1 = new List<object> { new List<object> { 2 } };
var dividerPacket2 = new List<object> { new List<object> { 6 } };
input.Add(dividerPacket1);
input.Add(dividerPacket2);

var orderedList = new List<List<object>>();
foreach (var package in input)
{
    if (!orderedList.Any())
    {
        orderedList.Add(package);
        continue;
    }

    for (int i = orderedList.Count - 1; i >= 0; i--)
    {
        if (CheckOrder(package, orderedList[i]).Value)
        {
            if (i == 0)
            {
                orderedList.Insert(0, package);
                break;
            }
        }
        else
        {
            orderedList.Insert(i+1, package);
            break;
        }
    }
}

var decoderKey = (orderedList.IndexOf(dividerPacket1) + 1) * (orderedList.IndexOf(dividerPacket2) + 1);

Console.WriteLine(decoderKey);
Console.ReadLine();

bool? CheckOrder(List<object> left, List<object> right)
{
    for (int i = 0; i < left.Count; i++)
    {
        if (right.Count < i + 1)
        {
            return false;
        }

        var lItem = left[i];
        var rItem = right[i];

        if (lItem is int && rItem is int)
        {
            if ((int)lItem < (int)rItem)
            {
                return true;
            }

            if ((int)lItem > (int)rItem)
            {
                return false;
            }
        }
        else
        {
            var l = lItem is int ? new List<object> { (int)lItem } : (List<object>)lItem;
            var r = rItem is int ? new List<object> { (int)rItem } : (List<object>)rItem;
            var check = CheckOrder(l, r);

            if (check.HasValue)
            {
                return check;
            }
        }
    }

    if (right.Count > left.Count)
    {
        return true;
    }

    return null;
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