using AdventOfCode._2023;
using Position = (int x, int y);

namespace AdventOfCode._2023;

public class Day16 : IAdventDay
{
	private List<string> input;
	private int[,] lightMap;
	private Dictionary<Position, Obstacle> obstacleMap;
	private List<(Position pos, Direction movingDir)> lightHeads;
	private List<int> toRemove;
	
	public Day16()
	{
		this.input = Helper.GetInput(16).ToList();
		lightMap = new int[input.Count, input[0].Length];
		obstacleMap = new();
		lightHeads = new();
		toRemove = new();
	}

	public long PartOne()
	{
		obstacleMap = ParseObstacles();
		lightHeads.Add(((0, 0), Direction.Right));
		lightMap[0, 0] = 1;
		for (int i = 0; i < 1_000; i++)
		{
			SimulateLight();
		}
		return lightMap.Cast<int>().Sum(i => i);
	}
	
	public long PartTwo()
	{
		obstacleMap = ParseObstacles();
		List<int> energizedTiles = new();
		//Y = 0 enthlang gehen
		for (int x = 0; x < input[0].Length; x++)
		{
			lightMap = new int[input.Count, input[0].Length];
			lightMap[0, x] = 1;
			lightHeads = new();
			lightHeads.Add(((x, 0), Direction.Down));
			for (int i = 0; i < 1_000; i++)
			{
				SimulateLight();
			}
			energizedTiles.Add(lightMap.Cast<int>().Sum(i => i));
		}

		Console.WriteLine("Obere Reihe fertig");
		//Für X = input[0].Length - 1 entlang gehen
		for (int y = 0; y < input.Count; y++)
		{
			lightMap = new int[input.Count, input[0].Length];
			lightMap[y, input[0].Length -1] = 1;
			lightHeads = new();
			lightHeads.Add(((input[0].Length - 1, y), Direction.Left));
			for (int i = 0; i < 1_000; i++)
			{
				SimulateLight();
			}
			energizedTiles.Add(lightMap.Cast<int>().Sum(i => i));
		}
		Console.WriteLine("Rechte Reihe fertig");
		//Für Y = input.Count - 1
		for (int x = 0; x < input[0].Length; x++)
		{
			lightMap = new int[input.Count, input[0].Length];
			lightMap[input.Count - 1, x] = 1;
			lightHeads = new();
			lightHeads.Add(((x, input.Count - 1), Direction.Up));
			for (int i = 0; i < 1_000; i++)
			{
				SimulateLight();
			}
			energizedTiles.Add(lightMap.Cast<int>().Sum(i => i));
		}
		Console.WriteLine("Untere Reihe fertig");
		//Für X = 0 entlang gehen
		for (int y = 0; y < input.Count; y++)
		{
			lightMap = new int[input.Count, input[0].Length];
			lightMap[y, 0] = 1;
			lightHeads = new();
			lightHeads.Add(((0, y), Direction.Right));
			for (int i = 0; i < 1_000; i++)
			{
				SimulateLight();
			}
			energizedTiles.Add(lightMap.Cast<int>().Sum(i => i));
		}
		return energizedTiles.Max();
	}
	
	private bool SimulateLight()
	{
		for (int i = 0; i < lightHeads.Count; i++)
		{
			lightHeads[i] = MoveLightHead(lightHeads[i], i);
		}

		int removed = 0;
		foreach (int i in toRemove)
		{
			lightHeads.RemoveAt(i - removed);
			removed++;
		}
		toRemove.Clear();
		lightHeads = lightHeads.Distinct().ToList();
		return lightHeads.Count > 0;
	}

	private (Position pos, Direction movingDir) MoveLightHead((Position pos, Direction movingDir) head, int index)
	{
		Position curPos = head.pos;
		if (!obstacleMap.ContainsKey(curPos))
		{
			return SimpleGo(head, index);
		}
		
		Obstacle obstacle = obstacleMap[curPos];
		switch (obstacle)
		{
			case Obstacle.LeftMirror:
				return HeadOnLeftMirror(head, index);
			case Obstacle.RightMirror:
				return HeadOnRightMirror(head, index);
			case Obstacle.HorizontalSplitter:
				return HeadOnHorizontalSplitter(head, index);
			case Obstacle.VerticalSplitter:
				return HeadOnVerticalSplitter(head, index);
			default:
				throw new ArgumentOutOfRangeException();
		}

	}

