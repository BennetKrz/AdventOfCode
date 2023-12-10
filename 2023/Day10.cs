using Position = (int x, int y);

namespace AdventOfCode._2023;

public class Day10 : IAdventDay
{
	private List<string> input;
	private Dictionary<Position, int> dfsResult;
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

	private void PartOneThreadBased()
	{
		Position startPos = (0, 0);
		for (int i = 0; i < input.Count; i++)
		{
			if (input[i].IndexOf('S') != -1)
			{
				startPos = (input[i].IndexOf('S'), i);
				input[startPos.y] = input[startPos.y].Replace('S', '|');
			}
		}
		GoNextStep(startPos, 0, Direction.North);
		Console.WriteLine("Part One: " + ((dfsResult.Values.ToArray()[^1] / 2) + 1).ToString());
	}
	
	public long PartOne()
	{
		Thread thread = new(PartOneThreadBased, 4 * 1024 * 1024);
		thread.Start();
		return long.MinValue;
	}

	public long PartTwo()
	{
		return 0;
	}

	private void GoNextStep(Position pos, int distance, Direction cameFrom)
	{
		char pipe = input[pos.y][pos.x];
		//Nächste Valide richtung bestimmen.
		Direction nextDirectionToGoTo = goNextMapping[new Lookup(pipe, cameFrom)];
		Position nextPosition = nextDirectionToGoTo switch
		{
			Direction.North => (pos.x, pos.y - 1),
			Direction.East => (pos.x + 1, pos.y),
			Direction.South => (pos.x, pos.y + 1),
			Direction.West => (pos.x - 1, pos.y),
			_ => throw new ArgumentOutOfRangeException()
		};
		char nextPipe = input[nextPosition.y][nextPosition.x];
		if (validMoves[new Lookup(pipe, cameFrom)].Contains(nextPipe))
		{
			if (dfsResult.TryAdd(pos, distance))
			{
				GoNextStep(nextPosition, distance + 1, cameFromMapping[new Lookup(pipe, cameFrom)]);
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