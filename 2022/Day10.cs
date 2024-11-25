namespace AdventOfCode._2022;

public class Day10 : AdventBase
{
	private readonly string[] input;

	public Day10() : base(2022, 10)
	{
		input = Input;
	}

	public override long PartOne()
	{
		int cicle = 0;
		int regX = 1;
		int ciclesToWait = 0;
		int instruction = 0;

		int sum = 0;

		while (instruction < input.Length)
		{
			if (input[instruction].Contains("noop"))
				ciclesToWait = 1;
			else if (input[instruction].Contains("addx")) ciclesToWait = 2;

			for (int i = 0; i < ciclesToWait; i++)
			{
				cicle++;
				if (cicle == 20 || cicle == 60 || cicle == 100 || cicle == 140 || cicle == 180 || cicle == 220)
					sum += regX * cicle;
				//Console.WriteLine($"cycle: {cicle} regX value: {regX} result value: {regX * cicle}");
			}

			ciclesToWait = 0;
			if (input[instruction].Contains("addx"))
			{
				string val = input[instruction].Substring(input[instruction].IndexOf(" "),
				                                          input[instruction].Length -
				                                          input[instruction].IndexOf(" "));
				regX += int.Parse(val);
			}

			instruction++;
		}

		return sum;
	}

	public override long PartTwo()
	{
		int regX = 1;
		int ciclesToWait = 0;
		int instruction = 0;

		string ctr = string.Empty;
		int ctrPos = 0;

		while (instruction < input.Length)
		{
			if (input[instruction].Contains("noop"))
				ciclesToWait = 1;
			else if (input[instruction].Contains("addx")) ciclesToWait = 2;

			for (int i = 0; i < ciclesToWait; i++)
			{
				ctr = ctrPos - 1 == regX || ctrPos == regX || ctrPos + 1 == regX ? ctr + "#" : ctr + ".";
				ctrPos++;
				if (ctrPos > 39) ctrPos = 0;
			}

			ciclesToWait = 0;
			if (input[instruction].Contains("addx"))
			{
				string val = input[instruction].Substring(input[instruction].IndexOf(" "),
				                                          input[instruction].Length -
				                                          input[instruction].IndexOf(" "));
				regX += int.Parse(val);
			}

			instruction++;
		}

		for (int i = 0; i < 6 * 40; i += 40)
		{
			for (int j = 0; j < 39; j++)
			{
				Console.Write(ctr[i + j]);
			}

			Console.Write("\n");
		}

		return long.MinValue;
	}
}