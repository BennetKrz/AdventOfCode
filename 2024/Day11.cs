namespace AdventOfCode._2024;

public class Day11 : AdventBase
{
    private Dictionary<long, long> stonesOriginal;

    public Day11() : base(2024, 11)
    {
        this.stonesOriginal = InputOneLine
            .Split(' ')
            .Select(long.Parse)
            .ToDictionary(x => x, x => 1L);
    }

    public override long PartOne()
    {
        Dictionary<long, long> stones = new(stonesOriginal);

        for (int blink = 0; blink < 25; blink++)
        {
            stones = Blink(stones);
        }

        return stones.Sum(x => x.Value);
    }

    public override long PartTwo()
    {
        Dictionary<long, long> stones = new(stonesOriginal);

        for (int blink = 0; blink < 75; blink++)
        {
            stones = Blink(stones);
        }

        return stones.Sum(x => x.Value);
    }

    private Dictionary<long, long> Blink(Dictionary<long, long> stones)
    {
        Dictionary<long, long> newStones = [];

        foreach (KeyValuePair<long, long> pair in stones)
        {
            long stone = pair.Key;
            int numberOfDigits = NumberOfDigits(stone);

            if (stone == 0)
            {
                long result = 1;
                if (!newStones.TryAdd(result, pair.Value))
                {
                    newStones[result] += pair.Value;
                }
            }
            else if (numberOfDigits % 2 == 0)
            {
                int halfDigits = numberOfDigits / 2;
                long divisor = (long)Math.Pow(10, halfDigits);

                long rightHalve = stone % divisor;
                long leftHalve = stone / divisor;

                if (!newStones.TryAdd(leftHalve, pair.Value))
                {
                    newStones[leftHalve] += pair.Value;
                }

                if (!newStones.TryAdd(rightHalve, pair.Value))
                {
                    newStones[rightHalve] += pair.Value;
                }
            }
            else
            {
                long result = stone * 2024;
                if (!newStones.TryAdd(result, pair.Value))
                {
                    newStones[result] += pair.Value;
                }
            }
        }

        return newStones;
    }

    private int NumberOfDigits(long number)
    {
        return (int)Math.Floor(Math.Log10(number) + 1);
    }
}