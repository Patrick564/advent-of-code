using _2023.Utils;

namespace _2023.Day04;

public class Day04
{
    private readonly List<Card> _cards;

    public Day04()
    {
        var rawCards = new LoadFile(4).AsList(FileType.Test);

        _cards = rawCards.Select(raw => new Card(raw)).ToList();
        _cards.ForEach(card => card.SetCopies(_cards));
    }

    public void Part01()
    {
        var totalPoints = _cards.Sum(card => card.Points());

        Console.WriteLine($"Total points: {totalPoints}");
    }

    public void Part02()
    {
        var totalCards = _cards.Sum(card => card.CopiesCount());

        Console.WriteLine($"Total cards processed: {totalCards + _cards.Count}");
    }
}

public class Card
{
    private int Id { get; }
    private List<int> Winners { get; }
    private List<int> Numbers { get; }
    private List<Card> Copies { get; }
    private int WinCount { get; }

    public Card(string raw)
    {
        var card = raw.Split(": ");
        var numbersList = card[1].Split(" | ");

        Id = int.Parse(card[0].Split(" ").Last());
        Winners = ParseToList(numbersList[0]);
        Numbers = ParseToList(numbersList[1]);
        Copies = new List<Card>();
        WinCount = Winners.Intersect(Numbers).Count();
    }

    public int CopiesCount()
    {
        return Copies.Count + Copies.Sum(copy => copy.CopiesCount());
    }

    public int Points()
    {
        return Convert.ToInt32(Math.Pow(2, WinCount) / 2);
    }

    public void SetCopies(List<Card> cards)
    {
        foreach (var id in Enumerable.Range(Id + 1, WinCount))
        {
            Copies.Add(cards.First(card => card.Id == id));
        }
    }

    private static List<int> ParseToList(string numbers)
    {
        var listNumbers = new List<int>();

        foreach (var number in numbers.Split())
        {
            if (int.TryParse(number, out var num)) listNumbers.Add(num);
        }

        return listNumbers;
    }
}
