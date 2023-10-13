using System.Runtime.InteropServices;

namespace AdventOfCode._2018;

public static class Helper
{
	private static string GetPath()
	{
		string defaultPath = "";
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			defaultPath = @"G:\C#\AdventOfCode\2018\Input\day_";
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			defaultPath = @"/home/d0d0/RiderProjects/AdventOfCode/2018/Input/day_";

		return defaultPath;
	}

	public static string[] GetInput(int day)
	{
		return File.ReadAllLines($"{GetPath()}{day}.txt");
	}

	public static string GetInputOneLine(int day)
	{
		return File.ReadAllText($"{GetPath()}{day}.txt");
	}
}