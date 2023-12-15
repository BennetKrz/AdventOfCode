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
		
		return WeightOnNorthBeams();
	}

	public long PartTwo()
	{
		positionsOfFixedRocks = GetPositionsOf('#');
		positionOfRocks = GetPositionsOf('O');

		/*List<List<Position>> cycles = new();
		
		Cycle();
		(int found, int current) result = (-1, -1);
		while (cycles.Count < 200)
		{
			result = LoopDetected(cycles);
			if (result == (-1, -1))
			{
				Console.WriteLine($"Loop Nr. {cycles.Count} has no loop");
			} else
			{ 
				Console.WriteLine($"Map Nr. {cycles.Count} is identical to Map Nr. {result.found}");
			}
			cycles.Add(positionOfRocks);
			Cycle();
		}*/

		//Gucken, wo der Loop beginnt: 129.
		//Dann Gucken, wie lang der Loop ist: 18
		//Dann einfach (1.000.000.000 - 129) % 18 rechnen
		//Dann weiß man, wie viele Durchläufe man noch machen muss. Somit bleiben das: (1.000.000.000 - 129) % 18 Plus 129
		//Also für diesen Input: 136 mal.
		for (int i = 0; i < 136; i++)
		{
			Cycle();
		}
		
		return WeightOnNorthBeams();
	}

	private (int found, int current) LoopDetected(List<List<Position>> previousMaps)
	{
		if (previousMaps.Count == 0)
		{
			return (-1, -1);
		}

		for (int i = 0; i < previousMaps.Count; i++)
		{
			bool isIdentical = true;
			for (int p = 0; p < previousMaps[i].Count; p++)
			{
				if (!positionOfRocks.Contains(previousMaps[i][p]))
				{
					isIdentical = false;
					break;
				}
			}

			if (isIdentical)
			{
				return (i, previousMaps.Count + 1);
			}
		}

		return (-1, -1);
	}

	private void Cycle()
	{
		positionOfRocks = positionOfRocks.OrderBy(p => p.y).ToList();
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
		positionOfRocks = positionOfRocks.OrderBy(p => p.x).ToList();
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
		positionOfRocks = positionOfRocks.OrderByDescending(p => p.y).ToList();
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
		positionOfRocks = positionOfRocks.OrderByDescending(p => p.x).ToList();
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
	
	private long WeightOnNorthBeams()
	{
		long sum = 0;
		for (int y = 0; y < input.Count; y++)
		{
			int factor = input.Count - y;
			sum += positionOfRocks.Count(r => r.y == y) * factor;
		}

		return sum;
	}
}