namespace _2017.Day02;

public class Day02
{
    private readonly string _filepath = Path.Join(".", "Day02", "input.txt");

    public void Part01()
    {
        var reader = new StreamReader(_filepath);
        var line = reader.ReadLine();

        var checksum = 0;

        while (line != null)
        {
            var smallest = int.MaxValue;
            var largest = int.MinValue;

            foreach (var c in line.Split("\t"))
            {
                if (int.Parse(c) > largest) largest = int.Parse(c);
                
                if (int.Parse(c) < smallest) smallest = int.Parse(c);

            }

            checksum += largest - smallest;
            line = reader.ReadLine();
        }
        
        reader.Close();
        
        Console.WriteLine($"Checksum: {checksum}");
    }

    public void Part02()
    {
        var reader = new StreamReader(_filepath);
        var line = reader.ReadLine();

        var checksum = 0;

        while (line != null)
        {
            var numbers = line.Split("\t").Select(int.Parse).ToArray();

            for (var i = 0; i < numbers.Length; i++)
            {
                for (var j = 0; j < numbers.Length; j++)
                {
                    if (i == j) continue;
                    
                    if (numbers[i] % numbers[j] == 0) checksum += numbers[i] / numbers[j];
                }
            }
            
            line = reader.ReadLine();
        }
        
        reader.Close();
        
        Console.WriteLine($"Checksum of module: {checksum}");
    }
}