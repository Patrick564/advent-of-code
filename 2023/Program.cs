namespace _2023;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var day01 = new Day01.Day01();
        
        switch (int.Parse(args[0]))
        {
            case 1:
                day01.Part01();
                day01.Part02();
                break;
        }
    }
}
