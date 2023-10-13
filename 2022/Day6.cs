namespace AdventOfCode._2022;

public class Day6 : IAdventDay
{
	private readonly string input;

	public Day6()
	{
		input = Helper.GetInputOneLine(6);
	}

	public long PartOne()
	{
		int counter = 0;
		while (counter < input.Length - 4)
		{
			string s = input.Substring(counter, 4);
			if (s.Distinct().Count() == 4)
				break;
			counter++;
		}

		return counter + 4;
	}

	public long PartTwo()
	{
		int counter = 0;
		while (counter < input.Length - 14)
		{
			string s = input.Substring(counter, 14);
			if (s.Distinct().Count() == 14)
				break;
			counter++;
		}

		return counter + 14;
	}
}