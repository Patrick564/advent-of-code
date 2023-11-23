namespace _2017;

internal abstract class _2017
{
    private static void Main(string[] args)
    {
        var day01 = new Day01.Day01();
        var day02 = new Day02.Day02();
        var day03 = new Day03.Day03();
        
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
            case 3:
                day03.Part01();
                day03.Part02();
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
}
