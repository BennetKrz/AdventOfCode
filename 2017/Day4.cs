namespace AdventOfCode._2017;

public class Day4 : IAdventDay
{
	private readonly List<string> input;

	public Day4()
	{
		input = Helper.GetInput(4).ToList();
	}

	public long PartOne()
	{
		int valid = 0;
		foreach (string s in input)
		{
			List<string> words = s.Split(" ").ToList();
			int sizeOld = words.Count;
			if (sizeOld == words.Distinct().ToList().Count) valid++;
		}

		return valid;
	}

	public long PartTwo()
	{
		int notValid = 0;
		foreach (string s in input)
		{
			List<string> words = s.Split(" ").ToList();
			List<List<char>> tmp = words.Select(x => x.Select(c => c).ToList()).ToList();
			for (int i = 0; i < tmp.Count; i++)
			{
				tmp[i].Sort();
			}

			for (int i = 0; i < tmp.Count; i++)
			{
				for (int j = 0; j < tmp.Count; j++)
				{
					if (i == j) continue;

					if (tmp[i].OrderBy(x => x).SequenceEqual(tmp[j].OrderBy(x => x)))
					{
						notValid++;
						goto GoHere;
					}
				}
			}

			GoHere:
			int t = 0;
		}

		return input.Count - notValid;
	}
}