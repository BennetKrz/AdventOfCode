namespace AdventOfCode._2018;

public class Day5 : AdventBase
{
    private string input;

    public Day5() : base(2018, 5)
    {
        input = InputOneLine;
    }

    public override long PartOne()
    {
        int i = 0;
        while(i < input.Length - 1)
        {
            char c1 = input[i];
            char c2 = input[i + 1];
            
            if (c1 - 32 == c2 || c1 == c2 - 32)
            {
                input = input.Remove(i, 2);
                i = 0;
                continue;
            }

            i++;
        }
        
        return input.Length;
    }

    public override long PartTwo()
    {
        return base.PartTwo();
    }
}