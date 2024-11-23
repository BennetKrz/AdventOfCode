namespace AdventOfCode._2015;

public class Day1 : AdventBase
{
	private readonly string _input;
	
	public Day1() : base(2015, 1)
	{
		_input = InputOneLine;
	}
	
	public override long PartOne()
	{
		int currentFloor = 0;
		foreach (char c in _input)
		{
			switch (c)
			{
				case '(':
					currentFloor++;
					break;
				case ')':
					currentFloor--;
					break;
			}
		}

		return currentFloor;
	}

	public override long PartTwo()
	{
		int currentFloor = 0;
		for (int i = 0; i < _input.Length; i++)
		{
			switch (_input[i])
			{
				case '(':
					currentFloor++;
					break;
				case ')':
					currentFloor--;
					break;
			}

			if (currentFloor == -1)
			{
				return i + 1;
			}
		}

		return long.MinValue;
	}
}