using AdventOfCode._2017;

namespace AdventOfCode._2017;

public class Day5 : IAdventDay
{
	List<string> input;

	public Day5()
	{
		this.input = Helper.GetInput(5).ToList();
	}

	public long PartOne()
	{
		List<int> instructions = input.Select(x => int.Parse(x)).ToList();
		int pos = 0;
		int steps = 0;
		while (pos < instructions.Count)
		{
			int oldPos = pos;
			pos += instructions[pos];
			instructions[oldPos]++;
			steps++;
			
		}
		return steps;
	}

	public long PartTwo()
	{
		List<int> instructions = input.Select(x => int.Parse(x)).ToList();
		int pos = 0;
		int steps = 0;
		while (pos < instructions.Count)
		{
			int oldPos = pos;
			int jumpWidth = instructions[pos];
			pos += jumpWidth;
			if (jumpWidth > 2)
			{
				instructions[oldPos]--;
			} else
			{
				instructions[oldPos]++;
			}
			steps++;
			
		}
		return steps;
	}
}