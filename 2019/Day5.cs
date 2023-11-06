namespace AdventOfCode._2019;

public class Day5 : IAdventDay
{
    private string[] input;

    private int latestOutputPartOne;
    private int latestOutputPartTwo;
    public Day5()
    {
        input = Helper.GetInputOneLine(5).Split(',');
    }

    public long PartOne()
    {
        IntCodeEmulator emulator = new(input, new List<int> { 1 }, false);
        emulator.ExecuteProgram();
        return long.Parse(emulator.GetLatestOutput());
    }

    public long PartTwo()
    {
        input = Helper.GetInputOneLine(5).Split(",");
        IntCodeEmulator emulator = new(input, new List<int> { 5 }, false);
        emulator.ExecuteProgram();
        return long.Parse(emulator.GetLatestOutput());
    }
}