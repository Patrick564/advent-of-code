using _2023.Utils;

namespace _2023.Day12;

public class Day12
{
    private readonly List<Record> _records;

    public Day12()
    {
        var rawRecords = new LoadFile(12).AsList(FileType.Input);

        _records = new List<Record>();

        foreach (var raw in rawRecords)
        {
            var record = raw.Split(" ");

            var springs = record[0]
                .Select(item => item switch
                {
                    '.' => Condition.Operational,
                    '#' => Condition.Damaged,
                    '?' => Condition.Unknown,
                    _ => throw new Exception("Unknown spring character...")
                })
                .ToList();
            var damaged = record[1]
                .Split(",")
                .Select(int.Parse)
                .ToList();

            _records.Add(new Record(springs, damaged));
        }
    }

    public void Solve()
    {
        Console.WriteLine($"Part 01: {Part01()}");
        Console.WriteLine($"Part 02: {Part02()}");
    }

    private string Part01()
    {
        var result = _records.Sum(spring => spring.PotentialArrangements());

        return result.ToString();
    }

    private string Part02()
    {
        var unfoldRecords = new List<Record>();

        foreach (var t in _records)
        {
            var s = Enumerable
                .Repeat(t.Springs.SkipLast(1).Prepend(Condition.Unknown), 4)
                .SelectMany(s => s);
            var damaged = Enumerable.Repeat(t.Damaged, 5).SelectMany(d => d);

            var unfoldRecord = new Record(t.Springs.SkipLast(1).Concat(s).ToList(), damaged.ToList());

            unfoldRecords.Add(unfoldRecord);
        }

        var result = unfoldRecords.Sum(spring => spring.PotentialArrangements());

        return result.ToString();
    }
}

internal enum Condition
{
    Operational,
    Damaged,
    Unknown
}

internal readonly struct Record(List<Condition> springs, List<int> damaged)
{
    public List<Condition> Springs { get; } = springs;
    public List<int> Damaged { get; } = damaged;

    public long PotentialArrangements()
    {
        // Makes the recursion easier because in damaged list the condition
        // is group by length and next spring of that group
        Springs.Add(Condition.Operational);

        var cache = new long[Damaged.Count][];

        for (var i = 0; i < Damaged.Count; i++)
        {
            cache[i] = new long[Springs.Count];

            Array.Fill(cache[i], -1);
        }

        return Arrangements(Springs, Damaged, cache);
    }

    private static long Arrangements(List<Condition> springs, List<int> damaged, long[][] cache)
    {
        // If all damaged reports have been used and there are still damaged springs, the arrangement not works
        if (damaged.Count == 0) return springs.Contains(Condition.Damaged) ? 0 : 1;

        // If springs are smaller than the fictitious arrangement of damaged, is not valid
        // .?.###. (with the Operational added at start) the damaged [1, 1, 3] => #.#.###.
        // In the above example the damaged possible arrangement is not possible by length
        if (springs.Count < damaged.Sum() + damaged.Count) return 0;

        if (cache[damaged.Count - 1][springs.Count - 1] is not -1)
        {
           return cache[damaged.Count - 1][springs.Count - 1];
        }

        long count = 0;

        // If first is not Damaged, it can be removed to try new possible arrangements
        if (springs.First() is not Condition.Damaged)
        {
            count += Arrangements(springs[1..], damaged, cache);
        }

        var next = damaged[0];

        // If springs are ?#.??. and next (first damaged) is 1 then the group is . and #
        // that is not a possible arrangement because must have an Operational between Damaged
        if (!springs[..next].Contains(Condition.Operational) && springs[next] is not Condition.Damaged)
        {
            count += Arrangements(springs[(next + 1)..], damaged[1..], cache);
        }

        cache[damaged.Count - 1][springs.Count - 1] = count;

        return count;
    }

    public override string ToString()
    {
        var record = Springs.Aggregate("", (current, spring) => current + spring switch
        {
            Condition.Operational => ".",
            Condition.Damaged => "#",
            Condition.Unknown => "?",
            _ => ""
        });

        return $"{record} [{string.Join(",", Damaged)}]";
    }
}
