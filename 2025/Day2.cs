namespace AdventOfCode._2025;

public class Day2 : AdventBase
{
    private readonly List<(long start, long end)> ranges;

    public Day2() : base(2025, 2)
    {
        var input = InputOneLine;
        ranges = ParseInput(input);
    }

    public override long PartOne()
    {
        long count = 0;
        foreach (var range in ranges)
        {
            for (long n = range.start; n <= range.end; n++)
            {
                string value = n.ToString();
                if (value.Length % 2 == 1)
                {
                    continue;
                }

                if (!IsInvalid(value))
                {
                    count += n;
                }
            }
        }
        return count;
    }

    public override long PartTwo()
    {
        long count = 0;
        foreach (var range in ranges)
        {
            for (long n = range.start; n <= range.end; n++)
            {
                string value = n.ToString();
                for (int i = 1; i <= value.Length / 2; i++)
                {
                    string part = value.Substring(0, i);
                    int occurrences = CountOccurrences(value, part);
                    if (occurrences == value.Length / (double)i)
                    {
                        count += n;
                        break;
                    }
                }
            }
        }

        return count;
    }

    private int CountOccurrences(string number, string part)
    {
        if ((double)number.Length / part.Length % 1 != 0)
        {
            return -1;
        }

        int count = 1;
        for (int i = part.Length; i < number.Length; i += part.Length)
        {
            for (int x = 0; x < part.Length; x++)
            {
                if (part[x] != number[i + x])
                {
                    return -1;
                }
            }

            count++;
        }

        return count;
    }

    private static bool IsInvalid(string value)
    {
        int middle = value.Length / 2;
        for (int pos = 0; pos < middle; pos++)
        {
            if (value[pos] != value[middle + pos])
            {
                return true;
            }
        }

        return false;
    }

    private List<(long start, long end)> ParseInput(string line)
    {
        return line.Split(",")
            .Select(x => x.Split("-"))
            .Select(x => (long.Parse(x[0]), long.Parse(x[1])))
            .ToList();
    }
}
