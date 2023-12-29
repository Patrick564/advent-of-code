namespace _2023.Utils;

public class LoadFile(int day)
{
    private readonly int _day = day;

    private string Load(FileType file)
    {
        var content = "";

        try
        {
            content = file switch
            {
                FileType.Input => File.ReadAllText(Path.Join(".", $"Day{_day}", "input.txt")),
                FileType.Test => File.ReadAllText(Path.Join(".", $"Day{_day}", "test.txt")),
                _ => ""
            };
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"The file not exists, create first or check the path.");
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
