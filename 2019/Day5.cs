namespace AdventOfCode._2019;

public class Day5 : AdventBase
{
    private string[] input;
    
    public Day5() : base(2019, 5)
    {
        input = InputOneLine.Split(',');
    }

    public override long PartOne()
    {
        IntCodeEmulator emulator = new(input, [1], false);
        emulator.ExecuteProgram();
        return long.Parse(emulator.GetLatestOutput());
    }

    public override long PartTwo()
    {
        input = InputOneLine.Split(",");
        IntCodeEmulator emulator = new(input, [5], false);
        emulator.ExecuteProgram();
        return long.Parse(emulator.GetLatestOutput());
    }
}