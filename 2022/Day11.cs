using System.Numerics;

namespace AdventOfCode._2022;

public class Day11 : IAdventDay
{
	private readonly string[] input;

	public Day11()
	{
		input = Helper.GetInput(11);
	}

	public long PartOne()
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
				for (int i = 0; i < monkeys[affe].holdingItems.Count; i++)
				{
					//Worry level increasen:
					monkeys[affe].holdingItems[i] = monkeys[affe].operation(monkeys[affe].holdingItems[i]);
					monkeys[affe].inspectedItems++;
					//Divide by 3 round down to nearest integer:
					double tmp = (double)monkeys[affe].holdingItems[i];
					tmp /= 3;
					monkeys[affe].holdingItems[i] = (int)Math.Floor(tmp);
					//Dividable check:
					if (monkeys[affe].holdingItems[i] % monkeys[affe].testDivisor == 0)
						//Werfe zu test = true
						monkeys[monkeys[affe].testTrueMonkey].holdingItems.Add(monkeys[affe].holdingItems[i]);
					else
						monkeys[monkeys[affe].testFalseMonkey].holdingItems.Add(monkeys[affe].holdingItems[i]);
				}

				monkeys[affe].holdingItems.Clear();
			}
		}

		//Jetzt die 2 activsten finden:
		List<int> inspectedItems = new();
		for (int i = 0; i < monkeys.Count; i++)
		{
			inspectedItems.Add(monkeys[i].inspectedItems);
		}

		inspectedItems.Sort();
		Console.WriteLine(inspectedItems[inspectedItems.Count - 1]);
		Console.WriteLine(inspectedItems[inspectedItems.Count - 2]);
		Console.Write(
			$"{inspectedItems[inspectedItems.Count - 1]} * {inspectedItems[inspectedItems.Count - 2]} = ");
		return inspectedItems[inspectedItems.Count - 1] * inspectedItems[inspectedItems.Count - 2];
	}

	public long PartTwo()
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
			divisors.Add(monkeys[i].testDivisor);
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
				for (int i = 0; i < monkeys[affe].holdingItems.Count; i++)
				{
					//Worry level increasen:
					monkeys[affe].holdingItems[i] = monkeys[affe].operation(monkeys[affe].holdingItems[i]);
					monkeys[affe].holdingItems[i] = monkeys[affe].holdingItems[i] % modulo;
					monkeys[affe].inspectedItems++;
					//Dividable check:
					if (monkeys[affe].holdingItems[i] % monkeys[affe].testDivisor == 0)
						//Werfe zu test = true
						monkeys[monkeys[affe].testTrueMonkey].holdingItems.Add(monkeys[affe].holdingItems[i]);
					else
						monkeys[monkeys[affe].testFalseMonkey].holdingItems.Add(monkeys[affe].holdingItems[i]);
				}

				monkeys[affe].holdingItems.Clear();
			}
		}

		//Jetzt die 2 activsten finden:
		List<int> inspectedItems = new();
		for (int i = 0; i < monkeys.Count; i++)
		{
			inspectedItems.Add(monkeys[i].inspectedItems);
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
	public int factor;
	public bool factorIsOld;
	public List<BigInteger> holdingItems = new();
	public int inspectedItems;
	private int modulo;
	public bool operationIsPlus;
	public int testDivisor;
	public int testFalseMonkey;
	public int testTrueMonkey;

	public Monkey(List<BigInteger> holdingItems, bool operationIsPlus, int factor, int testDivisor,
	              int testTrueMonkey, int testFalseMonkey, bool factorIsOld)
	{
		this.holdingItems = holdingItems;
		this.operationIsPlus = operationIsPlus;
		this.factor = factor;
		this.testDivisor = testDivisor;
		this.testTrueMonkey = testTrueMonkey;
		this.testFalseMonkey = testFalseMonkey;
		this.factorIsOld = factorIsOld;
	}

	public BigInteger operation(BigInteger value)
	{
		BigInteger newValue = 0;
		if (factorIsOld)
		{
			if (operationIsPlus)
				return newValue = value + value;
			return newValue = value * value;
		}

		if (operationIsPlus)
			return newValue = value + factor;
		return newValue = value * factor;
	}
}