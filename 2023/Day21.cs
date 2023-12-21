using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day21 : IAdventDay
{
	private List<string> input;
	private HashSet<(int x, int y)> rocks;
	private HashSet<Position> visited;
	
	public Day21()
	{
		this.input = Helper.GetInput(21).ToList();
		rocks = new();
		visited = new();
	}

	public long PartOne()
	{
		int maxSteps = 64;
		ParseRocks();
		Stack<Position> goNext = new();
		goNext.Push(GetStartPos());
		VisitPosition(new Position(GetStartPos().X, GetStartPos().Y, 0));
		while (goNext.TryPop(out Position pos))
		{
			foreach (Position position in GetNextPositions(pos, maxSteps))
			{
				VisitPosition(pos);
				goNext.Push(position);
			}
		}
		return visited.Count(p => p.Steps == maxSteps);
	}

	public long PartTwo()
	{
		return 0;
	}
	
	private void VisitPosition(Position pos)
	{
		visited.Add(pos);
	}

	private List<Position> GetNextPositions(Position pos, int maxSteps)
	{
		if (pos.Steps > maxSteps)
		{
			return new List<Position>();
		}
		List<Position> nextPositions = new();
		if (!rocks.Contains((pos.X + 1, pos.Y)) && !visited.Contains(new Position(pos.X + 1, pos.Y, pos.Steps + 1)))
		{
			nextPositions.Add(new Position(pos.X + 1, pos.Y, pos.Steps + 1));
		}
		if (!rocks.Contains((pos.X - 1, pos.Y)) && !visited.Contains(new Position(pos.X - 1, pos.Y, pos.Steps + 1)))
		{
			nextPositions.Add(new Position(pos.X - 1, pos.Y, pos.Steps + 1));
		}
		if (!rocks.Contains((pos.X, pos.Y + 1))  && !visited.Contains(new Position(pos.X, pos.Y + 1, pos.Steps + 1)))
		{
			nextPositions.Add(new Position(pos.X, pos.Y + 1, pos.Steps + 1));
		}
		if (!rocks.Contains((pos.X, pos.Y - 1))  && !visited.Contains(new Position(pos.X, pos.Y - 1, pos.Steps + 1)))
		{
			nextPositions.Add(new Position(pos.X, pos.Y - 1, pos.Steps + 1));
		}

		return nextPositions;
	}

	private Position GetStartPos()
	{
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				if (input[y][x] == 'S')
				{
					return new Position(x, y, 0);
				}
			}
		}

		return new Position(-1, -1, -1);
	}

	private void ParseRocks()
	{
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				if (input[y][x] == '#')
				{
					rocks.Add((x, y));
				}
			}
		}
	}

	private record Position(int X, int Y, int Steps);
}