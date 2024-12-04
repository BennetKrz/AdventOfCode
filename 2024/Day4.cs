namespace AdventOfCode._2024;

public class Day4 : AdventBase
{
    private string[] input;

    public Day4() : base(2024, 4)
    {
        input = Input;
    }

    public override long PartOne()
    {
        long xmasFound = 0;

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'X')
                {
                    xmasFound += SearchForXmasAtLocation(x, y);
                }
            }
        }
        
        return xmasFound;
    }

    private long SearchForXmasAtLocation(int x, int y)
    {
        int found = 0;
        found += SearchForward(x, y);
        found += SearchBackwards(x, y);
        found += SearchUpwards(x, y);
        found += SearchDownwards(x, y);
        found += SearchDiagonalLeftUp(x, y);
        found += SearchDiagonalRightUp(x, y);
        found += SearchDiagonalLeftDown(x, y);
        found += SearchDiagonalRightDown(x, y);
        return found;
    }

    private int SearchDiagonalRightUp(int x, int y)
    {
        if (x + 3 >= input[0].Length || y - 3 < 0)
        {
            return 0;
        }
        return input[y][x] == 'X' &&
               input[y - 1][x + 1] == 'M' &&
               input[y - 2][x + 2] == 'A' &&
               input[y - 3][x + 3] == 'S'
            ? 1
            : 0;
    }

    private int SearchForward(int x, int y)
    {
        if (x + 4 >= input[0].Length)
        {
            return 0;
        }
        return input[y].Substring(x, 4) == "XMAS" ? 1 : 0;
    }
    
    private int SearchBackwards(int x, int y)
    {
        if (x - 4 < 0)
        {
            return 0;
        }
        return input[y].Substring(x - 4, 4) == "XMAS" ? 1 : 0;
    }
    
    private int SearchUpwards(int x, int y)
    {
        if (y - 3 < 0)
        {
            return 0;
        }

        return input[y][x] == 'X' &&
               input[y - 1][x] == 'M' &&
               input[y - 2][x] == 'A' &&
               input[y - 3][x] == 'S'
            ? 1
            : 0;
    }
    
    private int SearchDownwards(int x, int y)
    {
        if (y + 3 >= input.Length)
        {
            return 0;
        }

        return input[y][x] == 'X' &&
               input[y + 1][x] == 'M' &&
               input[y + 2][x] == 'A' &&
               input[y + 3][x] == 'S'
            ? 1
            : 0;
    }
    
    private int SearchDiagonalLeftUp(int x, int y)
    {
        if (x - 3 < 0 || y - 3 < 0)
        {
            return 0;
        }
        return input[y][x] == 'X' &&
               input[y - 1][x - 1] == 'M' &&
               input[y - 2][x - 2] == 'A' &&
               input[y - 3][x - 3] == 'S' 
            ? 1
            : 0;
    }
}