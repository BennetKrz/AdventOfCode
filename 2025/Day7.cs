namespace AdventOfCode._2025;

using Position = (int x, int y);

public class Day7 : AdventBase
{
    private List<string> input;

    public Day7() : base(2025, 7)
    {
        input = base.Input.ToList();
    }

    public override long PartOne()
    {
        HashSet<Position> beamsToSimulate = GetPositionOfStart();
        HashSet<Position> spliters = GetSpliters();

        long count = 0;

        for (int y = 0; y < input.Count - 1; y++)
        {
            HashSet<Position> nextPositions = [];
            foreach (Position beam in beamsToSimulate)
            {
                var oneDown = (beam.x, beam.y + 1);
                if (spliters.Contains(oneDown))
                {
                    var left = (beam.x - 1, beam.y + 1);
                    var right = (beam.x + 1, beam.y + 1);

                    nextPositions.Add(left);
                    nextPositions.Add(right);

                    count++;
                }
                else
                {
                    nextPositions.Add(oneDown);
                }
            }

            beamsToSimulate = nextPositions;
        }

        return count;
    }

    public override long PartTwo()
    {
        long[] counter = new long[input[0].Length];
        int startX = GetPositionOfStart().First().x;
        counter[startX] = 1;

        for (int y = 0; y < input.Count; y++)
        {
            long[] nextCounter = new long[input[0].Length];

            for (int x = 0; x < input[y].Length; x++)
            {
                if (counter[x] == 0) continue;

                long currentCount = counter[x];

                int nextY = y + 1;
                if (nextY >= input.Count)
                {
                    continue;
                }

                char nextCell = input[nextY][x];

                if (nextCell == '^')
                {
                    if (x - 1 >= 0) nextCounter[x - 1] += currentCount;
                    if (x + 1 < input[0].Length) nextCounter[x + 1] += currentCount;
                }
                else
                {
                    nextCounter[x] += currentCount;
                }
            }

            if (y != input.Count - 1)
            {
                counter = nextCounter;
            }
        }

        return counter.Sum();
    }

    private HashSet<Position> GetSpliters()
    {
        HashSet<Position> positions = [];

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '^')
                {
                    positions.Add((x, y));
                }
            }
        }

        return positions;
    }

    private HashSet<Position> GetPositionOfStart()
    {
        for (int i = 0; i < input.Count; i++)
        {
            int index = input[i].IndexOf('S');
            if (index != -1)
            {
                return [(index, i)];
            }
        }

        return [(-1, -1)];
    }
}