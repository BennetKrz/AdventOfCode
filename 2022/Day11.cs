using System.Numerics;

namespace AdventOfCode._2022;

public class Day11 : AdventBase
{
	private readonly string[] input;

	public Day11() : base(2022, 11)
	{
		input = Input;
	}

	public override long PartOne()
	{
		List<Monkey> monkeys = new();
		//Parsing:
		for (int i = 0; i < input.Length; i += 7)
		{
			List<BigInteger> startItems = new();
			string s = input[i + 1];
			string[] items = s.Substring(s.IndexOf(":") + 1).Replace(" ", "").Split(",");
			for (int j = 0; j < items.Length; j++)
			{
				startItems.Add(int.Parse(items[j]));
			}

			s = input[i + 2];
			bool isPlus;
			int factor = 0;
			bool factorIsOld = false;
			if (s.Contains("+"))
			{
				isPlus = true;
				s = s.Substring(s.IndexOf("+") + 2);
				if (s == "old")
					factorIsOld = true;
				else
					factor = int.Parse(s);
			} else
			{
				isPlus = false;
				s = s.Substring(s.IndexOf("*") + 2);
				if (s == "old")
					factorIsOld = true;
				else
					factor = int.Parse(s);
			}

			int divisor;
			s = input[i + 3];
			divisor = int.Parse(s.Substring(s.IndexOf("y") + 1));
			int testTrueMonkey;
			int testFalseMonkey;
			s = input[i + 4];
			testTrueMonkey = int.Parse(s.Substring(s.IndexOf("y") + 1));
			s = input[i + 5];
			testFalseMonkey = int.Parse(s.Substring(s.IndexOf("y") + 1));
			//Parsing ist durch... Jetzt in die Classen ballern:
			Monkey m = new(startItems, isPlus, factor, divisor, testTrueMonkey, testFalseMonkey,
			               factorIsOld);
			monkeys.Add(m);
		}

		//So, jetzt die eigentliche Logik
		//Immer durch 3 teilen und dann abrunden auf den nächsten Integer
		//So, anzahl der Runden sicherstellen
		for (int runde = 0; runde < 20; ++runde)
		{
			//Für jeden Affen:
			for (int affe = 0; affe < monkeys.Count; affe++)
			{
				//Für jedes Item welches der Affe besitzt wiederholen: Am ende immer die ganze list clearen
				for (int i = 0; i < monkeys[affe].HoldingItems.Count; i++)
				{
					//Worry level increasen:
					monkeys[affe].HoldingItems[i] = monkeys[affe].Operation(monkeys[affe].HoldingItems[i]);
					monkeys[affe].InspectedItems++;
					//Divide by 3 round down to nearest integer:
					double tmp = (double)monkeys[affe].HoldingItems[i];
					tmp /= 3;
					monkeys[affe].HoldingItems[i] = (int)Math.Floor(tmp);
					//Dividable check:
					if (monkeys[affe].HoldingItems[i] % monkeys[affe].TestDivisor == 0)
						//Werfe zu test = true
						monkeys[monkeys[affe].TestTrueMonkey].HoldingItems.Add(monkeys[affe].HoldingItems[i]);
					else
						monkeys[monkeys[affe].TestFalseMonkey].HoldingItems.Add(monkeys[affe].HoldingItems[i]);
				}

				monkeys[affe].HoldingItems.Clear();
			}
		}

		//Jetzt die 2 activsten finden:
		List<int> inspectedItems = monkeys.Select(t => t.InspectedItems).ToList();

		inspectedItems.Sort();
		Console.WriteLine(inspectedItems[inspectedItems.Count - 1]);
		Console.WriteLine(inspectedItems[inspectedItems.Count - 2]);
		Console.Write(
			$"{inspectedItems[inspectedItems.Count - 1]} * {inspectedItems[inspectedItems.Count - 2]} = ");
		return inspectedItems[inspectedItems.Count - 1] * inspectedItems[inspectedItems.Count - 2];
	}

