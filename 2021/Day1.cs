namespace AdventOfCode._2021;

public class Day1 : AdventBase
{
	private readonly string[] input;

	public Day1() : base(2021, 1)
	{
		input = Input;
	}

	public override long PartOne()
	{
		return input.Where((num, index) =>
			                   index > 0 && Convert.ToInt32(num) > Convert.ToInt32(input[index - 1]))
		            .ToList().Count;
	}

	public override long PartTwo()
	{
		return input.Where((num, index) =>
				index > 3 &&
				int.Parse(input[index]) + int.Parse(input[index - 1]) +
				int.Parse(input[index - 2])
				>
				int.Parse(input[index - 1]) + int.Parse(input[index - 2]) +
				int.Parse(input[index - 3]))
			.Count();
	}
}