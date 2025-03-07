namespace AdventOfCode._2015;

public class Day10() : AdventBase(2015, 10)
{
    private readonly List<int> _input = [1, 3, 2, 1, 1, 3, 1, 1, 1, 2];

    public override long PartOne()
    {
        return Calculate(_input, 40);
    }

    public override long PartTwo()
    {
        return Calculate(_input, 50);
    }

    private int Calculate(List<int> input, int target)
    {
        return Calculate(input, 0, target);
    }

    private int Calculate(List<int> numbers, int curCount, int targetCount)
    {
        if (curCount == targetCount)
        {
            return numbers.Count;
        }

        List<int> newNumbers = [];

        for (int i = 0; i < numbers.Count; i++)
        {
            int number = numbers[i];
            int count = 0;

            while (i < numbers.Count && number == numbers[i])
            {
                count++;
                i++;
            }
            
            newNumbers.AddRange([count, number]);
            i--;
        }
        
        return Calculate(newNumbers, curCount + 1, targetCount);
    }
}