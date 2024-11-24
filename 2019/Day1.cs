namespace AdventOfCode._2019;

public class Day1 : AdventBase
{
	private readonly string[] input;

	public Day1() : base(2019, 1)
	{
		input = Input;
	}

	public override long PartOne()
	{
		return input.Select(i => int.Parse(i) / 3 - 2).Sum();
	}

	public override long PartTwo()
	{
		int result = 0;
		int[] fuel = new int[input.Length];
		for (int i = 0; i < fuel.Length; i++)
		{
			fuel[i] = int.Parse(input[i]);
			fuel[i] = fuel[i] / 3 - 2;
			result += fuel[i];
		}

		for (int i = 0; i < fuel.Length; i++)
		{
			while (fuel[i] > 0)
			{
				if (fuel[i] / 3 - 2 >= 0) result += fuel[i] / 3 - 2;
				fuel[i] = fuel[i] / 3 - 2;
			}
		}

		return result;
	}
}