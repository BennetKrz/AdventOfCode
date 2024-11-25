namespace AdventOfCode._2022;

public class Day1 : AdventBase
{
	private readonly string[] input;

	public Day1() : base(2022, 1)
	{
		input = Input;
	}

	public override long PartOne()
	{
		List<int> caloriesPerElf = new();
		int count = 0;
		for (int i = 0; i < input.Length; i++)
		{
			if (int.TryParse(input[i], out _))
			{
				count += Convert.ToInt32(input[i]);
			} else
			{
				caloriesPerElf.Add(count);
				count = 0;
			}
		}

		return caloriesPerElf.Max();
	}

	public override long PartTwo()
	{
		List<int> caloriesPerElf = new();
		int count = 0;
		for (int i = 0; i < input.Length; i++)
		{
			if (int.TryParse(input[i], out _))
			{
				count += Convert.ToInt32(input[i]);
			} else
			{
				caloriesPerElf.Add(count);
				count = 0;
			}
		}

		return caloriesPerElf.OrderByDescending(i => i).Take(3).Sum();
	}
}