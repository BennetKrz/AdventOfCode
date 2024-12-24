using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day18 : AdventBase
{
    private readonly List<Point> directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];
    
    private const int sizeX = 70;
    private const int sizeY = 70;

    private const int bytesToTake = 1024;
    
    private readonly string[] input;
    
    private bool IsInBounds(Point p) => p.x is >= 0 and <= sizeX && p.y is >= 0 and <= sizeY;
    
    public Day18() : base(2024, 18)
    {
        input = Input;
    }

    public override long PartOne()
    {
        HashSet<Point> blockedPositions = GetFallingBytes(bytesToTake)
            .ToHashSet();

        return FindShortestPath(blockedPositions);
    }

    public override long PartTwo()
    {
        Point before = (-1, -1);
        
        for (int i = input.Length - 1; i >= 0; i--)
        {
            HashSet<Point> blockedPositions = GetFallingBytes(i)
                .ToHashSet();

            
            long result = FindShortestPath(blockedPositions);
            if (result != -1)
            {
                Console.WriteLine($"No Path found when byte with position: {before} falls...");
                break;
            }
            before = blockedPositions.Last();
        }

        return long.MinValue;
    }

    private long FindShortestPath(HashSet<Point> blockedPositions)
    {
        PriorityQueue<(Point pos, int cost), int> queue = new();
        Dictionary<Point, int> costs = [];
        
        queue.Enqueue(((0, 0), 0), 0);
        costs[(0, 0)] = 0;

        while (queue.Count > 0)
        {
            (Point pos, int cost) = queue.Dequeue();

            if (pos == (sizeX, sizeY))
            {
                return cost;
            }

            List<Point> nextPositions = GetNeighbours(blockedPositions, pos);
            foreach (Point nextPosition in nextPositions)
            {
                int nextCosts = cost + 1;
                
                if (!costs.ContainsKey(nextPosition) || nextCosts < costs[pos])
                {
                    queue.Enqueue((nextPosition, nextCosts), nextCosts);
                    costs[nextPosition] = nextCosts;
                }
            }
        }

        return -1;
    }

    private List<Point> GetNeighbours(HashSet<Point> blockedPositions, Point pos)
    {
        return directions.Where(d => !blockedPositions.Contains((pos.x + d.x, pos.y + d.y)))
            .Where(d => IsInBounds((pos.x + d.x, pos.y + d.y)))
            .Select(d => (pos.x + d.x, pos.y + d.y))
            .ToList();
    }

    private IEnumerable<Point> GetFallingBytes(int n)
    {
        return input
            .Select(x => (int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1])))
            .Take(n);
    }
}