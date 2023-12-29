using _2023.Utils;

namespace _2023.Day06;

public class Day06
{
    private readonly List<Race> _races;

    public Day06()
    {
        var rawSheet = new LoadFile(6).AsList(FileType.Test);

        var timeList = rawSheet[0].Split(":")[1].Split().Where(item => item != "").ToList();
        var distanceList = rawSheet[1].Split(":")[1].Split().Where(item => item != "").ToList();

        _races = new List<Race>();

        for (var index = 0; index < timeList.Count; index++)
        {
            _races.Add(new Race(long.Parse(timeList[index]), long.Parse(distanceList[index])));
        }
    }

    public void Part01()
    {
        var result = _races.Aggregate(1, (current, race) => (int)(current * race.Wins()));

        Console.WriteLine($"Multiply win combinations: {result}");
    }

    public void Part02()
    {
        var times = string.Join("", _races.Select(race => race.Time));
        var distances = string.Join("", _races.Select(race => race.Distance));

        var race = new Race(long.Parse(times), long.Parse(distances));

        Console.WriteLine($"Possible win combinations: {race.Wins()}");
    }
}

public class Race(long time, long distance)
{
    public long Time { get; } = time;
    public long Distance { get; } = distance;

    public long Wins()
    {
        var options = 0;

        for (var i = 0; i <= (int)(Time / 2); i++)
        {
            if (i * (Time - i) > Distance) options++;
        }

        return Time % 2 == 0 ? options * 2 - 1 : options * 2;
    }
}
