using _2023.Utils;

namespace _2023.Day05;

public class Day05
{
    private readonly List<long> _seeds;
    private readonly List<List<Map>> _maps;

    public Day05()
    {
        var rawAlmanac = new ReadInput("Day05").LoadAsList("input");

        _seeds = rawAlmanac[0].Split(": ")[1].Split().Select(long.Parse).ToList();
        _maps = new List<List<Map>>();

        var map = new List<Map>();

        for (var index = 2; index < rawAlmanac.Count; index++)
        {
            var raw = rawAlmanac[index];

            if (raw.Contains("map")) continue;

            if (raw != "")
            {
                map.Add(new Map(raw));
                continue;
            }

            _maps.Add(map);
            map = new List<Map>();
        }

        _maps.Add(map);
    }

    public void Part01()
    {
        var locations = new List<long>();

        foreach (var seed in _seeds)
        {
            var conversion = seed;

            foreach (var maps in _maps)
            {
                var range = maps.FirstOrDefault(map => map.InRange(conversion));

                if (range is null) continue;

                conversion = range.Destination + (conversion - range.Source);
            }

            locations.Add(conversion);
        }

        Console.WriteLine($"Locations: {locations.Min()}");
    }

    public void Part02()
    {
        var lastConversion = _maps.Last().OrderBy(map => map.Destination).First();
        var locations = lastConversion.Destination + lastConversion.RangeLenght;
        var seeds = new Dictionary<long, long>();

        for (var index = 0; index < _seeds.Count - 2; index += 2)
        {
            seeds.Add(_seeds[index], _seeds[index] + _seeds[index + 1]);
        }

        _maps.Reverse();

        long result = -1;

        for (var index = 0; index <= locations; index++)
        {
            long original = index;

            foreach (var maps in _maps)
            {
                var range = maps.FirstOrDefault(map => map.InRangeReverse(original));

                if (range == null) continue;

                original = range.Source + (original - range.Destination);
            }

            if (!seeds.Any(item => original >= item.Key && original <= item.Value)) continue;

            result = index;
            break;
        }

        Console.WriteLine($"Second part: {result}");
    }
}

public class Map
{
    public long Destination { get; }
    public long Source { get; }
    public long RangeLenght { get; }

    public Map(string raw)
    {
        var map = raw.Split();

        Destination = long.Parse(map[0]);
        Source = long.Parse(map[1]);
        RangeLenght = long.Parse(map[2]);
    }

    public bool InRange(long value)
    {
        return value >= Source && value <= Source + RangeLenght;
    }

    public bool InRangeReverse(long value)
    {
        return value >= Destination && value <= Destination + RangeLenght;
    }
}
