using System.Runtime.InteropServices;

namespace AdventOfCode;

public abstract class AdventBase : IAdventDay
{
    protected AdventBase(int year, int day)
    {
        string path = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @$"H:\C#\AdventOfCode\{year}\Input\day_{day}.txt" : $"/home/dodo/AdventOfCode/{year}/Input/day_{day}.txt";
        InputOneLine = File.ReadAllText(path);
        Input = File.ReadAllLines(path);
    }

    protected string InputOneLine { get; }
    
    protected string[] Input { get; }

    public virtual long PartOne()
    {
        return long.MinValue;
    }

    public virtual long PartTwo()
    {
        return long.MinValue;
    }
}