namespace AdventOfCode._2019;

public class Day2 : AdventBase
{
	private string[] input;

	public Day2() : base(2019, 2)
	{
		input = InputOneLine.Split(',');
	}

	public override long PartOne()
	{
		input[1] = 12.ToString();
		input[2] = 2.ToString();
		IntCodeEmulator emulator = new(input, null, false);
		emulator.ExecuteProgram();
		return long.Parse(emulator.GetAddress(0));
	}

	public override long PartTwo()
	{
		input = InputOneLine.Split(",");
		string[] instructions = new string[input.Length];
		Array.Copy(input, instructions, this.input.Length);
		for (int noun = 0; noun < 100; noun++)
		{
			for (int verb = 0; verb < 100; verb++)
			{
				Array.Copy(this.input, instructions, this.input.Length);
				//replace
				instructions[1] = noun.ToString();
				instructions[2] = verb.ToString();
				IntCodeEmulator emulator = new(instructions, null, false);
				emulator.ExecuteProgram();
				if (long.Parse(emulator.GetAddress(0)) == 19690720)
				{
					return 100 * noun + verb;
				}
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