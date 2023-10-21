namespace AdventOfCode._2017;

public class Day6 : IAdventDay
{
	private readonly string input = Helper.GetInputOneLine(6);
	private readonly List<string> memoryStates = new();

	public long PartOne()
	{
		int[] memoryBanks = ParseInput("\t");
		int redistributionCounter = 0;
		while (memoryStates.Count == memoryStates.Distinct().ToList().Count)
		{
			memoryBanks = redistributeMemory(memoryBanks);
			memoryStates.Add(string.Join("-", memoryBanks));
			redistributionCounter++;
		}

		return redistributionCounter;
	}


	public long PartTwo()
	{
		memoryStates.Clear();
		int[] memoryBanks = ParseInput("\t");
		int redistributionCounter = 0;
		string targetMemoryConfig = string.Empty;
		while (memoryStates.Count == memoryStates.Distinct().ToList().Count)
		{
			memoryBanks = redistributeMemory(memoryBanks);
			memoryStates.Add(string.Join("-", memoryBanks));
			redistributionCounter++;
		}

		targetMemoryConfig = string.Join("-", memoryBanks);
		int counter = 0;
		while (true)
		{
			memoryBanks = redistributeMemory(memoryBanks);
			counter++;
			if (string.Join("-", memoryBanks) == targetMemoryConfig) return counter;
		}
	}

	private int[] ParseInput(string splitByValue)
	{
		return input.Split(splitByValue).Select(s => int.Parse(s)).ToArray();
	}

	private int[] redistributeMemory(int[] memoryBanks)
	{
		int index = IndexOfHighest(memoryBanks);
		int amount = memoryBanks[index];
		memoryBanks[index++] = 0;
		if (index >= memoryBanks.Length) index = 0;
		for (int i = 0; i < amount; i++)
		{
			memoryBanks[index]++;
			index++;
			if (index >= memoryBanks.Length) index = 0;
		}

		return memoryBanks;
	}

	private int IndexOfHighest(int[] memoryBanks)
	{
		return memoryBanks.ToList().IndexOf(memoryBanks.Max());
	}
}