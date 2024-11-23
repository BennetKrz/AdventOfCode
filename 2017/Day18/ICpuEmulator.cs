namespace AdventOfCode._2017.Day18;

public abstract class CpuEmulator
{
	protected readonly Dictionary<string, long> Registers = new();
	protected long Position;

	private protected void Add(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		Registers[targetRegister] += value;
		Position++;
	}

	private protected void Jgz(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		long offset = GetValue(operation.OperandTwo);
		if (value > 0)
		{
			Position += offset;
			return;
		}

		Position++;
	}

	private protected void Mod(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		Registers[targetRegister] %= value;
		Position++;
	}

	private protected void Mul(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		Registers[targetRegister] *= value;
		Position++;
	}

	private protected void Set(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		Registers[targetRegister] = value;
		Position++;
	}

	private protected long GetValue(string registerOrValue)
	{
		return !long.TryParse(registerOrValue, out _) ? Registers[registerOrValue] : long.Parse(registerOrValue);
	}
	public abstract void Rcv(Operation operation);
	public abstract void Snd(Operation operation);
}