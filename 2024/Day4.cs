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
                if (input[y][x] != 'X')
                {
                    continue;
                }

                xmasFound += GetAllStringsInDirectionsPartOne(x, y).Count(x => x == "XMAS");
            }
        }
        
        return xmasFound;
    }

    public override long PartTwo()
    {
        long xmasFound = 0;
        
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] != 'A')
                {
                    continue;
                }

                if (IsXmas(x, y))
                {
                    xmasFound++;
                }
            }
        }

        return xmasFound;
    }

    private bool IsXmas(int x, int y)
    {
        List<(int deltaX, int deltaY)> directions =
        [
            (-1, -1),   // DiagonalUpLeft
            (1, -1),   // DiagonalUpRight
            (-1, 1),   // DiagonalDownLeft
            (1, 1),   // DiagonalDownRight
        ];

        foreach ((int deltaX, int deltaY) direction in directions)
        {
            if (!IsInGrid(x + direction.deltaX, y + direction.deltaY))
            {
                return false;
            }
        }

        return ((input[y - 1][x - 1] == 'M' && input[y + 1][x + 1] == 'S') ||
                (input[y - 1][x - 1] == 'S' && input[y + 1][x + 1] == 'M'))
               &&
               ((input[y - 1][x + 1] == 'M' && input[y + 1][x - 1] == 'S') ||
                (input[y - 1][x + 1] == 'S' && input[y + 1][x - 1] == 'M'));
    }

    private List<string> GetAllStringsInDirectionsPartOne(int x, int y)
    {
        List<string> allStrings = [];
        List<(int xDelta, int yDelta)> directions =
        [
            (1, 0),     // Forwards
            (-1, 0),    // Backwards
            (0, -1),    // Upwards
            (0, 1),     // Downwards
            (-1, -1),   // UpLeftDiagonal
            (1, -1),    // UpRightDiagonal
            (-1, 1),    // DownLeftDiagonal
            (1, 1),     // DownRightDiagonal
        ];

        foreach ((int xDelta, int yDelta) direction in directions)
        {
            allStrings.Add(GetString(direction, x, y));
        }
        
        return allStrings;
    }

    private string GetString((int xDelta, int yDelta) direction, int x, int y)
    {
        string result = "X";
        for (int i = 0; i < 3; i++)
        {
            x += direction.xDelta;
            y += direction.yDelta;
            if (!IsInGrid(x, y))
            {
                return result;
            }
            result += input[y][x];
        }

        return result;
    }
    
    private bool IsInGrid(int x, int y) => x >= 0 && x < input[0].Length && y >= 0 && y < input.Length;
}