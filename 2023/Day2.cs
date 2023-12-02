using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day2 : IAdventDay
{
	private List<string> input;
	
	private const int MaxRed = 12;
	private const int MaxGreen = 13;
	private const int MaxBlue = 14;

	public Day2()
	{
		this.input = Helper.GetInput(2).ToList();
	}

	public long PartOne()
	{
		List<Game> games = ParseInput();

		int sum = 0;
		
		for (int i = 0; i < games.Count; i++)
		{
			Game g = games[i];
			if (g is { Red: <= MaxRed, Green: <= MaxGreen, Blue: <= MaxBlue })
			{
				sum += (i + 1);
			}
		}
		
		return sum;
	}

	public long PartTwo()
	{
		List<Game> games = ParseInput();
		return games.Select(g => g.Red * g.Green * g.Blue).Sum();
	}

	private List<Game> ParseInput()
	{
		List<Game> games = new();
		foreach (string line in input)
		{
			string importantPart = line.Split(":")[1];
			string[] allTakes = importantPart.Split(";");

			List<int> red = new();
			List<int> green = new();
			List<int> blue = new();
			
			foreach (string take in allTakes)
			{
				List<string> colors = take.Split(",")
				                        .Select(s => s.Trim())
				                        .ToList();
				(int r, int g, int b) color = GetColors(colors);
				red.Add(color.r);
				green.Add(color.g);
				blue.Add(color.b);
			}
			games.Add(new Game(red.Max(), green.Max(), blue.Max()));
		}
		return games;
	}

	private (int r, int g, int b) GetColors(List<string> colors)
	{
		int r = 0;
		int g = 0;
		int b = 0;
		foreach (string color in colors)
		{
			if (color.Contains("red"))
			{
				r = int.Parse(color.Split(" ")[0]);
			} else if (color.Contains("green"))
			{
				g = int.Parse(color.Split(" ")[0]);
			}
			else if (color.Contains("blue"))
			{
				b = int.Parse(color.Split(" ")[0]);
			}
		}

		return (r, g, b);
	}

	private record Game(int Red, int Green, int Blue);
}