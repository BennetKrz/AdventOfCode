namespace AdventOfCode._2017;

public class Day1 : AdventBase
{
	private readonly string input;

	public Day1() : base(2017, 1)
	{
		input = InputOneLine;
	}

	public override long PartOne()
	{
		return input.Where((t, i) => t == input[i + 1 == input.Length ? 0 : i + 1]).Sum(t => int.Parse(t.ToString()));
	}

	public override long PartTwo()
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