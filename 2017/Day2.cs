namespace AdventOfCode._2017;

public class Day2 : IAdventDay
{
	private readonly List<string> input;

	public Day2()
	{
		input = Helper.GetInput(2).ToList();
	}

	public long PartOne()
	{
		List<List<int>> spreadsheet =
			input.Select(x => x.Replace("\t", " ")
			                   .Split(" ")
			                   .ToList()
			                   .Select(y => int.Parse(y))
			                   .ToList())
			     .ToList();
		return spreadsheet.Select(x => x.Max() - x.Min()).Sum();
	}

	public long PartTwo()
	{
		long counter = 0;
		List<List<int>> spreadsheet =
			input.Select(x => x.Replace("\t", " ")
			                   .Split(" ")
			                   .ToList()
			                   .Select(y => int.Parse(y))
			                   .ToList())
			     .ToList();
		foreach (List<int> row in spreadsheet)
		{
			for (int i = 0; i < row.Count; i++)
			{
				for (int j = 0; j < row.Count; j++)
				{
					if (row[i] == 0 || row[j] == 0 || row[i] == row[j])
					{
						continue;
					}
					if (row[i] % row[j] == 0)
					{
						counter += row[i] / row[j];
					}
				}
			}
		}
		return counter;
	}
}