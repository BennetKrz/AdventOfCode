namespace AdventOfCode._2022;

public class Day14 : AdventBase
{
    private readonly string[] input;
    private int maxY;

    public Day14() : base(2022, 14)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        Dictionary<(int x, int y), char> map = ParseInput();
        maxY = map.Max(p => p.Key.y);
        SaveMapToFile(map);
        int sandUnitCounter = 0;
        while (true)
        {
            SandUnit sandGrain = new SandUnit(500, 0);
            map = ApplySandPhysicsPartOne(map, sandGrain);
            if (map == null)
            {
                return sandUnitCounter;
            }

            sandUnitCounter++;
        }
    }

    public override long PartTwo()
    {
        Dictionary<(int x, int y), char> map = ParseInput();
        maxY = map.Max(p => p.Key.y);
        SaveMapToFile(map);
        int sandUnitCounter = 0;
        while (true)
        {
            SandUnit sandGrain = new SandUnit(500, 0);
            map = ApplySandPhysicsPartTwo(map, sandGrain);
            if (map == null)
            {
                return sandUnitCounter + 1;
            }

            sandUnitCounter++;
        }
    }

    Dictionary<(int x, int y), char> ApplySandPhysicsPartTwo(Dictionary<(int x, int y), char> map, SandUnit sandGrain)
    {
        while (!sandGrain.IsResting)
        {
            int posX = sandGrain.Position.x;
            int posY = sandGrain.Position.y;
            if (posY == maxY + 1)
            {
                map.Add((posX, posY), sandGrain.SandChar);
                return map;
            }

            //Direkt unter dem Sandkorn ist nichts
            if (!map.ContainsKey((posX, posY + 1)))
            {
                sandGrain.Position = (posX, posY + 1);
                continue;
            }

            //direkt unter dem sandkorn ist was, also weiter links unten gucken...
            if (!map.ContainsKey((posX - 1, posY + 1)))
            {
                sandGrain.Position = (posX - 1, posY + 1);
                continue;
            }

            //unten links ist blockiert, also unten rechts gucken.
            if (!map.ContainsKey((posX + 1, posY + 1)))
            {
                sandGrain.Position = (posX + 1, posY + 1);
                continue;
            }

            //Alle drei bewegungsmöglichkeiten sind nicht möglich:
            sandGrain.IsResting = true;
            map.Add(sandGrain.Position, sandGrain.SandChar);
            if (posY == 0)
            {
                return null;
            }
        }

        return map;
    }

    Dictionary<(int x, int y), char> ApplySandPhysicsPartOne(Dictionary<(int x, int y), char> map, SandUnit sandGrain)
    {
        while (!sandGrain.IsResting)
        {
            int posX = sandGrain.Position.x;
            int posY = sandGrain.Position.y;
            if (posY > maxY)
            {
                return null;
            }

            //Direkt unter dem Sandkorn ist nichts
            if (!map.ContainsKey((posX, posY + 1)))
            {
                sandGrain.Position = (posX, posY + 1);
                continue;
            }

            //direkt unter dem sandkorn ist was, also weiter links unten gucken...
            if (!map.ContainsKey((posX - 1, posY + 1)))
            {
                sandGrain.Position = (posX - 1, posY + 1);
                continue;
            }

            //unten links ist blockiert, also unten rechts gucken.
            if (!map.ContainsKey((posX + 1, posY + 1)))
            {
                sandGrain.Position = (posX + 1, posY + 1);
                continue;
            }

            //Alle drei bewegungsmöglichkeiten sind nicht möglich:
            sandGrain.IsResting = true;
            map.Add(sandGrain.Position, sandGrain.SandChar);
        }

        return map;
    }

    Dictionary<(int x, int y), char> ParseInput()
    {
        Dictionary<(int x, int y), char> map = new();
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            string[] instructions = line.Split(" -> ");
            (int x, int y) startPoint = (int.Parse(instructions[0].Split(',')[0]),
                int.Parse(instructions[0].Split(',')[1]));
            for (int j = 1; j < instructions.Length; j++)
            {
                (int x, int y) endPoint = (int.Parse(instructions[j].Split(',')[0]), int.Parse(instructions[j].Split(',')[1]));
                if (startPoint.x == endPoint.x)
                {
                    if (startPoint.y < endPoint.y)
                    {
                        for (int y = startPoint.y; y <= endPoint.y; y++)
                        {
                            map.TryAdd((startPoint.x, y), '#');
                        }
                    }
                    else
                    {
                        for (int y = endPoint.y; y <= startPoint.y; y++)
                        {
                            map.TryAdd((startPoint.x, y), '#');
                        }
                    }
                }
                else
                {
                    if (startPoint.x < endPoint.x)
                    {
                        for (int x = startPoint.x; x <= endPoint.x; x++)
                        {
                            map.TryAdd((x, startPoint.y), '#');
                        }
                    }
                    else
                    {
                        for (int x = endPoint.x; x <= startPoint.x; x++)
                        {
                            map.TryAdd((x, startPoint.y), '#');
                        }
                    }
                }

                startPoint = endPoint;
            }
        }

        return map;
    }

    void SaveMapToFile(Dictionary<(int x, int y), char> map)
    {
        int maxX = map.Max(p => p.Key.x) + 1;
        int maxY = map.Max(p => p.Key.y) + 1;

        string output = string.Empty;
        for (int y = 0; y < maxY; y++)
        {
            string part = string.Empty;
            for (int x = 0; x < maxX; x++)
            {
                if (map.ContainsKey((x, y)))
                {
                    part += map[(x, y)];
                }
                else
                {
                    part += '.';
                }
            }

            output += part + "\n";
        }

        File.WriteAllText(@"G:\C#\AdventOfCode\2022\Input\DiffHelperDay14.txt", output);
    }
}

public class SandUnit(int x, int y)
{
    public (int x, int y) Position { get; set; } = (x, y);
    public bool IsResting { get; set; } = false;
    public readonly char SandChar = 'o';
}