namespace AdventOfCode._2024;

public class Day17PartTwoSolver(string targetOutput, long localStart, long localEnd, string[] input)
{
    public long Run()
    {
        Console.WriteLine(localStart);
        
        for (long a = localStart; a < localEnd; a++)
        {
            Day17.Day17.Computer computer = new(input, a);
            List<byte> output = computer.Execute();
            if (string.Join(",", output) == targetOutput)
            {
                return a;
            }
        }

        return -1;
    }
}