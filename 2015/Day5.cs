namespace AdventOfCode._2015;

public class Day5 : IAdventDay
{
	private readonly string[] input;

	public Day5()
	{
		input = Helper.GetInput(5);
	}

	public long PartOne()
	{
		string[] badStringParts = { "ab", "cd", "pq", "xy" };
		int niceStrings = 0;
		for (int i = 0; i < input.Length; i++)
		{
			string s = input[i];
			if (s.Count(f => f == 'a' || f == 'e' || f == 'i' || f == 'o' || f == 'u') < 3) continue;

			if (!containsCharTwoTimesInRow(s)) continue;

			if (stringContainsBadString(s, badStringParts)) continue;

			niceStrings++;
		}

		return niceStrings;
	}

	public long PartTwo()
	{
		int counter = 0;
		foreach (string s in input)
		{
			if (PartTwoRuleOne(s) && PartTwoRuleTwo(s)) counter++;
		}

		return counter;
	}

	private bool PartTwoRuleOne(string line)
	{
		string pair;
		for (int i = 0; i < line.Length - 3; i++)
		{
			pair = line.Substring(i, 2);
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

	private bool stringContainsBadString(string input, string[] stringToCheckFor)
	{
		foreach (string s in stringToCheckFor)
		{
			if (input.Contains(s)) return true;
		}

		return false;
	}

	private bool containsCharTwoTimesInRow(string input)
	{
		for (int i = 1; i < input.Length; i++)
		{
			if (input[i] == input[i - 1]) return true;
		}

		return false;
	}
}