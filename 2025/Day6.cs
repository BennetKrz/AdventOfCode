using System.Text.RegularExpressions;

namespace AdventOfCode._2025;

public class Day6 : AdventBase
{
    private readonly List<string> rawInput;
    private readonly List<List<long>> numbers;
    private readonly List<Operation> operations;

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
        List<Operation> operations =  ParseOperations();
        List<List<char>> result = [];

        int index = 0;
        for (int x = 0; x < rawInput[index].Length; x++)
        {
            List<char> buffer = [];
            for (int y = 0; y < rawInput.Count; y++)
            {
                char c = rawInput[y][x];
                if (char.IsAsciiDigit(c))
                {
                    buffer.Add(c);
                }
            }
            result.Add(buffer);
        }

        List<List<List<char>>> finalResults = [];
        List<List<char>> tmp = [];
        int count = 0;
        for (int i = 0; i < result.Count; i++)
        {
            if (result[i].Count == 0)
            {
                finalResults.Add(tmp);
                tmp = [];
            }
            else
            {
                tmp.Add(result[i]);
            }
        }
        finalResults.Add(tmp);

        long sum = 0;
        for (int i = 0; i < finalResults.Count; i++)
        {
            var o = operations[i];
            long r = o == Operation.Add ? 0 : 1;
            finalResults[i].Reverse();
            foreach (List<char> list in finalResults[i])
            {
                long number = long.Parse(string.Join("", list));
                if (o == Operation.Add)
                {
                    r += number;
                }
                else
                {
                    r *= number;
                }
            }

            sum += r;
        }

        return sum;
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
