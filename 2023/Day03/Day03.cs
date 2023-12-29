using _2023.Utils;

namespace _2023.Day03;

public class Day03
{
    private readonly string _filePath = new LoadFile(3).AsString(FileType.Test);

    private const string StringNumbers = "0123456789";

    private List<PartNumber> PartNumbers { get; }
    private List<Symbol> Symbols { get; }

    public Day03()
    {
        var engine = File.ReadAllLines(_filePath);

        PartNumbers = new List<PartNumber>();
        Symbols = new List<Symbol>();

        for (var row = 0; row < engine.Length; row++)
        {
            var number = "";
            var index = new List<int>();

            for (var col = 0; col < engine[row].Length; col++)
            {
                if (StringNumbers.Contains(engine[row][col]))
                {
                    number += engine[row][col];
                    index.Add(col);

                    continue;
                }

                if (engine[row][col] != '.')
                {
                    Symbols.Add(new Symbol(engine[row][col], row, col));
                }

                if (number.Length == 0) continue;

                PartNumbers.Add(new PartNumber(int.Parse(number), row, index));

                number = "";
                index = new List<int>();
            }

            if (number.Length == 0) continue;

            PartNumbers.Add(new PartNumber(int.Parse(number), row, index));
        }
    }

    public void Part01()
    {
        var result = 0;

        foreach (var symbol in Symbols)
        {
            for (var row = symbol.Row - 1; row < symbol.Row + 2; row++)
            {
                var numbers = new List<int>();

                for (var col = symbol.Col - 1; col < symbol.Col + 2; col++)
                {
                    var number = PartNumbers
                        .FirstOrDefault(item => item.Row == row && item.Cols.Contains(col))
                        .Value;

                    numbers.Add(number);
                }

                result += numbers.Distinct().Sum();
            }
        }

        Console.WriteLine($"Sum of part numbers: {result}");
    }

    public void Part02()
    {
        var result = 0;

        foreach (var symbol in Symbols.Where(symbol => symbol.Value == '*'))
        {
            var numbers = new List<int>();

            for (var row = symbol.Row - 1; row < symbol.Row + 2; row++)
            {
                for (var col = symbol.Col - 1; col < symbol.Col + 2; col++)
                {
                    var number = PartNumbers
                        .FirstOrDefault(item => item.Row == row && item.Cols.Contains(col))
                        .Value;

                    if (number == 0) continue;

                    numbers.Add(number);
                }
            }

            var uniques = numbers.Distinct().ToList();

            if (uniques.Count == 2) result += uniques[0] * uniques[1];
        }

        Console.WriteLine($"Sum of all gear ratios: {result}");
    }
}

public readonly struct PartNumber(int value = 0, int row = 0, IEnumerable<int>? cols = null)
{
    public int Value { get; } = value;
    public int Row { get; } = row;
    public IEnumerable<int> Cols { get; } = cols!;
}

public readonly struct Symbol(char value, int row, int col)
{
    public char Value { get; } = value;
    public int Row { get; } = row;
    public int Col { get; } = col;
}
