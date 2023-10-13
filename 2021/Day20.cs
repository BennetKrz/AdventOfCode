namespace AdventOfCode._2021;

public class Day20 : IAdventDay
{
	private readonly string[] input;
	private string enhancementAlgorithm;

	public Day20()
	{
		input = Helper.GetInput(20);
	}

	public long PartOne()
	{
		HashSet<(int x, int y)> inputImage = ParseInput();
		inputImage = EnhanceMap(inputImage);
		return inputImage.Count;
	}

	public long PartTwo()
	{
		int steps = 50;
		HashSet<(int x, int y)> inputImage = ParseInput();
		for (int i = 0; i < steps / 2; i++)
		{
			inputImage = EnhanceMap(inputImage);
		}

		return inputImage.Count;
	}

	private HashSet<(int x, int y)> EnhanceMap(HashSet<(int x, int y)> map)
	{
		map = LitMap(map, true);
		map = LitMap(map, false);

		return map;

		HashSet<(int x, int y)> LitMap(HashSet<(int x, int y)> map, bool first)
		{
			int minX = map.MinBy(pos => pos.x).x - 1;
			int minY = map.MinBy(pos => pos.y).y - 1;
			int maxX = map.MaxBy(pos => pos.x).x + 1;
			int maxY = map.MaxBy(pos => pos.y).y + 1;

			HashSet<(int x, int y)> litMap = new();

			for (int y = minY; y <= maxY; y++)
			{
				for (int x = minX; x <= maxX; x++)
				{
					if (Lit(x, y)) litMap.Add((x, y));
				}
			}

			if (first && enhancementAlgorithm[0] == '#')
			{
				minX = map.MinBy(pos => pos.x).x - 2;
				minY = map.MinBy(pos => pos.y).y - 2;
				maxX = map.MaxBy(pos => pos.x).x + 2;
				maxY = map.MaxBy(pos => pos.y).y + 2;

				for (int y = minY; y <= maxY; y++)
				{
					litMap.Add((minX, y));
					litMap.Add((maxX, y));
				}

				for (int x = minX; x <= maxX; x++)
				{
					litMap.Add((x, minY));
					litMap.Add((x, maxY));
				}
			}

			return litMap;

			bool Lit(int x, int y)
			{
				int index = 0;
				for (int dy = y - 1; dy <= y + 1; dy++)
				{
					for (int dx = x - 1; dx <= x + 1; dx++)
					{
						index *= 2;
						if (map.Contains((dx, dy)) || (!first && enhancementAlgorithm[0] == '#' &&
						                               (dy <= minY || dy >= maxY || dx <= minX || dx >= maxX)))
							index++;
					}
				}

				return enhancementAlgorithm[index] == '#';
			}
		}
	}

	private HashSet<(int x, int y)> ParseInput()
	{
		enhancementAlgorithm = input[0];
		HashSet<(int x, int y)> inputImage = new();
		for (int y = 2; y < input.Length; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				if (input[y][x] == '#') inputImage.Add((x, y - 2));
			}
		}

		return inputImage;
	}
}