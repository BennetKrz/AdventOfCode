namespace AdventOfCode._2020;

public class Day1 : IAdventDay
{
	private readonly string[] input;

	public Day1()
	{
		input = Helper.GetInput(1);
	}

	public long PartOne()
	{
		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < input.Length; j++)
			{
				int in_i = Convert.ToInt32(input[i]);
				int in_j = Convert.ToInt32(input[j]);
				if (in_i + in_j == 2020) return in_j * in_i;
			}
		}

		return long.MinValue;
	}

	public long PartTwo()
	{
		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < input.Length; j++)
			{
				for (int k = 0; k < input.Length; k++)
				{
					int in_i = Convert.ToInt32(input[i]);
					int in_j = Convert.ToInt32(input[j]);
					int in_k = Convert.ToInt32(input[k]);
					if (in_i + in_j + in_k == 2020) return in_i * in_j * in_k;
				}
			}
		}

		return long.MinValue;
	}
}