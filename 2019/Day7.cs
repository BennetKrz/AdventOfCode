namespace AdventOfCode._2019;

public class Day7 : AdventBase
{

    private readonly string[] instructions;
    
    public Day7() : base(2019, 7)
    {
        instructions = InputOneLine.Split(",");
    }
    public override long PartOne()
    {
        List<int> result = new();
        List<int[]> permutations = GetAllPermutations();
        string[] instructionBackup = new string[instructions.Length];
        Array.Copy(instructions, instructionBackup, instructions.Length);
        for (int i = 0; i < permutations.Count; i++)
        {
            int previousOutput = 0;
            for (int j = 0; j < permutations[i].Length; j++)
            {
                IntCodeEmulator emulator = new(instructions, new List<int> { permutations[i][j], previousOutput },
                                               false);
                emulator.ExecuteProgram();
                previousOutput = int.Parse(emulator.GetLatestOutput());
                Array.Copy(instructions, instructionBackup, instructions.Length);
            }
            result.Add(previousOutput);
        }
        return result.Max();
    }

    private List<int[]> GetAllPermutations()
    {
        List<int[]> permutations = new();
        for (int d1 = 0; d1 < 5; d1++)
        {
            for (int d2 = 0; d2 < 5; d2++)
            {
                if (d1 == d2)
                {
                    continue;
                }

                for (int d3 = 0; d3 < 5; d3++)
                {
                    if (d3 == d1 || d3 == d2)
                    {
                        continue;
                    }

                    for (int d4 = 0; d4 < 5; d4++)
                    {
                        if (d1 == d4 || d2 == d4 || d3 == d4)
                        {
                            continue;
                        }
                        for (int d5 = 0; d5 < 5; d5++)
                        {
                            if (d1 == d5 || d2 == d5 || d3 == d5 || d4 == d5)
                            {
                                continue;
                            }
                            permutations.Add([d1, d2, d3, d4, d5]);
                        }
                    }
                }
            }
        }

        return permutations;
    }

    public override long PartTwo()
    {
        return 0;
    }
}