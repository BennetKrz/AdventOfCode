namespace AdventOfCode._2018;

public class Day3 : AdventBase
{
	private readonly string[] input;

	public Day3() : base(2018, 3)
	{
		input = Input;
	}

	public override long PartOne()
	{
		string[] localInput = new string[this.input.Length];
		this.input.CopyTo(localInput, 0);
		List<Claims> claims = new();

		for (int i = 0; i < localInput.Length; i++)
		{
			int id = int.Parse(localInput[i].Substring(1, localInput[i].IndexOf(" ")));
			localInput[i] = localInput[i].Remove(0, localInput[i].IndexOf("@") + 2);
			string s = localInput[i][..localInput[i].IndexOf(":")];
			localInput[i] = localInput[i].Replace(s, "").Replace(": ", "");
			string[] test = s.Split(",");
			(int x, int y) position = (int.Parse(test[0]), int.Parse(test[1]));
			string[] tmp = localInput[i].Split("x");
			(int breit, int hoch) size = (int.Parse(tmp[0]), int.Parse(tmp[1]));
			Claims c = new(id, position, size);
			claims.Add(c);
		}

		int[,] fabric = new int[1000, 1000];

		for (int id = 0; id < claims.Count; id++)
		{
			Claims c = claims[id];
			for (int x = c.Position.x; x < c.Position.x + c.Size.breit; x++)
			{
				for (int y = c.Position.y; y < c.Position.y + c.Size.hoch; y++)
				{
					fabric[x, y]++;
				}
			}
		}

		int count = 0;

		for (int x = 0; x < fabric.GetLength(0); x++)
		{
			for (int y = 0; y < fabric.GetLength(1); y++)
			{
				if (fabric[x, y] > 1) ++count;
			}
		}

		return count;
	}

	public override long PartTwo()
	{
		string[] localInput = new string[this.input.Length];
		this.input.CopyTo(localInput, 0);
		List<Claims> claims = new();

		for (int i = 0; i < localInput.Length; i++)
		{
			int id = int.Parse(localInput[i].Substring(1, localInput[i].IndexOf(" ")));
			localInput[i] = localInput[i].Remove(0, localInput[i].IndexOf("@") + 2);
			string s = localInput[i][..localInput[i].IndexOf(":")];
			localInput[i] = localInput[i].Replace(s, "").Replace(": ", "");
			string[] test = s.Split(",");
			(int x, int y) position = (int.Parse(test[0]), int.Parse(test[1]));
			string[] tmp = localInput[i].Split("x");
			(int breit, int hoch) size = (int.Parse(tmp[0]), int.Parse(tmp[1]));
			Claims c = new(id, position, size);
			claims.Add(c);
		}

		int[,] fabric = new int[1000, 1000];

		for (int id = 0; id < claims.Count; id++)
		{
			Claims c = claims[id];
			for (int x = c.Position.x; x < c.Position.x + c.Size.breit; x++)
			{
				for (int y = c.Position.y; y < c.Position.y + c.Size.hoch; y++)
				{
					fabric[x, y]++;
				}
			}
		}

		for (int id = 0; id < claims.Count; id++)
		{
			Claims c = claims[id];
			for (int x = c.Position.x; x < c.Position.x + c.Size.breit; x++)
			{
				for (int y = c.Position.y; y < c.Position.y + c.Size.hoch; y++)
				{
					if (fabric[x, y] != 1) c.DoesOverlap = true;
				}
			}
		}

		Console.WriteLine(claims.FirstOrDefault(c => c.DoesOverlap == false).Id);
		return long.MinValue;
	}
}

internal class Claims(int id, (int x, int y) position, (int breit, int hoch) size)
{
	public bool DoesOverlap = false;
	public readonly int Id = id;
	public (int x, int y) Position = position;
	public (int breit, int hoch) Size = size;
}