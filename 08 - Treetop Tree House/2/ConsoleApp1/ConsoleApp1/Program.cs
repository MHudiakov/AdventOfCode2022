var input = File.ReadAllLines(@"c:\aoc/1.txt");
var treeMap = new int[input.First().Length, input.Length];

for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        treeMap[i,j] = int.Parse(input[i][j].ToString());
    }
}

int visibleTreesCount = 0;
for (int i = 0; i < treeMap.GetLength(0); i++)
{
    for (int j = 0; j < treeMap.GetLength(1); j++)
    {
        var treeLength = treeMap[i,j];

        bool isVisibleFromTheLeft = true;
        if (i != 0)
        {
            for (int k = 0; k < i; k++)
            {
                if (treeMap[k, j] >= treeLength)
                {
                    isVisibleFromTheLeft = false;
                    break;
                }
            }
        }

        bool isVisibleFromTheRight = true;
        if (i != treeMap.GetLength(0)-1)
        {
            for (int k = i+1; k < treeMap.GetLength(0); k++)
            {
                if (treeMap[k, j] >= treeLength)
                {
                    isVisibleFromTheRight = false;
                    break;
                }
            }
        }

        bool isVisibleFromTheTop = true;
        if (j != 0)
        {
            for (int k = 0; k < j; k++)
            {
                if (treeMap[i, k] >= treeLength)
                {
                    isVisibleFromTheTop = false;
                    break;
                }
            }
        }

        bool isVisibleFromTheBottom = true;
        if (j != treeMap.GetLength(1) - 1)
        {
            for (int k = j + 1; k < treeMap.GetLength(1); k++)
            {
                if (treeMap[i, k] >= treeLength)
                {
                    isVisibleFromTheBottom = false;
                    break;
                }
            }
        }

        bool visible = isVisibleFromTheLeft || isVisibleFromTheRight || isVisibleFromTheTop || isVisibleFromTheBottom;

        if (visible)
            visibleTreesCount++;
    }
}

Console.WriteLine(visibleTreesCount);
Console.ReadLine();