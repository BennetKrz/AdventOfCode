namespace AdventOfCode._2016;

public class Day1 : AdventBase
{
    private readonly string[] input;

    private readonly List<(int x, int y)> directions =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0)
    ];

    public Day1() : base(2016, 1)
    {
        input = Input;
    }

    public override long PartOne()
    {
        (int x, int y) position = (0, 0);
        int direction = 0;
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = input[i].Replace(" ", "");
            if (input[i][0] == 'R')
            {
                direction = (direction + 1) % 4;
            }
            else
            {
                direction = (direction - 1) % 4;
                if (direction < 0)
                {
                    direction += 4;
                }
            }

            position.x += directions[direction].x * int.Parse(input[i][1..]);
            position.y += directions[direction].y * int.Parse(input[i][1..]);
        }

        return Math.Abs(position.x) + Math.Abs(position.y);
    }

    public override long PartTwo()
    {
        (int x, int y) position = (0, 0);
        int direction = 0;
        Dictionary<(int x, int y), int> allCoords = new();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i][0] == 'R')
            {
                direction = (direction + 1) % 4;
            }
            else
            {
                direction = (direction - 1) % 4;
                if (direction < 0)
                {
                    direction += 4;
                }
            }

            for (int j = 0; j < int.Parse(input[i][1..]); j++)
            {
                position.x += directions[direction].x;
                position.y += directions[direction].y;
                if (allCoords.ContainsKey(position))
                {
                    return Math.Abs(position.x) + Math.Abs(position.y);
                }

                allCoords.Add(position, 1);
            }
        }

        return long.MinValue;
    }
}