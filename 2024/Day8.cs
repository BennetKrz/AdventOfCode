using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day8 : AdventBase
{
    private readonly string[] input;
    private bool partTwo;
    private Dictionary<char, List<Point>> antennaPositions;
    
    public Day8() : base(2024, 8)
    {
        input = Input;
    }

    public override long PartOne()
    {
        partTwo = false;
        antennaPositions = ParseInput();
        
        return PlaceAllAntiNodes(antennaPositions).Count;
    }

    public override long PartTwo()
    {
        partTwo = true;
        return PlaceAllAntiNodes(antennaPositions).Count;
    }

    private HashSet<Point> PlaceAllAntiNodes(Dictionary<char,List<Point>> allAntennaPositions)
    {
        HashSet<Point> uniqueAntiNodePositions = [];
        
        foreach (char key in allAntennaPositions.Keys)
        {
            List<Point> antennas = allAntennaPositions[key];
            HashSet<Point> antiNodeForFrequency = PlaceAntiNodeForFrequency(antennas);
            
            uniqueAntiNodePositions.UnionWith(antiNodeForFrequency);
        }
        
        return uniqueAntiNodePositions;
    }

    private HashSet<Point> PlaceAntiNodeForFrequency(List<Point> positionsOfAntenna)
    {
        HashSet<Point> antiNodeForFrequency = [];
        
        if(positionsOfAntenna.Count <= 1)
        {
            return antiNodeForFrequency;
        }

        for (int i = 0; i < positionsOfAntenna.Count; i++)
        {
            for (int j = 0; j < positionsOfAntenna.Count; j++)
            {
                if (j == i)
                {
                    continue;
                }
                
                Point point = positionsOfAntenna[i];
                Point delta = CalculateDelta(positionsOfAntenna[i], positionsOfAntenna[j]);

                if (partTwo)
                {
                    PlaceAntiNodePartTwo(point, delta, input[0].Length, input.Length, antiNodeForFrequency);
                }
                else
                {
                    PlaceAntiNodePartOne(point, delta, antiNodeForFrequency);
                }
            }
        }
        
        return antiNodeForFrequency;
    }

    private void PlaceAntiNodePartTwo(Point point, Point delta, int width, int height, HashSet<Point> antiNodeForFrequency)
    {
        antiNodeForFrequency.Add(point);
        Point nextPos = (point.x + delta.x, point.y + delta.y);
        while (nextPos.x >= 0 && nextPos.x < width && nextPos.y >= 0 && nextPos.y < height)
        {
            antiNodeForFrequency.Add(nextPos);
            nextPos = (nextPos.x + delta.x, nextPos.y + delta.y);
        }
    }

    private void PlaceAntiNodePartOne(Point point, Point delta, HashSet<Point> antiNodeForFrequency)
    {
        Point nextPos = (point.x + delta.x, point.y + delta.y);
        if (nextPos.x >= 0 && nextPos.x < input[0].Length && nextPos.y >= 0 && nextPos.y < input.Length)
        {
            antiNodeForFrequency.Add(nextPos);
        }
    }

    private Point CalculateDelta(Point pos1, Point pos2)
    {
        return (pos1.x - pos2.x, pos1.y - pos2.y);
    }

    private Dictionary<char, List<Point>> ParseInput()
    {
        Dictionary<char, List<Point>> positions = [];
        
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                char frequency = input[y][x];
                if (frequency == '.')
                {
                    continue;
                }

                if (!positions.ContainsKey(frequency))
                {
                    positions.Add(frequency, []);
                }
                
                positions[frequency].Add((x, y));
            }
        }
        
        return positions;
    }
}