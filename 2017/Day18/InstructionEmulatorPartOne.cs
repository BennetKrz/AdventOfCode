namespace AdventOfCode._2017.Day18;

public class InstructionEmulatorPartOne : CpuEmulator
{
	
	private readonly int amountRecover;
	private int amountCounter;
	private readonly int programSize;
	private long latestSoundFrequency;
	private readonly List<Operation> operations;
	public InstructionEmulatorPartOne(List<Operation> operations, int upperLimit, int amountRecover)
	{
		this.operations = operations;
		programSize = upperLimit;
		this.amountRecover = amountRecover;
		CreateAllRegisters();
	}

	private void CreateAllRegisters()
	{
		//Create all registers
		foreach (Operation operation in operations)
		{
			if (!int.TryParse(operation.OperandOne, out _))
			{
				Registers.TryAdd(operation.OperandOne, 0);
			}

			if (operation.OperandTwo == null)
			{
				continue;
			}
			if (!int.TryParse(operation.OperandTwo, out _))
			{
				Registers.TryAdd(operation.OperandTwo, 0);
			}
		}
	}
	
	public void Execute()
	{
		while (Position < programSize && Position > -1 && amountCounter < amountRecover)
		{
			Operation operation = this.operations[(int)Position];
			switch (operation.Action)
			{
				case Action.Add:
					Add(operation);
					break;
				case Action.Jgz:
					Jgz(operation);
					break;
				case Action.Mod:
					Mod(operation);
					break;
				case Action.Mul:
					Mul(operation);
					break;
				case Action.Rcv:
					Rcv(operation);
					break;
				case Action.Set:
					Set(operation);
					break;
				case Action.Snd:
					Snd(operation);
					break;
			}
		}
	}

	public override void Rcv(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		if (value != 0)
		{
			amountCounter++;
			Console.WriteLine($"Recover operation trigger, last frequency was: {latestSoundFrequency}");
		}

		Position++;
	}

	public override void Snd(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		latestSoundFrequency = value;
		Position++;
	}
}
