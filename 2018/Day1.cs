namespace AdventOfCode._2018;

public class Day1 : IAdventDay
{
	private readonly string[] input;

	public Day1()
	{
		input = Helper.GetInput(1);
	}

	public long PartOne()
	{
		int[] input_int = new int[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			input_int[i] = Convert.ToInt32(input[i]);
		}

		return input_int.Sum();
	}

	public long PartTwo()
	{
		int frequency = 0;
		Dictionary<int, bool> allValues = new();
		int[] input_int = new int[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			input_int[i] = Convert.ToInt32(input[i]);
		}

		bool b = true;
		while (b)
		{
			for (int i = 0; i < input_int.Length; i++)
			{
				if (allValues.ContainsKey(frequency))
					return frequency;
				allValues.Add(frequency, true);

				frequency = frequency + input_int[i];
			}
		}

		return long.MinValue;
	}
}