namespace AdventOfCode._2017;

public class Day7 : IAdventDay
{
	private readonly List<string> input;

	public Day7()
	{
		input = Helper.GetInput(7).ToList();
	}

	public long PartOne()
	{
		/*
        //Parse input
		Dictionary<string, Disc> allDiscs = new();
		for (int i = 0; i < input.Count; i++)
		{
			string[] values = input[i].Split(" ");
			allDiscs.Add(values[0], ParseLineSimple(input[i]));
		}

		Dictionary<string, string> allPaths = GetPaths();
		//Build paths
		for(int i = 0; i < allPaths.Count;)
		{
			string key = allPaths.Keys.ToArray()[i];
			List<string>
			allDiscs[key]
		}*/
		return 0;
	}

	public long PartTwo()
	{
		return 0;
	}

	private Disc ParseLineSimple(string line)
	{
		string[] values = line.Split(" ");
		Disc d = new(values[0], int.Parse(values[1].Replace("(", "").Replace(")", "")));
		return d;
	}

	private Dictionary<string, string> GetPaths()
	{
		Dictionary<string, string> result = new();
		List<string> tmp = input.Where(s => s.Contains("->")).ToList();
		for (int i = 0; i < tmp.Count; i++)
		{
			result.Add(tmp[i].Substring(0, tmp[i].IndexOf(" ")), tmp[i]);
		}

		return result;
	}

	private List<string> GetNextDiscs(string line)
	{
		return line.Replace(" ", "").Remove(0, line.IndexOf("->")).Split(",").ToList();
	}
}

public class Disc
{
	public string name;
	public int size;

	public Disc(string name, int size)
	{
		this.name = name;
		this.size = size;
	}

	public Disc previousDisc { get; set; }
	public HashSet<Disc> nextDisc { get; }

	public Disc getFirstDisc()
	{
		if (this.previousDisc != null) return this.previousDisc.getFirstDisc();

		return this;
	}

	public void AddDisc(Disc discToAdd, string nameOfDisc)
	{ }
}