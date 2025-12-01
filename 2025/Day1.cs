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
}