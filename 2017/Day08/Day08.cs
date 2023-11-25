namespace _2017.Day08;

public class Day08
{
    private readonly string _pathFile = Path.Join(".", "Day08", "input.txt");

    public void Part01()
    {
        var instructions = File.ReadAllLines(_pathFile);

        var registers = new Dictionary<string, int>();
        var maxValuesList = new List<int>();
        
        foreach (var i in instructions)
        {
            var instruction = new Instruction(i);

            registers.TryAdd(instruction.Register, 0);
            registers.TryAdd(instruction.CondRegister, 0);

            if (instruction.GetResult(registers[instruction.CondRegister]))
            {
                registers[instruction.Register] += instruction.OperationValue * instruction.GetSymbol();
            }
            
            maxValuesList.Add(registers.Values.Max());
        }
        
        Console.WriteLine($"Max final value: {registers.Values.Max()}");
        
        Console.WriteLine($"Max historic value: {maxValuesList.Max()}");
    }
}

public class Instruction
{
    public string Register { get; }
    private string Operation { get; }
    public int OperationValue { get; }
    public string CondRegister { get; }
    private string Condition { get; }
    private int CondValue { get; }

    public Instruction(string rawInstruction)
    {
        var instruction = rawInstruction.Split(" ");

        Register = instruction[0];
        Operation = instruction[1];
        OperationValue = int.Parse(instruction[2]);
        
        CondRegister = instruction[4];
        Condition = instruction[5];
        CondValue = int.Parse(instruction[6]);
    }

    public int GetSymbol()
    {
        return Operation == "inc" ? 1 : -1;
    }

    public bool GetResult(int condRegister)
    {
        var result = Condition switch
        {
            ">" => condRegister > CondValue,
            "<" => condRegister < CondValue,
            ">=" => condRegister >= CondValue,
            "<=" => condRegister <= CondValue,
            "==" => condRegister == CondValue,
            "!=" => condRegister != CondValue,
            _ => false
        };

        return result;
    }
}
