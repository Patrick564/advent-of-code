namespace _2017.Day12;

public class Day12
{
    private readonly string _filePath = Path.Join(".", "Day12", "input.txt");
    
    public void Part01()
    {
        var rawPrograms = File.ReadAllLines(_filePath);
        var pipes = rawPrograms.Select(raw => new Pipe(raw)).ToList();
        
        pipes.ForEach(pipe => pipe.AddPipes(pipes));

        var groupZero = pipes
            .First(pipe => pipe.Id == 0)
            .ContainsGroup(new List<Pipe>());

        Console.WriteLine($"Total groups contains 0: {groupZero.Count}");

        var tem = pipes
            .Select(pipe => pipe.Id)
            .Select(id => pipes.First(pipe => pipe.Id == id)
                .ContainsGroup(new List<Pipe>())
                .OrderBy(pipe => pipe.Id))
            .GroupBy(group => string.Join(" ", group))
            .Select(group => group.ToString())
            .ToList();
        
        Console.WriteLine($"Total groups: {tem.Count}");
    }
}

public class Pipe
{
    public int Id { get; }
    private List<int> Connections { get; }
    private List<Pipe>? Pipes { get; set; }

    public Pipe(string raw)
    {
        var pipe = raw.Split(" <-> ");

        Id = int.Parse(pipe[0]);
        Connections = pipe[1].Split(", ").Select(int.Parse).ToList();
    }

    public void AddPipes(List<Pipe> pipes)
    {
        Pipes = Connections.Select(comm => pipes.First(pipe => comm == pipe.Id)).ToList();
    }

    public List<Pipe> ContainsGroup(List<Pipe> group)
    {
        group.Add(this);

        foreach (var pipe in Pipes!.Where(pipe => !group.Contains(pipe)))
        {
            pipe.ContainsGroup(group);
        }

        return group;
    }

    public override string ToString()
    {
        return $"{Id}";
    }
}
