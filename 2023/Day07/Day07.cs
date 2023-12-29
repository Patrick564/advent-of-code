using _2023.Utils;

namespace _2023.Day07;

public class Day07
{
    private readonly List<Hand> _hands;

    public Day07()
    {
        var rawHands = new LoadFile(7).AsList(FileType.Test);

        _hands = rawHands.Select(hand => new Hand(hand)).ToList();
    }

    public void Part01()
    {
        OrderHands(_hands, ByStandard);

        var totalWins = _hands.Select((hand, index) => hand.Bid * (index + 1)).Sum();

        Console.WriteLine($"Total winnings: {totalWins}");
    }

    public void Part02()
    {
        OrderHands(_hands, ByJoker);

        var totalWins = _hands.Select((hand, index) => hand.Bid * (index + 1)).Sum();

        Console.WriteLine($"Total winnings with Jokers: {totalWins}");
    }

    private static void OrderHands(List<Hand> hands, Comparison<Hand> comparison)
    {
        hands.Sort(comparison);
    }

    private static int ByStandard(Hand current, Hand other)
    {
        if (current.Strong > other.Strong) return 1;

        if (current.Strong < other.Strong) return -1;

        for (var index = 0; index < current.Cards.Length; index++)
        {
            var card = current.Cards[index];
            var otherCard = other.Cards[index];

            if (Value.Standard[card] > Value.Standard[otherCard]) return 1;

            if (Value.Standard[card] < Value.Standard[otherCard]) return -1;
        }

        return 0;
    }

    private static int ByJoker(Hand current, Hand other)
    {
        if (current.StrongJoker > other.StrongJoker) return 1;

        if (current.StrongJoker < other.StrongJoker) return -1;

        for (var index = 0; index < current.Cards.Length; index++)
        {
            var card = current.Cards[index];
            var otherCard = other.Cards[index];

            if (Value.Joker[card] > Value.Joker[otherCard]) return 1;

            if (Value.Joker[card] < Value.Joker[otherCard]) return -1;
        }

        return 0;
    }
}



public readonly struct Hand
{
    public string Cards { get; }
    public int Bid { get; }
    public int Strong { get; }
    public int StrongJoker { get; }

    public Hand(string hand)
    {
        var raw = hand.Split();

        Cards = raw[0];
        Bid = int.Parse(raw[1]);
        Strong = TypeHand(Cards);

        var strongJoker = -1;

        foreach (var card in Value.Standard.Keys)
        {
            var strong = TypeHand(Cards.Replace("J", card.ToString()));

            if (strong > strongJoker) strongJoker = strong;
        }

        StrongJoker = strongJoker;
    }

    private static int TypeHand(string cards)
    {
        var groups = cards
            .GroupBy(letter => letter)
            .Select(group => new { Letter = group.Key, Count = group.Count() })
            .OrderBy(group => group.Count)
            .ToList();

        return cards.Distinct().Count() switch
        {
            1 => 7,
            2 => groups[0].Count == 1 ? 6 : 5,
            3 => groups.Last().Count == 3 ? 4 : 3,
            4 => 2,
            5 => 1,
            _ => 0
        };
    }
}

public static class Value
{
    public static readonly Dictionary<char, int> Standard = new()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 }
    };

    public static readonly Dictionary<char, int> Joker = new()
    {
        { 'A', 13 },
        { 'K', 12 },
        { 'Q', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
        { 'J', 1 }
    };
}
