namespace AdventOfCode._2025;

using Position = (long x, long y);

public class Day9 : AdventBase
{
    private List<string> rawInput;

    private List<Position> positions;

    public Day9() : base(2025, 9)
    {
        rawInput = base.Input.ToList();

        positions = ParseInput();
    }

    public override long PartOne()
    {
        var areas = AreaOfEach();

        return areas.MaxBy(x => x.area).area;
    }

    public override long PartTwo()
    {
        return long.MaxValue;
    }

    private List<(Position p1, Position p2, long area)> AreaOfEach()
    {
        HashSet<(Position p1, Position p2, long area)> areas = [];
        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = 0; j < positions.Count; j++)
            {
                if (i == j) continue;
                Position p1 = positions[i];
                Position p2 = positions[j];
                long area = (Math.Abs(p1.x - p2.x) + 1) * (Math.Abs(p1.y - p2.y) + 1);
                if (areas.Contains((p2, p1, area)))
                {
                    continue;
                }

                areas.Add((p1, p2, area));
            }
        }

        return areas.ToList();
    }

    private List<Position> ParseInput()
    {
        return rawInput
            .Select(x => (long.Parse(x.Split(",")[0]), long.Parse(x.Split(",")[1])))
            .ToList();
    }
}