namespace _2017.Day01;

public class Day01
{
    private readonly string _filepath = Path.Join(".", "Day01", "input.txt");
    
    public void Part01()
    {
        var reader = new StreamReader(_filepath);
        var character = reader.Read();

        var firstDigit = -1;
        var sumDigit = 0;

        while (character != -1)
        {
            var intDigit = int.Parse(((char)character).ToString());
            var intNextDigit = reader.Peek() == -1 ? -1 : int.Parse(((char)reader.Peek()).ToString());
            
            if (firstDigit < 0) firstDigit = intDigit;

            if (intNextDigit > 0)
            {
                sumDigit += intDigit == intNextDigit ? intDigit : 0;
            }
            else
            {
                sumDigit += intDigit == firstDigit ? intDigit : 0;
            }
            
            character = reader.Read();
        }
        
        reader.Close();
        
        Console.WriteLine($"Sum of all digits: {sumDigit}");
    }

    public void Part02()
    {
        var digits = File.ReadAllText(_filepath);
        
        var sumDigit = 0;
        var half = digits.Length / 2;

        for (var i = 0; i < digits.Length; i++)
        {
            // Max digits length
            if (i + half > digits.Length - 1)
            {
                if (digits[i] == digits[i + half - (digits.Length - 1) - 1])
                {
                    sumDigit += int.Parse(digits[i].ToString());
                }
            }
            // Standard digit length
            else
            {
                if (digits[i] == digits[i + half]) sumDigit += int.Parse(digits[i].ToString());
            }
        }
        
        Console.WriteLine($"Sum of all digits: {sumDigit}");
    }
}
