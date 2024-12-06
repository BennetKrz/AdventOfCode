using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day6 : AdventBase
{
    private readonly string[] input;
    private Direction currentDirection; 
    
    public Day6() : base(2024, 6)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        return GetAllVisited().Count;
    }

    private HashSet<Point> GetAllVisited()
    {
        Point curPos = GetStartPosOfGuard();
        HashSet<Point> visited = [];
        while (true)
        {
            try
            {
                visited.Add(curPos);
                curPos = GetNextPos(curPos);
            }
            catch (IndexOutOfRangeException iore)
            {
                return visited;
            }
        }
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