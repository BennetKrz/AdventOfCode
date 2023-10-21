namespace AdventOfCode._2017;

public class Day1 : IAdventDay
{
	private readonly string input;

	public Day1()
	{
		input = Helper.GetInputOneLine(1);
	}

	public long PartOne()
	{
		int counter = 0;
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] == input[i + 1 == input.Length ? 0 : i + 1]) counter += int.Parse(input[i].ToString());
		}

		return counter;
	}

	public long PartTwo()
	{
		int counter = 0;
		for (int i = 0; i < input.Length; i++)
		{
			int halfLengthOfList = input.Length / 2;
			if (input[i] == input[i + halfLengthOfList >= input.Length
				                      ? i + halfLengthOfList - input.Length
				                      : i +
				                        halfLengthOfList])
				counter += int.Parse(input[i].ToString());
		}

		return counter;
	}
}