using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day6 : AdventBase
{
    private readonly char[][] input;
    private Direction currentDirection;
    private bool foundLoop = false;
    
    public Day6() : base(2024, 6)
    {
        this.input = Input.Select(x => x.ToCharArray()).ToArray();
    }

    public override long PartOne()
    {
        return GetAllVisited()
            .Select(x => x.position)
            .ToHashSet()
            .Count;
    }

    public override long PartTwo()
    {
        long possibleSpots = 0;
        Point curPos = GetStartPosOfGuard();
        HashSet<Point> visited = GetAllVisited()
            .Select(x => x.position)
            .Where(x => x != curPos)
            .ToHashSet();
        
        foreach (Point pos in visited)
        {
            char valueBefore = input[pos.y][pos.x];
            input[pos.y][pos.x] = '#';
            GetAllVisited();
            if (foundLoop)
            {
                possibleSpots++;
            }
            input[pos.y][pos.x] = valueBefore;
        }

        return possibleSpots;
    }

    private HashSet<(Point position, Direction direction)> GetAllVisited()
    {
        foundLoop = false;
        
        Point curPos = GetStartPosOfGuard();
        HashSet<(Point points, Direction direction)> visited = [];
        while (visited.Add((curPos, currentDirection)))
        {
            try
            {
                curPos = GetNextPos(curPos);
            }
            catch (IndexOutOfRangeException _)
            {
                return visited;
            }
        }
        
        foundLoop = true;
        return visited;
    }

    private Point GetNextPos(Point curPos)
    {
        Point nextPos = currentDirection switch
        {
            Direction.NORTH => (curPos.x, curPos.y - 1),
            Direction.SOUTH => (curPos.x, curPos.y + 1),
            Direction.EAST => (curPos.x + 1, curPos.y),
            Direction.WEST => (curPos.x - 1, curPos.y),
        };
        
        if (input[nextPos.y][nextPos.x] == '#')
        {
            currentDirection = (Direction)((int)(this.currentDirection + 1) % 4);
            return curPos;
        }
        return nextPos;
    }

    private Point GetStartPosOfGuard()
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                currentDirection = input[y][x] switch
                {
                    'v' => Direction.SOUTH,
                    '^' => Direction.NORTH,
                    '>' => Direction.EAST,
                    '<' => Direction.WEST,
                    _ => Direction.NORTH
                };
                if (input[y][x] is '^' or 'v' or '<' or '>')
                {
                    return (x, y);
                }
            }
        }
        throw new InvalidDataException();
    }

    private enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
}