using System.Text.RegularExpressions;

namespace _2023.Day01;

public class Day01
{
    private readonly string _filePath = Path.Join(".", "Day01", "input.txt");
    
    private readonly Dictionary<string, string> _equivalence = new()
    {
        { "one", "o1e" },
        { "two", "t2o" },
        { "three", "t3e" },
        { "four", "f4r" },
        { "five", "f5e" },
        { "six", "s6x" },
        { "seven", "s7n" },
        { "eight", "e8t" },
        { "nine", "n9e" }
    };

    public void Part01()
    {
        var calibration = File.ReadAllLines(_filePath);

        Console.WriteLine($"Calibration values sum: {_sumValues(calibration)}");
    }

    public void Part02()
    {
        var calibration = File.ReadAllLines(_filePath);
        
        // This works for a first left search then a right search finding first digits of every side.
        // new Regex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[1-9]");
        
        for (var i = 0; i < calibration.Length; i++)
        {
            foreach (var item in _equivalence)
            {
                if (calibration[i].Contains(item.Key))
                {
                    calibration[i] = calibration[i].Replace(item.Key, item.Value);
                }
            }
        }
        
        Console.WriteLine($"Calibration values sum: {_sumValues(calibration)}");
    }

    private static int _sumValues(IEnumerable<string> values)
    {
        var rx = new Regex("[0-9]");
        
        return values
            .Select(value => rx.Matches(value))
            .Select(match => int.Parse(match.First().Value + match.Last().Value))
            .Sum();
    }
}
