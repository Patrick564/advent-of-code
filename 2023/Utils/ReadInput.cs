namespace _2023.Utils;

public class ReadInput(string day)
{
    private string InputFile { get; } = Path.Join(".", day, "input.txt");
    private string TestFile { get; } = Path.Join(".", day, "test.txt");

    private string Load(string mode)
    {
        var file = "";

        try
        {
            file = mode switch
            {
                "input" => File.ReadAllText(InputFile),
                "test" => File.ReadAllText(TestFile),
                _ => ""
            };
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"The file {e.FileName} not exists, create first or check the path.");
        }

        return file;
    }

    public string LoadAsString(string mode)
    {
        return Load(mode);
    }

    public List<string> LoadAsList(string mode)
    {
        var load = Load(mode).Split('\n').ToList();

        if (load.Last() == "") load.RemoveAt(load.Count - 1);

        return load;
    }
}
