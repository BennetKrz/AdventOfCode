using Priority_Queue;

namespace AdventOfCode._2022;

public class Day12 : AdventBase
{
	private readonly char endChar = 'E';
	private readonly char endCharToHightValue = 'z';
	private readonly string[] input;

	private readonly char startChar = 'S';
	private readonly char startCharToHightValue = 'a';

	public Day12() : base(2022, 12)
	{
		input = Input;
	}

	public override long PartOne()
	{
		char[][] map = Map(input);
		int rows = map.Length, cols = map[0].Length;
		((int y, int x) startPoint, (int y, int x) endPoint) startEnd = findStartAndEnd(map);
		map[startEnd.startPoint.y][startEnd.startPoint.x] = startCharToHightValue;
		map[startEnd.endPoint.y][startEnd.endPoint.x] = endCharToHightValue;
		(int x, int y)[] moves = Extensions.Directions.HorizontalAndVertical;
		SimplePriorityQueue<((int y, int x) pos, int stepsTaken)> q = new();
		q.Enqueue((startEnd.startPoint, 0), 0);
		int result = FindPathDijkstra(
			map,
			new Dictionary<(int y, int x), int>(),
			q,
			(targetPos, targetVal) => targetPos == startEnd.endPoint,
			(targetPos, currentVal, targetVal) => targetVal - currentVal <= 1,
			cols - 1,
			rows - 1,
			moves);
		return result;
	}

	public override long PartTwo()
	{
		char[][] map = Map(input);
		int rows = map.Length, cols = map[0].Length;
		((int y, int x) startPoint, (int y, int x) endPoint) startEnd = findStartAndEnd(map);
		map[startEnd.startPoint.y][startEnd.startPoint.x] = startCharToHightValue;
		map[startEnd.endPoint.y][startEnd.endPoint.x] = endCharToHightValue;
		(int x, int y)[] moves = Extensions.Directions.HorizontalAndVertical;
		SimplePriorityQueue<((int y, int x) pos, int stepsTaken)> q = new();
		q.Enqueue((startEnd.endPoint, 0), 0);
		int result = FindPathDijkstra(
			map,
			new Dictionary<(int y, int x), int>(),
			q,
			(targetPos, targetVal) => targetVal == startCharToHightValue,
			(targetPos, currentVal, targetVal) => currentVal - targetVal <= 1,
			cols - 1,
			rows - 1,
			moves);
		return result;
	}

	private char[][] Map(string[] input)
	{
		return input.Select(x => x.ToCharArray()).ToArray();
	}

	private ((int y, int x) startPoint, (int y, int x) endPoint) findStartAndEnd(char[][] map)
	{
		bool startFound = false, endFound = false;
		(int x, int y) startPoint = (-1, -1), endPoint = (-1, -1);
		int rows = map.Length, cols = map[0].Length;
		char val;
		for (int y = 0; y < rows; y++)
		{
			for (int x = 0; x < cols; x++)
			{
				val = map[y][x];
				if (val == startChar)
				{
					if (startFound) Console.WriteLine("Found more than one starting point");
					startPoint = (y, x);
					startFound = true;
					if (endFound) break;
				} else if (val == endChar)
				{
					if (endFound) Console.WriteLine("Found more than one end point");
					endPoint = (y, x);
					endFound = true;
					if (startFound) break;
				}
			}
		}

		return (startPoint, endPoint);
	}

	private int FindPathDijkstra(char[][] map,
	                             Dictionary<(int y, int x), int> visitedFields,
	                             SimplePriorityQueue<((int y, int x) pos, int stepsTaken)> q,
	                             Func<(int y, int x), char, bool> isEnd,
	                             Func<(int y, int x), char, char, bool> isValidMove,
	                             int maxX,
	                             int maxY,
	                             (int y, int x)[] moves)
	{
		while (q.Count > 0)
		{
			((int y, int x) pos, int stepsTaken) target = q.Dequeue();
			char currentVal = map[target.pos.y][target.pos.x];
			if (visitedFields.ContainsKey(target.pos)) continue;
			visitedFields.Add(target.pos, target.stepsTaken);
			foreach ((int y, int x) move in moves)
			{
				int x = target.pos.x + move.x;
				int y = target.pos.y + move.y;
				var targetPos = (y, x);
				if (x >= 0 && y >= 0 && x <= maxX && y <= maxY && !visitedFields.ContainsKey(targetPos))
				{
					var targetVal = map[y][x];
					if (isValidMove((y, x), currentVal, targetVal))
					{
						int stepsTaken = target.stepsTaken + 1;
						if (isEnd((y, x), targetVal)) return stepsTaken;
						q.Enqueue((targetPos, stepsTaken), stepsTaken);
					}
				}
			}
		}

		return -1;
	}
}