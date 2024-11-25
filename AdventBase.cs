using System.Runtime.InteropServices;

namespace AdventOfCode;

public abstract class AdventBase(int year, int day) : IAdventDay
{
    private readonly string path = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @$"H:\C#\AdventOfCode\{year}\Input\day_{day}.txt" 
        : $"/home/dodo/AdventOfCode/{year}/Input/day_{day}.txt";

    protected string InputOneLine => File.ReadAllText(path);

    protected string[] Input => File.ReadAllLines(path);
    protected T[] GetConvertedInput<T>()
    {
        return Input.Select(x => (T)x.Cast<T>()).ToArray();
    }

    public virtual long PartOne()
    {
        return long.MinValue;
    }

    public virtual long PartTwo()
    {
        return long.MinValue;
    }
}