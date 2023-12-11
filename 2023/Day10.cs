using Position = (int x, int y);
using Result = (int wayLength, char pipe);

namespace AdventOfCode._2023;

public class Day10 : IAdventDay
{
	private readonly Dictionary<Lookup, Direction> cameFromMapping;
	private readonly Dictionary<Lookup, Direction> goNextMapping;
	private readonly Dictionary<Position, Result> loopPositions;
	private readonly List<Position> neighbourPositions;
	private readonly Dictionary<Lookup, string> validMoves;
	private readonly List<string> input;

	public Day10()
	{
		input = Helper.GetInput(10).ToList();
		validMoves = new Dictionary<Lookup, string>
		{
			{ new Lookup('|', Direction.North), "JL|" },
			{ new Lookup('|', Direction.South), "7F|" },
			{ new Lookup('-', Direction.West), "7J-" },
			{ new Lookup('-', Direction.East), "FL-" },
			{ new Lookup('L', Direction.North), "-7J" },
			{ new Lookup('L', Direction.East), "|F7" },
			{ new Lookup('J', Direction.North), "-FL" },
			{ new Lookup('J', Direction.West), "|F7" },
			{ new Lookup('7', Direction.South), "-FL" },
			{ new Lookup('7', Direction.West), "|LJ" },
			{ new Lookup('F', Direction.East), "|LJ" },
			{ new Lookup('F', Direction.South), "-7J" }
		};
		goNextMapping = new Dictionary<Lookup, Direction>
		{
			{ new Lookup('|', Direction.North), Direction.South },
			{ new Lookup('|', Direction.South), Direction.North },
			{ new Lookup('-', Direction.West), Direction.East },
			{ new Lookup('-', Direction.East), Direction.West },
			{ new Lookup('L', Direction.North), Direction.East },
			{ new Lookup('L', Direction.East), Direction.North },
			{ new Lookup('J', Direction.North), Direction.West },
			{ new Lookup('J', Direction.West), Direction.North },
			{ new Lookup('7', Direction.South), Direction.West },
			{ new Lookup('7', Direction.West), Direction.South },
			{ new Lookup('F', Direction.East), Direction.South },
			{ new Lookup('F', Direction.South), Direction.East }
		};
		cameFromMapping = new Dictionary<Lookup, Direction>
		{
			{ new Lookup('|', Direction.North), Direction.North },
			{ new Lookup('|', Direction.South), Direction.South },
			{ new Lookup('-', Direction.West), Direction.West },
			{ new Lookup('-', Direction.East), Direction.East },
			{ new Lookup('L', Direction.North), Direction.West },
			{ new Lookup('L', Direction.East), Direction.South },
			{ new Lookup('J', Direction.North), Direction.East },
			{ new Lookup('J', Direction.West), Direction.South },
			{ new Lookup('7', Direction.South), Direction.East },
			{ new Lookup('7', Direction.West), Direction.North },
			{ new Lookup('F', Direction.East), Direction.North },
			{ new Lookup('F', Direction.South), Direction.West }
		};
		neighbourPositions = new List<Position>
		{
			(0, 0),
			(0, 1),
			(0, -1),
			(1, 0),
			(-1, 0),
			(1, 1),
			(-1, 1),
			(1, -1),
			(-1, -1)
		};
		loopPositions = new Dictionary<Position, Result>();
	}

	public long PartOne()
	{
		Position startPos = GetStartPos();
		DepthFirstSearchLoop(startPos, 0, Direction.North);
		return loopPositions.Values.ToArray()[^1].wayLength / 2 + 1;
	}

	public long PartTwo()
	{
		Dictionary<Position, Result> tmp = TransferLoop();

		//Print(tmp);

		int[,] map = GetMapFromDic(tmp);
		map = FloodFillClear(map);
		//Print(map);
		return CountEnclosed(map);
	}

	private long CountEnclosed(int[,] map)
	{
		long counter = 0;
		for (int y = 1; y < map.GetLength(0); y += 3)
		{
			for (int x = 1; x < map.GetLength(1); x += 3)
			{
				if (map[y - 1, x - 1] == 0 &&
				    map[y - 1, x] == 0 &&
				    map[y - 1, x + 1] == 0 &&
				    map[y, x - 1] == 0 &&
				    map[y, x] == 0 &&
				    map[y, x + 1] == 0 &&
				    map[y + 1, x - 1] == 0 &&
				    map[y + 1, x] == 0 &&
				    map[y + 1, x + 1] == 0)
				{
					counter++;
				}
			}
		}
		return counter;
	}

	private int[,] FloodFillClear(int[,] map)
	{
		Stack<Position> stack = new();
		stack.Push((0, 0));

		while (stack.Count != 0)
		{
			Position pos = stack.Pop();
			List<Position> neighbours = GetNeighbours(pos, map);
			foreach (Position p in neighbours)
			{
				stack.Push(p);
			}

			map[pos.y, pos.x] = 8;
		}

		return map;
	}

