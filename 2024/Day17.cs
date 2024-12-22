namespace AdventOfCode._2024;

public class Day17 : AdventBase
{
    private string[] input;
    private Dictionary<char, long> registers;
    private List<byte> instructions;
    private List<byte> output;
    
    private int instructionPointer;
    
    public Day17() : base(2024, 17)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        registers = InitializeRegisters();
        instructions = GetInstructions();
        instructionPointer = 0;
        output = [];
        
        while (instructionPointer < instructions.Count)
        {
            ExecuteInstruction();
        }

        Console.WriteLine(string.Join(",", output));
        
        return long.MinValue;
    }

    private void ExecuteInstruction()
    {
        byte opcode = instructions[instructionPointer];
        byte operand = instructions[instructionPointer + 1];

        switch (opcode)
        {
            case 0:
                long numerator = registers['A'];
                long denominator = (long)Math.Pow(2, GetValueOfComboOperand(operand));
                registers['A'] = numerator / denominator;
                break;
            case 1:
                registers['B'] ^= operand;
                break;
            case 2:
                registers['B'] = GetValueOfComboOperand(operand) % 8;
                break;
            case 3:
                if (registers['A'] != 0)
                {
                    instructionPointer = operand;
                    return;
                }

                break;
            case 4:
                registers['B'] ^= registers['C'];
                break;
            case 5:
                output.Add((byte)(GetValueOfComboOperand(operand) % 8));
                break;
            case 6:
                long n = registers['A'];
                long d = (long)Math.Pow(2, GetValueOfComboOperand(operand));
                registers['B'] = n / d;
                break;
            case 7:
                long nn = registers['A'];
                long dd = (long)Math.Pow(2, GetValueOfComboOperand(operand));
                registers['C'] = nn / dd;
                break;
        }

        instructionPointer += 2;
    }

    private long GetValueOfComboOperand(byte operand)
    {
        return operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => registers['A'],
            5 => registers['B'],
            6 => registers['C'],
            _ => throw new ArgumentException("Invalid operand")
        };
    }

    private List<byte> GetInstructions()
    {
        return input[4]
            .Split(" ")
            .Skip(1)
            .First()
            .Split(",")
            .Select(byte.Parse)
            .ToList();
    }

    private Dictionary<char,long> InitializeRegisters()
    {
        return new Dictionary<char, long>()
        {
            {'A', long.Parse(input[0].Split(":")[1])},
            {'B', long.Parse(input[1].Split(":")[1])},
            {'C', long.Parse(input[2].Split(":")[1])}
        };
    }
}