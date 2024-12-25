namespace AdventOfCode._2024;

public class Day25 : AdventBase
{
    private readonly string[] input;
    private readonly List<byte[]> keys;
    private readonly List<byte[]> locks;
    
    
    public Day25() : base(2024, 25)
    {
        input = Input;
        keys = [];
        locks = [];
    }

    public override long PartOne()
    {
        ParseInput();

        int correctCount = 0;
        
        foreach (byte[] lockHeights in locks)
        {
            foreach (byte[] keyHeights in keys)
            {
                if(lockHeights.Select((x, i) => x + keyHeights[i]).All(x => x <= 5))
                {
                    correctCount++;
                }
            }
        }
        
        return correctCount;
    }

    private void ParseInput()
    {
        List<string[]> grids = [];
        for (int i = 0; i < input.Length; i += 8)
        {
            grids.Add(input.Skip(i).Take(7).ToArray());
        }

        foreach (string[] grid in grids)
        {
            byte[] heights = new byte[5];
            
            for (int x = 0; x < grid[0].Length; x++)
            {
                byte count = 0;
                for (int y = 0; y < grid.Length; y++)
                {
                    if (grid[y][x] == '#')
                    {
                        count++;
                    }
                }
                heights[x] = (byte)(count - 1);
            }
            
            if (grid[0] == "#####")
            {
                locks.Add(heights);
            }
            else
            {
                keys.Add(heights);
            }
        }
    }
}