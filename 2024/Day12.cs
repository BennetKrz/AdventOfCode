using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day12 : AdventBase
{
    private readonly string[] map;
    private readonly List<Point> directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];
    
    private bool IsInBounds(Point p) => p.y >= 0 && p.y < map.Length && p.x >= 0 && p.x < map[p.y].Length;
    
    public Day12() : base(2024, 12)
    {
        this.map = Input;
    }

    public override long PartOne()
    {
        return GetRegions()
            .Sum(x => x.Count * PerimeterOfRegion(x));
    }

    public override long PartTwo()
    {
        List<HashSet<Point>> regions = GetRegions();

        return long.MinValue;
    }

    private List<HashSet<Point>> GetRegions()
    {
        List<HashSet<Point>> regions =
        [
            []
        ];

        int counter = 0;
        HashSet<Point> visited = [];

        for (int y = 0; y < this.map.Length; y++)
        {
            for (int x = 0; x < this.map[y].Length; x++)
            {
                if (visited.Contains((x, y)))
                {
                    continue;
                }

                char regionChar = map[y][x];
                regions[counter].Add((x, y));
                
                Queue<Point> queue = [];
                queue.Enqueue((x, y));

                while (queue.Count > 0)
                {
                    Point current = queue.Dequeue();

                    if (!visited.Add(current))
                    {
                        continue;
                    }

                    List<Point> neighbours = GetNeighbours(current, regionChar);

                    foreach (Point nextPos in neighbours)
                    {
                        if (!visited.Contains(nextPos))
                        {
                            queue.Enqueue(nextPos);
                            regions[counter].Add(nextPos);
                        }
                    }
                }

                counter++;
                regions.Add([]);
            }
        }

        return regions[..^1];
    }

    private long PerimeterOfRegion(HashSet<Point> region)
    {
        return region
            .Sum(p => directions
                .Count(d => !region.Contains((p.x + d.x, p.y + d.y)))
            );
    }

    private List<Point> GetNeighbours(Point p, char value)
    {
        return directions
            .Select(d => new Point(d.x + p.x, d.y + p.y))
            .Where(n => IsInBounds(n) && map[n.y][n.x] == value)
            .ToList();
    }
}