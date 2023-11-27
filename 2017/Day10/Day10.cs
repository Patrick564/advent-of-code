using System.Text;

namespace _2017.Day10;

public class Day10
{
    private readonly string _pathFile = Path.Join(".", "Day10", "input.txt");
    
    public void Part01()
    {
        var lengths = File.ReadAllText(_pathFile).Split(",").Select(int.Parse).ToList();
        var rope = new Rope(256);

        lengths.ForEach(lenght => rope.Twist(lenght));
        
        Console.WriteLine($"First two multiplication: {rope.Knots[0] * rope.Knots[1]}");
    }

    public void Part02()
    {
        var lengths = Encoding.ASCII.GetBytes(File.ReadAllText(_pathFile))
            .Concat(new List<byte> { 17, 31, 73, 47, 23 })
            .ToList();
        var rope = new Rope(256);
        
        foreach (var _ in Enumerable.Range(0, 64))
        {
            lengths.ForEach(lenght => rope.Twist(lenght));
        }

        var denseHash = rope.Knots
            .Select((value, index) => new { value, index })
            .GroupBy(item => item.index / 16)
            .Select(group => group.Select(item => (byte)item.value)
                .Aggregate((acc, curr) => (byte)(acc ^ curr)))
            .ToArray();
        var knotHash = BitConverter.ToString(denseHash).Replace("-", "").ToLower();
        
        Console.WriteLine($"Knot Hash: {knotHash}");
    }
}

public class Rope
{
    public List<int> Knots { get; }
    private int InitialLenght { get; }
    private int Position { get; set; }
    private int SkipSize { get; set; }

    public Rope(int size)
    {
        Knots = Enumerable.Range(0, size).ToList();
        InitialLenght = Knots.Count;
    }
    
    private void Increment(int range)
    {
        for (var i = 0; i < range; i++)
        {
            if (Position > InitialLenght - 1) Position = 0;
            
            Position++;
        }
        
        for (var i = 0; i < SkipSize; i++)
        {
            if (Position > InitialLenght - 1) Position = 0;
            
            Position++;
        }
        
        SkipSize += 1;
    }

    public void Twist(int range)
    {
        var subKnot = new List<int>();
        var subPosition = Position;

        for (var i = 0; i < range; i++)
        {
            if (subPosition > InitialLenght - 1) subPosition = 0;
            
            subKnot.Add(Knots[subPosition]);
            subPosition += 1;
        }

        subKnot.Reverse();
        subPosition = Position;

        foreach (var sub in subKnot)
        {
            if (subPosition > InitialLenght - 1) subPosition = 0;
            
            Knots[subPosition] = sub;
            subPosition += 1;
        }
        
        Increment(range);
    }
}
