namespace AdventOfCode._2021;

public class Day1 : IAdventDay
{
	private readonly string[] input;

	public Day1()
	{
		input = Helper.GetInput(1);
	}

	public long PartOne()
	{
		return input.Where((num, index) =>
			                   index > 0 && Convert.ToInt32(num) > Convert.ToInt32(input[index - 1]))
		            .ToList().Count;
	}

	public long PartTwo()
	{
		return input.Where((num, index) =>
			                   index > 3 &&
			                   Convert.ToInt32(input[index]) + Convert.ToInt32(input[index - 1]) +
			                   Convert.ToInt32(input[index - 2])
			                   >
			                   Convert.ToInt32(input[index - 1]) + Convert.ToInt32(input[index - 2]) +
			                   Convert.ToInt32(input[index - 3]))
		            .ToList().Count;
	}
}