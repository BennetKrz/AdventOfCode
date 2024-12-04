using System.Diagnostics;

namespace AdventOfCode;

public class Program
{
    private static void Main(string[] args)
    {
        IAdventDay day = new AdventOfCode._2024.Day4();
        Console.WriteLine(day.GetType().FullName + ":");
        Console.WriteLine("- - - - - - - - - PART ONE - - - - - - - - -  Starting Stopwatch...");
        var sw = Stopwatch.StartNew();
        var resultPartOne = day.PartOne();
        if (resultPartOne != long.MinValue) Console.WriteLine(resultPartOne);

        sw.Stop();
        Console.WriteLine(
            $"Finished part one. Time needed: {sw.Elapsed.Minutes}m : {sw.Elapsed.Seconds}s : {sw.Elapsed.Milliseconds}ms : {sw.Elapsed.Microseconds}µs");
        Console.WriteLine("- - - - - - - - - PART TWO - - - - - - - - -  Starting Stopwatch...");
        sw.Restart();
        var resultPartTwo = day.PartTwo();
        if (resultPartTwo != long.MinValue) Console.WriteLine(resultPartTwo);

        sw.Stop();
        Console.WriteLine(
            $"Finished part two. Time needed: {sw.Elapsed.Minutes}m : {sw.Elapsed.Seconds}s : {sw.Elapsed.Milliseconds}ms : {sw.Elapsed.Microseconds}µs");
        Console.WriteLine();
    }
}

public interface IAdventDay
{
    public long PartOne();
    public long PartTwo();
}