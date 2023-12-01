using System.Text.RegularExpressions;

namespace _2023.Day01;

public class Day01
{
    private readonly string _filePath = Path.Join(".", "Day01", "input.txt");
    
    private readonly Dictionary<string, string> _equivalence = new()
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };

    public void Part01()
    {
        var calibrationValues = File.ReadAllLines(_filePath);
        var rx = new Regex("[0-9]");
        
        var calibrationSum = calibrationValues
            .Select(value => rx.Matches(value))
            .Select(match => int.Parse(match.First().Value + match.Last().Value))
            .Sum();

        Console.WriteLine($"Calibration values sum: {calibrationSum}");
    }

    public void Part02()
    {
        var calibration = File.ReadAllLines(_filePath);
        
        var totalSum = 0;

        foreach (var value in calibration)
        {
            var values = new List<string>();
            
            for (var i = 0; i < value.Length; i++)
            {
                if (int.TryParse(value[i].ToString(), out var digit))
                {
                    values.Add(digit.ToString());
                    continue;
                }

                for (var j = i + 1; j < value.Length; j++)
                {
                    if (_equivalence.TryGetValue(value[i] + value.Substring(i + 1, j - i), out var word))
                    {
                        values.Add(word);
                    }
                }
            }
            
            totalSum += int.Parse(values.First() + values.Last());
        }
        
        Console.WriteLine($"Calibration values sum: {totalSum}");
    }
}
