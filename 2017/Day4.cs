namespace AdventOfCode._2017;

public class Day4 : AdventBase
{
	private readonly List<string> input;

	public Day4() : base(2017, 4)
	{
		input = Input.ToList();
	}

	public override long PartOne()
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

	public override long PartTwo()
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
			;
		}

		return input.Count - notValid;
	}
}