	private List<Position> GetNeighbours(Position pos, int[,] map)
	{
		List<Position> neighbours = new();
		for (int i = 0; i < neighbourPositions.Count; i++)
		{
			int posX = pos.x + neighbourPositions[i].x;
			int posY = pos.y + neighbourPositions[i].y;
			if (posX >= map.GetLength(1) || posX < 0 || posY >= map.GetLength(0) || posY < 0) continue;

			if (map[posY, posX] == 0) neighbours.Add((posX, posY));
		}

		return neighbours;
	}

	private Dictionary<Position, Result> TransferLoop()
	{
		Dictionary<Position, Result> tmp = new();
		int minX = loopPositions.MinBy(r => r.Key.x).Key.x;
		int minY = loopPositions.MinBy(r => r.Key.y).Key.y;
		for (short y = 0; y <= 150; y++)
		{
			for (short x = 0; x <= 150; x++)
			{
				if (loopPositions.ContainsKey((x, y)))
					tmp.Add((x - minX, y - minY), loopPositions[(x, y)]);
				else
					tmp.Add((x - minX, y - minY), (-1, '.'));
			}
		}

		return tmp;
	}

	private void DepthFirstSearchLoop(Position pos, int distance, Direction cameFrom)
	{
		Stack<(Position pos, int distance, Direction cameFrom)> stack = new();
		stack.Push((pos, distance, cameFrom));
		while (stack.Count != 0)
		{
			(Position pos, int distance, Direction cameFrom) currentPos = stack.Pop();

			if (loopPositions.ContainsKey(currentPos.pos)) continue;

			char currentPipe = input[currentPos.pos.y][currentPos.pos.x];
			Direction nextDirectionToGoTo = goNextMapping[new Lookup(currentPipe, currentPos.cameFrom)];
			Position nextPosition = nextDirectionToGoTo switch
			{
				Direction.North => (currentPos.pos.x, currentPos.pos.y - 1),
				Direction.East => (currentPos.pos.x + 1, currentPos.pos.y),
				Direction.South => (currentPos.pos.x, currentPos.pos.y + 1),
				Direction.West => (currentPos.pos.x - 1, currentPos.pos.y),
				_ => throw new ArgumentOutOfRangeException()
			};
			char nextPipe = input[nextPosition.y][nextPosition.x];
			if (validMoves[new Lookup(currentPipe, currentPos.cameFrom)].Contains(nextPipe))
			{
				stack.Push((nextPosition, currentPos.distance + 1,
				            cameFromMapping[new Lookup(currentPipe, currentPos.cameFrom)]));
				loopPositions.TryAdd(currentPos.pos, (currentPos.distance, currentPipe));
			}
		}
	}

	private Position GetStartPos()
	{
		for (int i = 0; i < input.Count; i++)
		{
			if (!input[i].Contains('S')) continue;
			int posX = input[i].IndexOf('S');
			input[i] = input[i].Replace('S', '|');
			return (posX, i);
		}

		return (0, 0);
	}

	private int[,] GetMapFromDic(Dictionary<Position, Result> tmp)
	{
		int maxX = tmp.MaxBy(r => r.Key.x).Key.x;
		int maxY = tmp.MaxBy(r => r.Key.y).Key.y;

		int[,] map = new int[(maxY + 1) * 3, (maxX + 1) * 3];

		short counterX = 0;
		short counterY = 0;

		for (int y = 1; y <= (maxY + 1) * 3; y += 3)
		{
			for (int x = 1; x <= (maxX + 1) * 3; x += 3)
			{
				char pipe = tmp[(counterX, counterY)].pipe;
				counterX++;
				switch (pipe)
				{
					case '|':
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 1;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 0;
						map[y, x] = 1;
						map[y, x + 1] = 0;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 1;
						map[y + 1, x + 1] = 0;
						break;
					case '-':
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 1;
						map[y, x] = 1;
						map[y, x + 1] = 1;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
					case 'L':
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 1;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 0;
						map[y, x] = 1;
						map[y, x + 1] = 1;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
					case 'J':
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 1;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 1;
						map[y, x] = 1;
						map[y, x + 1] = 0;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
					case '7':
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 1;
						map[y, x] = 1;
						map[y, x + 1] = 0;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 1;
						map[y + 1, x + 1] = 0;
						break;
					case 'F':
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 0;
						map[y, x] = 1;
						map[y, x + 1] = 1;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 1;
						map[y + 1, x + 1] = 0;
						break;
					default:
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						map[y, x - 1] = 0;
						map[y, x] = 0;
						map[y, x + 1] = 0;
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
				}
			}

			counterX = 0;
			counterY++;
		}

		return map;
	}

	private enum Direction
	{
		North,
		East,
		South,
		West
	}

	private record Lookup(char CurPipe, Direction CameFrom) : IEqualityComparer<Lookup>
	{
		public bool Equals(Lookup? x, Lookup? y)
		{
			return y != null && x != null && x.CurPipe == y.CurPipe && x.CameFrom == y.CameFrom;
		}

		public int GetHashCode(Lookup obj)
		{
			return HashCode.Combine(obj.CurPipe, (int)obj.CameFrom);
		}
	}
}