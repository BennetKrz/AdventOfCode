namespace AdventOfCode._2024;

public class Day7 : AdventBase
{
    private readonly string[] input;
    private bool useConcatination = false;
    
    public Day7() : base(2024, 7)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        return input.Where(SolveEquation).Sum(x => long.Parse(x.Split(":")[0]));
    }

    public override long PartTwo()
    {
        useConcatination = true;
        return PartOne();
    }

    private bool SolveEquation(string equation)
    {
        string[] splitEquation = equation.Split(":");
        long target = long.Parse(splitEquation[0]);
        string[] numbers = splitEquation[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return AllPossibleEquations(numbers, target, 0, 0);
    }

    private bool AllPossibleEquations(string[] numbers, long target, int index, long accumulator)
    {
        if (accumulator > target)
        {
            return false;
        }
        
        if (index >= numbers.Length)
        {
            return target == accumulator;
        }
        
        int nextNumber = int.Parse(numbers[index]);
        
        if (AllPossibleEquations(numbers, target, index + 1, accumulator + nextNumber))
        {
            return true;
        }

        if (AllPossibleEquations(numbers, target, index + 1, accumulator * nextNumber))
        {
            return true;
        }
        
        return useConcatination && AllPossibleEquations(numbers, target, index + 1,
            long.Parse((accumulator.ToString() + nextNumber.ToString())));
    }
}