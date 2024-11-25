namespace AdventOfCode._2021;

public class Day25 : AdventBase
{
	private readonly string[] input;

	public Day25() : base(2021, 25)
	{
		input = Input;
	}

	public override long PartOne()
	{
		char[,] map = ParseInput();
		//PrintArray(map);
		int stepMax = 1000;
		for (int step = 0; step < stepMax; step++)
		{
			//Jetzt gucken, dass alle die nach osten Zeigen, ob die sich aktuell bewegen können. Wenn ja, dann zu liste Hinzufügen.
			List<(int tx, int ty, int ox, int oy)> allEastToMove = new();
			for (int y = 0; y < map.GetLength(1); y++)
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					if (map[x, y] == '>')
					{
						(int tx, int ty, int ox, int oy) pos = GetCanMove(x, y, map);
						if (pos.tx != -1 && pos.tx != -1) allEastToMove.Add(pos);
					}
				}
			}

			if (allEastToMove.Count > 0) map = MoveAll(allEastToMove, map);

			List<(int tx, int ty, int ox, int oy)> allSouthToMove = new();
			for (int y = 0; y < map.GetLength(1); y++)
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					if (map[x, y] == 'v')
					{
						(int tx, int ty, int ox, int oy) pos = GetCanMove(x, y, map);
						if (pos.tx != -1 && pos.tx != -1) allSouthToMove.Add(pos);
					}
				}
			}

			if (allSouthToMove.Count > 0) map = MoveAll(allSouthToMove, map);

			if (allSouthToMove.Count == 0 && allEastToMove.Count == 0) return step + 1;
			//PrintArray(map);
		}

		return long.MinValue;
	}

	public override long PartTwo()
	{
		return long.MinValue;
	}

	private char[,] MoveAll(List<(int tx, int ty, int ox, int oy)> moves, char[,] map)
	{
		for (int i = 0; i < moves.Count; i++)
		{
			(int tx, int ty, int ox, int oy) p = moves[i];
			map[p.tx, p.ty] = map[p.ox, p.oy];
			map[p.ox, p.oy] = '.';
		}

		return map;
	}

	private (int tx, int ty, int ox, int oy) GetCanMove(int x, int y, char[,] map)
	{
		//Richtung feststellen:
		if (map[x, y] == '>')
		{
			if (x < map.GetLength(0) - 1)
			{
				if (map[x + 1, y] == '.') return (x + 1, y, x, y);
			} else
			{
				if (map[0, y] == '.') return (0, y, x, y);
			}
		} else if (map[x, y] == 'v')
		{
			if (y < map.GetLength(1) - 1)
			{
				if (map[x, y + 1] == '.') return (x, y + 1, x, y);
			} else
			{
				if (map[x, 0] == '.') return (x, 0, x, y);
			}
		}

		return (-1, -1, -1, -1);
	}

	private char[,] ParseInput()
	{
		char[,] map = new char[input[0].Length, input.Length];
		for (int y = 0; y < input.Length; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				map[x, y] = input[y][x];
			}
		}

		return map;
	}

	private void PrintArray(char[,] map)
	{
		string output = string.Empty;
		for (int y = 0; y < map.GetLength(1); ++y)
		{
			string part = string.Empty;
			for (int x = 0; x < map.GetLength(0); x++)
			{
				part += map[x, y];
			}

			output += part + "\n";
		}

		Console.WriteLine(output);
	}
}