namespace AdventOfCode._2023;

public class Day15 : IAdventDay
{

    private List<string> input;
    
    public Day15()
    {
        this.input = Helper.GetInputOneLine(15).Split(",").ToList();
    }
    public long PartOne()
    {
        long sum = 0;
        Hasher h = new(input[0]);
        sum += h.Hash();
        
        for (int i = 1; i < input.Count; i++)
        {
            h.NextWord(input[i]);
            sum += h.Hash();
        }
        return sum;
    }

    public long PartTwo()
    {
        return 0;
    }
}

public class Hasher(string wordToHash)
{
    private string wordToHash = wordToHash;
    private int currentValue = 0;
    private const int factor = 17;
    private const int divisor = 256;

    public int Hash()
    {
        foreach (int asciiValue in wordToHash)
        {
            currentValue += asciiValue;
            currentValue *= factor;
            currentValue %= divisor;
        }

        return currentValue;
    }

    public void NextWord(string wordToHash)
    {
        this.wordToHash = wordToHash;
        this.currentValue = 0;
    }
}