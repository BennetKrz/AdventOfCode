namespace AdventOfCode._2017.Day18;

public class InstructionEmulatorPartTwo : CpuEmulator
{
	private readonly Queue<long> valuesToReceive = new();
	private InstructionEmulatorPartTwo otherEmulator;
	private readonly int programSize;
	private readonly List<Operation> operations;
	private readonly int programId;
	
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public InstructionEmulatorPartTwo(List<Operation> operations, int programSize, int programId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		this.operations = operations;
		this.programSize = programSize;
		this.programId = programId;
		CreateAllRegisters();
	}

	public void SetOtherEmulator(InstructionEmulatorPartTwo otherEmulator)
	{
		this.otherEmulator = otherEmulator;
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

		Registers["p"] = programId;
	}
	
	public void Execute()
	{
		while (Position < programSize && Position > -1)
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

	public void ReceiveNewValue(long newValue)
	{
		valuesToReceive.Enqueue(newValue);
	}

	public override void Rcv(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		if (valuesToReceive.Count == 0)
		{
			//TODO: Wenn keine Daten da sind, dann soll der andere Emulator weiter machen
		}
		long value = valuesToReceive.Dequeue();
		Registers[targetRegister] = value;
	}

	public override void Snd(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		otherEmulator.ReceiveNewValue(value);
	}
}