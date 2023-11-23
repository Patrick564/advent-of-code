namespace _2017.Day04;

public class Day04
{
    private readonly string _filepath = Path.Join(".", "Day04", "input.txt");
    
    public void Part01()
    {
        var reader = new StreamReader(_filepath);
        var line = reader.ReadLine();

        var count = 0;

        while (line != null)
        {
            var passphrase = line.Split(" ");
            var uniquePass = passphrase.Distinct().ToArray();

            if (passphrase.Length == uniquePass.Length) count += 1;
            
            line = reader.ReadLine();
        }
        
        reader.Close();
        
        Console.WriteLine($"Count of valid passphrases: {count}");
    }

    public void Part02()
    {
        var reader = new StreamReader(_filepath);
        var line = reader.ReadLine();

        var count = 0;

        while (line != null)
        {
            var passphrase = line.Split(" ").Select(p => new string(p.Order().ToArray())).ToArray();
            var uniquePass = passphrase.Distinct().ToArray();

            if (passphrase.Length == uniquePass.Length) count += 1;
            
            line = reader.ReadLine();
        }
        
        reader.Close();
        
        Console.WriteLine($"Count of valid and unique passphrases: {count}");
    }
}