namespace AdventOfCode._2017;

public class Day8 : IAdventDay
{
	private readonly List<string> input;
	private readonly Dictionary<string, int> registers = new();

	public Day8()
	{
		input = Helper.GetInput(8).ToList();
	}


	public long PartOne()
	{
		InitializeRegisters();

		foreach (string s in input)
		{
			//Erst in instruction umwandeln
			Instruction currentInstruction = ParseInstruction(s);
			//Instruction ausführen:
			ExecuteInstruction(currentInstruction);
		}

		return registers.MaxBy(kvp => kvp.Value).Value;
	}


	public long PartTwo()
	{
		InitializeRegisters();
		List<int> maxValuesDuringExecution = new();
		foreach (string s in input)
		{
			//Erst in instruction umwandeln
			Instruction currentInstruction = ParseInstruction(s);
			//Instruction ausführen:
			ExecuteInstruction(currentInstruction);
			//Save max value
			maxValuesDuringExecution.Add(registers.MaxBy(kvp => kvp.Value).Value);
		}

		return maxValuesDuringExecution.Max();
	}

	private bool IsConditionSatisfied(Instruction inst)
	{
		return inst.Operator switch
		{
			Operator.Equals => inst.ConditionValueOne == inst.ConditionValueTwo,
			Operator.NotEquals => inst.ConditionValueOne != inst.ConditionValueTwo,
			Operator.Less => inst.ConditionValueOne < inst.ConditionValueTwo,
			Operator.More => inst.ConditionValueOne > inst.ConditionValueTwo,
			Operator.LessOrEquals => inst.ConditionValueOne <= inst.ConditionValueTwo,
			Operator.MoreOrEquals => inst.ConditionValueOne >= inst.ConditionValueTwo,
			_ => throw new Exception()
		};
	}

	private void ExecuteInstruction(Instruction currentInstruction)
	{
		if (IsConditionSatisfied(currentInstruction)) DoInstruction(currentInstruction);
	}

	private void DoInstruction(Instruction currentInstruction)
	{
		if (currentInstruction.Decrease)
			registers[currentInstruction.TargetRegister] -= currentInstruction.ValueToChangeBy;
		else
			registers[currentInstruction.TargetRegister] += currentInstruction.ValueToChangeBy;
	}

	private Instruction ParseInstruction(string s)
	{
		string[] instructionData = s.Split(" ");
		string targetRegister = instructionData[0];
		bool decrease = instructionData[1] == "dec";
		int valueToChangeBy = int.Parse(instructionData[2]);
		int conditionValueOne = GetConditionValue(instructionData[4]);
		int conditionValueTwo = GetConditionValue(instructionData[6]);
		Operator @operator = GetCondition(instructionData[5]);
		return new Instruction(targetRegister,
		                       decrease,
		                       valueToChangeBy,
		                       conditionValueOne,
		                       conditionValueTwo,
		                       @operator);
	}

	private Operator GetCondition(string s)
	{
		return s switch
		{
			">" => Operator.More,
			"<" => Operator.Less,
			">=" => Operator.MoreOrEquals,
			"<=" => Operator.LessOrEquals,
			"==" => Operator.Equals,
			"!=" => Operator.NotEquals,
			_ => throw new Exception()
		};
	}

	private int GetConditionValue(string s)
	{
		if (int.TryParse(s, out _))
			return int.Parse(s);
		return registers[s];
	}

	private void InitializeRegisters()
	{
		registers.Clear();
		foreach (string s in input)
		{
			string[] instructionData = s.Split(" ");
			registers.TryAdd(instructionData[0], 0);
		}
	}
}

public record Instruction(string TargetRegister, bool Decrease, int ValueToChangeBy, int ConditionValueOne,
                          int ConditionValueTwo, Operator Operator)
{
	public string TargetRegister { get; set; } = TargetRegister;
	public bool Decrease { get; set; } = Decrease;
	public int ValueToChangeBy { get; set; } = ValueToChangeBy;
	public int ConditionValueOne { get; set; } = ConditionValueOne;
	public int ConditionValueTwo { get; set; } = ConditionValueTwo;
	public Operator Operator { get; set; } = Operator;
}

public enum Operator
{
	Equals,
	NotEquals,
	More,
	Less,
	LessOrEquals,
	MoreOrEquals
}