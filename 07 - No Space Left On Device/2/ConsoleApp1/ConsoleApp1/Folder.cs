namespace ConsoleApp1
{
    internal class Folder
    {
        public Folder()
        {
            Subfolders = new List<Folder>();
            ParentFolder = null;
        }

        public List<Folder> Subfolders { get; set; }

        public Folder? ParentFolder { get; set; }

        public int Volume { get; set; }

        public string Name { get; set; }
    }
}
