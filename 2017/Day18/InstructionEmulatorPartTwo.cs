namespace AdventOfCode._2017.Day18;

public class InstructionEmulatorPartTwo : ICpuEmulator
{
	private Queue<long> _valuesToReceive = new();
	private InstructionEmulatorPartTwo _otherEmulator;
	private readonly int _programSize;
	private readonly List<Operation> _operations;
	private readonly int _programId;
	
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public InstructionEmulatorPartTwo(List<Operation> operations, int programSize, int programId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		_operations = operations;
		_programSize = programSize;
		_programId = programId;
		CreateAllRegisters();
	}

	public void SetOtherEmulator(InstructionEmulatorPartTwo otherEmulator)
	{
		_otherEmulator = otherEmulator;
	}
	
	private void CreateAllRegisters()
	{
		//Create all registers
		foreach (Operation operation in _operations)
		{
			if (!int.TryParse(operation.OperandOne, out _))
			{
				registers.TryAdd(operation.OperandOne, 0);
			}

			if (operation.OperandTwo == null)
			{
				continue;
			}
			if (!int.TryParse(operation.OperandTwo, out _))
			{
				registers.TryAdd(operation.OperandTwo, 0);
			}
		}

		registers["p"] = _programId;
	}
	
	public void Execute()
	{
		while (position < _programSize && position > -1)
		{
			Operation operation = this._operations[(int)position];
			switch (operation.Action)
			{
				case Action.add:
					Add(operation);
					break;
				case Action.jgz:
					Jgz(operation);
					break;
				case Action.mod:
					Mod(operation);
					break;
				case Action.mul:
					Mul(operation);
					break;
				case Action.rcv:
					Rcv(operation);
					break;
				case Action.set:
					Set(operation);
					break;
				case Action.snd:
					Snd(operation);
					break;
			}
		}
	}

	public void ReceiveNewValue(long newValue)
	{
		_valuesToReceive.Enqueue(newValue);
	}

	public override void Rcv(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		if (_valuesToReceive.Count == 0)
		{
			//TODO: Wenn keine Daten da sind, dann soll der andere Emulator weiter machen
		}
		long value = _valuesToReceive.Dequeue();
		registers[targetRegister] = value;
	}

	public override void Snd(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		_otherEmulator.ReceiveNewValue(value);
	}
}