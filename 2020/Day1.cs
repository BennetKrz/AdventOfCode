namespace AdventOfCode._2020;

public class Day1 : AdventBase
{
	private readonly string[] input;

	public Day1() : base(2020, 1)
	{
		input = Input;
	}

	public override long PartOne()
	{
		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < input.Length; j++)
			{
				int inI = Convert.ToInt32(input[i]);
				int inJ = Convert.ToInt32(input[j]);
				if (inI + inJ == 2020) return inJ * inI;
			}
		}

		return long.MinValue;
	}

	public override long PartTwo()
	{
		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < input.Length; j++)
			{
				for (int k = 0; k < input.Length; k++)
				{
					int inI = Convert.ToInt32(input[i]);
					int inJ = Convert.ToInt32(input[j]);
					int inK = Convert.ToInt32(input[k]);
					if (inI + inJ + inK == 2020) return inI * inJ * inK;
				}
			}
		}

		return long.MinValue;
	}
}