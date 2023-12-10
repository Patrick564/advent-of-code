namespace _2023.Utils;

public class LoadFile(string dir)
{
    private readonly string _dir = dir;

    private string Load(FileType file)
    {
        var content = "";

        try
        {
            content = file switch
            {
                FileType.Input => File.ReadAllText(Path.Join(".", _dir, "input.txt")),
                FileType.Test => File.ReadAllText(Path.Join(".", _dir, "test.txt")),
                _ => ""
            };
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"The file {e.FileName} not exists, create first or check the path.");
        }

        return content;
    }

    public string AsString(FileType file)
    {
        return Load(file);
    }

    public List<string> AsList(FileType file)
    {
        var load = Load(file).Split('\n').ToList();

        if (load.Last() == "") load.RemoveAt(load.Count - 1);

        return load;
    }
}
