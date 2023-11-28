namespace _2017.Day11;

public class Day11
{
    private readonly string _filePath = Path.Join(".", "Day11", "input.txt");

    public void Part01()
    {
        var childPath = File.ReadAllText(_filePath).Split(",");
        
        var register = new List<Hexagon>();
        var current = new Hexagon(0, 0, 0);
        
        foreach (var path in childPath)
        {
            switch (path)
            {
                case "n":
                    current.R--;
                    current.S++;
                    break;
                case "ne":
                    current.Q++;
                    current.R--;
                    break;
                case "nw":
                    current.Q--;
                    current.S++;
                    break;
                case "s":
                    current.R++;
                    current.S--;
                    break;
                case "se":
                    current.Q++;
                    current.S--;
                    break;
                case "sw":
                    current.Q--;
                    current.R++;
                    break;
            }
            
            register.Add(current);
        }

        Console.WriteLine($"Short steps to center: {current.Distance()}");
        Console.WriteLine($"Further steps to center: {register.MaxBy(h => h.AbsSum()).Distance()}");
    }
}

public struct Hexagon(int q, int r, int s)
{
    public int Q { get; set; } = q;
    public int R { get; set; } = r;
    public int S { get; set; } = s;

    public int Distance()
    {
        return (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
    }

    public int AbsSum()
    {
        return Math.Abs(Q) + Math.Abs(R) + Math.Abs(S);
    }
}
