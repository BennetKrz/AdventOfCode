namespace AdventOfCode._2015;

public class Day1 : IAdventDay
{
	private readonly string input;

	public Day1()
	{
		input = Helper.GetInputOneLine(1);
	}

	public long PartOne()
	{
		int currentFloor = 0;
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] == '(')
				currentFloor++;
			else if (input[i] == ')') currentFloor--;
		}

		return currentFloor;
	}

	public long PartTwo()
	{
		int currentFloor = 0;
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] == '(')
				currentFloor++;
			else if (input[i] == ')') currentFloor--;

			if (currentFloor == -1) return i + 1;
		}

		return long.MinValue;
	}
}