using Position = (int x, int y);

namespace AdventOfCode._2023;

public class Day14 : IAdventDay
{
	private readonly List<string> input;
	private List<Position> positionOfRocks;
	private List<Position> positionsOfFixedRocks;

	public Day14()
	{
		input = Helper.GetInput(14).ToList();
	}

	public long PartOne()
	{
		long sum = 0;
		positionsOfFixedRocks = GetPositionsOf('#');
		positionOfRocks = GetPositionsOf('O');
		for (int i = 0; i < positionOfRocks.Count; i++)
		{
			Position rock = positionOfRocks[i];
			while (CanMoveNorth(rock))
			{
				positionOfRocks[i] = (rock.x, rock.y - 1);
				rock = positionOfRocks[i];
			}
		}

		for (int y = 0; y < input.Count; y++)
		{
			int factor = input.Count - y;
			sum += positionOfRocks.Count(r => r.y == y) * factor;
		}

		SchimmelPrint();
		return sum;
	}

	public long PartTwo()
	{
		long sum = 0;
		positionsOfFixedRocks = GetPositionsOf('#');
		positionOfRocks = GetPositionsOf('O');
		for (int i = 0; i < 1_000_000_000; i++)
		{
			Cycle();
		}
		for (int y = 0; y < input.Count; y++)
		{
			int factor = input.Count - y;
			sum += positionOfRocks.Count(r => r.y == y) * factor;
		}
		return sum;
	}

	private void Cycle()
	{
		//Alle Nördlich schieben
		for (int i = 0; i < positionOfRocks.Count; i++)
		{
			Position rock = positionOfRocks[i];
			while (CanMoveNorth(rock))
			{
				positionOfRocks[i] = (rock.x, rock.y - 1);
				rock = positionOfRocks[i];
			}
		}
		//Alle Westlich schieben
		for (int i = 0; i < positionOfRocks.Count; i++)
		{
			Position rock = positionOfRocks[i];
			while (CanMoveWest(rock))
			{
				positionOfRocks[i] = (rock.x - 1, rock.y);
				rock = positionOfRocks[i];
			}
		}
		//Alle Südlich schieben
		for (int i = 0; i < positionOfRocks.Count; i++)
		{
			Position rock = positionOfRocks[i];
			while (CanMoveSouth(rock))
			{
				positionOfRocks[i] = (rock.x, rock.y + 1);
				rock = positionOfRocks[i];
			}
		}
		//Alle Östlich schieben
		for (int i = 0; i < positionOfRocks.Count; i++)
		{
			Position rock = positionOfRocks[i];
			while (CanMoveEast(rock))
			{
				positionOfRocks[i] = (rock.x + 1, rock.y);
				rock = positionOfRocks[i];
			}
		}
	}

	private bool CanMoveNorth(Position rock)
	{
		if (rock.y == 0) return false;
		if (positionsOfFixedRocks.Contains((rock.x, rock.y - 1))) return false;
		return !positionOfRocks.Contains((rock.x, rock.y - 1));
	}
	
	private bool CanMoveEast(Position rock)
	{
		if (rock.x == input[0].Length - 1) return false;
		if (positionsOfFixedRocks.Contains((rock.x + 1, rock.y))) return false;
		return !positionOfRocks.Contains((rock.x + 1, rock.y));
	}
	
	private bool CanMoveSouth(Position rock)
	{
		if (rock.y == input.Count - 1) return false;
		if (positionsOfFixedRocks.Contains((rock.x, rock.y + 1))) return false;
		return !positionOfRocks.Contains((rock.x, rock.y + 1));
	}
	
	private bool CanMoveWest(Position rock)
	{
		if (rock.x == 0) return false;
		if (positionsOfFixedRocks.Contains((rock.x - 1, rock.y))) return false;
		return !positionOfRocks.Contains((rock.x - 1, rock.y));
	}

	private List<Position> GetPositionsOf(char target)
	{
		List<Position> fixedRocks = new();
		for (int x = 0; x < input[0].Length; x++)
		{
			for (int y = 0; y < input.Count; y++)
			{
				if (input[y][x] == target) fixedRocks.Add((x, y));
			}
		}

		return fixedRocks;
	}

	private void SchimmelPrint()
	{
		string result = string.Empty;
		for (int y = 0; y < 10; y++)
		{
			for (int x = 0; x < 10; x++)
			{
				if (positionOfRocks.Contains((x, y)))
					result += "O";
				else if (positionsOfFixedRocks.Contains((x, y)))
					result += "#";
				else
					result += ".";
			}

			result += "\n";
		}

		Console.WriteLine(result);
	}
}