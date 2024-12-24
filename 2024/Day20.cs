using System.Collections;
using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day20 : AdventBase
{
    private readonly string[] input;

    private readonly List<Point> directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];
    
    private HashSet<Point> walls;
    private HashSet<Point> path;
    private Dictionary<Point, int> distances;
    
    public Day20() : base(2024, 20)
    {
        input = Input;
        walls = [];
        path = [];
        distances = [];
    }

    public override long PartOne()
    {
        SetWallsAndPath();

        Point startPos = GetPosOf('S');
        Point endPos = GetPosOf('E');
        
        SetDistances(startPos);

        Dictionary<(Point start, Point end), int> cheatIdentifierAndSavedDistance = [];
        foreach (Point startPositionOfCheat in path)
        {
            List<Point> possibleCheats = GetPossibleCheatPositions(startPositionOfCheat);
            foreach (Point endPositionOfCheat in possibleCheats)
            {
                (Point start, Point end) cheatIdentifier = (startPositionOfCheat, endPositionOfCheat);
                int savedTime = distances[startPositionOfCheat] - distances[endPositionOfCheat] - 2;
                if (savedTime > 0)
                {
                    cheatIdentifierAndSavedDistance[cheatIdentifier] = savedTime;
                }
            }
        }
        
        var grouped = cheatIdentifierAndSavedDistance.GroupBy(x => x.Value).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Count());
        
        return cheatIdentifierAndSavedDistance
            .Count(x => x.Value >= 100);
    }

    private List<Point> GetPossibleCheatPositions(Point startPos)
    {
        List<Point> positionsInWall = directions
            .Select(d => (startPos.x + d.x, startPos.y + d.y))
            .Where(p => walls.Contains(p))
            .ToList();

        List<Point> positionsInPath = [];

        foreach (Point positionInWall in positionsInWall)
        {
            List<Point> nextPos = directions
                .Select(d => (positionInWall.x + d.x, positionInWall.y + d.y))
                .Where(p => path.Contains(p))
                .Where(p => p != startPos)
                .ToList();
            
            positionsInPath.AddRange(nextPos);
        }
        
        return positionsInPath;
    }

    private void SetDistances(Point startPos)
    {
        Stack<Point> stack = [];
        HashSet<Point> visited = [];
        Dictionary<Point, int> posSteps = [];
        int step = 0;
        
        stack.Push(startPos);
        
        while (stack.Count > 0)
        {
            Point cur = stack.Pop();
            
            visited.Add(cur);
            posSteps.Add(cur, step++);

            List<Point> nextPositions = GetNextPossiblePositions(cur);
            foreach (Point nextPosition in nextPositions)
            {
                if (!visited.Contains(nextPosition) && !walls.Contains(nextPosition))
                {
                    stack.Push(nextPosition);
                }
            }
        }

        int max = posSteps.Values.Max();
        distances = posSteps.Select(x => (x.Key, max - x.Value)).ToDictionary(x => x.Key, x => x.Item2);
    }

    private List<Point> GetNextPossiblePositions(Point curPos)
    {
        return directions.Select(d => (curPos.x + d.x, curPos.y + d.y))
            .ToList();
    }
    
    private Point GetPosOf(char target)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == target)
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }

    private void SetWallsAndPath()
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#')
                {
                    walls.Add((x, y));
                }
                else
                {
                    path.Add((x, y));
                }
            }
        }
    }
}