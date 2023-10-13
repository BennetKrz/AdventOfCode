namespace AdventOfCode._2022;

public class Day7 : IAdventDay
{
	private readonly string[] input;

	public Day7()
	{
		input = Helper.GetInput(7);
	}

	public long PartOne()
	{
		return DictionarySizes(input).Where(size => size < 100_000).Sum();
	}

	public long PartTwo()
	{
		List<long> sizes = DictionarySizes(input);
		return sizes.Where(size => size + (70_000_000 - sizes.Max()) >= 30_000_000).Min();
	}

	private List<long> DictionarySizes(string[] input)
	{
		Stack<string> path = new();
		Dictionary<string, long> sizes = new();
		foreach (string line in input)
		{
			if (line == "$ cd ..")
			{
				path.Pop();
			} else if (line.StartsWith("$ cd"))
			{
				path.Push(string.Join("", path) + line.Split(' ')[2]);
			} else if (line != "$ ls" && !line.Contains("dir "))
			{
				int size = int.Parse(line.Split(" ")[0]);
				foreach (string dir in path)
				{
					sizes[dir] = sizes.GetValueOrDefault(dir) + size;
				}
			}
		}

		return sizes.Values.ToList();
	}
}