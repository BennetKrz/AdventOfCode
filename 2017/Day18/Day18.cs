namespace AdventOfCode._2017.Day18;

public class Day18 : AdventBase
{
	private List<string> input;

	public Day18() : base(2017, 18)
	{
		this.input = Input.ToList();
	}

	public override long PartOne()
	{
		List<Operation> operations = ParseInput();
		InstructionEmulatorPartOne emulatorPartOne = new InstructionEmulatorPartOne(operations, input.Count, 1);
		emulatorPartOne.Execute();
		return long.MinValue;
	}

	public override long PartTwo()
	{
		List<Operation> operations = ParseInput();
		InstructionEmulatorPartTwo emulatorOne = new(operations, input.Count, 0);
		InstructionEmulatorPartTwo emulatorTwo = new(operations, input.Count, 1);
		emulatorOne.SetOtherEmulator(emulatorTwo);
		emulatorTwo.SetOtherEmulator(emulatorOne);
		return 0;
	}

	private List<Operation> ParseInput()
	{
		List<Operation> operations = new();
		foreach (string line in input)
		{
			Operation operation = new();

			string[] parts = line.Split(" ");
			operation.Action = parts[0] switch
			{
				"snd" => Action.Snd,
				"set" => Action.Set,
				"add" => Action.Add,
				"mul" => Action.Mul,
				"mod" => Action.Mod,
				"rcv" => Action.Rcv,
				"jgz" => Action.Jgz,
				_ => operation.Action
			};

			operation.OperandOne = parts[1];
			if (parts.Length > 2)
			{
				operation.OperandTwo = parts[2];
			}

			operations.Add(operation);
		}

		return operations;
	}
}