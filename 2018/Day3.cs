namespace AdventOfCode._2018;

public class Day3 : IAdventDay
{
	private readonly string[] input;

	public Day3()
	{
		input = Helper.GetInput(3);
	}

	public long PartOne()
	{
		string[] input = new string[this.input.Length];
		this.input.CopyTo(input, 0);
		List<Claims> claims = new();

		for (int i = 0; i < input.Length; i++)
		{
			int id = int.Parse(input[i].Substring(1, input[i].IndexOf(" ")));
			input[i] = input[i].Remove(0, input[i].IndexOf("@") + 2);
			string s = input[i].Substring(0, input[i].IndexOf(":"));
			input[i] = input[i].Replace(s, "").Replace(": ", "");
			string[] test = s.Split(",");
			(int x, int y) position = (int.Parse(test[0]), int.Parse(test[1]));
			string[] tmp = input[i].Split("x");
			(int breit, int hoch) size = (int.Parse(tmp[0]), int.Parse(tmp[1]));
			Claims c = new(id, position, size);
			claims.Add(c);
		}

		int[,] fabric = new int[1000, 1000];

		for (int id = 0; id < claims.Count; id++)
		{
			Claims c = claims[id];
			for (int x = c.position.x; x < c.position.x + c.size.breit; x++)
			{
				for (int y = c.position.y; y < c.position.y + c.size.hoch; y++)
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

	public long PartTwo()
	{
		string[] input = new string[this.input.Length];
		this.input.CopyTo(input, 0);
		List<Claims> claims = new();

		for (int i = 0; i < input.Length; i++)
		{
			int id = int.Parse(input[i].Substring(1, input[i].IndexOf(" ")));
			input[i] = input[i].Remove(0, input[i].IndexOf("@") + 2);
			string s = input[i].Substring(0, input[i].IndexOf(":"));
			input[i] = input[i].Replace(s, "").Replace(": ", "");
			string[] test = s.Split(",");
			(int x, int y) position = (int.Parse(test[0]), int.Parse(test[1]));
			string[] tmp = input[i].Split("x");
			(int breit, int hoch) size = (int.Parse(tmp[0]), int.Parse(tmp[1]));
			Claims c = new(id, position, size);
			claims.Add(c);
		}

		int[,] fabric = new int[1000, 1000];

		for (int id = 0; id < claims.Count; id++)
		{
			Claims c = claims[id];
			for (int x = c.position.x; x < c.position.x + c.size.breit; x++)
			{
				for (int y = c.position.y; y < c.position.y + c.size.hoch; y++)
				{
					fabric[x, y]++;
				}
			}
		}

		for (int id = 0; id < claims.Count; id++)
		{
			Claims c = claims[id];
			for (int x = c.position.x; x < c.position.x + c.size.breit; x++)
			{
				for (int y = c.position.y; y < c.position.y + c.size.hoch; y++)
				{
					if (fabric[x, y] != 1) c.doesOverlap = true;
				}
			}
		}

		Console.WriteLine(claims.FirstOrDefault(c => c.doesOverlap == false).id);
		return long.MinValue;
	}
}

internal class Claims
{
	public bool doesOverlap;
	public int id;
	public (int x, int y) position;
	public (int breit, int hoch) size;

	public Claims(int id, (int x, int y) position, (int breit, int hoch) size)
	{
		this.id = id;
		this.position = position;
		this.size = size;
		doesOverlap = false;
	}
}