namespace AdventOfCode._2022;

public class Day3 : AdventBase
{
	private readonly string[] input;
	private const string Points = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public Day3() : base(2022, 3)
	{
		input = Input;
	}

	public override long PartOne()
	{
		
		string[] inputFirstHalf = new string[input.Length];
		string[] inputSecondHalf = new string[input.Length];

		for (int i = 0; i < input.Length; i++)
		{
			inputFirstHalf[i] = input[i].Substring(0, input[i].Length / 2);
			inputSecondHalf[i] = input[i].Substring(input[i].Length / 2, input[i].Length / 2);
		}

		int score = 0;

		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < inputFirstHalf[i].Length; j++)
			{
				if (inputFirstHalf[i].Contains(inputSecondHalf[i][j]))
				{
					char c = inputSecondHalf[i][j];
					int index = Points.IndexOf(c);
					score += index + 1;
					break;
				}
			}
		}

		return score;
	}

	public override long PartTwo()
	{
		int score = 0;
		string[] group = new string[input.Length / 3];
		for (int i = 0; i < input.Length; i++)
		{
			input[i] = new string(input[i].Distinct().ToArray());
		}

		for (int i = 0; i < input.Length; i++)
		{
			if (i % 3 == 0) group[i / 3] = input[i] + input[i + 1] + input[i + 2];
		}

		for (int i = 0; i < group.Length; i++)
		{
			IEnumerable<IGrouping<char, char>> groups = group[i].GroupBy(c => c).Where(g => g.Count() > 2);
			char c = 'A';
			foreach (IGrouping<char, char> item in groups)
			{
				c = item.Key;
			}

			int index = Points.IndexOf(c);
			score += index + 1;
		}

		return score;
	}
}