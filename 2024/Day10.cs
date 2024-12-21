using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day10 : AdventBase
{
    private readonly List<Point> directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];
    private readonly string[] map;
    private bool partTwo;
    
    private bool IsInBounds(Point p) => p.y >= 0 && p.y < map.Length && p.x >= 0 && p.x < map[p.y].Length;
    
    public Day10() : base(2024, 10)
    {
        this.map = Input;
    }

    public override long PartOne()
    {
        partTwo = false;
        
        return FindStartingPoints()
            .Select(GetScore)
            .Sum();
    }

    public override long PartTwo()
    {
        partTwo = true;
        
        return FindStartingPoints()
            .Select(GetScore)
            .Sum();
    }

    private int GetScore(Point p)
    {
        int endsFound = 0;
        
        HashSet<Point> visited = [];
        Queue<Point> queue = [];
        
        queue.Enqueue(p);

        while (queue.Count > 0)
        {
            Point pos = queue.Dequeue();
            visited.Add(pos);
            
            List<Point> nextPositions = GetNextPositions(pos);
            foreach (Point nextPos in nextPositions)
            {
                if (!visited.Contains(nextPos))
                {
                    queue.Enqueue(nextPos);
                    if (map[nextPos.y][nextPos.x] == '9')
                    {
                        endsFound++;
                        if (!partTwo)
                        {
                            visited.Add(nextPos);
                        }
                    }
                }
            }
        }
        
        return endsFound;
    }

    private List<Point> GetNextPositions(Point p)
    {
        return directions
            .Select(d => new Point(d.x + p.x, d.y + p.y))
            .Where(IsInBounds)
            .Where(n => map[p.y][p.x] == map[n.y][n.x] - 1)
            .ToList();
    }

    private List<Point> FindStartingPoints()
    {
        List<Point> startingPoints = [];
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == '0')
                {
                    startingPoints.Add((x, y));
                }
            }
        }
        
        return startingPoints;
    }
}