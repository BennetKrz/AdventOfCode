namespace AdventOfCode._2024;

public class Day11 : AdventBase
{
    private List<long> stonesOriginal;
    
    public Day11() : base(2024, 11)
    {
        this.stonesOriginal = InputOneLine
            .Split(' ')
            .Select(long.Parse)
            .ToList();
    }

    public override long PartOne()
    {
        List<long> stones = [..this.stonesOriginal];
        
        for (int blink = 0; blink < 25; blink++)
        {
            stones = Blink(stones);
        }

        return stones.Count;
    }

    public override long PartTwo()
    {
        List<long> stones = [..this.stonesOriginal];
        
        for (int blink = 0; blink < 75; blink++)
        {
            Console.WriteLine(blink);
            stones = Blink(stones);
        }

        return stones.Count;
    }

    private List<long> Blink(List<long> stones)
    {
        List<long> newStones = [];
        
        for (int i = 0; i < stones.Count; i++)
        {
            long stone = stones[i];
            int numberOfDigits = NumberOfDigits(stone);
            
            if (stone == 0)
            {
                newStones.Add(1);
            } else if (numberOfDigits % 2 == 0)
            {
                int halfDigits = numberOfDigits / 2;
                long divisor = (long)Math.Pow(10, halfDigits);

                long rightHalve = stone % divisor;
                long leftHalve = stone / divisor;
                
                newStones.Add(leftHalve);
                newStones.Add(rightHalve);
            }
            else
            {
                newStones.Add(stone * 2024);
            }
        }
        
        return newStones;
    }

    private int NumberOfDigits(long number)
    {
        return (int)Math.Floor(Math.Log10(number) + 1);
    }
}