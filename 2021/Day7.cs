namespace AdventOfCode._2021;

public class Day7 : AdventBase
{
	private readonly int[] input;

	public Day7() : base(2021, 7)
	{
		input = GetConvertedInput<int>();
	}

	public override long PartOne()
	{
		Dictionary<int, int> fuelForPosition = new();
		for (int i = 0; i < input.Max(); i++)
		{
			List<int> fuelNeededAllCrabs = input.Select(t => CalculateFuel(t, i)).ToList();

			fuelForPosition.Add(i, fuelNeededAllCrabs.Sum());
		}

		return fuelForPosition.Values.Min();
	}

	public override long PartTwo()
	{
		Dictionary<int, int> fuelForPosition = new();
		for (int i = 0; i < input.Max(); i++)
		{
			List<int> fuelNeededAllCrabs = input.Select(t => CalculateFuelPartTwo(t, i)).ToList();

			fuelForPosition.Add(i, fuelNeededAllCrabs.Sum());
		}

		return fuelForPosition.Values.Min();
	}

	private int CalculateFuel(int startPosition, int targetPosition)
	{
		return Math.Abs(startPosition - targetPosition);
	}

	private int CalculateFuelPartTwo(int startPosition, int targetPosition)
	{
		int delta = CalculateFuel(startPosition, targetPosition);
		int fuel = 0;
		for (int i = startPosition; i < startPosition + delta; i++)
		{
			fuel += i - startPosition + 1;
		}

		return fuel;
	}
}