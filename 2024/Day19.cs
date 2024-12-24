namespace AdventOfCode._2024;

public class Day19 : AdventBase
{
    private readonly string[] input;
    private readonly Dictionary<string, long> patterns;
    private readonly string[] towels;
    
    public Day19() : base(2024, 19)
    {
        input = Input;
        patterns = [];
        towels = GetPatterns();
    }

    public override long PartOne()
    {
        return GetLogos()
            .Count(l => LogoPossibilities(l) != 0);
    }

    public override long PartTwo()
    {
        return GetLogos()
            .Sum(LogoPossibilities);
    }

    private long LogoPossibilities(string remainingLogo)
    {
        if (patterns.TryGetValue(remainingLogo, out long count))
        {
            return count;
        }

        foreach (string towel in towels)
        {
            if (remainingLogo == "")
            {
                return 1;
            }
            if (remainingLogo.StartsWith(towel))
            {
                count += LogoPossibilities(remainingLogo[towel.Length..]);
            }
        }
        patterns[remainingLogo] = count;
        return count;
    }

    private string[] GetLogos()
    {
        return input
            .Skip(2)
            .ToArray();
    }

    private string[] GetPatterns()
    {
        return input[0]
            .Split(",")
            .Select(x => x.Trim())
            .ToArray();
    }
}