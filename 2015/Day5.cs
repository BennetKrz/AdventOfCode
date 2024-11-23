namespace AdventOfCode._2015;

public class Day5 : AdventBase
{
	private readonly string[] input;

	public Day5() : base(2015, 5)
	{
		input = Input;
	}

	public override long PartOne()
	{
		string[] badStringParts = { "ab", "cd", "pq", "xy" };
		int niceStrings = 0;
		for (int i = 0; i < input.Length; i++)
		{
			string s = input[i];
			if (s.Count(f => f is 'a' or 'e' or 'i' or 'o' or 'u') < 3) continue;

			if (!ContainsCharTwoTimesInRow(s)) continue;

			if (StringContainsBadString(s, badStringParts)) continue;

			niceStrings++;
		}

		return niceStrings;
	}

	public override long PartTwo()
	{
		return input.Count(s => PartTwoRuleOne(s) && PartTwoRuleTwo(s));
	}

	private bool PartTwoRuleOne(string line)
	{
		for (int i = 0; i < line.Length - 3; i++)
		{
			string pair = line.Substring(i, 2);
			for (int j = i + 2; j < line.Length - 1; j++)
			{
				string pairTwo = line.Substring(j, 2);
				if (pairTwo == pair) return true;
			}
		}

		return false;
	}

	private bool PartTwoRuleTwo(string line)
	{
		for (int i = 0; i < line.Length - 2; i++)
		{
			if (line[i] == line[i + 2]) return true;
		}

		return false;
	}

	private bool StringContainsBadString(string input, string[] stringToCheckFor)
	{
		foreach (string s in stringToCheckFor)
		{
			if (input.Contains(s)) return true;
		}

		return false;
	}

	private bool ContainsCharTwoTimesInRow(string input)
	{
		for (int i = 1; i < input.Length; i++)
		{
			if (input[i] == input[i - 1]) return true;
		}

		return false;
	}
}