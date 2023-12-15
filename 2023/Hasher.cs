namespace AdventOfCode._2023;

public class Hasher(string wordToHash)
{
    private const int factor = 17;
    private const int divisor = 256;
    private string wordToHash = wordToHash;
    private int currentValue = 0;

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