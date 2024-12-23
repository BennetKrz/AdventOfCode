namespace AdventOfCode._2024;

public class Day17 : AdventBase
{
    private readonly string[] input;
    
    public Day17() : base(2024, 17)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        Computer computer = new(input, long.Parse(input[0].Split(" ")[2]));
        Console.WriteLine(string.Join(",", computer.Execute()));

        return long.MinValue;
    }

    public override long PartTwo()
    {
        string targetOutput = string.Join(",", input[4]
            .Split(" ")
            .Skip(1)
            .First()
            .Split(",")
            .Select(byte.Parse));
        
        const int numberOfThreads = 12;
        long start = 0;
        
        while (true)
        {
            const long delta = 10_000_000;

            List<Task<long>> threads = [];

            List<(long start, long end)> ranges = [];
            for (int i = 0; i < numberOfThreads; i++)
            {
                ranges.Add((start, start + delta));
                start += delta;
            }

            foreach ((long start, long end) range in ranges)
            {
                (long start, long end) localRange = range;
                
                Task<long> t = Task.Run(() => 
                    new Day17PartTwoSolver(targetOutput, localRange.start, localRange.end, input).Run());
                threads.Add(t);
            }

            
            long[] results = Task.WhenAll(threads).Result;

            ranges.Clear();
            threads.Clear();
            
            if (results.Any(r => r != -1))
            {
                return results.Max();
            }
        }
    }
    
    public class Computer
    {
        private readonly string[] input;
        private long rA;
        private long rB;
        private long rC;
        private readonly byte[] instructions;
        private readonly List<byte> output;
        
        private int instructionPointer;

        public Computer(string[] input, long startValue)
        {
            this.input = input;
            
            InitializeRegisters(startValue);
            instructions = GetInstructions();
            instructionPointer = 0;
            output = [];
        }

        public List<byte> Execute(bool partTwo = false)
        {
            while (instructionPointer < instructions.Length)
            {
                ExecuteInstruction();
                if (partTwo && output.Count >= instructions.Length)
                {
                    return output;
                }
            }
            
            return output;
        }

        private void ExecuteInstruction()
        {
            byte opcode = instructions[instructionPointer++];
            byte operand = instructions[instructionPointer++];

            switch (opcode)
            {
                case 0:
                    rA >>= (int)GetValueOfComboOperand(operand);
                    break;
                case 1:
                    rB ^= operand;
                    break;
                case 2:
                    rB = GetValueOfComboOperand(operand) & 0b111;
                    break;
                case 3:
                    if (rA != 0)
                    {
                        instructionPointer = operand;
                    }
                    break;
                case 4:
                    rB ^= rC;
                    break;
                case 5:
                    output.Add((byte)(GetValueOfComboOperand(operand) & 0b111));
                    break;
                case 6:
                    rB = rA >> (int)GetValueOfComboOperand(operand);
                    break;
                case 7:
                    rC = rA >> (int)GetValueOfComboOperand(operand);
                    break;
            }
        }

        private long GetValueOfComboOperand(byte operand)
        {
            return operand switch
            {
                0 or 1 or 2 or 3 => operand,
                4 => rA,
                5 => rB,
                6 => rC,
                _ => throw new ArgumentException("Invalid operand")
            };
        }

        private void InitializeRegisters(long startValue)
        {
            rA = startValue;
            rB = 0;
            rC = 0;
        }

        private byte[] GetInstructions()
        {
            return input[4]
                .Split(" ")
                .Skip(1)
                .First()
                .Split(",")
                .Select(byte.Parse)
                .ToArray();
        }
    }
}