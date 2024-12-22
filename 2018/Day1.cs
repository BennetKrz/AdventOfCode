namespace AdventOfCode._2018;

public class Day1 : AdventBase
{
	private readonly string[] input;

	public Day1() : base(2018, 1)
	{
		input = Input;
	}

	public override long PartOne()
	{
		int[] inputInt = new int[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			inputInt[i] = Convert.ToInt32(input[i]);
		}

		return inputInt.Sum();
	}

	public override long PartTwo()
	{
		int frequency = 0;
		Dictionary<int, bool> allValues = new();
		int[] inputInt = new int[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			inputInt[i] = Convert.ToInt32(input[i]);
		}

		while (true)
		{
			for (int i = 0; i < inputInt.Length; i++)
			{
				if (allValues.ContainsKey(frequency))
					return frequency;
				allValues.Add(frequency, true);

				frequency = frequency + inputInt[i];
			}
		}
	}
}