namespace AdventOfCode._2023;

public class Day5 : AdventBase
{
    private readonly List<string> input;

    public Day5() : base(2023, 5)
    {
        this.input = Input.ToList();
    }
    public override long PartOne()
    {
        List<long> seeds = GetSeeds();
        
        List<(long start, long dest, long length)>[] maps = ParseInput();

        List<long> results = new();

        foreach (long tmpSeed in seeds)
        {
            long seed = tmpSeed;
            foreach (List<(long start, long dest, long length)> currentMap in maps)
            {
                foreach ((long start, long dest, long length) cur in currentMap)
                {
                    if (seed >= cur.start && seed < cur.start + cur.length)
                    {
                        seed = (seed - cur.start) + cur.dest;
                        break;
                    }
                }
            }
            results.Add(seed);
        }
        
        return results.Min();
    }


    //Runtime 22 Minuten xD - 10 als exe
    public override long PartTwo()
    {
        List<long> rawSeeds = GetSeeds();
        List<(long start, long length)> seeds = new List<(long start, long length)>();
        for (int i = 0; i < rawSeeds.Count; i += 2)
        {
            seeds.Add((rawSeeds[i], rawSeeds[i + 1]));
        }
        
        List<(long start, long dest, long length)>[] maps = ParseInput();

        long lowestResult = long.MaxValue;

        for (int i = 0; i < seeds.Count; i++)
        {
            for (long s = 0; s < seeds[i].length; s++)
            {
                long seed = seeds[i].start + s;
                foreach (List<(long start, long dest, long length)> currentMap in maps)
                {
                    foreach ((long start, long dest, long length) cur in currentMap)
                    {
                        if (seed >= cur.start && seed < cur.start + cur.length)
                        {
                            seed = (seed - cur.start) + cur.dest;
                            break;
                        }
                    }
                }

                if (seed < lowestResult)
                {
                    lowestResult = seed;
                }
            }

            Console.WriteLine($"Durchlauf von {i + 1}. Seed beendet");
        }

        return lowestResult;
    }

    private List<(long start, long dest, long length)>[] ParseInput()
    {
        List<(long start, long dest, long length)>[] maps = new List<(long start, long dest, long length)>[7]; 
        List<string>[] rawMaps = new List<string>[7];
        GetRawMapData(rawMaps);

        
        for (int i = 0; i < maps.Length; i++)
        {
            List<List<long>> value = GetMapData(rawMaps[i]);

            List<(long start, long dest, long length)> map = new();

            for (int n = 0; n < value.Count; n++)
            {
                map.Add((value[n][1], value[n][0], value[n][2]));
            }

            maps[i] = map;
        }
        
        return maps;
    }

    private List<List<long>> GetMapData(List<string> data)
    {
        List<List<long>> result = new();
        for (int i = 0; i < data.Count; i++)
        {
            List<long> tmp = data[i]
                                .Split(" ")
                                .Select(s => long.Parse(s.Trim()))
                                .ToList();
            result.Add(tmp);
        }

        return result;
    }

    private void GetRawMapData(List<string>[] rawMaps)
    {
        rawMaps[0] = new();
        int counter = 0;

        for (int i = 3; i < input.Count; i++)
        {
            if (input[i].Contains(':'))
            {
                counter++;
                rawMaps[counter] = new();
                continue;
            }

            if (input[i] != string.Empty)
            {
                rawMaps[counter].Add(input[i]);
            }
        }
    }
    
    private List<long> GetSeeds()
    {
        return input[0]
            .Split(":")[1]
            .Split(" ")
            .Where(s => s != "")
            .Select(long.Parse)
            .ToList();
    }
}
