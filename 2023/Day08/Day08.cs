using _2023.Utils;

namespace _2023.Day08;

public class Day08
{
    private readonly List<Step> _steps;
    private readonly List<Node> _network;

    public Day08()
    {
        var rawInstructions = new LoadFile(8).AsList(FileType.Test);
        var network = rawInstructions.Slice(2, rawInstructions.Count - 2);

        _steps = rawInstructions[0]
            .ToList()
            .Select(step => step == 'L' ? Step.Left : Step.Right)
            .ToList();
        _network = network.Select(node => new Node(node)).ToList();
    }

    public void Part01()
    {
        var currentNode = _network.First(node => node.Id == "AAA");

        var steps = Cycles(currentNode, true);

        Console.WriteLine($"Steps to reach 'ZZZ': {steps}");
    }

    public void Part02()
    {
        var currentNodes = _network.FindAll(node => node.Id.EndsWith('A'));
        var cycles = currentNodes.Select(node => Cycles(node, false)).ToList();

        long result = 1;

        foreach (var cycle in cycles)
        {
            // LCM = (a * b) / gcd(a, b)
            result *= cycle / Gcd(result, cycle);
        }

        Console.WriteLine($"Steps to all nodes reach nodes end with 'Z': {result}");
    }

    private int Cycles(Node node, bool withId)
    {
        var steps = 0;
        var index = 0;
        var currentNode = node;

        while (true)
        {
            currentNode = _network.First(n => n.Id == currentNode.Move(_steps[index]));

            steps++;
            index++;

            if (index > _steps.Count - 1) index = 0;

            if (withId)
            {
                if (currentNode.Id == "ZZZ") break;
            }
            else
            {
                if (currentNode.Id.EndsWith('Z')) break;
            }
        }

        return steps;
    }

    // Euclidean Algorithm to get GCD
    private static long Gcd(long a, long b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b) a %= b;
            else b %= a;
        }

        return a == 0 ? b : a;
    }
}

public enum Step
{
    Left,
    Right
}

public readonly struct Node
{
    public string Id { get; }
    private string Left { get; }
    private string Right { get; }

    public Node(string rawNode)
    {
        var node = rawNode.Split(" = ");
        var instructions = node[1].Substring(1, node[1].Length - 2).Split(", ");

        Id = node[0];
        Left = instructions[0];
        Right = instructions[1];
    }

    public string Move(Step direction)
    {
        return direction == Step.Left ? Left : Right;
    }

    public override string ToString()
    {
        return $"{Id} = ({Left}, {Right})";
    }
}
