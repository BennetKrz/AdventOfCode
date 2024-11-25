namespace AdventOfCode._2021;

public class Day3 : AdventBase
{
	private readonly string[] input;

	public Day3() : base(2021, 3)
	{
		input = Input;
	}

	public override long PartOne()
	{
		string gammaRateB = string.Empty;
		string epsilonRateB = string.Empty;
		for (int i = 0; i < input[0].Length; i++)
		{
			int numberOfOnes = 0;
			for (int j = 0; j < input.Length; j++)
			{
				if (input[j][i] == '1') numberOfOnes++;
			}

			if (numberOfOnes > input.Length / 2)
			{
				gammaRateB += "1";
				epsilonRateB += "0";
			} else
			{
				gammaRateB += "0";
				epsilonRateB += "1";
			}
		}

		int epsilonRateD = Convert.ToInt32(epsilonRateB, 2);
		int gammaRateD = Convert.ToInt32(gammaRateB, 2);
		return epsilonRateD * gammaRateD;
	}

	public override long PartTwo()
	{
		char criticalBit;
		List<string> input = Input.ToList();

		for (int i = 0; i < input[0].Length; i++)
		{
			List<string> worklist = new();
			foreach (string s in input)
			{
				worklist.Add(s);
			}

			int occurence1 = 0;
			int occurence0 = 0;
			for (int j = 0; j < worklist.Count; j++)
			{
				char c = worklist[j][i];
				if (c == '1')
					occurence1++;
				else
					occurence0++;
			}

			if (occurence1 > worklist.Count / 2)
				criticalBit = '1';
			else if (occurence0 > worklist.Count / 2)
				criticalBit = '0';
			else
				criticalBit = '1';

			foreach (string s in worklist)
			{
				if (s[i] != criticalBit)
				{
					input.Remove(s);
					if (input.Count == 1) break;
				}
			}
		}

		int ox = Convert.ToInt32(input[0], 2);

		input = new List<string>(this.input);

		for (int i = 0; i < input[0].Length; i++)
		{
			List<string> worklist = new();
			foreach (string s in input)
			{
				worklist.Add(s);
			}

			int occurence1 = 0;
			int occurence0 = 0;
			for (int j = 0; j < worklist.Count; j++)
			{
				char c = worklist[j][i];
				if (c == '1')
					occurence1++;
				else
					occurence0++;
			}

			if (occurence1 > worklist.Count / 2)
				criticalBit = '1';
			else if (occurence0 > worklist.Count / 2)
				criticalBit = '0';
			else
				criticalBit = '1';

			foreach (var s in worklist.Where(s => s[i] == criticalBit))
			{
				input.Remove(s);
				if (input.Count == 1) break;
			}

			if (input.Count == 1) break;
		}

		int co = Convert.ToInt32(input[0], 2);
		return ox * co;
	}
}