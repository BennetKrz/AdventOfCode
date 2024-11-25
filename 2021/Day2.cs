namespace AdventOfCode._2021;

public class Day2 : AdventBase
{
	private readonly string[] input;

	public Day2() : base(2021, 2)
	{
		input = Input;
	}

	public override long PartOne()
	{
		List<Instruction> instructions = [];
		int forward = 0;
		int depth = 0;
		for (int i = 0; i < input.Length; i++)
		{
			Instruction instruction;
			if (input[i][0] == 'f')
				instruction = new Instruction(0, 0);
			else
				instruction = new Instruction(0, 1);

			string[] tmp = input[i].Split(' ');
			if (instruction.Dir == 0)
			{
				instruction.Value = Convert.ToInt32(tmp[1]);
			} else
			{
				if (tmp[0][0] == 'd')
					instruction.Value = Convert.ToInt32(tmp[1]);
				else
					instruction.Value = Convert.ToInt32(tmp[1]) * -1;
			}

			instructions.Add(instruction);
		}

		for (int i = 0; i < instructions.Count; i++)
		{
			if (instructions[i].Dir == 0)
				forward += instructions[i].Value;
			else
				depth += instructions[i].Value;
		}

		return forward * depth;
	}

	public override long PartTwo()
	{
		int aim = 0;
		int horizontal = 0;
		int depth = 0;
		List<Instruction> instructions = new();
		for (int i = 0; i < input.Length; i++)
		{
			Instruction instruction;
			if (input[i][0] == 'f')
				instruction = new Instruction(0, 0);
			else
				instruction = new Instruction(0, 1);

			string[] tmp = input[i].Split(' ');
			if (instruction.Dir == 0)
			{
				instruction.Value = Convert.ToInt32(tmp[1]);
			} else
			{
				if (tmp[0][0] == 'd')
					instruction.Value = Convert.ToInt32(tmp[1]);
				else
					instruction.Value = Convert.ToInt32(tmp[1]) * -1;
			}

			instructions.Add(instruction);
		}

		for (int i = 0; i < instructions.Count; i++)
		{
			if (instructions[i].Dir == 0)
			{
				horizontal += instructions[i].Value;
				depth += aim * instructions[i].Value;
			} else
			{
				aim += instructions[i].Value;
			}
		}

		return horizontal * depth;
	}

	private struct Instruction(int value, int dir)
	{
		public int Value = value;
		public readonly int Dir = dir;
	}
}