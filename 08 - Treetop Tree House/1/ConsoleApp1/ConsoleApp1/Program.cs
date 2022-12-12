var input = File.ReadAllLines(@"c:\aoc/1.txt");
var treeMap = new int[input.First().Length, input.Length];

for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        treeMap[i,j] = int.Parse(input[i][j].ToString());
    }
}

int highestScenicScore = 0;
for (int i = 0; i < treeMap.GetLength(0); i++)
{
    for (int j = 0; j < treeMap.GetLength(1); j++)
    {
        var treeLength = treeMap[i,j];

        int visibleTreesCountLeft = 0;
        if (i != 0)
        {
            for (int k = i-1; k >= 0; k--)
            {
                visibleTreesCountLeft++;
                if (treeMap[k, j] >= treeLength)
                {
                    break;
                }
            }
        }

        int visibleTreesCountRight = 0;
        if (i != treeMap.GetLength(0)-1)
        {
            for (int k = i+1; k < treeMap.GetLength(0); k++)
            {
                visibleTreesCountRight++;
                if (treeMap[k, j] >= treeLength)
                {
                    break;
                }
            }
        }

        int visibleTreesCountTop = 0;
        if (j != 0)
        {
            for (int k = j-1; k >= 0; k--)
            {
                visibleTreesCountTop++;
                if (treeMap[i, k] >= treeLength)
                {
                    break;
                }
            }
        }

        int visibleTreesCountBottom = 0;
        if (j != treeMap.GetLength(1) - 1)
        {
            for (int k = j + 1; k < treeMap.GetLength(1); k++)
            {
                visibleTreesCountBottom++;
                if (treeMap[i, k] >= treeLength)
                {
                    break;
                }
            }
        }

        int score = visibleTreesCountLeft * visibleTreesCountRight * visibleTreesCountTop * visibleTreesCountBottom;

        if (score > highestScenicScore)
            highestScenicScore = score;
    }
}

Console.WriteLine(highestScenicScore);
Console.ReadLine();