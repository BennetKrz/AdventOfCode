namespace AdventOfCode._2025;

public class Day1 : AdventBase
{
    private readonly List<string> input;
    
    public Day1() : base(2025, 1)
    {
        this.input = Input.ToList();
    }

    public override long PartOne()
    {
        int curPosition = 50;
        int timesAtZero = 0;
        
        foreach (string line in input)
        {
            char direction = line[0];
            int count = int.Parse(line[1..]);
            curPosition = direction == 'R' ? curPosition + count : curPosition - count;
            
            if (direction == 'R')
            {
                while (curPosition > 99)
                {
                    curPosition -= 100;
                }
            }
            else
            {
                while (curPosition < 0)
                {
                    curPosition += 100;
                }
            }
            
            if (curPosition == 0) timesAtZero++;
        }
        
        return timesAtZero;
    }
    
    public override long PartTwo()
    {
        int curPosition = 50;
        int timesAtZero = 0;
        
        foreach (string line in input)
        {
            char direction = line[0];
            int count = int.Parse(line[1..]);

            while (count > 100)
            {
                count -= 100;
                timesAtZero++;
            }

            int prevPosition = curPosition;
            curPosition += direction == 'R' ? count : -count;

            if (curPosition is < 0 or > 99)
            {
                curPosition += direction == 'R' ? -100 : 100;
                if (curPosition != 0 && prevPosition != 0)
                {
                    timesAtZero++;
                }
            }

            if (curPosition == 0)
            {
                timesAtZero++;
            }
        }

        return timesAtZero;
    }
}