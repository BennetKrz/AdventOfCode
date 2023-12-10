using System.Runtime.InteropServices;
using System.Text;
using Position = (short x, short y);
using Result = (short wayLength, char pipe);

namespace AdventOfCode._2023;

public class Day10 : IAdventDay
{
	private List<string> input;
	private Dictionary<Position, Result> dfsResult;
	private Dictionary<Lookup, string> validMoves;
	private Dictionary<Lookup, Direction> goNextMapping;
	private Dictionary<Lookup, Direction> cameFromMapping;
 	
	public Day10()
	{
		this.input = Helper.GetInput(10).ToList();
		validMoves = new Dictionary<Lookup, string>
		{
			//Vertical
			{new Lookup('|', Direction.North), "JL|"},
			{new Lookup('|', Direction.South), "7F|"},
			//Horizontal
			{new Lookup('-', Direction.West), "7J-"},
			{new Lookup('-', Direction.East), "FL-"},
			//Jetzt die ecken
			{new Lookup('L', Direction.North), "-7J"},
			{new Lookup('L', Direction.East), "|F7"},
			//Nächste Ecke
			{new Lookup('J', Direction.North), "-FL"},
			{new Lookup('J', Direction.West), "|F7"},
			//Nächste
			{new Lookup('7', Direction.South), "-FL"},
			{new Lookup('7', Direction.West), "|LJ"},
			//Nächste
			{new Lookup('F', Direction.East), "|LJ"},
			{new Lookup('F', Direction.South), "-7J"}
		};
		goNextMapping = new Dictionary<Lookup, Direction>
		{
			{new Lookup('|', Direction.North), Direction.South},
			{new Lookup('|', Direction.South), Direction.North},
			//Horizontal
			{new Lookup('-', Direction.West), Direction.East},
			{new Lookup('-', Direction.East), Direction.West},
			//Jetzt die ecken
			{new Lookup('L', Direction.North), Direction.East},
			{new Lookup('L', Direction.East), Direction.North},
			//Nächste Ecke
			{new Lookup('J', Direction.North), Direction.West},
			{new Lookup('J', Direction.West), Direction.North},
			//Nächste
			{new Lookup('7', Direction.South), Direction.West},
			{new Lookup('7', Direction.West), Direction.South},
			//Nächste
			{new Lookup('F', Direction.East), Direction.South},
			{new Lookup('F', Direction.South), Direction.East}
		};
		cameFromMapping = new Dictionary<Lookup, Direction>
		{
			{new Lookup('|', Direction.North), Direction.North},
			{new Lookup('|', Direction.South), Direction.South},
			//Horizontal
			{new Lookup('-', Direction.West), Direction.West},
			{new Lookup('-', Direction.East), Direction.East},
			//Jetzt die ecken
			{new Lookup('L', Direction.North), Direction.West},
			{new Lookup('L', Direction.East), Direction.South},
			//Nächste Ecke
			{new Lookup('J', Direction.North), Direction.East},
			{new Lookup('J', Direction.West), Direction.South},
			//Nächste
			{new Lookup('7', Direction.South), Direction.East},
			{new Lookup('7', Direction.West), Direction.North},
			//Nächste
			{new Lookup('F', Direction.East), Direction.North},
			{new Lookup('F', Direction.South), Direction.West}
		};
		dfsResult = new();
		
	}
	
	public long PartOne()
	{
		Thread thread = new(PartOneThreadBased, 8 * 1024 * 1024);
		thread.Start();
		return long.MinValue;
	}

	public long PartTwo()
	{
		Thread.Sleep(1000);
		Thread thread = new(PartTwoThreadBased, 8 * 1024 * 1024);
		thread.Start();
		return long.MinValue;
	}

	private void PartOneThreadBased()
	{
		Position startPos = (0, 0);
		for (short i = 0; i < input.Count; i++)
		{
			if (input[i].IndexOf('S') != -1)
			{
				startPos = ((short)input[i].IndexOf('S'), i);
				input[startPos.y] = input[startPos.y].Replace('S', '|');
			}
		}
		GoNextStep(startPos, 0, Direction.North);
		Console.WriteLine("Part One: " + ((dfsResult.Values.ToArray()[^1].wayLength / 2) + 1).ToString());
	}
	
	private void PartTwoThreadBased()
	{
		input = Helper.GetInput(10).ToList();
		Position startPos = (0, 0);
		for (short i = 0; i < input.Count; i++)
		{
			if (input[i].IndexOf('S') != -1)
			{
				startPos = ((short)input[i].IndexOf('S'), i);
				input[startPos.y] = input[startPos.y].Replace('S', '|');
			}
		}
		GoNextStep(startPos, 0, Direction.North);
		//Jetzt hat man die Struktur


		Dictionary<Position, Result> tmp = TransferLoop();
		Print(tmp);

		int[,] map = GetMapFromDic(tmp);

		Print(map);
		Console.WriteLine("Part Two: " + 0);
	}

