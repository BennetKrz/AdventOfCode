namespace AdventOfCode._2015;

public class Day6 : AdventBase
{
	private readonly string[] input;

	private Mode SwitchMode = Mode.OFF;

	public Day6() : base(2015, 6)
	{
		input = Input;
	}


	public override long PartOne()
	{
		bool[,] map = GenerateMapPartOne();
		for (int i = 0; i < input.Length; i++)
		{
			string line = input[i];
			string[] numbers = line.Split(',', ' ')
			                       .Where(s => s.All(c => char.IsDigit(c)))
			                       .ToArray();
			int startX = int.Parse(numbers[0]);
			int endX = int.Parse(numbers[2]);
			int startY = int.Parse(numbers[1]);
			int endY = int.Parse(numbers[3]);
			if (startX == endX)
				for (int y = startY; y <= endY; y++)
				{
					if ((int)GetMode(line) > 1)
						map[y, startX] = !map[y, startX];
					else
						map[y, startX] = Convert.ToBoolean((int)GetMode(line));
				}
			else if (startY == endY)
				for (int x = startX; x <= endX; x++)
				{
					if ((int)GetMode(line) > 1)
						map[startY, x] = !map[startX, x];
					else
						map[startY, x] = Convert.ToBoolean((int)GetMode(line));
				}
			else
				for (int y = startY; y <= endY; y++)
				{
					for (int x = startX; x <= endX; x++)
					{
						if ((int)GetMode(line) > 1)
							map[y, x] = !map[y, x];
						else
							map[y, x] = Convert.ToBoolean((int)GetMode(line));
					}
				}
		}

		return map.Cast<bool>().Count(b => b);
	}

	public override long PartTwo()
	{
		int[,] map = GenerateMapPartTwo();
		for (int i = 0; i < input.Length; i++)
		{
			string line = input[i];
			string[] numbers = line.Split(',', ' ')
			                       .Where(s => s.All(c => char.IsDigit(c)))
			                       .ToArray();
			int startX = int.Parse(numbers[0]);
			int endX = int.Parse(numbers[2]);
			int startY = int.Parse(numbers[1]);
			int endY = int.Parse(numbers[3]);
			if (startX == endX)
				for (int y = startY; y <= endY; y++)
				{
					if ((int)GetMode(line) > 1)
					{
						map[y, startX] += 2;
					} else
					{
						map[y, startX] += GetMode(line) == Mode.ON ? 1 : -1;
						if (map[y, startX] < 0) map[y, startX] = 0;
					}
				}
			else if (startY == endY)
				for (int x = startX; x <= endX; x++)
				{
					if ((int)GetMode(line) > 1)
					{
						map[startY, x] += 2;
					} else
					{
						map[startY, x] += GetMode(line) == Mode.ON ? 1 : -1;
						if (map[startY, x] < 0) map[startY, x] = 0;
					}
				}
			else
				for (int y = startY; y <= endY; y++)
				{
					for (int x = startX; x <= endX; x++)
					{
						if ((int)GetMode(line) > 1)
						{
							map[y, x] += 2;
						} else
						{
							map[y, x] += GetMode(line) == Mode.ON ? 1 : -1;
							if (map[y, x] < 0) map[y, x] = 0;
						}
					}
				}
		}

		return map.Cast<int>().Sum();
	}

	private Mode GetMode(string s)
	{
		if (s.Contains("off"))
			return Mode.OFF;
		if (s.Contains("on"))
			return Mode.ON;
		if (s.Contains("toggle"))
			return Mode.TOGGLE;
		return Mode.TOGGLE;
	}

	private bool[,] GenerateMapPartOne()
	{
		bool[,] map = new bool[1000, 1000];
		for (int y = 0; y < map.GetLength(0); y++)
		{
			for (int x = 0; x < map.GetLength(1); x++)
			{
				map[y, x] = false;
			}
		}

		return map;
	}

	private int[,] GenerateMapPartTwo()
	{
		int[,] map = new int[1000, 1000];
		for (int y = 0; y < map.GetLength(0); y++)
		{
			for (int x = 0; x < map.GetLength(1); x++)
			{
				map[y, x] = 0;
			}
		}

		return map;
	}

	private enum Mode
	{
		OFF,
		ON,
		TOGGLE
	}
}