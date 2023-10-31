namespace AdventOfCode._2017.Day18;

public class Day18 : IAdventDay
{
	private List<string> input;

	public Day18()
	{
		this.input = Helper.GetInput(18).ToList();
	}

	public long PartOne()
	{
		List<Operation> operations = ParseInput();
		InstructionEmulatorPartOne emulatorPartOne = new InstructionEmulatorPartOne(operations, input.Count, 1);
		emulatorPartOne.Execute();
		return long.MinValue;
	}

	public long PartTwo()
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
				"snd" => Action.snd,
				"set" => Action.set,
				"add" => Action.add,
				"mul" => Action.mul,
				"mod" => Action.mod,
				"rcv" => Action.rcv,
				"jgz" => Action.jgz,
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