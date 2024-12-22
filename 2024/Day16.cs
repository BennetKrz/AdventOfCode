using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day16 : AdventBase
{
    private readonly string[] input;
    
    public Day16() : base(2024, 16)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        char[][] map = ParseInput();
        Point startingPos = GetStartingPos(map);
        
        return FindCheapestPathDijkstra(map, Direction.East, startingPos);
    }

    private long FindCheapestPathDijkstra(char[][] map, Direction direction, Point startingPos)
    {
        PriorityQueue<(Point pos, Direction dir, int cost), int> queue = new();
        Dictionary<(Point, Direction), int> costDic = [];
        
        queue.Enqueue((startingPos, direction, 0), 0);
        costDic.Add((startingPos, direction), 0);
        
        while (queue.Count > 0)
        {
            (Point curPos, Direction curDir, int curCost) = queue.Dequeue();

            if (map[curPos.y][curPos.x] == 'E')
            {
                return curCost;
            }

            List<(Point, int, Direction)> neighbors = GetNeighbours(map, curPos, curDir);

            foreach ((Point delta, int nextCost, Direction dir) move in neighbors)
            {
                Point nextPos = (curPos.x + move.delta.x, curPos.y + move.delta.y);
                int nextCost = curCost + move.nextCost;
                
                if (!costDic.ContainsKey((nextPos, move.dir)) || costDic[(nextPos, move.dir)] > nextCost)
                {
                    
                    queue.Enqueue((nextPos, move.dir, nextCost), nextCost);
                    if (costDic.ContainsKey((nextPos, move.dir)))
                    {
                        costDic[(nextPos, move.dir)] = nextCost;
                    }
                    else
                    {
                        costDic.Add((nextPos, move.dir), nextCost);
                    }
                }
            }
        }

        return -1;
    }

    private List<(Point, int, Direction)> GetNeighbours(char[][] map, Point pos, Direction dir)
    {
        List<(Point delta, int cost, Direction dir)> nextMoves = GetNextDirections(dir);
        List<(Point delta, int cost, Direction dir)> approved = [];
        for (int i = 0; i < nextMoves.Count; i++)
        {
            Point delta = nextMoves[i].delta;
            if (map[pos.y + delta.y][pos.x + delta.x] != '#')
            {
                approved.Add(nextMoves[i]);
            }
        }
        
        return approved;
    }

    private List<(Point delta, int cost, Direction dir)> GetNextDirections(Direction dir)
    {
        return dir switch
        {
            Direction.North =>
            [
                ((0, -1), 1, Direction.North),
                ((0, 0), 1000, Direction.West),
                ((0, 0), 1000, Direction.East)
            ],
            Direction.East =>
            [
                ((1, 0), 1, Direction.East),
                ((0, 0), 1000, Direction.North),
                ((0, 0), 1000, Direction.South)
            ],
            Direction.South =>
            [
                ((0, 1), 1, Direction.South),
                ((0, 0), 1000, Direction.East),
                ((0, 0), 1000, Direction.West)
            ],
            Direction.West =>
            [
                ((-1, 0), 1, Direction.West),
                ((0, 0), 1000, Direction.North),
                ((0, 0), 1000, Direction.South)
            ]
        };
    }

    private char[][] ParseInput()
    {
        return input
            .Select(x => x.ToCharArray())
            .ToArray();
    }

    private Point GetStartingPos(char[][] map)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 'S')
                {
                    return (x, y);
                }
            }
        }
        
        throw new ArgumentException("Invalid input");
    }

    private enum Direction
    {
        North,
        South,
        East,
        West
    }
}