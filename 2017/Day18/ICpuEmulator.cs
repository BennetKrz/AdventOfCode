namespace AdventOfCode._2017.Day18;

public abstract class ICpuEmulator
{
	public Dictionary<string, long> registers = new();
	public long position = 0;

	private protected void Add(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] += value;
		position++;
	}

	private protected void Jgz(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		long offset = GetValue(operation.OperandTwo);
		if (value > 0)
		{
			position += offset;
			return;
		}

		position++;
	}

	private protected void Mod(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] %= value;
		position++;
	}

	private protected void Mul(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] *= value;
		position++;
	}

	private protected void Set(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] = value;
		position++;
	}

	private protected long GetValue(string registerOrValue)
	{
		return !long.TryParse(registerOrValue, out _) ? registers[registerOrValue] : long.Parse(registerOrValue);
	}
	public abstract void Rcv(Operation operation);
	public abstract void Snd(Operation operation);
}