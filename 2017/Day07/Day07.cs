namespace _2017.Day07;

public class Day07
{
    private readonly string _filepath = Path.Join(".", "Day07", "input.txt");
    private readonly List<Disk> _disks;

    public Day07()
    {
        var rawTowers = File.ReadAllLines(_filepath);

        _disks = rawTowers.Select(t => new Disk(t)).ToList();
        
        _disks.ForEach(d => d.AddChildDisks(_disks));
    }

    public void Part01()
    {
        Console.WriteLine($"Base disk: {_disks.First(d => d.Parent == null).Name}");
    }

    public void Part02()
    {
        var disk = _disks.First(d => d.Parent == null);
        var target = 0;

        while (!disk.Balanced())
        {
            (disk, target) = disk.UnbalancedChild();
        }

        var diff = target - disk.TotalWeight();
        
        Console.WriteLine($"Bad weight disk: {disk.Name} {disk.Weight} correct to {disk.Weight + diff}");
    }
}

public class Disk
{
    public string Name { get; }
    public int Weight { get; }
    public Disk? Parent { get; private set; }
    private List<string> ChildList { get; }
    private List<Disk>? ChildDisks { get; set; }
    
    public Disk(string rawDisk)
    {
        var disk = rawDisk.Split(" -> ");
        
        Name = disk[0].Split(" ")[0];
        Weight = int.Parse(disk[0].Split(" ")[1].Replace("(", "").Replace(")", ""));
        ChildList = disk.Length > 1 ? disk[1].Split(", ").ToList() : new List<string>();
    }

    public void AddChildDisks(List<Disk> disks)
    {
        ChildDisks = ChildList.Select(c => disks.First(d => c == d.Name)).ToList();
        
        ChildDisks.ForEach(c => c.Parent = this);
    }

    public bool Balanced()
    {
        var childWeights = ChildDisks!.GroupBy(c => c.TotalWeight());

        return childWeights.Distinct().Count() == 1;
    }

    public int TotalWeight()
    {
        var childSum = ChildDisks!.Sum(c => c.TotalWeight());
        
        return childSum + Weight;
    }

    public (Disk disk, int targetWeight) UnbalancedChild()
    {
        var weights = ChildDisks!.GroupBy(x => x.TotalWeight()).ToList();

        var target = weights.First(s => s.Count() > 1).Key;
        var unbalanced = weights.First(s => s.Count() == 1).First();
        
        return (unbalanced, target);
    }
}
