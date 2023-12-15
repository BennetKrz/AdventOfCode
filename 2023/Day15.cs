namespace AdventOfCode._2023;

public class Day15 : IAdventDay
{

    private List<string> input;
    
    public Day15()
    {
        this.input = Helper.GetInputOneLine(15).Split(",").ToList();
    }
    public long PartOne()
    {
        long sum = 0;
        Hasher h = new(input[0]);
        sum += h.Hash();
        
        for (int i = 1; i < input.Count; i++)
        {
            h.NextWord(input[i]);
            sum += h.Hash();
        }
        return sum;
    }

    public long PartTwo()
    {
        List<Instruction> instructions = ParseInput();
        return 0;
    }

    private List<Instruction> ParseInput()
    {
        List<Instruction> instructions = new();
        foreach (string line in input)
        {
            string label = line.Split(new char[] { '=', '-' })[0];
            Hasher h = new(label);
            int box = h.Hash();
            Operation operation = line.Contains("-") ? Operation.Remove : Operation.Replace;
            int focal = int.Parse(line[^1].ToString());
            instructions.Add(new Instruction(box, label, focal, operation));
        }
        return instructions;
    }

    private record Instruction(int BoxNumber, string LensLabel, int LensFocal, Operation Operation);

    private enum Operation
    {
        Replace,
        Remove
    }
}