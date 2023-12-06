namespace AdventOfCode._2023;

public class Day6 : IAdventDay
{
    public long PartOne()
    {
        long[] possibleWaysCounter = new long[4];
        (long time, long targetDist)[] input =
        {
            (49, 356),
            (87, 1378),
            (78, 1502),
            (95, 1882),
        };
        for (int i = 0; i < input.Length; i++)
        {
            for (int t = 0; t < input[i].time; t++)
            {
                if (CalculateDistance(t, input[i].time) > input[i].targetDist)
                {
                    possibleWaysCounter[i]++;
                }
            }
        }
        
        
        return possibleWaysCounter.Aggregate(1, (product, counter) => (int)(product * counter));
    }

    public long PartTwo()
    {
        long counter = 0;
        (long time, long targetDist) data = (49877895, 356137815021882);
        for (long i = 0; i < data.time; i++)
        {
            if (CalculateDistance(i, data.time) > data.targetDist)
            {
                counter++;
            }
        }

        return counter;
    }

    private long CalculateDistance(long acc, long time)
    {
        return acc * (time - acc);
    }
}