using AdventOfCode._2017;

namespace AdventOfCode._2017;

public class Day17 : IAdventDay
{
	List<string> input;

	public Day17()
	{
		this.input = Helper.GetInput(17).ToList();
	}

	public long PartOne()
	{
		
		return 0;
	}

	public long PartTwo()
	{
		return 0;
	}
}

public record Operation(Action a, string operandOne, string operandTwo)
{
	public Action action { get; set; } = a;
	public string operandOne { get; set; } = operandOne;
	public string operandTwo { get; set; } = operandTwo;
}

public enum Action
{
	sound,
	set,
	add,
	mul,
	mod,
	rcv,
	jgz
}