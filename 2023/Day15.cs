namespace AdventOfCode._2023;

public class Day15 : AdventBase
{
    private readonly List<string> input;
    
    public Day15() : base(2023, 15)
    {
        this.input = InputOneLine
            .Split(",")
            .ToList();
    }
    public override long PartOne()
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

    public override long PartTwo()
    {
        Box[] boxes = InitializeBoxes(256);
        List<Instruction> instructions = ParseInput();
        foreach (Instruction instruction in instructions)
        {
            if (instruction.Operation == Operation.Remove)
            {
                boxes[instruction.BoxNumber].RemoveLens(instruction.Lens);
            }
            else
            {
                boxes[instruction.BoxNumber].OverwriteLens(instruction.Lens);
            }
        }

        return boxes.Select((b, i) => b.GetFocusingPower(i))
                    .Sum();
    }

    private Box[] InitializeBoxes(int length)
    {
        Box[] boxes = new Box[length];
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new Box();
        }

        return boxes;
    }

    private List<Instruction> ParseInput()
    {
        List<Instruction> instructions = new();
        foreach (string line in input)
        {
            string label = line.Split(new char[] { '=', '-' })[0];
            Hasher h = new(label);
            int box = h.Hash();
            Operation operation;
            int focal;
            if (line.Contains('='))
            {
                operation = Operation.Replace;
                focal = int.Parse(line[^1].ToString());
            }
            else
            {
                focal = -1;
                operation = Operation.Remove;
            }
            instructions.Add(new Instruction(box, new Lens(label, focal), operation));
        }
        return instructions;
    }

    private record Instruction(int BoxNumber, Lens Lens, Operation Operation);

    private enum Operation
    {
        Replace,
        Remove
    }
}