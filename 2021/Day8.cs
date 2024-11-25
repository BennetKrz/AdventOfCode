namespace AdventOfCode._2021;

public class Day8 : AdventBase
{
	private readonly string[] input;

	public Day8() : base(2021, 8)
	{
		input = Input;
	}

	public override long PartOne()
	{
		int countOne = 0;
		int countFour = 0;
		int countSeven = 0;
		int countEight = 0;

		for (int i = 0; i < input.Length; i++)
		{
			string behindDelimiter = input[i].Split('|')[1];
			List<string> strings = behindDelimiter.Split(' ').ToList();
			strings.RemoveAt(0);
			for (int j = 0; j < strings.Count; j++)
			{
				if (strings[j].Length == 2) countOne++;
				if (strings[j].Length == 3) countSeven++;
				if (strings[j].Length == 4) countFour++;
				if (strings[j].Length == 7) countEight++;
			}
		}

		return countOne + countFour + countSeven + countEight;
	}

	public override long PartTwo()
	{
		List<(List<string> left, List<string> right)> lines = ParseInput();
		int total = 0;

		foreach ((List<string> left, List<string> right) line in lines)
		{
			string digit1 = line.left.Single(x => x.Length == 2);
			string digit4 = line.left.Single(x => x.Length == 4);
			string digit7 = line.left.Single(x => x.Length == 3);
			string digit8 = line.left.Single(x => x.Length == 7);

			List<string> d235 = line.left.Where(x => x.Length == 5).ToList();

			List<string> d069 = line.left.Where(x => x.Length == 6).ToList();

			string digit3 = d235.Single(x => digit1.All(x.Contains));
			d235.Remove(digit3);

			string digit9 = d069.Single(x => digit3.All(x.Contains));
			d069.Remove(digit9);

			string digit0 = d069.Single(x => digit1.All(x.Contains));
			d069.Remove(digit0);
			string digit6 = d069[0];

			string digit5 = d235.Single(x => x.All(digit6.Contains));
			d235.Remove(digit5);
			string digit2 = d235[0];

			List<string> numbers =
			[
				string.Join(string.Empty, digit0.OrderBy(x => x)),
				string.Join(string.Empty, digit1.OrderBy(x => x)),
				string.Join(string.Empty, digit2.OrderBy(x => x)),
				string.Join(string.Empty, digit3.OrderBy(x => x)),
				string.Join(string.Empty, digit4.OrderBy(x => x)),
				string.Join(string.Empty, digit5.OrderBy(x => x)),
				string.Join(string.Empty, digit6.OrderBy(x => x)),
				string.Join(string.Empty, digit7.OrderBy(x => x)),
				string.Join(string.Empty, digit8.OrderBy(x => x)),
				string.Join(string.Empty, digit9.OrderBy(x => x))
			];

			string output = string.Empty;

			foreach (string number in line.right)
			{
				string n = string.Join("", number.OrderBy(x => x));
				output += numbers.IndexOf(n);
			}

			total += int.Parse(output);
		}

		return total;
	}

	private List<(List<string> left, List<string> right)> ParseInput()
	{
		List<(List<string> left, List<string> right)> test = new();
		for (int i = 0; i < input.Length; i++)
		{
			string[] split = input[i].Split("|");
			List<string> left = split[0].Split(' ').ToList();
			left.RemoveAt(left.Count - 1);
			List<string> right = split[1].Split(' ').ToList();
			right.RemoveAt(0);
			test.Add((left, right));
		}

		return test;
	}
}