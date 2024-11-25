namespace AdventOfCode._2021;

public class Day6 : AdventBase
{
	private readonly string _input;
	private readonly string[] input;

	public Day6() : base(2021, 6)
	{
		_input = InputOneLine;
		input = _input.Split(',');
	}

	public override long PartOne()
	{
		return SimulateFishes(80);
	}

	public override long PartTwo()
	{
		return SimulateFishes(256);
	}

	private long SimulateFishes(int days)
	{
		int maxDays = days;
		ulong[] ages = new ulong[9];
		foreach (string s in input)
		{
			int i = int.Parse(s);
			ages[i]++;
		}

		for (int day = 0; day < maxDays; day++)
		{
			ulong fish = ages[0];
			for (int i = 0; i < 8; i++)
			{
				ages[i] = ages[i + 1];
			}

			ages[6] += fish;
			ages[8] = fish;
		}

		long fishes = 0;
		foreach (ulong u in ages)
		{
			fishes += (long)u;
		}

		return fishes;
	}
}