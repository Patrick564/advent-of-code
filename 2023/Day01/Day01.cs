using System.Text.RegularExpressions;

namespace _2023.Day01;

public class Day01
{
    private readonly string _filePath = Path.Join(".", "Day01", "input.txt");

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
        var calibrationValues = File.ReadAllLines(_filePath);
        var eq = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };
        var calibrationSum = 0;

        foreach (var value in calibrationValues)
        {
            var values = new List<int>();
            
            for (var i = 0; i < value.Length; i++)
            {
                if (int.TryParse(value[i].ToString(), out var digit))
                {
                    values.Add(digit);
                    continue;
                }

                for (var j = i + 1; j < value.Length; j++)
                {
                    if (eq.TryGetValue(value[i] + value.Substring(i + 1, j - i), out var word))
                    {
                        values.Add(word);
                    }
                }
            }
            
            calibrationSum += values.First() + values.Last();
        }
        
        Console.WriteLine($"Calibration values sum: {calibrationSum}");
    }
}
