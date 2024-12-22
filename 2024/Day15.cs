using System.Text;
using Point = (int x, int y);

namespace AdventOfCode._2024;

public class Day15 : AdventBase
{
    private readonly string[] input;
    
    public Day15() : base(2024, 15)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        char[][] map = GetMap();
        char[] instructions = GetInstructions();
        Point curRobotPos = GetRobotPos(map);

        map[curRobotPos.y][curRobotPos.x] = '.';
        
        foreach (char instruction in instructions)
        {
            curRobotPos = ExecuteInstruction(map, instruction, curRobotPos);
        }
        
        return CalculateGps(map);
    }

    private long CalculateGps(char[][] map)
    {
        long sum = 0;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] != 'O')
                {
                    continue;
                }

                sum += y * 100 + x;
            }
        }
        
        return sum;
    }

    private Point ExecuteInstruction(char[][] map, char instruction, Point curRobotPos)
    {
        Point direction = GetDirectionFromInstruction(instruction);
        
        Point nextRobotPos = (curRobotPos.x + direction.x, curRobotPos.y + direction.y);

        if (map[nextRobotPos.y][nextRobotPos.x] == '.')
        {
            return nextRobotPos;
        }

        if (map[curRobotPos.y][curRobotPos.x] == '#')
        {
            return curRobotPos;
        }

        if (CanPushBoxes(map, nextRobotPos, direction))
        {
            PushAllBoxes(map, nextRobotPos, direction);
            return nextRobotPos;
        }
        
        return curRobotPos;
    }

    private void PushAllBoxes(char[][] map, Point curPos, Point direction)
    {
        Stack<Point> boxes = [];
        while (map[curPos.y][curPos.x] != '.')
        {
            boxes.Push(curPos);
            curPos = (curPos.x + direction.x, curPos.y + direction.y);
        }

        while (boxes.Count > 1)
        {
            Point box = boxes.Pop();
            map[box.y + direction.y][box.x + direction.x] = 'O';
        }
        
        Point lastBox = boxes.Pop();

        map[lastBox.y + direction.y][lastBox.x + direction.x] = 'O';
        map[lastBox.y][lastBox.x] = '.';
    }

    private bool CanPushBoxes(char[][] map, Point curPos, Point direction)
    {
        
        while (map[curPos.y][curPos.x] != '#')
        {
            char current = map[curPos.y][curPos.x];
            if (current == '.')
            {
                return true;
            }
            
            curPos = (curPos.x + direction.x, curPos.y + direction.y);
        }
        
        return false;
    }

    private Point GetDirectionFromInstruction(char instruction)
    {
        return instruction switch
        {
            'v' => (0, 1),
            '^' => (0, -1),
            '>' => (1, 0),
            '<' => (-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null)
        };
    }

    private char[][] GetMap()
    {
        return input
            .TakeWhile(x => x != "")
            .Select(x => x.ToCharArray())
            .ToArray();
    }
    
    private char[] GetInstructions()
    {
        return input
            .SkipWhile(x => x != "")
            .Skip(1)
            .Aggregate("", (current, next) => current + next)
            .ToCharArray();
    }
    
    private Point GetRobotPos(char[][] map)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == '@')
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }

    private void PrintMap(char[][] map, Point curRobotPos, char direction)
    {
        StringBuilder sb = new();
        
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (curRobotPos.x == x && curRobotPos.y == y)
                {
                    sb.Append('@');
                }
                else
                {
                    sb.Append(map[y][x]);
                }
            }

            sb.AppendLine();
        }

        sb.AppendLine($"Move: {direction}");
        sb.AppendLine();
        
        Console.WriteLine(sb.ToString());
    }
}