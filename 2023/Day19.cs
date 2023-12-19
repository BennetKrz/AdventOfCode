using System.Globalization;
using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day19 : IAdventDay
{
	private List<string> input;

	public Day19()
	{
		this.input = Helper.GetInput(19).ToList();
	}

	public long PartOne()
	{
		Dictionary<string, Workflow> workflows = new();
		List<Part> parts = new();
		ParseInput(workflows, parts);
		return 0;
	}

	public long PartTwo()
	{
		return 0;
	}

	private void ParseInput(Dictionary<string, Workflow> workflows, List<Part> parts)
	{
		int emptyLine = input.FindIndex(s => s == "");
		//Workflows Parsen:
		for (int i = 0; i < emptyLine; i++)
		{
			string workflowName = input[i].Split("{")[0];
			string exitCondition = string.Empty;
			List<Condition> conditions = new();
			string[] cons = input[i].Substring(input[i].IndexOf("{") + 1).Split(",");
			for (int c = 0; c < cons.Length; c++)
			{
				if (c == cons.Length - 1)
				{
					exitCondition = cons[c][0..^1];
					workflows.Add(workflowName, new Workflow(conditions, exitCondition));
					continue;
				}

				string onExit = cons[c].Split(":")[1];
				string operandOne = cons[c].Split(new char[] { '>', '<', ':' })[0];
				string operandTwo = cons[c].Split(new char[] { '>', '<', ':' })[1];
				char operatorHere =
					cons[c].Contains("<") ? cons[c][cons[c].IndexOf("<")] : cons[c][cons[c].IndexOf(">")];
				Operator o = operatorHere == '<' ? Operator.LessThan : Operator.MoreThan;
				conditions.Add(new Condition(operandOne, o, int.Parse(operandTwo), onExit));
			}
		}
		//Parse Parts:
		for (int i = emptyLine + 1; i < input.Count; i++)
		{
			string[] data = input[i].Replace("{", "")
									.Replace("}", "")
									.Split(",");
			parts.Add(new Part(int.Parse(data[0].Split("=")[1]),
				int.Parse(data[1].Split("=")[1]),
				int.Parse(data[2].Split("=")[1]),
				int.Parse(data[3].Split("=")[1])));
			
		}
	}

	private enum Operator {
		LessThan,
		MoreThan
	}
	
	private record Part(int X, int M, int A, int S);

	private record Condition(string OperandOne, Operator Operator, int OperandTwo, string OnTrue);

	private record Workflow(List<Condition> Conditions, string ExitFlow);
}