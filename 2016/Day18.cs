namespace AdventOfCode._2016;

using ThreeTiles = (bool left, bool center, bool right);

public class Day18 : AdventBase
{
    private List<string> input;

    public Day18() : base(2016, 18)
    {
        input = Input.ToList();
    }

    public override long PartOne()
    {
        for (int i = 0; i < 39; i++)
        {
            string line = input[i];
            string nextLine = string.Empty;
            for (int c = 0; c < line.Length; c++)
            {
                ThreeTiles tiles = GetNextTiles(line, c);
                bool conditionTrue = tiles is { left: true, center: true, right: false } 
                    or { left: false, center: true, right: true } 
                    or { left: true, center: false, right: false } 
                    or { left: false, center: false, right: true };

                nextLine += conditionTrue ? '^' : '.';
            }

            input.Add(nextLine);
        }

        return input.Sum(s => s.Count(c => c == '.'));
    }

    private ThreeTiles GetNextTiles(string line, int pos)
    {
        char cLeft;
        char cCentral = line[pos];
        char cRight;
        cLeft = pos == 0 ? '.' : line[pos - 1];

        cRight = pos == line.Length - 1 ? '.' : line[pos + 1];

        return (cLeft == '^', cCentral == '^', cRight == '^');
    }

    public override long PartTwo()
    {
        input.Clear();
        input = Input.ToList();
        for (int i = 0; i < 400_000 - 1; i++)
        {
            string line = input[i];
            string nextLine = string.Empty;
            for (int c = 0; c < line.Length; c++)
            {
                ThreeTiles tiles = GetNextTiles(line, c);
                bool conditionTrue = tiles is { left: true, center: true, right: false } 
                    or { left: false, center: true, right: true }
                    or { left: true, center: false, right: false } 
                    or { left: false, center: false, right: true };

                nextLine += conditionTrue ? '^' : '.';
            }

            input.Add(nextLine);
        }

        return input.Sum(s => s.Count(c => c == '.'));
    }
}