	private ((int x, int y) pos, Direction movingDir) HeadOnVerticalSplitter((Position pos, Direction movingDir) head, int index)
	{
		switch (head.movingDir)
		{
			case Direction.Up:
				return SimpleGo(head, index);
			case Direction.Right:
				if (IsInBounds((head.pos.x, head.pos.y + 1)))
				{
					lightMap[head.pos.x, head.pos.y + 1] = 1;
					lightHeads.Add(((head.pos.x, head.pos.y + 1), Direction.Down));
				}
				if (IsInBounds((head.pos.x, head.pos.y - 1)))
				{
					lightMap[head.pos.x, head.pos.y - 1] = 1;
					lightHeads.Add(((head.pos.x, head.pos.y - 1), Direction.Up));
				}
				toRemove.Add(index);
				return head;
			case Direction.Down:
				return SimpleGo(head, index);
			case Direction.Left:
				if (IsInBounds((head.pos.x, head.pos.y + 1)))
				{
					lightMap[head.pos.x, head.pos.y + 1] = 1;
					lightHeads.Add(((head.pos.x, head.pos.y + 1), Direction.Down));
				}
				if (IsInBounds((head.pos.x, head.pos.y - 1)))
				{
					lightMap[head.pos.x, head.pos.y - 1] = 1;
					lightHeads.Add(((head.pos.x, head.pos.y - 1), Direction.Up));
				}
				toRemove.Add(index);
				return head;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private (Position pos, Direction movingDir) HeadOnHorizontalSplitter((Position pos, Direction movingDir) head, int index)
	{
		switch (head.movingDir)
		{
			case Direction.Up:
				if (IsInBounds((head.pos.x - 1, head.pos.y)))
				{
					lightMap[head.pos.x - 1, head.pos.y] = 1;
					lightHeads.Add(((head.pos.x - 1, head.pos.y), Direction.Left));
				}
				if (IsInBounds((head.pos.x + 1, head.pos.y)))
				{
					lightMap[head.pos.x + 1, head.pos.y] = 1;
					lightHeads.Add(((head.pos.x + 1, head.pos.y), Direction.Right));
				}
				toRemove.Add(index);
				return head;
			case Direction.Right:
				return SimpleGo(head, index);
			case Direction.Down:
				if (IsInBounds((head.pos.x - 1, head.pos.y)))
				{
					lightMap[head.pos.x - 1, head.pos.y] = 1;
					lightHeads.Add(((head.pos.x - 1, head.pos.y), Direction.Left));
				}
				if (IsInBounds((head.pos.x + 1, head.pos.y)))
				{
					lightMap[head.pos.x + 1, head.pos.y] = 1;
					lightHeads.Add(((head.pos.x + 1, head.pos.y), Direction.Right));
				}
				toRemove.Add(index);
				return head;
			case Direction.Left:
				return SimpleGo(head, index);
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private (Position pos, Direction movingDir) HeadOnLeftMirror((Position pos, Direction movingDir) head, int index)
	{
		switch (head.movingDir)
		{
			case Direction.Up:
				if (IsInBounds((head.pos.x + 1, head.pos.y)))
				{
					lightMap[head.pos.x + 1, head.pos.y] = 1;
					return ((head.pos.x + 1, head.pos.y), Direction.Right);
				}
				toRemove.Add(index);
				return head;
			case Direction.Right:
				if (IsInBounds((head.pos.x, head.pos.y - 1)))
				{
					lightMap[head.pos.x, head.pos.y - 1] = 1;
					return ((head.pos.x, head.pos.y - 1), Direction.Up);
				}
				toRemove.Add(index);
				return head;
			case Direction.Down:
				if (IsInBounds((head.pos.x - 1, head.pos.y)))
				{
					lightMap[head.pos.x - 1, head.pos.y] = 1;
					return ((head.pos.x - 1, head.pos.y), Direction.Left);
				}
				toRemove.Add(index);
				return head;
			case Direction.Left:
				if (IsInBounds((head.pos.x, head.pos.y + 1)))
				{
					lightMap[head.pos.x, head.pos.y + 1] = 1;
					return ((head.pos.x, head.pos.y + 1), Direction.Down);
				}
				toRemove.Add(index);
				return head;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
	
	private (Position pos, Direction movingDir) HeadOnRightMirror((Position pos, Direction movingDir) head, int index)
	{
		switch (head.movingDir)
		{
			case Direction.Up:
				if (IsInBounds((head.pos.x - 1, head.pos.y)))
				{
					lightMap[head.pos.x - 1, head.pos.y] = 1;
					return ((head.pos.x - 1, head.pos.y), Direction.Left);
				}
				toRemove.Add(index);
				return head;
			case Direction.Right:
				if (IsInBounds((head.pos.x, head.pos.y + 1)))
				{
					lightMap[head.pos.x, head.pos.y + 1] = 1;
					return ((head.pos.x, head.pos.y + 1), Direction.Down);
				}
				toRemove.Add(index);
				return head;
			case Direction.Down:
				if (IsInBounds((head.pos.x + 1, head.pos.y)))
				{
					lightMap[head.pos.x + 1, head.pos.y] = 1;
					return ((head.pos.x + 1, head.pos.y), Direction.Right);
				}
				toRemove.Add(index);
				return head;
			case Direction.Left:
				if (IsInBounds((head.pos.x, head.pos.y - 1)))
				{
					lightMap[head.pos.x, head.pos.y - 1] = 1;
					return ((head.pos.x, head.pos.y - 1), Direction.Up);
				}
				toRemove.Add(index);
				return head;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private (Position pos, Direction movingDir) SimpleGo((Position pos, Direction movingDir) head, int index)
	{
		switch (head.movingDir)
		{
			case Direction.Up:
				if (IsInBounds((head.pos.x, head.pos.y - 1)))
				{
					lightMap[head.pos.x, head.pos.y - 1] = 1;
					return ((head.pos.x, head.pos.y - 1), Direction.Up);
				}
				toRemove.Add(index);
				return head;
			case Direction.Right:
				if (IsInBounds((head.pos.x + 1, head.pos.y)))
				{
					lightMap[head.pos.x + 1, head.pos.y] = 1;
					return ((head.pos.x + 1, head.pos.y), Direction.Right);
				}
				toRemove.Add(index);
				return head;
			case Direction.Down:
				if (IsInBounds((head.pos.x, head.pos.y + 1)))
				{
					lightMap[head.pos.x, head.pos.y + 1] = 1;
					return ((head.pos.x, head.pos.y + 1), Direction.Down);
				}
				toRemove.Add(index);
				return head;
			case Direction.Left:
				if (IsInBounds((head.pos.x - 1, head.pos.y)))
				{
					lightMap[head.pos.x - 1, head.pos.y] = 1;
					return ((head.pos.x - 1, head.pos.y), Direction.Left);
				}
				toRemove.Add(index);
				return head;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private bool IsInBounds(Position pos)
	{
		return pos.x >= 0 && pos.x < lightMap.GetLength(1) && pos.y >= 0 && pos.y < lightMap.GetLength(0);
	}
	
	private Dictionary<Position, Obstacle> ParseObstacles()
	{
		Dictionary<Position, Obstacle> mapOfObstacles = new();
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				if(input[y][x] == '.') continue;
				Obstacle o = (input[y][x]) switch
				{
					'|' => Obstacle.VerticalSplitter,
					'-' => Obstacle.HorizontalSplitter,
					'/' => Obstacle.LeftMirror,
					'\\' => Obstacle.RightMirror,
					_ => throw new ArgumentException()
				};
				mapOfObstacles.Add((x, y), o);
			}
		}

		return mapOfObstacles;
	}

	private enum Obstacle
	{
		LeftMirror,
		RightMirror,
		HorizontalSplitter,
		VerticalSplitter
	}
	
	private enum Direction {
		Up,
		Right,
		Down,
		Left
	}
}