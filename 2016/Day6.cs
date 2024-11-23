namespace AdventOfCode._2016;

public class Day6 : AdventBase
{
    private readonly string[] input;

    public Day6() : base(2016 , 6)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        string result = string.Empty;
        for (int i = 0; i < input[0].Length; i++)
        {
            result += GetMostFrequent(i);
        }

        Console.WriteLine(result);
        return long.MinValue;
    }

    string GetMostFrequent(int pos)
    {
        Dictionary<char, int> charCounter = new();
        for (int i = 0; i < input.Length; i++)
        {
            char key = input[i][pos];
            if (charCounter.ContainsKey(key))
            {
                charCounter[key]++;
            }
            else
            {
                charCounter.Add(key, 1);
            }
        }

        charCounter = charCounter.OrderByDescending(p => p.Value)
            .ToDictionary(x => x.Key, x => x.Value);
        return charCounter.Keys.ToArray()[0].ToString();
    }

    string GetLeastFrequent(int pos)
    {
        Dictionary<char, int> charCounter = new();
        for (int i = 0; i < input.Length; i++)
        {
            char key = input[i][pos];
            if (charCounter.ContainsKey(key))
            {
                charCounter[key]++;
            }
            else
            {
                charCounter.Add(key, 1);
            }
        }

        charCounter = charCounter.OrderByDescending(p => p.Value)
            .ToDictionary(x => x.Key, x => x.Value);
        return charCounter.Keys.ToArray()[charCounter.Count - 1].ToString();
    }

    public override long PartTwo()
    {
        string result = string.Empty;
        for (int i = 0; i < input[0].Length; i++)
        {
            result += GetLeastFrequent(i);
        }

        Console.WriteLine(result);
        return long.MinValue;
    }
}