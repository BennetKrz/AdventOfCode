namespace AdventOfCode._2021;

public class Day6 : IAdventDay
{
	private readonly string _input;
	private readonly string[] input;

	public Day6()
	{
		_input = Helper.GetInputOneLine(6);
		input = _input.Split(',');
	}

	public long PartOne()
	{
		return SimulateFishes(80);
	}

	public long PartTwo()
	{
		return SimulateFishes(256);
	}

	private long SimulateFishes(int days)
	{
		int maxDays = days;
		ulong[] Ages = new ulong[9];
		foreach (string s in input)
		{
			int i = int.Parse(s);
			Ages[i]++;
		}

		for (int day = 0; day < maxDays; day++)
		{
			ulong fish = Ages[0];
			for (int i = 0; i < 8; i++)
			{
				Ages[i] = Ages[i + 1];
			}

			Ages[6] += fish;
			Ages[8] = fish;
		}

		long fishes = 0;
		foreach (ulong u in Ages)
		{
			fishes += (long)u;
		}

		return fishes;
	}
}