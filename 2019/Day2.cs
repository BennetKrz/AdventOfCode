namespace AdventOfCode._2019;

public class Day2 : IAdventDay
{
	private readonly string[] input;

	public Day2()
	{
		input = Helper.GetInputOneLine(2).Split(',');
	}

	public long PartOne()
	{
		string[] input = new string[this.input.Length];
		Array.Copy(this.input, input, this.input.Length);
		input[1] = 12.ToString();
		input[2] = 2.ToString();
		return ExecuteProgram(input);
	}

	public long PartTwo()
	{
		string[] input = new string[this.input.Length];
		Array.Copy(this.input, input, this.input.Length);
		for (int noun = 0; noun < 100; noun++)
		{
			for (int verb = 0; verb < 100; verb++)
			{
				Array.Copy(this.input, input, this.input.Length);
				//replace
				input[1] = noun.ToString();
				input[2] = verb.ToString();
				long result = ExecuteProgram(input);
				if (result == 19690720) return 100 * noun + verb;
			}
		}

		return long.MinValue;
	}

	private long ExecuteProgram(string[] input)
	{
		for (int i = 0; i < input.Length - 3; i += 4)
		{
			int opCode = int.Parse(input[i]);
			int source1 = int.Parse(input[i + 1]);
			int numberAtSource1 = int.Parse(input[source1]);
			int source2 = int.Parse(input[i + 2]);
			int numberAtSource2 = int.Parse(input[source2]);
			int targetPos = int.Parse(input[i + 3]);
			if (opCode == 1)
				input[targetPos] = (numberAtSource1 + numberAtSource2).ToString();
			else if (opCode == 2)
				input[targetPos] = (numberAtSource1 * numberAtSource2).ToString();
			else if (opCode == 99) break;
		}

		return long.Parse(input[0]);
	}
}