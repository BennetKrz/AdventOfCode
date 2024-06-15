namespace AdventOfCode._2019;

public class Day1 : IAdventDay
{
	private readonly string[] input;

	public Day1()
	{
		input = Helper.GetInput(1);
	}

	public long PartOne()
	{
		return input.Select(i => Convert.ToInt32(i) / 3 - 2).Sum();
	}

	public long PartTwo()
	{
		int result = 0;
		int[] fuel = new int[input.Length];
		for (int i = 0; i < fuel.Length; i++)
		{
			fuel[i] = Convert.ToInt32(input[i]);
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