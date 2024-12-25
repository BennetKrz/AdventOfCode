using Point = (int x, int y);

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

    private Point ButtonPressesRequired(SlotMaschine maschine)
    {
        for (int a = 1; a <= 100; a++)
        {
            for (int b = 1; b <= 100; b++)
            {
                if (maschine.ButtonAVelocity.x * a + maschine.ButtonBVelocity.x * b == maschine.Prize.x
                    && maschine.ButtonAVelocity.y * a + maschine.ButtonBVelocity.y * b== maschine.Prize.y)
                {
                    return (a, b);
                }
            }
        }

        return (-1, -1);
    } 

    private List<SlotMaschine> ParseInput()
    {
        List<SlotMaschine> maschines = [];

        for (int i = 0; i < input.Length; i += 4)
        {
            string[] infos = input
                .Skip(i)
                .Take(4)
                .ToArray();
            
            int xA = int.Parse(infos[0].Substring(infos[0].IndexOf('X') + 1, infos[0].IndexOf(',') - (infos[0].IndexOf('X') + 1)));
            int yA = int.Parse(infos[0].Substring(infos[0].IndexOf('Y') + 1, infos[0].Length - (infos[0].IndexOf('Y') + 1)));
            
            int xB = int.Parse(infos[1].Substring(infos[1].IndexOf('X') + 1, infos[1].IndexOf(',') - (infos[1].IndexOf('X') + 1)));
            int yB = int.Parse(infos[1].Substring(infos[1].IndexOf('Y') + 1, infos[0].Length - (infos[1].IndexOf('Y') + 1)));
            
            int xP = int.Parse(infos[2].Substring(infos[2].IndexOf('=') + 1, infos[2].IndexOf(',') - (infos[2].IndexOf('=') + 1)));
            int yP = int.Parse(infos[2].Substring(infos[2].LastIndexOf('=') + 1, infos[2].Length - (infos[2].LastIndexOf('=') + 1)));

            maschines.Add(new((xA, yA), (xB, yB), (xP, yP)));
        }

        return maschines;
    }

    private record SlotMaschine(Point ButtonAVelocity, Point ButtonBVelocity, Point Prize);
}

