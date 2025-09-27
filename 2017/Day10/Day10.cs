using System.Text;

namespace _2017.Day10;

public class Rope
{
    public List<int> Knots { get; }
    private int Size { get; }
    private int CurrentPos { get; set; }
    private int SkipSize { get; set; }

    public Rope(int size)
    {
        Knots = [.. Enumerable.Range(0, size)];
        Size = Knots.Count;
        CurrentPos = 0;
        SkipSize = 0;
    }

    private void Increment(int length)
    {
        for (var i = 0; i < length + SkipSize; i++)
        {
            if (CurrentPos > Size - 1) CurrentPos = 0;

            CurrentPos++;
        }

        SkipSize++;
    }

    public void Twist(int length)
    {
        var tmpPos = CurrentPos;
        var tmpKnots = new List<int>(Knots);
        var subList = new List<int>();

        if ((Size - CurrentPos) < length) tmpKnots.AddRange(Knots);

        subList.AddRange(tmpKnots.GetRange(CurrentPos, length));
        subList.Reverse();

        foreach (var item in subList)
        {
            if (tmpPos > Size - 1) tmpPos = 0;

            Knots[tmpPos] = item;
            tmpPos += 1;
        }

        Increment(length);
    }
}

public class Day10
{
    private readonly string _pathFile = Path.Join(".", "Day10", "input.txt");

    public void Part01()
    {
        var lengths = File.ReadAllText(_pathFile).Split(",").Select(int.Parse).ToList();
        var rope = new Rope(256);

        lengths.ForEach(rope.Twist);

        Console.WriteLine($"First two multiplication: {rope.Knots[0] * rope.Knots[1]}");
    }

    public void Part02()
    {
        var lengths = Encoding.ASCII.GetBytes(File.ReadAllText(_pathFile))
            .Concat(new List<byte> { 17, 31, 73, 47, 23 })
            .ToList();
        var rope = new Rope(256);

        Enumerable.Range(0, 64)
            .ToList()
            .ForEach(_ => lengths.ForEach(length => rope.Twist(length)));

        var denseHash = rope.Knots
            .Select((value, index) => new { value, index })
            .GroupBy(item => item.index / 16)
            .Select(group => group.Select(item => (byte)item.value)
                .Aggregate((acc, curr) => (byte)(acc ^ curr))
            )
            .ToArray();
        var knotHash = Convert.ToHexStringLower(denseHash);

        Console.WriteLine($"Knot Hash: {knotHash}");
    }
}
