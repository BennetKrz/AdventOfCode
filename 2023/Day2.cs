using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day2 : IAdventDay
{
	List<string> input;

	public Day2()
	{
		this.input = Helper.GetInput(2).ToList();
	}

	public long PartOne()
	{
		const int maxRed = 12;
		const int maxGreen = 13;
		const int maxBlue = 14;
		List<Game> games = ParseInput();

		int sum = 0;
		
		for (int i = 0; i < games.Count; i++)
		{
			Game g = games[i];
			if (g is { Red: <= maxRed, Green: <= maxGreen, Blue: <= maxBlue })
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
		for (int i = 0; i < input.Count; i++)
		{
			string importantPart = input[i].Split(":")[1];
			string[] allTakes = importantPart.Split(";");

			List<int> red = new();
			List<int> green = new();
			List<int> blue = new();
			
			for (int t = 0; t < allTakes.Length; t++)
			{
				List<string> colors = allTakes[t].Split(",")
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