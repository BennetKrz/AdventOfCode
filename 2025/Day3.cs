namespace AdventOfCode._2025;

using IndexValue = (int index, long value);

public class Day3 : AdventBase
{
    private readonly List<List<IndexValue>> input;

    public Day3() : base(2025, 3)
    {
        input = ParseInput(base.Input);
    }

    public override long PartOne()
    {
        long sum = 0;
        foreach (var bank in input)
        {
            var max = bank[..^1].MaxBy(x => x.value);
            var secondMax = bank[(max.index + 1)..].MaxBy(x => x.value);

            sum += max.value * 10 + secondMax.value;
        }
        return sum;
    }

    public override long PartTwo()
    {
        long sum = 0;

        foreach (var bank in input)
        {
            List<IndexValue> numbers =
            [
                bank.Where(x => x.index < bank.Count - 11).MaxBy(x => x.value)
            ];

            for (int i = 0; i < 11; i++)
            {
                var filtered = bank
                    .Where(x => x.index < (bank.Count - (11 - i) + 1) && x.index > (numbers[^1].index));
                long max = filtered.MaxBy(x => x.value).value;
                var lowestIndex = filtered.Where(x => x.value == max).OrderBy(x => x.index).First();

                numbers.Add(lowestIndex);
            }

            long number = 0;
            for (int i = 12; i > 0; i--)
            {
                number += numbers[^i].value * (long)Math.Pow(10, i - 1);
            }

            Console.WriteLine(number);

            sum += number;
        }
        return sum;
    }

    private List<List<IndexValue>> ParseInput(string[] strings)
    {
        return strings
            .Select(x =>
                x.Select((c, i) => (i, long.Parse(c.ToString())))
                    .ToList())
            .ToList();
    }
}