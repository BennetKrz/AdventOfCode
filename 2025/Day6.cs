using System.Text.RegularExpressions;

namespace AdventOfCode._2025;

public class Day6 : AdventBase
{
    private List<string> rawInput;
    private List<List<long>> numbers;
    private List<Operation> operations;

    public Day6() : base(2025, 6)
    {
        rawInput = base.Input.ToList();
        numbers = ParseNumbers();
        operations = ParseOperations();
    }

    public override long PartOne()
    {
        long sum = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            if (operations[i] == Operation.Multiply)
            {
                long product = 1;
                for (int x = 0; x < numbers[i].Count; x++)
                {
                    product *= numbers[i][x];
                }
                sum += product;
            }
            else
            {
                sum += numbers[i].Sum();
            }
        }

        return sum;
    }

    public override long PartTwo()
    {
        return long.MaxValue;
    }

    private List<List<long>> ParseNumbers()
    {
        List<List<long>> result = [];
        foreach (string line in rawInput)
        {
            var r = Regex.Matches(line, @"\d*").Where(x => x.Length > 0).ToList();
            for (int i = 0; i < r.Count; i++)
            {
                if (result.Count <= i)
                {
                    result.Add([]);
                }
                result[i].Add(long.Parse(r[i].Value));
            }
        }

        return result;
    }

    private List<Operation> ParseOperations()
    {
        return rawInput[^1]
            .Where(c => c is '+' or '*')
            .Select(o => o == '*' ? Operation.Multiply : Operation.Add)
            .ToList();
    }

    private enum Operation
    {
        Add,
        Multiply
    }
}
