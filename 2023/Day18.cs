using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using AdventOfCode._2023;

namespace AdventOfCode._2023;

using Position = (long x, long y);

public class Day18 : AdventBase
{
	private readonly List<Position> neighbourPositions;
	private readonly List<string> input;

	public Day18() : base(2023, 18)
	{
		this.input = Input.ToList();
		neighbourPositions =
		[
			(0, 1),
			(0, -1),
			(1, 0),
			(-1, 0),
			(1, 1),
			(-1, 1),
			(1, -1),
			(-1, -1)
		];
	}

	public override long PartOne()
	{
		List<Instruction> instructions = ParseInputPartOne();
		HashSet<Position> wall = DigWall(instructions);
		byte[,] map = GenerateMap(wall);
		map = FloodFill(map);
		return map.Cast<byte>().Count(p => p != 2);
	}

	public override long PartTwo()
	{
		List<Instruction> instructions = ParseInputPartTwo();
		HashSet<Position> wall = DigWall(instructions);
		byte[,] map = GenerateMap(wall);
		map = FloodFill(map);
		return map.Cast<byte>().Count(p => p != 2);
	}

	private byte[,] FloodFill(byte[,] map)
	{
		Stack<Position> toVisit = new();
		toVisit.Push((0, 0));
		while (toVisit.Count != 0)
		{
			Position pos = toVisit.Pop();
			List<Position> neighbours = GetNeighbours(pos, map);
			foreach (Position neighbour in neighbours)
			{
				toVisit.Push(neighbour);
			}

			map[pos.y, pos.x] = 2;
		}

		return map;
	}

	private List<Position> GetNeighbours(Position pos, byte[,] map)
	{
		List<Position> neighbours = new();
		for (int i = 0; i < neighbourPositions.Count; i++)
		{
			long posX = pos.x + neighbourPositions[i].x;
			long posY = pos.y + neighbourPositions[i].y;
			if (posX >= map.GetLength(1) || posX < 0 || posY >= map.GetLength(0) || posY < 0) continue;

			if (map[posY, posX] == 0) neighbours.Add((posX, posY));
		}

		return neighbours;
	}

	private byte[,] GenerateMap(HashSet<Position> wall)
	{
		long minX = wall.MinBy(p => p.x).x - 1;
		long minY = wall.MinBy(p => p.y).y - 1;
		long maxX = wall.MaxBy(p => p.x).x + 2;
		long maxY = wall.MaxBy(p => p.y).y + 2;

		byte[,] map = new byte[Math.Abs(maxY - minY), Math.Abs(maxX - minX)];
		
		for (long y = minY; y < maxY; y++)
		{
			for (long x = minX; x < maxX; x++)
			{
				if (wall.Contains((x, y)))
				{
					map[y - minY, x - minX] = 1;
					wall.Remove((x, y));
				} else
				{
					map[y - minY, x - minX] = 0;
				}
			}
		}

		return map;
	}
	
	private HashSet<Position> DigWall(List<Instruction> instructions)
	{
		HashSet<Position> walls = new();
		Position curPos = (0, 0);
		foreach (Instruction instruction in instructions)
		{
			Position goInDir = instruction.Dir switch
			{
				Direction.Up => (0, -1),
				Direction.Right => (1, 0),
				Direction.Down => (0, 1),
				Direction.Left => (-1, 0),
				_ => throw new Exception()
			};

			for (int i = 0; i < instruction.Length; i++)
			{
				walls.Add(curPos);
				curPos = (curPos.x + goInDir.x, curPos.y + goInDir.y);	
			}
		}
		return walls;
	}

	private List<Instruction> ParseInputPartOne()
	{
		List<Instruction> instructions = new();
		for (int i = 0; i < input.Count; i++)
		{
			string[] data = input[i].Split(' ');
			Direction d = data[0] switch
			{
				"U" => Direction.Up,
				"R" => Direction.Right,
				"D" => Direction.Down,
				"L" => Direction.Left,
				_ => throw new ArgumentOutOfRangeException()
			};
			int length = int.Parse(data[1]);
			instructions.Add(new Instruction(d, length));
		}

		return instructions;
	}
	
	private List<Instruction> ParseInputPartTwo()
	{
		List<Instruction> instructions = new();
		for (int i = 0; i < input.Count; i++)
		{
			string hexData = input[i].Split(' ')[2];
			int length = int.Parse(hexData.Substring(2, 5), NumberStyles.HexNumber);
			Direction d = hexData[7] switch
			{
				'0' => Direction.Right,
				'1' => Direction.Down,
				'2' => Direction.Left,
				'3' => Direction.Up,
				_ => throw new ArgumentOutOfRangeException()
			};
			instructions.Add(new Instruction(d, length));
		}

		return instructions;
	}
	
	private record Instruction(Direction Dir, int Length);
		
	private enum Direction {
		Up,
		Right,
		Down,
		Left
	}
}