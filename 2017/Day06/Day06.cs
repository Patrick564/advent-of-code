namespace _2017.Day06;

public class Day06
{
    private readonly string _filepath = Path.Join(".", "Day06", "input.txt");

    public void Part01()
    {
        var memoryBanks = File.ReadAllText(_filepath).Split("\t").Select(int.Parse).ToArray();
        var states = new List<string>();
        string memBanksString;

        while (!states.Contains(string.Join("", memoryBanks)))
        {
            memBanksString = string.Join("", memoryBanks);
            states.Add(memBanksString);
            
            var largestBlock = memoryBanks.Max();
            var blockPosition = Array.IndexOf(memoryBanks, largestBlock);

            memoryBanks[blockPosition] = 0;
            blockPosition += 1;
            
            foreach (var _ in Enumerable.Range(0, largestBlock))
            {
                if (blockPosition > memoryBanks.Length - 1) blockPosition = 0;
                
                memoryBanks[blockPosition] += 1;
                blockPosition += 1;
            }
        }

        memBanksString = string.Join("", memoryBanks);
        states.RemoveAt(0);
        states.Add(memBanksString);
        
        Console.WriteLine($"Redistribution cycles: {states.Count}");
        Console.WriteLine($"Redist. cycles: {states.LastIndexOf(memBanksString) - states.IndexOf(memBanksString)}");
    }
}