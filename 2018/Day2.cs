namespace AdventOfCode._2018;

public class Day2 : AdventBase
{
	private readonly string[] input;

	public Day2() : base(2018, 2)
	{
		input = Input;
	}

	public override long PartOne()
	{
		int twoSameLetterCounter = 0;
		int threeSameLetterCounter = 0;

		for (int i = 0; i < input.Length; i++)
		{
			Dictionary<char, int> occurrence = new();
			foreach (char c in input[i])
			{
				if (!occurrence.ContainsKey(c)) occurrence.Add(c, 0);
			}

			foreach (char c in input[i])
			{
				if (input[i].Contains(c)) occurrence[c]++;
			}

			bool b1 = true;
			bool b2 = true;
			foreach (KeyValuePair<char, int> entry in occurrence)
			{
				switch (entry.Value)
				{
					case 3 when b1:
						threeSameLetterCounter++;
						b1 = false;
						break;
					case 2 when b2:
						twoSameLetterCounter++;
						b2 = false;
						break;
				}
			}
		}

		return twoSameLetterCounter * threeSameLetterCounter;
	}

	public override long PartTwo()
	{
		List<(string one, string two)> differByOne = new();
		for (int wordOne = 0; wordOne < input.Length; wordOne++)
		{
			for (int wordTwo = 0; wordTwo < input.Length; wordTwo++)
			{
				int count = 0;
				for (int curChar = 0; curChar < input[wordOne].Length; curChar++)
				{
					if (input[wordOne][curChar] != input[wordTwo][curChar]) count++;
				}

				if (count == 1) differByOne.Add((input[wordOne], input[wordTwo]));
			}
		}

		int index = 0;
		for (int i = 0; i < differByOne[0].one.Length; i++)
		{
			if (differByOne[0].one[i] != differByOne[0].two[i]) index = i;
		}

		differByOne.RemoveAt(1);
		string result = differByOne[0].one.Remove(index, 1);
		Console.WriteLine(result);
		return long.MinValue;
	}
}