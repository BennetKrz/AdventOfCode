using AdventOfCode._2017;

namespace AdventOfCode._2017;

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
		InstructionEmulator emulator = new InstructionEmulator(operations, input.Count, 1);
		emulator.Execute();
		return long.MinValue;
	}

	public long PartTwo()
	{
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

public class InstructionEmulator
{
	private int amountRecover;
	private int amountCounter = 0;
	private int programSize;
	private long position = 0;
	private long latestSoundFrequency = 0;
	private Dictionary<string, long> registers = new();
	private List<Operation> operations;
	public InstructionEmulator(List<Operation> operations, int upperLimit, int amountRecover)
	{
		this.operations = operations;
		this.programSize = upperLimit;
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
		while (position < programSize && position > -1 && amountCounter < amountRecover)
		{
			Operation operation = this.operations[(int)position];
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

	private void Add(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] += value;
		position++;
	}

	private void Jgz(Operation operation)
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

	private void Mod(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] %= value;
		position++;
	}

	private void Mul(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] *= value;
		position++;
	}

	private void Rcv(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		if (value != 0)
		{
			amountCounter++;
			Console.WriteLine($"Recover operation trigger, last frequency was: {latestSoundFrequency}");
		}

		position++;
	}

	private void Set(Operation operation)
	{
		string targetRegister = operation.OperandOne;
		long value = GetValue(operation.OperandTwo);
		registers[targetRegister] = value;
		position++;
	}

	private void Snd(Operation operation)
	{
		long value = GetValue(operation.OperandOne);
		latestSoundFrequency = value;
		position++;
	}

	private long GetValue(string registerOrValue)
	{
		return !long.TryParse(registerOrValue, out _) ? registers[registerOrValue] : long.Parse(registerOrValue);
	}
}

public class Operation()
{
	public Action Action { get; set; }
	public string OperandOne { get; set; }
	public string? OperandTwo { get; set; }
}

public enum Action
{
	snd,
	set,
	add,
	mul,
	mod,
	rcv,
	jgz
}