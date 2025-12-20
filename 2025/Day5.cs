namespace AdventOfCode._2025;

public class Day5 : AdventBase
{
    private readonly List<string> input;

    public Day5() : base(2025, 5)
    {
        input = base.Input.ToList();
    }

    public override long PartOne()
    {
        var ranges =  GetRanges();
        var ids =  GetIdsToCheck();

        int fresh = 0;

        foreach (long id in ids)
        {
            foreach (Range range in ranges)
            {
                if (range.IsInRange(id))
                {
                    fresh++;
                    break;
                }
            }
        }
        return fresh;
    }

    public override long PartTwo()
    {
        var ranges = GetRanges().OrderByDescending(x => x.End - x.Start).ToList();

        List<Range> finalRanges = [];

        foreach (Range rangeToCheck in ranges)
        {
            if (finalRanges.Any(r => r.IsFullyEnclosed(rangeToCheck)))
            {
                continue;
            }

            var allOverlaps = finalRanges.Where(r => r.Overlaps(rangeToCheck)).ToList();
            if (allOverlaps.Count == 0)
            {
                finalRanges.Add(rangeToCheck);
                continue;
            }

            var r = new Range
            {
                Start = rangeToCheck.Start,
                End = rangeToCheck.End
            };
            foreach (Range overlap in allOverlaps)
            {
                var newRange = overlap.UnionRange(r);
                finalRanges.Remove(overlap);
                finalRanges.Remove(r);
                finalRanges.Add(newRange);
                r = newRange;
            }
        }

        return finalRanges.Sum(x => (x.End - x.Start) + 1);
    }

    private List<Range> GetRanges()
    {
        return input
            .Where(l => l.Contains('-'))
            .Select(l => l.Split("-"))
            .Select(x => new Range {Start = long.Parse(x[0]), End = long.Parse(x[1])})
            .ToList();
    }

    private List<long> GetIdsToCheck()
    {
        return input
                .Where(l => !l.Contains('-'))
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(long.Parse)
                .ToList();
    }
}

internal class Range
{
    public long Start { get; init; }
    public long End { get; init; }

    public bool IsInRange(long value) =>  value >= Start && value <= End;

    public bool Overlaps(Range range) => IsInRange(range.Start) || IsInRange(range.End);

    public bool IsFullyEnclosed(Range other) => IsInRange(other.Start) && IsInRange(other.End);

    public Range UnionRange(Range other)
    {
        return new Range { Start = Math.Min(Start, other.Start), End = Math.Max(End, other.End) };
    }
}