	public override long PartTwo()
	{
		List<Monkey> monkeys = new();
		//Parsing:
		for (int i = 0; i < input.Length; i += 7)
		{
			List<BigInteger> startItems = new();
			string s = input[i + 1];
			string[] items = s.Substring(s.IndexOf(":") + 1).Replace(" ", "").Split(",");
			for (int j = 0; j < items.Length; j++)
			{
				startItems.Add(int.Parse(items[j]));
			}

			s = input[i + 2];
			bool isPlus;
			int factor = 0;
			bool factorIsOld = false;
			if (s.Contains("+"))
			{
				isPlus = true;
				s = s.Substring(s.IndexOf("+") + 2);
				if (s == "old")
					factorIsOld = true;
				else
					factor = int.Parse(s);
			} else
			{
				isPlus = false;
				s = s.Substring(s.IndexOf("*") + 2);
				if (s == "old")
					factorIsOld = true;
				else
					factor = int.Parse(s);
			}

			int divisor;
			s = input[i + 3];
			divisor = int.Parse(s.Substring(s.IndexOf("y") + 1));
			int testTrueMonkey;
			int testFalseMonkey;
			s = input[i + 4];
			testTrueMonkey = int.Parse(s.Substring(s.IndexOf("y") + 1));
			s = input[i + 5];
			testFalseMonkey = int.Parse(s.Substring(s.IndexOf("y") + 1));
			//Parsing ist durch... Jetzt in die Classen ballern:
			Monkey m = new(startItems, isPlus, factor, divisor, testTrueMonkey, testFalseMonkey,
			               factorIsOld);
			monkeys.Add(m);
		}

		//Modulo bestimmen
		List<int> divisors = new();
		for (int i = 0; i < monkeys.Count; i++)
		{
			divisors.Add(monkeys[i].TestDivisor);
		}

		int modulo = divisors.Aggregate((x, y) => x * y);

		//So, jetzt die eigentliche Logik
		//Immer durch 3 teilen und dann abrunden auf den nächsten Integer
		//So, anzahl der Runden sicherstellen
		for (int runde = 0; runde < 10000; ++runde)
		{
			//Für jeden Affen:
			for (int affe = 0; affe < monkeys.Count; affe++)
			{
				//Für jedes Item welches der Affe besitzt wiederholen: Am ende immer die ganze list clearen
				for (int i = 0; i < monkeys[affe].HoldingItems.Count; i++)
				{
					//Worry level increasen:
					monkeys[affe].HoldingItems[i] = monkeys[affe].Operation(monkeys[affe].HoldingItems[i]);
					monkeys[affe].HoldingItems[i] = monkeys[affe].HoldingItems[i] % modulo;
					monkeys[affe].InspectedItems++;
					//Dividable check:
					if (monkeys[affe].HoldingItems[i] % monkeys[affe].TestDivisor == 0)
						//Werfe zu test = true
						monkeys[monkeys[affe].TestTrueMonkey].HoldingItems.Add(monkeys[affe].HoldingItems[i]);
					else
						monkeys[monkeys[affe].TestFalseMonkey].HoldingItems.Add(monkeys[affe].HoldingItems[i]);
				}

				monkeys[affe].HoldingItems.Clear();
			}
		}

		//Jetzt die 2 activsten finden:
		List<int> inspectedItems = new();
		for (int i = 0; i < monkeys.Count; i++)
		{
			inspectedItems.Add(monkeys[i].InspectedItems);
		}

		inspectedItems.Sort();
		long maxOne = inspectedItems[inspectedItems.Count - 1];
		long maxTwo = inspectedItems[inspectedItems.Count - 2];
		Console.WriteLine(maxOne);
		Console.WriteLine(maxTwo);
		Console.Write($"{maxOne} * {maxTwo} = {maxTwo * maxOne}");
		return maxTwo * maxOne;
	}
}

public class Monkey
{
	public readonly int Factor;
	public readonly bool FactorIsOld;
	public readonly List<BigInteger> HoldingItems;
	public int InspectedItems;
	public readonly bool OperationIsPlus;
	public readonly int TestDivisor;
	public readonly int TestFalseMonkey;
	public readonly int TestTrueMonkey;

	public Monkey(List<BigInteger> holdingItems, bool operationIsPlus, int factor, int testDivisor,
	              int testTrueMonkey, int testFalseMonkey, bool factorIsOld)
	{
		this.HoldingItems = holdingItems;
		this.OperationIsPlus = operationIsPlus;
		this.Factor = factor;
		this.TestDivisor = testDivisor;
		this.TestTrueMonkey = testTrueMonkey;
		this.TestFalseMonkey = testFalseMonkey;
		this.FactorIsOld = factorIsOld;
	}

	public BigInteger Operation(BigInteger value)
	{
		if (FactorIsOld)
		{
			if (OperationIsPlus)
				return value + value;
			return value * value;
		}

		if (OperationIsPlus)
			return value + Factor;
		return value * Factor;
	}
}