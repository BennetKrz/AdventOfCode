using AdventOfCode._2023;

using Position = (int x, int y);

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
		Dictionary<Position, char> map = ParseInput();
		Position startPos = (input[0].IndexOf('.'), 0);
		Position endPos = (input[^1].IndexOf('.'), input.Count - 1);
		return FindLongestPath(map, startPos, endPos);
	}

	public long PartTwo()
	{
		return 0;
	}

	private long FindLongestPath(Dictionary<Position, char> map, Position startPos, Position endPos)
	{
		List<long> distances = new();
		
		PriorityQueue<Position, StepPriority> pq = new(new StepPriorityComparer());
		pq.Enqueue(startPos, new StepPriority(0));

		HashSet<Position> seen = new();
		while (pq.TryDequeue(out Position curPos, out StepPriority steps))
		{
			if (curPos == endPos)
			{
				distances.Add(steps.Steps);
			}

			List<Position> nextPositions = GetNextPositions(curPos, map);
			foreach (Position position in nextPositions)
			{
				if (!seen.Contains(position))
				{
					seen.Add(position);
					pq.Enqueue(position, new StepPriority(steps.Steps + 1));
				}
			}
		}
		return distances.Max();
	}

	private List<Position> GetNextPositions(Position pos, Dictionary<Position, char> map)
	{
		List<Position> result = new();
		//Erstmal gucken, ob man auf einer Slope steht
		if ("^v><".Contains(map[pos]))
		{
			switch (map[pos])
			{
				case '^':
					result.Add((pos.x, pos.y - 1));
					return result;
				case '>':
					result.Add((pos.x + 1, pos.y));
					return result;
				case 'v':
					result.Add((pos.x, pos.y + 1));
					return result;
				case '<':
					result.Add((pos.x - 1, pos.y));
					return result;
			}
		}
		
		
		//Nach oben gucken
		Position nextPos = (pos.x, pos.y - 1);
		if (map.TryGetValue(nextPos, out char v1) && v1 != '#')
		{
			result.Add(nextPos);
		}

		nextPos = (pos.x + 1, pos.y);
		if (map.TryGetValue(nextPos, out char v2) && v2 != '#')
		{
			result.Add(nextPos);
		}
		
		nextPos = (pos.x, pos.y + 1);
		if (map.TryGetValue(nextPos, out char v3) && v3 != '#')
		{
			result.Add(nextPos);
		}
		
		nextPos = (pos.x - 1, pos.y);
		if (map.TryGetValue(nextPos, out char v4) && v4 != '#')
		{
			result.Add(nextPos);
		}
		return result;
	}

	private Dictionary<Position, char> ParseInput()
	{
		Dictionary<Position, char> map = new();
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				map.Add((x, y), input[y][x]);
			}
		}

		return map;
	}

	private record StepPriority(int Steps);
	
	private class StepPriorityComparer : IComparer<StepPriority>
	{
		public int Compare(StepPriority? x, StepPriority? y)
		{
			return y.Steps - x.Steps;
		}
	}
}