using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day23 : IAdventDay
{
	List<string> input;

	public Day23()
	{
		this.input = Helper.GetInput(23).ToList();
	}

	public long PartOne()
	{
		Position startPos = new(input[0].IndexOf('.'), 0);
		Position endPos = new(input[^1].IndexOf('.'), input.Count - 1);

		Dictionary<Position, HashSet<Position>> graph = GenerateGraph();
		
		return FindLongestPath(graph, [], startPos, endPos);
	}

	public long PartTwo()
	{
		Position startPos = new(input[0].IndexOf('.'), 0);
		Position endPos = new(input[^1].IndexOf('.'), input.Count - 1);

		Dictionary<Position, HashSet<Position>> graph = GenerateGraph(false);
		
		return FindLongestPath(graph, [], startPos, endPos);
	}

	private int FindLongestPath(Dictionary<Position, HashSet<Position>> graph, HashSet<Position> visited, Position curPos, Position endPos)
	{
		int result = 0;
		if (curPos == endPos)
		{
			return visited.Count;
		}

		List<Position> nextPossiblePositions = graph[curPos].ToList();
		foreach (Position pos in nextPossiblePositions)
		{
			if (!visited.Add(pos))
			{
				continue;
			}

			int length = FindLongestPath(graph, visited, pos, endPos);
			result = Math.Max(result, length);
			visited.Remove(pos);
		}
		
		return result;
	}

	private Dictionary<Position,HashSet<Position>> GenerateGraph(bool isPartOne = true)
	{
		Dictionary<Position, HashSet<Position>> graph = new();

		for (int y = 0; y < input.Count; y++)
		{
			string line = input[y];
			for (int x = 0; x < line.Length; x++)
			{
				if (line[x] == '#')
				{
					continue;
				}

				Position pos = new(x, y);
				graph.Add(pos, []);

				if (isPartOne)
				{
					switch (line[x])
					{
						case '^':
							graph[pos].Add(pos.Move(Direction.Up));
							continue;
						case '>':
							graph[pos].Add(pos.Move(Direction.Right));
							continue;
						case 'v':
							graph[pos].Add(pos.Move(Direction.Down));
							continue;
						case '<':
							graph[pos].Add(pos.Move(Direction.Left));
							continue;
					}
				}

				Position targetPos = new(pos.X , pos.Y - 1);
				if (IsInBounds(targetPos) && input[targetPos.Y][targetPos.X] != '#')
				{
					graph[pos].Add(targetPos);
				}
				targetPos = new(pos.X + 1, pos.Y);
				if (IsInBounds(targetPos) && input[targetPos.Y][targetPos.X] != '#')
				{
					graph[pos].Add(targetPos);
				}
				targetPos = new(pos.X, pos.Y + 1);
				if (IsInBounds(targetPos) && input[targetPos.Y][targetPos.X] != '#')
				{
					graph[pos].Add(targetPos);
				}
				targetPos = new(pos.X - 1, pos.Y);
				if (IsInBounds(targetPos) && input[targetPos.Y][targetPos.X] != '#')
				{
					graph[pos].Add(targetPos);
				}
			}
		}

		return graph;
	}

	private bool IsInBounds(Position pos)
	{
		return pos.X >= 0 && pos.X < input[0].Length && pos.Y >= 0 && pos.Y < input.Count;
	}

	private record Direction(int X, int Y)
	{
		public static Direction Up = new(0, -1);
		public static Direction Right = new(1, 0);
		public static Direction Down = new(0, 1);
		public static Direction Left = new(-1, 0);
	}
	
	private record Position(int X, int Y){
		public Position Move(Direction dir)
		{
			return new Position(this.X + dir.X, this.Y + dir.Y);
		}
	}
}