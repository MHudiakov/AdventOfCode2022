//Read moves
var buffer = File.ReadAllText(@"c:\aoc/1.txt");
const int markerLength = 14;
int markerPosition = 0;

for (int bufferIndex = 0; bufferIndex < buffer.Length - markerLength; bufferIndex++)
{
    var markerCandidate = buffer.Substring(bufferIndex, markerLength).ToList();
    if (markerCandidate.Distinct().Count() == markerLength)
    {
        markerPosition = bufferIndex + markerLength;
        break;
    }
}

Console.WriteLine(markerPosition);
Console.ReadLine();