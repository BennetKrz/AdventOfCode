using System.Drawing;

namespace AdventOfCode._2022;

public class Day9 : AdventBase
{
	private readonly string[] input;

	public Day9() : base(2022, 5)
	{
		input = Input;
	}

	public override long PartOne()
	{
		List<Instruction> instructions = new();
		for (int i = 0; i < input.Length; i++)
		{
			string direction = input[i].Substring(0, 1);
			int steps = int.Parse(input[i]
				                      .Substring(input[i].IndexOf(" "),
				                                 input[i].Length - input[i].IndexOf(" ")));
			Instruction intstruction = new(direction, steps);
			instructions.Add(intstruction);
		}

		//Jetzt alle Anweisungen abarbeiten
		Dictionary<Point, bool> visititPoints = new();
		Point headPos = new(0, 0);
		Point tailPos = new(0, 0);
		for (int numberOfInstruction = 0; numberOfInstruction < instructions.Count; numberOfInstruction++)
		{
			Instruction a = instructions[numberOfInstruction];
			for (int i = 0; i < instructions[numberOfInstruction].Steps; i++)
			{
				if (a.Direction == "R")
					headPos.X++;
				else if (a.Direction == "U")
					headPos.Y++;
				else if (a.Direction == "D")
					headPos.Y--;
				else if (a.Direction == "L") headPos.X--;

				//Um wie weit weicht der tail vom Kopf ab, größer als 1?
				int deltaX = headPos.X - tailPos.X;
				int delatY = headPos.Y - tailPos.Y;

				if (Math.Abs(deltaX) > 1 || Math.Abs(delatY) > 1)
				{
					tailPos.X += Math.Sign(deltaX);
					tailPos.Y += Math.Sign(delatY);
				}

				if (!visititPoints.ContainsKey(tailPos)) visititPoints.Add(tailPos, true);
			}
		}

		return visititPoints.Count;
	}

	public override long PartTwo()
	{
		List<Instruction> instructions = new();
		for (int i = 0; i < input.Length; i++)
		{
			string direction = input[i].Substring(0, 1);
			int steps = int.Parse(input[i]
				                      .Substring(input[i].IndexOf(" "),
				                                 input[i].Length - input[i].IndexOf(" ")));
			Instruction intstruction = new(direction, steps);
			instructions.Add(intstruction);
		}

		//Jetzt alle Anweisungen abarbeiten
		Dictionary<Point, bool> visititPoints = new();
		Point[] knots = new Point[10];
		for (int numberOfInstruction = 0; numberOfInstruction < instructions.Count; numberOfInstruction++)
		{
			Instruction a = instructions[numberOfInstruction];
			for (int i = 0; i < instructions[numberOfInstruction].Steps; i++)
			{
				if (a.Direction == "R")
					knots[0].X++;
				else if (a.Direction == "U")
					knots[0].Y++;
				else if (a.Direction == "D")
					knots[0].Y--;
				else if (a.Direction == "L") knots[0].X--;

				//Ist der Tail noch im direkten Umkreis des Kopfes? Wenn ja, mache nichts, sonst hinterherbewegen
				for (int j = 1; j < knots.Length; j++)
				{
					int deltaX = knots[j - 1].X - knots[j].X;
					int deltaY = knots[j - 1].Y - knots[j].Y;

					if (Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1)
					{
						knots[j].X += Math.Sign(deltaX);
						knots[j].Y += Math.Sign(deltaY);
					}
				}

				if (!visititPoints.ContainsKey(knots[knots.Length - 1]))
					visititPoints.Add(knots[knots.Length - 1], true);
			}
		}

		return visititPoints.Count;
	}

	private struct Instruction(string direction, int steps)
	{
		public readonly string Direction = direction;
		public readonly int Steps = steps;
	}
}