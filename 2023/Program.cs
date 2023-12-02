namespace _2023;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var day01 = new Day01.Day01();
        var day02 = new Day02.Day02();

        switch (int.Parse(args[0]))
        {
            case 1:
                day01.Part01();
                day01.Part02();
                break;
            case 2:
                day02.Part01();
                day02.Part02();
                break;
        }
    }
}
