namespace _2023.Day02;

public class Day02
{
    private readonly string _filePath = Path.Join(".", "Day02", "input.txt");

    public void Part01()
    {
        var rawGames = File.ReadAllLines(_filePath).ToList();
        var games = rawGames.Select(item => new Game(item)).ToList();

        var result = games.Where(game => game.IsPossible()).Sum(game => game.Id);

        Console.WriteLine($"Possible sum games id: {result}");
    }

    public void Part02()
    {
        var rawGames = File.ReadAllLines(_filePath).ToList();
        var games = rawGames.Select(item => new Game(item)).ToList();

        var result = games.Sum(game => game.MinCubes());

        Console.WriteLine($"Min cubes to game: {result}");
    }
}

public class Game
{
    public int Id { get; }
    private List<Set> SubSets { get; }

    public Game(string raw)
    {
        var game = raw.Split(": ");
        var cubes = game[1].Split("; ").Select(item => item.Split(", ")).ToList();

        Id = int.Parse(game[0].Split(" ")[1]);
        SubSets = new List<Set>();

        foreach (var set in cubes)
        {
            var rawSet = new Set();

            set.ToList().ForEach(cube => rawSet.AddCube(cube));

            SubSets.Add(rawSet);
        }
    }

    public bool IsPossible()
    {
        return SubSets.All(set => set.IsPossible());
    }

    public int MinCubes()
    {
        var red = new List<int>();
        var green = new List<int>();
        var blue = new List<int>();

        foreach (var cube in SubSets)
        {
            if (cube.Red != 0) red.Add(cube.Red);
            if (cube.Green != 0) green.Add(cube.Green);
            if (cube.Blue != 0) blue.Add(cube.Blue);
        }

        return red.Max() * green.Max() * blue.Max();
    }
}

public class Set
{
    public int Red { get; private set; }
    public int Green { get; private set; }
    public int Blue { get; private set; }

    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;

    public void AddCube(string cube)
    {
        var type = cube.Split(" ");

        switch (type[1])
        {
            case "red":
                Red += int.Parse(type[0]);
                break;
            case "green":
                Green += int.Parse(type[0]);
                break;
            case "blue":
                Blue += int.Parse(type[0]);
                break;
        }
    }

    public bool IsPossible()
    {
        return Red <= MaxRed && Green <= MaxGreen && Blue <= MaxBlue;
    }
}
