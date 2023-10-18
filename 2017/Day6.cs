namespace AdventOfCode._2017;

public class Day6 : IAdventDay
{
	private List<string> memoryStates = new(); 
	private string input = Helper.GetInputOneLine(6);
	public long PartOne()
	{
		int[] memoryBanks = ParseInput("\t");
		int redistributionCounter = 0;
		while (this.memoryStates.Count == this.memoryStates.Distinct().ToList().Count)
		{
			memoryBanks = redistributeMemory(memoryBanks);
			this.memoryStates.Add(string.Join("-", memoryBanks));
			redistributionCounter++;
		}
		return redistributionCounter;
	}

	private int[] redistributeMemory(int[] memoryBanks)
	{
		int index = IndexOfHighest(memoryBanks);
		int amount = memoryBanks[index];
		memoryBanks[index++] = 0;
		if (index >= memoryBanks.Length)
		{
			index = 0;
		}
		for (int i = 0; i < amount; i++)
		{
			memoryBanks[index]++;
			index++;
			if (index >= memoryBanks.Length)
			{
				index = 0;
			}
		}

		return memoryBanks;
	}

	private int IndexOfHighest(int[] memoryBanks)
	{
		return memoryBanks.ToList().IndexOf(memoryBanks.Max());
	}

	public long PartTwo()
	{
		return 0;
	}

	private int[] ParseInput(string splitByValue)
	{
		return this.input.Split(splitByValue).Select(s => int.Parse(s)).ToArray();
	}
}