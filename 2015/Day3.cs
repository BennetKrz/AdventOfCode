using System.Drawing;

namespace AdventOfCode._2015;

public class Day3 : IAdventDay
{
	private readonly string input;

	public Day3()
	{
		input = Helper.GetInputOneLine(3);
	}

	public long PartOne()
	{
		Dictionary<Point, int> visitedHouses = new();
		Point currentPos = new(0, 0);
		//Handle Movement instructions
		for (int i = 0; i < input.Length; i++)
		{
			switch (input[i])
			{
				case '^': //north
					currentPos.Y++;
					break;
				case 'v': //south
					currentPos.Y--;
					break;
				case '>': //East
					currentPos.X++;
					break;
				case '<': //West
					currentPos.X--;
					break;
			}

			if (visitedHouses.ContainsKey(currentPos))
				visitedHouses[currentPos]++;
			else
				visitedHouses.Add(currentPos, 1);
		}

		return visitedHouses.Count + 1;
	}

	public long PartTwo()
	{
		Dictionary<Point, int> visitedHouses = new();
		Point[] positions = new Point[2];
		//Handle Movement instructions
		for (int i = 0; i < input.Length; i++)
		{
			switch (input[i])
			{
				case '^': //north
					positions[i % 2].Y++;
					break;
				case 'v': //south
					positions[i % 2].Y--;
					break;
				case '>': //East
					positions[i % 2].X++;
					break;
				case '<': //West
					positions[i % 2].X--;
					break;
			}

			if (visitedHouses.ContainsKey(positions[i % 2]))
				visitedHouses[positions[i % 2]]++;
			else
				visitedHouses.Add(positions[i % 2], 1);
		}

		return visitedHouses.Count;
	}
}