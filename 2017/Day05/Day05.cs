namespace _2017.Day05;

public class Day05
{
    private readonly string _filepath = Path.Join(".", "Day05", "input.txt");

    public void Part01()
    {
        var instructions = File.ReadAllLines(_filepath).Select(int.Parse).ToArray();

        var steps = 0;

        for (var jump = 0;;)
        {
            if (jump > instructions.Length - 1) break;
            
            var prevJump = jump;
            
            jump += instructions[prevJump];
            instructions[prevJump] += 1;
            steps += 1;
        }
        
        Console.WriteLine($"Steps: {steps}");
    }

    public void Part02()
    {
        var instructions = File.ReadAllLines(_filepath).Select(int.Parse).ToArray();

        var steps = 0;

        for (var jump = 0;;)
        {
            if (jump > instructions.Length - 1) break;
            
            var prevJump = jump;
            
            jump += instructions[prevJump];
            instructions[prevJump] += instructions[prevJump] >= 3 ? -1 : 1;
            steps += 1;
        }
        
        Console.WriteLine($"Steps: {steps}");
    }
}