	private int[,] GetMapFromDic(Dictionary<Position, Result> tmp)
	{
		short minX = tmp.MinBy(r => r.Key.x).Key.x;
		short minY = tmp.MinBy(r => r.Key.y).Key.y;
		short maxX = tmp.MaxBy(r => r.Key.x).Key.x;
		short maxY = tmp.MaxBy(r => r.Key.y).Key.y;
		
		int[,] map = new int[(maxY + 1) * 3, (maxX + 1) * 3];

		short counterX = 0;
		short counterY = 0;
		
		for (int y = 1; y <= ((maxY + 1) * 3); y += 3)
		{
			for (int x = 1; x <= ((maxX + 1) * 3); x += 3)
			{
				char pipe = tmp[(counterX, counterY)].pipe;
				counterX++;
				switch (pipe)
				{
					case '|':
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 1;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 0;
						map[y, x] = 1;
						map[y, x + 1] = 0;
						//Unterste Reihe
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 1;
						map[y + 1, x + 1] = 0;
						break;
					case '-':
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 1;
						map[y, x] = 1;
						map[y, x + 1] = 1;
						//Unterste Reihe
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
					case 'L':
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 1;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 0;
						map[y, x] = 1;
						map[y, x + 1] = 1;
						//Unterste Reihe
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
					case 'J':
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 1;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 1;
						map[y, x] = 1;
						map[y, x + 1] = 0;
						//Unterste Reihe
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 0;
						map[y + 1, x + 1] = 0;
						break;
					case '7':
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 1;
						map[y, x] = 1;
						map[y, x + 1] = 0;
						//Unterste Reihe
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 1;
						map[y + 1, x + 1] = 0;
						break;
					case 'F':
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 0;
						map[y, x] = 1;
						map[y, x + 1] = 1;
						//Unterste Reihe
						map[y + 1, x - 1] = 0;
						map[y + 1, x] = 1;
						map[y + 1, x + 1] = 0;
						break;
					default:
						//oberste reihe
						map[y - 1, x - 1] = 0;
						map[y - 1, x] = 0;
						map[y - 1, x + 1] = 0;
						//Mittlere reihe
						map[y, x - 1] = 0;
						map[y, x] = 0;
						map[y, x + 1] = 0;
						//Unterste Reihe
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

	private void Print(Dictionary<Position, Result> tmp)
	{
		StringBuilder sb = new();
		for (short y = tmp.MinBy(k => k.Key.y).Key.y; y < tmp.MaxBy(k => k.Key.y).Key.y; y++)
		{
			for (short x = tmp.MinBy(k => k.Key.x).Key.x; x < tmp.MaxBy(k => k.Key.x).Key.x; x++)
			{
				sb.Append(tmp[(x, y)].pipe);
			}

			sb.Append('\n');
		}
		File.WriteAllText("G:\\result.txt", sb.ToString());
	}

	private void Print(int[,] map)
	{
		StringBuilder sb = new();
		for (int y = 0; y < map.GetLength(0); y++)
		{
			for (int x = 0; x < map.GetLength(1); x++)
			{
				sb.Append(map[y, x]);
			}

			sb.Append('\n');
		}
		File.WriteAllText("G:\\result3x.txt", sb.ToString());
	}

	private Dictionary<Position, Result> TransferLoop()
	{
		Dictionary<Position, Result> tmp = new();
		//Maximale/Minimale X/Y Werte Bekommen
		short minX = dfsResult.MinBy(r => r.Key.x).Key.x;
		short minY = dfsResult.MinBy(r => r.Key.y).Key.y;
		short maxX = dfsResult.MaxBy(r => r.Key.x).Key.x;
		short maxY = dfsResult.MaxBy(r => r.Key.y).Key.y;
		for (short x = minX; x <= maxX; x++)
		{
			for (short y = minY; y <= maxY; y++)
			{
				if (dfsResult.ContainsKey((x, y)))
				{
					tmp.Add(((short)(x - minX), (short)(y - minY)), dfsResult[(x, y)]);
				} else
				{
					tmp.Add(((short)(x - minX), (short)(y - minY)), (-1, '.'));
				}
			}
		}

		return tmp;
	}

	private void GoNextStep(Position pos, short distance, Direction cameFrom)
	{
		char pipe = input[pos.y][pos.x];
		//Nächste Valide richtung bestimmen.
		Direction nextDirectionToGoTo = goNextMapping[new Lookup(pipe, cameFrom)];
		Position nextPosition = nextDirectionToGoTo switch
		{
			Direction.North => (pos.x, (short)(pos.y - 1)),
			Direction.East => ((short)(pos.x + 1), pos.y),
			Direction.South => (pos.x, (short)(pos.y + 1)),
			Direction.West => ((short)(pos.x - 1), pos.y),
			_ => throw new ArgumentOutOfRangeException()
		};
		char nextPipe = input[nextPosition.y][nextPosition.x];
		if (validMoves[new Lookup(pipe, cameFrom)].Contains(nextPipe))
		{
			if (dfsResult.TryAdd(pos, (distance, pipe)))
			{
				GoNextStep(nextPosition, (short)(distance + 1), cameFromMapping[new Lookup(pipe, cameFrom)]);
			}
		}
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
		public bool Equals(Lookup x, Lookup y)
		{
			return x.CurPipe == y.CurPipe && x.CameFrom == y.CameFrom;
		}

		public int GetHashCode(Lookup obj)
		{
			return HashCode.Combine(obj.CurPipe, (int)obj.CameFrom);
		}
	}
}