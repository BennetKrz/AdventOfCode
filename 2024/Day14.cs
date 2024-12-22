using System.Text;
using Point = (double x, double y);

namespace AdventOfCode._2024;

public class Day14 : AdventBase
{
    private readonly string[] input;
    
    private const int FieldSizeX = 101;
    private const int FieldSizeY = 103;
    
    public Day14() : base(2024, 14)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        List<List<(Point position, Point velocity)>> allFrames =
        [
            ParseInput()
        ];
        
        for (int sec = 0; sec < 100; sec++)
        {
            allFrames.Add(SimulateOneSecond(allFrames[^1]));
        }

        return CalculateQuadrants(allFrames[^1]);
    }

    public override long PartTwo()
    {
        List<(List<(Point position, Point velocity)> robotPositions, double entropy)> allFrames =
        [
            (ParseInput(), CalculateEntropy(ParseInput().Select(x => x.position).ToList(), 5))
        ];

        for (int sec = 0; sec < 10_000; sec++)
        {
            List<(Point position, Point velocity)> result = SimulateOneSecond(allFrames[^1].robotPositions);
            allFrames.Add((result, CalculateEntropy(result.Select(x => x.position).ToList(), 5)));
        }

        int min = allFrames.Select((e, i) => (i, e)).MinBy(x => x.e.entropy).i;
        
        List<(Point position, Point velocity)> robots = allFrames[min].robotPositions;
        
        PrintRobots(robots, min);
        
        return min;
    }

    private long CalculateQuadrants(List<(Point position, Point velocity)> robotPositions)
    {
        const int halveX = FieldSizeX / 2;
        const int halveY = FieldSizeY / 2;

        int countFirstQ = robotPositions.Count(x => x.position is { x: < halveX, y: < halveY });
        int countSecondQ = robotPositions.Count(x => x.position is { x: > halveX, y: < halveY });
        int countThirdQ = robotPositions.Count(x => x.position is { x: < halveX, y: > halveY });
        int coutFourthQ = robotPositions.Count(x => x.position is { x: > halveX, y: > halveY });
        
        return countFirstQ * countSecondQ * countThirdQ * coutFourthQ;
    }

    private List<(Point position, Point velocity)> SimulateOneSecond(List<(Point position, Point velocity)> robotPositions)
    {
        return robotPositions.Select(CalculateNextPosOfRobot).ToList();
    }

    private (Point position, Point velocity) CalculateNextPosOfRobot((Point position, Point velocity) robotPosition)
    {
        Point curPos = robotPosition.position;
        Point vel = robotPosition.velocity;
        
        Point nextPos = (curPos.x + vel.x, curPos.y + vel.y);

        if (nextPos.x >= FieldSizeX)
        {
            nextPos.x -= FieldSizeX;
        }

        if (nextPos.y >= FieldSizeY)
        {
            nextPos.y -= FieldSizeY;
        }

        if (nextPos.x < 0)
        {
            nextPos.x += FieldSizeX;
        }

        if (nextPos.y < 0)
        {
            nextPos.y += FieldSizeY;
        }

        return (nextPos, vel);
    }

    private List<(Point position, Point velocity)> ParseInput()
    {
        List<(Point position, Point velocity)> robotPositions = [];

        foreach (string line in input)
        {
            string[] parts = line.Split(" ");
            
            string[] splitPos = parts[0].Split("=");
            int x = int.Parse(splitPos[1].Split(",")[0]);
            int y = int.Parse(splitPos[1].Split(",")[1]);

            string[] splitVel = parts[1].Split("=");
            int vx = int.Parse(splitVel[1].Split(",")[0]);
            int vy = int.Parse(splitVel[1].Split(",")[1]);
            
            robotPositions.Add(((x, y), (vx, vy)));
        }
        
        return robotPositions;
    }

    private void PrintRobots(List<(Point position, Point velocity)> robotPositions, int second)
    {
        StringBuilder sb = new();
        for (int y = 0; y < FieldSizeY; y++)
        {
            for (int x = 0; x < FieldSizeX; x++)
            {
                int count = robotPositions.Count(r => r.position.x == x && r.position.y == y);
                sb.Append(count == 0 ? "." : count.ToString());
            }
            
            sb.AppendLine();
        }
        
        sb.AppendLine($"This is the {second} second");
        sb.AppendLine();

        Console.WriteLine(sb.ToString());
    }
    
    private static double CalculateEntropy(List<(double x, double y)> points, double binSize)
    {
        var binCounts = new Dictionary<(int, int), int>();
        foreach (var point in points)
        {
            int binX = (int)(point.x / binSize);
            int binY = (int)(point.y / binSize);
            var bin = (binX, binY);
            if (!binCounts.TryAdd(bin, 1))
                binCounts[bin]++;
        }

        // Step 2: Calculate total number of points
        int totalPoints = points.Count;

        // Step 3: Calculate probabilities and entropy
        double entropy = 0.0;
        foreach (var count in binCounts.Values)
        {
            double probability = (double)count / totalPoints;
            entropy -= probability * Math.Log2(probability);
        }

        return entropy;
    }
}