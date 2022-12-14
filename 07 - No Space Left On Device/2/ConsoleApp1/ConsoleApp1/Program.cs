using ConsoleApp1;

var rootFolder = new Folder
{
    Name = "/",
};

var pointer = rootFolder;

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    switch (line)
    {
        case "$ ls":
            break;
        case { } s when s.StartsWith("dir"):
            var name = line.Split(' ').Last();

            if (!pointer.Subfolders.Exists(f => f.Name.Equals(name)))
            {
                var subfolder = new Folder
                {
                    Name = name,
                    ParentFolder = pointer
                };

                pointer.Subfolders.Add(subfolder);
            }
            break;

        case { } s when char.IsDigit(s[0]):
            var fileVolume = int.Parse(line.Split(' ').First());
            pointer.Volume += fileVolume;
            break;
        case "$ cd ..":
            pointer = pointer.ParentFolder;
            break;
        case { } s when s.StartsWith("$ cd"):
            var folderName = line.Split(' ').Last();
            var folderToNavigate = pointer.Subfolders.Find(s => s.Name.Equals(folderName));
            pointer = folderToNavigate;
            break;
    }
}

GetSubfoldersValue(rootFolder);

int GetSubfoldersValue(Folder folder)
{
    var totalValue = folder.Subfolders.Sum(subfolder => subfolder.Volume + GetSubfoldersValue(subfolder));
    folder.Volume += totalValue;
    return totalValue;
}

var result = 0;
IterateSubfolders(rootFolder);
void IterateSubfolders(Folder folder)
{
    if (folder.Volume <= 100_000)
    {
        result+= folder.Volume;
    }

    foreach (var subfolder in folder.Subfolders)
    {
        IterateSubfolders(subfolder);
    }
}

Console.WriteLine(result);
Console.ReadLine();