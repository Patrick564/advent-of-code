namespace _2017.Day09;

public class Day09
{
    private readonly string _pathFile = Path.Join(".", "Day09", "input.txt");

    public void Part01()
    {
        var stream = File.ReadAllText(_pathFile);

        var group = 0;
        var score = 0;
        
        var garbage = false;

        for (var i = 0; i < stream.Length; i++)
        {
            switch (stream[i])
            {
                case '{':
                    if (garbage) break;
                    
                    group += 1;
                    break;
                case '}':
                    if (garbage) break;
                    
                    score += 1 * group;
                    group -= 1;
                    break;
                case '!':
                    i += 1;
                    break;
                case '<':
                    garbage = true;
                    break;
                case '>':
                    garbage = false;
                    break;
            }
        }
        
        Console.WriteLine($"Total score is: {score}");
    }

    public void Part02()
    {
        var stream = File.ReadAllText(_pathFile);
        
        var garbage = false;
        var garbageCount = 0;

        for (var i = 0; i < stream.Length; i++)
        {
            switch (stream[i])
            {
                case '!':
                    i += 1;
                    continue;
                case '<':
                {
                    if (garbage)
                    {
                        garbageCount += 1;
                    }
                
                    garbage = true;
                    continue;
                }
                case '>':
                    garbage = false;
                    continue;
            }

            if (garbage)
            {
                garbageCount += 1;
            }
        }
        
        Console.WriteLine($"Total score in garbage: {garbageCount}");
    }
}