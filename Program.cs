using System.Diagnostics;

namespace AdventOfCode;

public class Program
{
	private static void Main(string[] args)
	{
		IAdventDay day = new AdventOfCode._2023.Day15();
		Console.WriteLine(day.GetType().FullName + ":");
		Console.WriteLine("- - - - - - - - - PART ONE - - - - - - - - -  Starting Stopwatch...");
		Stopwatch sw = Stopwatch.StartNew();
		long resultPartOne = day.PartOne();
		if (resultPartOne != long.MinValue) Console.WriteLine(resultPartOne);

		sw.Stop();
		Console.WriteLine(
			$"Finished part one. Time needed: {sw.Elapsed.Minutes}m : {sw.Elapsed.Seconds}s : {sw.Elapsed.Milliseconds}ms");
		Console.WriteLine("- - - - - - - - - PART TWO - - - - - - - - -  Starting Stopwatch...");
		sw.Restart();
		long resultPartTwo = day.PartTwo();
		if (resultPartTwo != long.MinValue) Console.WriteLine(resultPartTwo);

		sw.Stop();
		Console.WriteLine(
			$"Finished part two. Time needed: {sw.Elapsed.Minutes}m : {sw.Elapsed.Seconds}s : {sw.Elapsed.Milliseconds}ms");
		Console.WriteLine();
	}
}

public interface IAdventDay
{
	public long PartOne();
	public long PartTwo();
}