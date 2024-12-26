using Point = (long x, long y);

namespace AdventOfCode._2024;

public class Day13 : AdventBase
{
    private readonly string[] input;

    private const int CostButtonA = 3;
    private const int CostButtonB = 1;
    
    public Day13() : base(2024, 13)
    {
        input = Input;
    }

    public override long PartOne()
    {
        return ParseInput()
            .Select(ButtonPressesRequired)
            .Where(x => x != (-1, -1))
            .Sum(x => x.x * CostButtonA + x.y * CostButtonB);
    }

    public override long PartTwo()
    {
        long totalCost = 0;
        
        long toAdd = 10000000000000;
        
        List<ClawMachine> clawMachines = ParseInput();

        clawMachines = clawMachines
            .Select(x => new ClawMachine(x.ButtonAVelocity, x.ButtonBVelocity, (x.Prize.x + toAdd, x.Prize.y + toAdd)))
            .ToList();

        foreach (ClawMachine clawMachine in clawMachines)
        {
            double pX = clawMachine.Prize.x;
            double pY = clawMachine.Prize.y;

            double aX = clawMachine.ButtonAVelocity.x;
            double aY = clawMachine.ButtonAVelocity.y;
            
            double bX = clawMachine.ButtonBVelocity.x;
            double bY = clawMachine.ButtonBVelocity.y;

            double a = (pX * bY - pY * bX) / (aX * bY - bX * aY);
            double b = -(pX * aY - pY * aX) / (aX * bY - bX * aY);

            if (double.IsInteger(a) && double.IsInteger(b))
            {
                totalCost += (long)a * CostButtonA + (long)b * CostButtonB;
            }
        }
        
        return totalCost;
    }

    private Point ButtonPressesRequired(ClawMachine machine)
    {
        for (int a = 1; a <= 100; a++)
        {
            for (int b = 1; b <= 100; b++)
            {
                if (machine.ButtonAVelocity.x * a + machine.ButtonBVelocity.x * b == machine.Prize.x
                    && machine.ButtonAVelocity.y * a + machine.ButtonBVelocity.y * b== machine.Prize.y)
                {
                    return (a, b);
                }
            }
        }

        return (-1, -1);
    } 

    private List<ClawMachine> ParseInput()
    {
        List<ClawMachine> machines = [];

        for (int i = 0; i < input.Length; i += 4)
        {
            string[] infos = input
                .Skip(i)
                .Take(4)
                .ToArray();
            
            long xA = long.Parse(infos[0].Substring(infos[0].IndexOf('X') + 1, infos[0].IndexOf(',') - (infos[0].IndexOf('X') + 1)));
            long yA = long.Parse(infos[0].Substring(infos[0].IndexOf('Y') + 1, infos[0].Length - (infos[0].IndexOf('Y') + 1)));
            
            long xB = long.Parse(infos[1].Substring(infos[1].IndexOf('X') + 1, infos[1].IndexOf(',') - (infos[1].IndexOf('X') + 1)));
            long yB = long.Parse(infos[1].Substring(infos[1].IndexOf('Y') + 1, infos[0].Length - (infos[1].IndexOf('Y') + 1)));
            
            long xP = long.Parse(infos[2].Substring(infos[2].IndexOf('=') + 1, infos[2].IndexOf(',') - (infos[2].IndexOf('=') + 1)));
            long yP = long.Parse(infos[2].Substring(infos[2].LastIndexOf('=') + 1, infos[2].Length - (infos[2].LastIndexOf('=') + 1)));

            machines.Add(new((xA, yA), (xB, yB), (xP, yP)));
        }

        return machines;
    }

    private record ClawMachine(Point ButtonAVelocity, Point ButtonBVelocity, Point Prize);
}

