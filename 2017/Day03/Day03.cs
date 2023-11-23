namespace _2017.Day03;

public class Day03
{
    private const int Input = 368078;
    // private const int Input = 1024;

    public void Part01()
    {
        var squareStart = 2;
        var squareEnd = 9;
        var squareSide = 2;

        var steps = 0;

        // Loop until input is between start and end nums
        for (var i = 1; ; i++)
        {
            steps += 1;
            
            if (Input >= squareStart && Input <= squareEnd)
            {
                // Corner sections: right, top, left, down
                int[] corners =
                {
                    squareStart - 1,
                    squareEnd - squareSide * 3,
                    squareEnd - squareSide * 2,
                    squareEnd - squareSide
                };

                // Break when input is in a section (top, down, left, right)
                for (var j = 3; j > -1; j--)
                {
                    if (Input < corners[j]) continue;
                    
                    steps += Math.Abs(corners[j] + squareSide / 2 - Input);
                    break;
                }
                
                break;
            }
            
            // Set values for next square
            squareStart += 8 * i;
            squareEnd += 8 * (i + 1);
            squareSide += 2;
        }
        
        Console.WriteLine($"Total steps: {steps}");
    }

    public void Part02()
    {
        var squaresOrder = new List<string>();
        var squares = new Dictionary<string, int>
        {
            { "0,0", 1 }
        };
        
        var lastBlock = "0,0";

        var restMove = 2;
        
        while (Input > squares[lastBlock])
        {
            // Convert coordinates to int
            var position = lastBlock.Split(",").Select(int.Parse).ToArray();

            for (var i = 1; i < 6; i++)
            {
                int adjacentSum;
                
                switch (i)
                {
                    case 1:
                        position[0] += 1;

                        adjacentSum = _adjacentSquare(position).Aggregate(0, 
                            (acc, curr) => acc + squares.GetValueOrDefault(curr, 0));
                        
                        squares.Add($"{position[0]},{position[1]}", adjacentSum);
                        squaresOrder.Add($"{position[0]},{position[1]}");
                        
                        break;
                    case 2:
                        for (var m = 0; m < restMove - 1; m++)
                        {
                            position[1] += 1;

                            adjacentSum = _adjacentSquare(position).Aggregate(0, 
                                (acc, curr) => acc + squares.GetValueOrDefault(curr, 0));
                        
                            squares.Add($"{position[0]},{position[1]}", adjacentSum);
                            squaresOrder.Add($"{position[0]},{position[1]}");
                        }
                        
                        break;
                    case > 2:
                        for (var m = 0; m < restMove; m++)
                        {
                            position[i == 4 ? 1 : 0] += i == 5 ? 1 : -1;

                            adjacentSum = _adjacentSquare(position).Aggregate(0, 
                                (acc, curr) => acc + squares.GetValueOrDefault(curr, 0));
                        
                            squares.Add($"{position[0]},{position[1]}", adjacentSum);
                            squaresOrder.Add($"{position[0]},{position[1]}");
                        }
                        
                        break;
                }
            }
            
            // Update moves
            restMove += 2;
            lastBlock = $"{position[0]},{position[1]}";
        }

        foreach (var coord in squaresOrder)
        {
            if (squares[coord] <= Input) continue;
            
            Console.WriteLine($"First larger value than input: {squares[coord]}");
            break;
        }
    }

    private static List<string> _adjacentSquare(int[] coordinates)
    {
        var adjacentSquare = new List<string>();
        
        for (var i = -1; i < 2; i++)
        {
            adjacentSquare.Add($"{coordinates[0] + i},{coordinates[1] + (i == 0 ? -1 : 1)}");
            
            if (i != 0) adjacentSquare.Add($"{coordinates[0] + i},{coordinates[1]}");
            
            adjacentSquare.Add($"{coordinates[0] + i},{coordinates[1] + (i == 0 ? 1 : -1)}");
        }

        return adjacentSquare;
    }
}
