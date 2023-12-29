using _2023.Utils;

namespace _2023.Day09;

public class Day09
{
    private readonly List<History> _report;

    public Day09()
    {
        var rawReport = new LoadFile(9).AsList(FileType.Test);

        _report = rawReport.Select(report => new History(report.Split().Select(int.Parse))).ToList();
    }

    public void Solve()
    {
        Console.WriteLine($"Sum of all next values: {Part01()}");

        Console.WriteLine($"Sum of all previous values: {Part02()}");
    }

    private int Part01()
    {
        return _report.Sum(history => history.NextValue());
    }

    private int Part02()
    {
        return _report.Sum(history => history.PrevValue());
    }
}

public readonly struct History(IEnumerable<int> values)
{
    private IEnumerable<int> Values { get; } = values;

    public int PrevValue()
    {
        return FindValue(Values.Reverse());
    }

    public int NextValue()
    {
        return FindValue(Values);
    }

    private static int FindValue(IEnumerable<int> numbers)
    {
        var progression = new List<int>(numbers);
        var last = progression.Last();

        while (true)
        {
            var subLevel = new List<int>();

            for (var index = 0; index < progression.Count - 1; index++)
            {
                subLevel.Add(progression[index + 1] - progression[index]);
            }

            if (subLevel.Any(item => item != 0))
            {
                progression = subLevel;
                last += progression.Last();
                continue;
            }

            break;
        }

        return last;
    }
}
