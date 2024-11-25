using Position = (int x, int y);

namespace AdventOfCode._2023;
using Map = Dictionary<Position, int>;

public class Day17 : AdventBase
{
	private readonly List<string> input;

	public Day17() : base(2023, 17)
	{
		input = Input.ToList();
	}

	public override long PartOne()
	{
		Map map = ParseInput();
		return FindShortest(map, 0, 3);
	}

	public override long PartTwo()
	{
		Map map = ParseInput();
		return FindShortest(map, 4, 10);
	}

	private long FindShortest(Map map, int minStraight, int maxStraight)
	{
		Position target = (input[0].Length - 1, input.Count - 1);
		PriorityQueue<Crucible, int> pq = new();
		//Da beginn oben links, nach unten und nach Rechts starten
		pq.Enqueue(new Crucible((0, 0), Direction.Right, 0), 0);
		pq.Enqueue(new Crucible((0, 0), Direction.Down, 0), 0);

		HashSet<Crucible> seen = new();
		
		while (pq.TryDequeue(out Crucible c, out int heatloss))
		{
			//Stop
			if (c.Pos.x == target.x && c.Pos.y == target.y && c.MovesStraight >= minStraight)
			{
				return heatloss;
			}

			List<Crucible> nextPositions = GetNextPositions(c, minStraight, maxStraight);
			foreach (Crucible crucible in nextPositions)
			{
				if (map.ContainsKey(crucible.Pos) && !seen.Contains(crucible))
				{
					seen.Add(crucible);
					pq.Enqueue(crucible, heatloss + map[crucible.Pos]);
				}
			}
		}
		
		return -1;
	}

	private List<Crucible> GetNextPositions(Crucible crucible, int minStraight, int maxStraight)
	{
		List<Crucible> nextPositions = new();
		Position p = crucible.Pos;

		if (crucible.MovesStraight >= minStraight)
		{

			switch (crucible.Dir)
			{
				case Direction.Up:
					nextPositions.Add(new Crucible((p.x + 1, p.y), Direction.Right, 1));
					nextPositions.Add(new Crucible((p.x - 1, p.y), Direction.Left, 1));
					break;
				case Direction.Right:
					nextPositions.Add(new Crucible((p.x, p.y - 1), Direction.Up, 1));
					nextPositions.Add(new Crucible((p.x, p.y + 1), Direction.Down, 1));
					break;
				case Direction.Down:
					nextPositions.Add(new Crucible((p.x + 1, p.y), Direction.Right, 1));
					nextPositions.Add(new Crucible((p.x - 1, p.y), Direction.Left, 1));
					break;
				case Direction.Left:
					nextPositions.Add(new Crucible((p.x, p.y + 1), Direction.Down, 1));
					nextPositions.Add(new Crucible((p.x, p.y - 1), Direction.Up, 1));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		if (crucible.MovesStraight < maxStraight)
		{
			switch (crucible.Dir)
			{
				case Direction.Up:
					nextPositions.Add(new Crucible((p.x, p.y - 1), Direction.Up, crucible.MovesStraight + 1));
					break;
				case Direction.Right:
					nextPositions.Add(new Crucible((p.x + 1, p.y), Direction.Right, crucible.MovesStraight + 1));
					break;
				case Direction.Down:
					nextPositions.Add(new Crucible((p.x, p.y + 1), Direction.Down, crucible.MovesStraight + 1));
					break;
				case Direction.Left:
					nextPositions.Add(new Crucible((p.x - 1, p.y), Direction.Left, crucible.MovesStraight + 1));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		return nextPositions;
	}

	private Map ParseInput()
	{
		Map m = new();
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				m.Add((x, y), int.Parse(input[y][x].ToString()));
			}
		}

		return m;
	}

	/*
	 * Dir: 0 --> Right
	 *		1 --> Down
	 *		2 --> Left
	 *		3 --> Up
	 */
	private record Crucible(Position Pos, Direction Dir, int MovesStraight);

	private enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}
}
