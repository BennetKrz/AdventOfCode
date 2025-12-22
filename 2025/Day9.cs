using System.Text;

namespace AdventOfCode._2025;

using Position = (long x, long y);

public class Day9 : AdventBase
{
    private readonly List<string> rawInput;
    private readonly List<Position> positions;

    public Day9() : base(2025, 9)
    {
        rawInput = base.Input.ToList();

        positions = ParseInput();
    }

    public override long PartOne()
    {
        return AreaOfEach().MaxBy(x => x.area).area;
    }

    public override long PartTwo()
    {
        var areas = AreaOfEach();
        var outline = GetOutline();

        for (int i = 0; i < areas.Count; i++)
        {
            (Position p1, Position p2, long area) = areas[i];

            var directionsToTake = GetMoveDirectionRect(p1, p2);
            var corners = GetRectangle(p1, p2, directionsToTake);


        }

        //DebugPrint(outline);
        return long.MaxValue;
    }

    private void DebugPrint(HashSet<Position> outline)
    {
        StringBuilder sb = new();
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 14; x++)
            {
                sb.Append(outline.Contains((x, y)) ? '#' : '.');
            }
            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }

    private HashSet<Position> GetOutline()
    {
        positions.Add(positions[0]);

        HashSet<Position> linePoints = [];

        for (int i = 0; i < positions.Count - 1; i++)
        {
            linePoints.Add(positions[i]);
            Position start = positions[i];
            Position target = positions[i + 1];

            Position cur = start;
            Position direction = GetMoveDirectionOutline(start, target);
            while (cur != target)
            {
                cur = (cur.x + direction.x, cur.y + direction.y);
                linePoints.Add(cur);
            }
        }

        return linePoints;
    }

    private Position GetMoveDirectionOutline(Position p1, Position p2)
    {
        if (p1.x == p2.x)
        {
            if (p1.y < p2.y) return (0, 1);
            return (0, -1);
        }

        if (p1.x < p2.x) return (1, 0);
        return (-1, 0);
    }

    private List<Position> GetMoveDirectionRect(Position p1, Position p2)
    {
        if (p1.x < p2.x && p1.y > p2.y)
        {
            return [(0, -1), (1, 0), (0, 1), (-1, 0)];
        }

        if (p1.x > p2.x && p1.y > p2.y)
        {
            return [(-1, 0), (0, -1), (1, 0), (0, 1)];
        }

        if (p1.x < p2.x && p1.y < p2.y)
        {
            return [(1, 0), (0, 1), (-1, 0), (0, -1)];
        }

        if (p1.x > p2.x && p1.y < p2.y)
        {
            return [(0, 1), (-1, 0), (0, -1), (1, 0)];
        }

        throw new Exception("Should never happen");
    }

    private (Position p1, Position p2, Position p3, Position p4) GetRectangle(Position p1, Position p2, List<Position> directions)
    {
        if (directions[0] == (0, 1))
        {
            return (p1, (p1.x, p2.y), p2, (p2.x, p1.y));
        }

        if (directions[0] == (1, 0))
        {
            return (p1, (p2.x, p1.y), p2, (p1.x, p2.y));
        }

        if (directions[0] == (-1, 0))
        {
            return (p1, (p2.x, p1.y), p2, (p1.x, p2.y));
        }

        if (directions[0] == (0, -1))
        {
            return (p1, (p1.x, p2.y), p2, (p2.x, p1.y));
        }

        throw new Exception("Should never happen");
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
                if (areas.Contains((p2, p1, area))) continue;

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