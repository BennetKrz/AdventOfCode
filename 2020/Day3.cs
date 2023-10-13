namespace AdventOfCode._2020;

public class Day3 : IAdventDay
{
	private readonly bool[,] field;
	private readonly string[] input;
	private readonly int width;
	private int posX;
	private int posY;

	public Day3()
	{
		input = Helper.GetInput(3);
		field = new bool[input.Length, input[0].Length];
		width = input[0].Length;
		umwandeln();
	}

	public long PartOne()
	{
		return countTrees(3, 1);
	}

	public long PartTwo()
	{
		int[] right = { 1, 3, 5, 7, 1 };
		int[] down = { 1, 1, 1, 1, 2 };
		int[] results = new int[5];
		for (int i = 0; i < 5; i++)
		{
			results[i] = countTrees(right[i], down[i]);
		}

		long result = 1;
		foreach (int i in results)
		{
			result *= i;
		}

		return result;
	}

	private void umwandeln()
	{
		for (int y = 0; y < input.Length; y++)
		{
			for (int x = 0; x < input[0].Length; x++)
			{
				if (input[y][x] == '#') field[y, x] = true;
			}
		}
	}

	private int countTrees(int r, int d)
	{
		int count = 0;
		posX = 0;
		posY = 0;
		while (posY < field.GetLength(0) - 1)
		{
			posX += r;
			posY += d;
			if (posX >= width) posX -= width;

			if (field[posY, posX]) count++;
		}

		return count;
	}
}