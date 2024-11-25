using System.Globalization;
using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day19 : AdventBase
{
	private readonly List<string> input;

	public Day19() : base(2023, 19)
	{
		this.input = Input.ToList();
	}

	public override long PartOne()
	{
		long sum = 0;
		Dictionary<string, Workflow> workflows = new();
		List<Part> parts = new();
		ParseInput(workflows, parts);
		
		for (int i = 0; i < parts.Count; i++)
		{
			Part p = parts[i];
			string nextWorkflow = "in";
			int conditionCounter = 0;
			while (nextWorkflow != "A" && nextWorkflow != "R")
			{
				var currentWorkflow = workflows[nextWorkflow];
				if (conditionCounter >= currentWorkflow.Conditions.Count)
				{
					nextWorkflow = currentWorkflow.ExitFlow;
					if (nextWorkflow == "A" || nextWorkflow == "R")
					{
						break;
					}
					conditionCounter = 0;
					currentWorkflow = workflows[nextWorkflow];
				}
				Condition c = currentWorkflow.Conditions[conditionCounter];
				if (IsConditionTrue(p, c))
				{
					nextWorkflow = c.OnTrue;
					conditionCounter = 0;
				}
				else
				{
					conditionCounter++;
				}
			}

			if (nextWorkflow == "A")
			{
				sum += CalculateRating(p);
			}
		}
		return sum;
	}

	public override long PartTwo()
	{
		return 0;
	}

	private long CalculateRating(Part part)
	{
		return part.values.Sum(i => i.Value);
	}

	private bool IsConditionTrue(Part part, Condition condition)
	{
		switch (condition.Operator)
		{
			case Operator.LessThan:
				return part.values[condition.OperandOne] < condition.OperandTwo;
			case Operator.MoreThan:
				return part.values[condition.OperandOne] > condition.OperandTwo;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void ParseInput(Dictionary<string, Workflow> workflows, List<Part> parts)
	{
		int emptyLine = input.FindIndex(s => s == "");
		//Workflows Parsen:
		for (int i = 0; i < emptyLine; i++)
		{
			string workflowName = input[i].Split("{")[0];
			List<Condition> conditions = new();
			string[] cons = input[i].Substring(input[i].IndexOf("{") + 1).Split(",");
			for (int c = 0; c < cons.Length; c++)
			{
				if (c == cons.Length - 1)
				{
					var exitCondition = cons[c][0..^1];
					workflows.Add(workflowName, new Workflow(conditions, exitCondition));
					continue;
				}

				string onExit = cons[c].Split(":")[1];
				string operandOne = cons[c].Split(new char[] { '>', '<', ':' })[0];
				string operandTwo = cons[c].Split(new char[] { '>', '<', ':' })[1];
				Operator o = cons[c].Contains("<") ? Operator.LessThan : Operator.MoreThan;
				conditions.Add(new Condition(operandOne, o, int.Parse(operandTwo), onExit));
			}
		}
		//Parse Parts:
		for (int i = emptyLine + 1; i < input.Count; i++)
		{
			string[] data = input[i].Replace("{", "")
									.Replace("}", "")
									.Split(",");
			Dictionary<string, int> values = new Dictionary<string, int>
			{
				{ "x", int.Parse(data[0].Split("=")[1]) },
				{ "m", int.Parse(data[1].Split("=")[1]) },
				{ "a", int.Parse(data[2].Split("=")[1]) },
				{ "s", int.Parse(data[3].Split("=")[1]) }
			};
			parts.Add(new Part(values));
		}
	}

	private enum Operator {
		LessThan,
		MoreThan
	}
	
	private record Part(Dictionary<string, int> values);

	private record Condition(string OperandOne, Operator Operator, int OperandTwo, string OnTrue);

	private record Workflow(List<Condition> Conditions, string ExitFlow);
}