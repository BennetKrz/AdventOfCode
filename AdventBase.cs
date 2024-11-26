using System.Runtime.InteropServices;

namespace AdventOfCode;

public abstract class AdventBase(int year, int day) : IAdventDay
{
    private readonly string path = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @$"C:\C#\AdventOfCode\{year}\Input\day_{day}.txt" 
        : $"/home/dodo/AdventOfCode/{year}/Input/day_{day}.txt";

    protected string InputOneLine
    {
        get
        {
            CheckFile();
            return File.ReadAllText(path);
        }
    }

    protected string[] Input
    {
        get
        {
            CheckFile();
            return File.ReadAllLines(path);
        }
    }

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

    private void CheckFile()
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, GetInput());
        }
    }
    
    private string GetInput()
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri("https://adventofcode.com"),
        };
        client.DefaultRequestHeaders.Add("cookie", "session=53616c7465645f5fabfaae14a84ec937e6e067f2a853fa26b81796cbb09a783128e4b1d920d63c9df2019b5a1631ce2e87d86c6d7a19a5df8314c098ee4718c9");

        Task<string> content = client.GetStringAsync($"/{year}/day/{day}/input");
        content.Wait();
        return content.Result;
    }
}