namespace _2017.Day13;

public class Day13
{
    private readonly string _filePath = Path.Join(".", "Day13", "input.txt");

    public void Part01()
    {
        var rawLayers = File.ReadAllLines(_filePath);
        var firewall = new Firewall(rawLayers);

        var severity = firewall.Layers
            .Where(layer => layer.Depth % (2 * layer.Range - 2) == 0)
            .Sum(layer => layer.Depth * layer.Range);

        Console.WriteLine($"Total severity: {severity}");
    }

    public void Part02()
    {
        var rawLayers = File.ReadAllLines(_filePath);
        var firewall = new Firewall(rawLayers);

        var delay = 0;

        while (true)
        {
            var severity = firewall.Layers
                .All(layer => (layer.Depth + delay) % (2 * layer.Range - 2) != 0);
            
            if (severity) break;
            
            delay++;
        }
        
        Console.WriteLine($"Min delay: {delay}");
    }
}

internal struct Firewall
{
    public List<Layer> Layers { get; }

    public Firewall(IEnumerable<string> rawLayers)
    {
        Layers = new List<Layer>();
        
        foreach (var raw in rawLayers)
        {
            var parts = raw.Split(": ").Select(int.Parse).ToList();
            
            Layers.Add(new Layer(parts[0], parts[1]));
        }
    }
}

internal readonly struct Layer(int depth, int range)
{
    public int Depth { get; } = depth;
    public int Range { get; } = range;
}
