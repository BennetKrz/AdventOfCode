namespace AdventOfCode._2017;

public class Day6 : AdventBase
{
	private readonly string input;
	private readonly List<string> memoryStates = [];

	public Day6() : base(2017, 6)
	{
		input = InputOneLine;
	}
	
	public override long PartOne()
	{
		int[] memoryBanks = ParseInput("\t");
		int redistributionCounter = 0;
		while (memoryStates.Count == memoryStates.Distinct().ToList().Count)
		{
			memoryBanks = RedistributeMemory(memoryBanks);
			memoryStates.Add(string.Join("-", memoryBanks));
			redistributionCounter++;
		}

		return redistributionCounter;
	}


	public override long PartTwo()
	{
		memoryStates.Clear();
		int[] memoryBanks = ParseInput("\t");
		while (memoryStates.Count == memoryStates.Distinct().ToList().Count)
		{
			memoryBanks = RedistributeMemory(memoryBanks);
			memoryStates.Add(string.Join("-", memoryBanks));
		}

		string targetMemoryConfig = string.Join("-", memoryBanks);
		int counter = 0;
		while (true)
		{
			memoryBanks = RedistributeMemory(memoryBanks);
			counter++;
			if (string.Join("-", memoryBanks) == targetMemoryConfig) return counter;
		}
	}

	private int[] ParseInput(string splitByValue)
	{
		return input.Split(splitByValue).Select(int.Parse).ToArray();
	}

	private int[] RedistributeMemory(int[] memoryBanks)
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