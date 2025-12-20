namespace AdventOfCode._2025;

using Position = (int x, int y);

public class Day4 : AdventBase
{
    private readonly List<string> input;
    private readonly HashSet<Position> positions;
    private readonly List<Position> offsets = [
        (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (1, -1), (-1, -1), (-1, 1)
    ];

    public Day4() : base(2025, 4)
    {
        input = Input.ToList();
        positions = ParseInput();
    }

    public override long PartOne()
    {
        return positions.Count(p => RollsInArea((p.x, p.y)) < 4);
    }

    public override long PartTwo()
    {
        int removed = 1;
        int sum = 0;
        while (removed > 0)
        {
            var positionsToRemove = positions.Where(p => RollsInArea((p.x, p.y)) < 4).ToList();
            positionsToRemove.ForEach(p => positions.Remove(p));
            removed = positionsToRemove.Count;
            sum += removed;
        }
        return sum;
    }

    private int RollsInArea(Position position)
    {
        return offsets.Count(o => positions.Contains((o.x + position.x, o.y + position.y)));
    }

    private HashSet<Position> ParseInput()
    {
        return input
            .Select((l, y) => l
                .Select((c, x) => (x, y, c))
                .Where(t => t.c == '@'))
            .SelectMany(x => x)
            .Select(x => (x.x, x.y))
            .ToHashSet();
    }
}