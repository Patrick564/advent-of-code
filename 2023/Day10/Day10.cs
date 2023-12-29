using _2023.Utils;

namespace _2023.Day10;

public class Day10
{
    private readonly List<List<char>> _gridTiles;
    private readonly Coordinate _start;

    public Day10()
    {
        var rawGridTiles = new LoadFile(10).AsList(FileType.Input);

        _gridTiles = rawGridTiles.Select(tile => tile.ToList()).ToList();
        _start = new Coordinate(0, 0);

        for (var y = 0; y < _gridTiles.Count; y++)
        {
            for (var x = 0; x < _gridTiles[y].Count; x++)
            {
                if (_gridTiles[y][x] != 'S') continue;

                _start = new Coordinate(x, y);
                return;
            }
        }
    }

    public void Solve()
    {
        Console.WriteLine($"Part 1: {Part01()}");

        Console.WriteLine($"Part 2: {Part02()}");
    }

    private string Part01()
    {
        var borders = GetBorders().Count + 1;

        return (borders / 2).ToString();
    }

    private string Part02()
    {
        var borders = GetBorders();

        var vertices = GetVertices(borders);
        var area = GetArea(vertices);

        // Use Pick's Theorem => A = i + (b / 2) - 1
        // A = area, i = inner points, b points in borders of polygon
        // To find i the formula is => i = A - (b / 2) - 1
        var result = area - (borders.Count + 1) / 2 + 1;

    return result.ToString();
    }

    // List of all borders of loop except 'S'
    private List<Coordinate> GetBorders()
    {
        var startPipe = StartPipe();

        var direction = startPipe.Item1;
        var position = startPipe.Item2;

        var borders = new List<Coordinate>();

        while (true)
        {
            borders.Add(position);

            position = position.Sum(NextCoordinate(direction));

            if (position == _start) break;

            direction = NextDirection(_gridTiles[position.Y][position.X], direction);
        }

        return borders;
    }

    // Get the first next 'S' pipe
    private (Direction, Coordinate) StartPipe()
    {
        var ways = new List<Direction>
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        };

        foreach (var way in ways)
        {
            var next = _start.Sum(NextCoordinate(way));

            if (next.X > _gridTiles[0].Count || next.X < 0) continue;

            if (next.Y > _gridTiles.Count || next.Y < 0) continue;

            var pipe = _gridTiles[next.Y][next.X];

            switch (way)
            {
                case Direction.North when "|F7".Contains(pipe):
                    return (NextDirection(pipe, Direction.North), next);
                case Direction.East when "-7J".Contains(pipe):
                    return (NextDirection(pipe, Direction.East), next);
                case Direction.South when "|LJ".Contains(pipe):
                    return (NextDirection(pipe, Direction.South), next);
                case Direction.West when "-LF".Contains(pipe):
                    return (NextDirection(pipe, Direction.West), next);
                default:
                    continue;
            }
        }

        throw new Exception("This should not happen...");
    }

    private static Direction NextDirection(char pipe, Direction from)
    {
        return pipe switch
        {
            '|' => from == Direction.North ? Direction.North : Direction.South,
            '-' => from == Direction.East ? Direction.East : Direction.West,
            'L' => from == Direction.West ? Direction.North : Direction.East,
            'J' => from == Direction.East ? Direction.North : Direction.West,
            '7' => from == Direction.East ? Direction.South : Direction.West,
            'F' => from == Direction.West ? Direction.South : Direction.East,
            _ => throw new Exception("This char is not permitted...")
        };
    }

    private static Coordinate NextCoordinate(Direction from)
    {
        var x = from switch
        {
            Direction.East => 1,
            Direction.West => -1,
            _ => 0
        };
        var y = from switch
        {
            Direction.North => -1,
            Direction.South => 1,
            _ => 0
        };

        return new Coordinate(x, y);
    }

    private List<Coordinate> GetVertices(IEnumerable<Coordinate> borders)
    {
        return borders
            .Where(border => "JLF7".Contains(_gridTiles[border.Y][border.X]))
            .ToList();
    }

    // Get the area of loop using Trapezoid or Shoelace formula
    private static int GetArea(IReadOnlyList<Coordinate> vertices)
    {
        var area = 0;

        for (var index = 0; index < vertices.Count; index++)
        {
            var nextIndex = (index + 1) % vertices.Count;

            var currVertex = vertices[index];
            var nextVertex = vertices[nextIndex];

            area += currVertex.X * nextVertex.Y - currVertex.Y * nextVertex.X;
        }

        return Math.Abs(area) / 2;
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}

public record Coordinate(int X, int Y)
{
    public int X { get; } = X;
    public int Y { get; } = Y;

    public Coordinate Sum(Coordinate coordinate)
    {
        return new Coordinate(X + coordinate.X, Y + coordinate.Y);
    }
}
