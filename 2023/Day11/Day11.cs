using _2023.Utils;

namespace _2023.Day11;

public class Day11
{
    private readonly List<string> _universe = new LoadFile("Day11").AsList(FileType.Input);

    public void Solve()
    {
        Console.WriteLine($"Part 01: {Part01()}");

        Console.WriteLine($"Part 02: {Part02()}");
    }

    private string Part01()
    {
        var universe = ExpandUniverse(_universe, 1);

        var result = DistanceBetweenGalaxies(universe);

        return result.ToString();
    }

    private string Part02()
    {
        // Get the base universe distance and 10 distance universe
        var universeOne = ExpandUniverse(_universe, 0);
        var universeTwo = ExpandUniverse(_universe, 9);

        // Get first two distances to get the step between every x10 expansion
        var distanceOne = DistanceBetweenGalaxies(universeOne);
        var distanceTwo = DistanceBetweenGalaxies(universeTwo);

        var step = distanceTwo - distanceOne;

        var round = 1;
        var result = distanceOne;

        // Multiply by 10 every step, to simulate the 1 000 000 distance
        // With 1 => x1 distance
        // With 7 => x1000000 distance
        for (var expand = 1; round < 7; expand *= 10)
        {
            result += step * expand;
            round++;
        }

        return result.ToString();
    }

    private static List<List<char>> ExpandUniverse(IEnumerable<string> baseUniverse, int distance)
    {
        var oldUniverse = new List<string>(baseUniverse);
        var newUniverse = new List<List<char>>();
        var emptyColumns = new List<int>();

        // Add every empty column in all list
        for (var i = 0; i < oldUniverse[0].Length; i++)
        {
            if (oldUniverse.All(item => item[i] == '.')) emptyColumns.Add(i);
        }

        // For every empty column add a '.' in all rows
        for (var i = 0; i < oldUniverse.Count; i++)
        {
            var scroll = 0;

            foreach (var col in emptyColumns)
            {
                // Add '.' spaces in range of distance
                for (var d = 0; d < distance; d++)
                {
                    oldUniverse[i] = oldUniverse[i].Insert(col + scroll, ".");
                    scroll++;
                }
            }
        }

        // If a entire row have not galaxies expand it
        foreach (var row in oldUniverse)
        {
            if (row.All(item => item == '.'))
            {
                // Add new row in range of distance
                for (var d = 0; d < distance; d++)
                {
                    newUniverse.Add(row.ToCharArray().ToList());
                }
            }

            newUniverse.Add(row.ToCharArray().ToList());
        }

        return newUniverse;
    }

    private static long DistanceBetweenGalaxies(IReadOnlyList<List<char>> universe)
    {
        long distance = 0;
        var galaxies = new List<Coordinate>();

        // Save coordinate of every galaxy in universe '#'
        for (var i = 0; i < universe.Count; i++)
        {
            for (var j = 0; j < universe[i].Count; j++)
            {
                if (universe[i][j] == '#') galaxies.Add(new Coordinate(j, i));
            }
        }

        // Get the sum of sides distance, like in Pythagoras theorem
        for (var i = 0; i < galaxies.Count - 1; i++)
        {
            foreach (var galaxy in galaxies.Slice(i + 1, galaxies.Count - i - 1))
            {
                distance += Math.Abs(galaxies[i].X - galaxy.X) + Math.Abs(galaxies[i].Y - galaxy.Y);
            }
        }

        return distance;
    }
}

public readonly struct Coordinate(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}
