namespace AdventOfCode._2017.Day18;

public class InstructionEmulatorPartOne : ICpuEmulator
{
	
	private readonly int _amountRecover;
	private int _amountCounter = 0;
	private readonly int _programSize;
	private long _latestSoundFrequency = 0;
	private List<Operation> _operations;
	public InstructionEmulatorPartOne(List<Operation> operations, int upperLimit, int amountRecover)
	{
		_operations = operations;
		_programSize = upperLimit;
		_amountRecover = amountRecover;
		CreateAllRegisters();
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
	}
	
	public void Execute()
	{
		while (position < _programSize && position > -1 && _amountCounter < _amountRecover)
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

	public override void Rcv(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		if (value != 0)
		{
			_amountCounter++;
			Console.WriteLine($"Recover operation trigger, last frequency was: {_latestSoundFrequency}");
		}

		position++;
	}

	public override void Snd(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		_latestSoundFrequency = value;
		position++;
	}
}
