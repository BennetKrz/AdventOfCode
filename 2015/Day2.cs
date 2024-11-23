namespace AdventOfCode._2015;

public class Day2 : AdventBase
{
	private string[] _input;

	public Day2() : base(2015, 2)
	{
		_input = Input;
	}
	
	public override long PartOne()
	{
		List<int> allArea = new();
		for (int i = 0; i < _input.Length; i++)
		{
			int[] t = Array.ConvertAll(_input[i].Split('x'), int.Parse);
			//t[0] = l; t[1] = w; t[2] = h;
			int[] allThreeSites = { t[0] * t[1], t[1] * t[2], t[2] * t[0] };
			allArea.Add(allThreeSites[0] * 2 + allThreeSites[1] * 2 + allThreeSites[2] * 2 + allThreeSites.Min());
		}

		return allArea.Sum();
	}

	public override long PartTwo()
	{
		List<int> ribbonNeeded = new();
		for (int i = 0; i < _input.Length; i++)
		{
			int[] t = Array.ConvertAll(_input[i].Split('x'), int.Parse).Order().ToArray();
			//t[0] = l; t[1] = w; t[2] = h;
			ribbonNeeded.Add(t[0] + t[0] + t[1] + t[1] + t[0] * t[1] * t[2]);
		}

		return ribbonNeeded.Sum();
	}
}