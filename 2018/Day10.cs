using System.Runtime.CompilerServices;
using System.Text;
using AdventOfCode._2018;

namespace AdventOfCode._2018;

public class Day10 : IAdventDay
{
	List<string> input;

	public Day10()
	{
		this.input = Helper.GetInput(10).ToList();
	}

	public long PartOne()
	{
		if (File.Exists(@"G:\C#\AdventOfCode\2018\result.txt"))
		{
			File.Delete(@"G:\C#\AdventOfCode\2018\result.txt");
		}
		List<(Position, Position)> positions = ParseInput();
		for (int i = 0; i < 11_000; i++)
		{
			if (i > 10_000)
			{
				Console.WriteLine($"Durchlauf: {i}: \n");
			}
			Print(positions);
			positions = Move(positions);
		}
		return long.MinValue;
	}

	public long PartTwo()
	{
		return long.MinValue;
	}
	
	private List<(Position, Position)> Move(List<(Position, Position)> positions)
	{
		List<(Position, Position)> newPositions = new();
		for (int i = 0; i < positions.Count; i++)
		{
			Position curPos = positions[i].Item1;
			Position vel = positions[i].Item2;
			Position nextPos = new(curPos.X + vel.X, curPos.Y + vel.Y);
			newPositions.Add((nextPos, vel));
		}

		return newPositions;
	}

	private void Print(List<(Position, Position)> positions, bool toTextFile = false)
	{
		int minX = positions.MinBy(p => p.Item1.X).Item1.X - 5;
		int minY = positions.MinBy(p => p.Item1.Y).Item1.Y - 5;
		int maxX = positions.MaxBy(p => p.Item1.X).Item1.X + 5;
		int maxY = positions.MaxBy(p => p.Item1.Y).Item1.Y + 5;
		if (Math.Abs(maxX - minX) > 1000 || Math.Abs(maxY - minY) > 200)
		{
			return;
		}
		StringBuilder sb = new();
		for (int y = minY; y <= maxY; y++)
		{
			for (int x = minX; x <= maxX; x++)
			{
				sb.Append(positions.Count(p => p.Item1.X == x && p.Item1.Y == y) >= 1 ? '#' : '.');
			}

			sb.Append('\n');
		}

		if (toTextFile)
		{
			File.AppendAllText("G:\\C#\\AdventOfCode\\2018\\result.txt", sb.ToString());
		} else
		{
			Console.WriteLine(sb.ToString());
		}
	}

	private List<(Position, Position)> ParseInput()
	{
		List<(Position, Position)> positions = new();
		foreach (string line in input)
		{
			List<int> pos = line
			                .Substring(line.IndexOf("<") + 1, line.IndexOf(">") - line.IndexOf("<") - 1)
			                .Split(",")
			                .Select(s => int.Parse(s.Trim()))
			                .ToList();
			List<int> vel = line
			                .Substring(line.LastIndexOf("<") + 1, line.LastIndexOf(">") - line.LastIndexOf("<") - 1)
			                .Split(",")
			                .Select(s => int.Parse(s.Trim()))
			                .ToList();
			positions.Add((new(pos[0], pos[1]), new (vel[0], vel[1])));
		}

		return positions;
	}

	private record Position(int X, int Y);
}