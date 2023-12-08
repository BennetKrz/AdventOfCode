using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day8 : IAdventDay
{
	private List<string> input;

	public Day8()
	{
		this.input = Helper.GetInput(8).ToList();
	}

	public long PartOne()
	{
		Dictionary<string, (string l, string r)> map = ParseInput();
		string instructions = input[0];
		string currentPos = "AAA";
		long steps = 0;
		int i = 0;
		
		while (currentPos != "ZZZ")
		{
			if (currentPos == "ZZZ")
			{
				return steps;
			}

			if (instructions[i] == 'L')
			{
				currentPos = map[currentPos].l;
			} else
			{
				currentPos = map[currentPos].r;
			}
			
			steps++;
			i++;
			if (i >= instructions.Length)
			{
				i = 0;
			}
		}
		return steps;
	}

	public long PartTwo()
	{
		Dictionary<string, (string l, string r)> map = ParseInput();
		string instructions = input[0];
		List<string> currentPos = map.Keys.Where(p => p[^1] == 'A').ToList();
		List<long> wayLength = new(); 

		for (int p = 0; p < currentPos.Count; p++)
		{
			long steps = 0;
			int i = 0;
			
			while (currentPos[p][^1] != 'Z')
			{
				if (instructions[i] == 'L')
				{
					currentPos[p] = map[currentPos[p]].l;
				} else
				{
					currentPos[p] = map[currentPos[p]].r;
				}

				steps++;
				i++;
				if (i >= instructions.Length)
				{
					i = 0;
				}
			}
			wayLength.Add(steps);
		}
		
		return Lcm(wayLength);
		
	}

	private long Lcm(List<long> numbers)
	{
		return numbers.Aggregate((s, val) => s * val / Gcd(s, val));
	}

	private static long Gcd(long n1, long n2)
	{
		while (true)
		{
			if (n2 == 0) return n1;
			long n3 = n1;
			n1 = n2;
			n2 = n3 % n2;
		}
	}

	private Dictionary<string, (string l, string r)> ParseInput()
	{
		Dictionary<string, (string l, string r)> map = new();
		for (int i = 2; i < input.Count; i++)
		{
			string level = input[i].Split(" ")[0];
			string l = input[i].Split("(")[1][..3];
			string r = input[i].Split(" ")[3][..3];
			map.Add(level,  (l, r));
		}
		return map;
	}
}