using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day8 : IAdventDay
{
	private List<string> input;

	public Day8()
	{
		this.input = Helper.GetInput(8).ToList();
	}

	public long PartOne()
	{
		Dictionary<string, (string l, string r)> map = ParseInput();
		string instructions = input[0];
		string currentPos = "AAA";
		long steps = 0;
		int i = 0;
		
		while (currentPos != "ZZZ")
		{
			if (currentPos == "ZZZ")
			{
				return steps;
			}

			if (instructions[i] == 'L')
			{
				currentPos = map[currentPos].l;
			} else
			{
				currentPos = map[currentPos].r;
			}
			
			steps++;
			i++;
			if (i >= instructions.Length)
			{
				i = 0;
			}
		}
		return steps;
	}

	public long PartTwo()
	{
		return 0;
	}

	private Dictionary<string, (string l, string r)> ParseInput()
	{
		Dictionary<string, (string l, string r)> map = new();
		for (int i = 2; i < input.Count; i++)
		{
			string level = input[i].Split(" ")[0];
			string l = input[i].Split("(")[1].Substring(0, 3);
			string r = input[i].Split(" ")[3].Substring(0, 3);
			map.Add(level,  (l, r));
		}
		return map;
	}
}