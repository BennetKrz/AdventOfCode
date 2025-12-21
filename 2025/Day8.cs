namespace AdventOfCode._2025;

public class Day8 : AdventBase
{
    private readonly List<string> input;

    private const int Limit = 1000;

    public Day8() : base(2025, 8)
    {
        input = base.Input.ToList();
    }

    public override long PartOne()
    {
        List<Position> positions = ParseInput();
        var closest = OrderByClosest(positions);

        List<HashSet<Position>> circuits = [];

        for (int i = 0; i < Limit; i++)
        {
            (Position p1, Position p2) = closest[i];
            var indices = GetCircuits(circuits, p1, p2);
            if (indices is { i1: -1, i2: -1 })
            {
                circuits.Add([p1, p2]);
            } else if (indices.i1 == -1)
            {
                circuits[indices.i2].Add(p1);
            } else if (indices.i2 == -1)
            {
                circuits[indices.i1].Add(p2);
            }
            else
            {
                var c1 = circuits[indices.i1];
                var c2 = circuits[indices.i2];

                var result = c1.Union(c2).ToHashSet();
                circuits.Remove(c1);
                circuits.Remove(c2);
                circuits.Add(result);
            }
        }

        var sorted = circuits.OrderByDescending(x => x.Count).Select(x => x.Count).Take(3).ToList();

        return sorted[0] * sorted[1] * sorted[2];
    }

    private (int i1, int i2) GetCircuits(List<HashSet<Position>> circuits, Position p1, Position p2)
    {
        (int i1, int i2) result = (-1, -1);
        for (int i = 0; i < circuits.Count; i++)
        {
            if (circuits[i].Contains(p1))
            {
                result = (i, result.i2);
            }
            else if (circuits[i].Contains(p2))
            {
                result = (result.i1, i);
            }
        }

        return result;
    }

    public override long PartTwo()
    {
        return long.MaxValue;
    }

    private List<(Position p1, Position p2)> OrderByClosest(List<Position> positions)
    {
        HashSet<(Position p1, Position p2, double distance)> distances = [];
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < positions.Count; j++)
            {
                if (i == j) continue;

                double distance = positions[i].Distance(positions[j]);
                if (distances.Contains((positions[j], positions[i], distance)))
                {
                    continue;
                }
                distances.Add((positions[i], positions[j], distance));
            }
        }

        return distances.OrderBy(x => x.distance).Select(x => (x.p1, x.p2)).ToList();
    }

    private List<Position> ParseInput()
    {
        return input.Select(x =>
        {
            var s = x.Split(',');
            return new Position(long.Parse(s[0]), long.Parse(s[1]), long.Parse(s[2]));
        }).ToList();
    }

    private class Position(long x, long y, long z)
    {
        private long X { get; } = x;
        private long Y { get; } = y;
        private long Z { get; } = z;

        public double Distance(Position o)
        {
            return Math.Sqrt(Math.Pow(X - o.X, 2) + Math.Pow(Y - o.Y, 2) + Math.Pow(Z - o.Z, 2));
        }
    }
}