namespace AdventOfCode._2023;

public class Day1 : IAdventDay
{
	private readonly List<string> input;

	private readonly List<string> numbers = new()
		{ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

	public Day1()
	{
		input = Helper.GetInput(1).ToList();
	}

	public long PartOne()
	{
		return 0;
		long sum = 0;
		foreach (string line in input)
		{
			sum += CombineTwoNumbersPartOne(line);
		}

		return sum;
	}

	public long PartTwo()
	{
		long sum = 0;
		foreach (string line in input)
		{
			sum += CombineTwoNumbersPartTwo(line);
		}

		return sum;
	}

	private int CombineTwoNumbersPartOne(string line)
	{
		string numberOne = string.Empty;
		string numberTwo = string.Empty;
		//Get first Number;
		for (int i = 0; i < line.Length; i++)
		{
			if (char.IsDigit(line[i]))
			{
				numberOne = line[i].ToString();
				break;
			}
		}

		//Get second digit;
		for (int i = line.Length - 1; i >= 0; i--)
		{
			if (char.IsDigit(line[i]))
			{
				numberTwo = line[i].ToString();
				break;
			}
		}

		return int.Parse(numberOne + numberTwo);
	}

	private int CombineTwoNumbersPartTwo(string line)
	{
		int numberOne = 0;
		int numberTwo = 0;

		int indexOfFirstNumber = IndexOfFirstNumber(line);
		int indexOfLastNumber = IndexOfLastNumber(line);

		string partBeforFirstNumber = line.Substring(0, indexOfFirstNumber);
		string partAfterLastNumber = line.Substring(indexOfLastNumber);

		//enthält dieser Teil jeweils eine Zahl?
		if (numbers.Any(s => partBeforFirstNumber.Contains(s)))
		{
			if (partBeforFirstNumber.Length <= 5)
			{
				numberOne = IdentifyNumber(partBeforFirstNumber);
				goto ZweiteNummer;
			}
			//Ja, jetzt von vorne heran immer 5 buchstaben nehmen und wenn eine Nummer gefunden wird, ersetzen
			for (int i = 0; i < partBeforFirstNumber.Length; i++)
			{
				string tmp = partBeforFirstNumber.Substring(i, 5);
				tmp = tmp.Replace("one", "1");
				tmp = tmp.Replace("two", "2");
				tmp = tmp.Replace("three", "3");
				tmp = tmp.Replace("four", "4");
				tmp = tmp.Replace("five", "5");
				tmp = tmp.Replace("six", "6");
				tmp = tmp.Replace("seven", "7");
				tmp = tmp.Replace("eight", "8");
				tmp = tmp.Replace("nine", "9");

				for (int j = 0; j < tmp.Length; j++)
				{
					if (char.IsDigit(tmp[j]))
					{
						numberOne = int.Parse(tmp[j].ToString());
						goto ZweiteNummer;
					}
				}
			}
		} else {
			numberOne = CombineTwoNumbersPartOne(line) / 10;
		}
		
		ZweiteNummer:
		//enthält dieser Teil jeweils eine Zahl?
		if (numbers.Any(s => partAfterLastNumber.Contains(s)))
		{
			if (partAfterLastNumber.Length <= 5)
			{
				numberTwo = IdentifyNumber(partAfterLastNumber);
				goto Ende;
			}
			
			//Ja, jetzt von vorne heran immer 5 buchstaben nehmen und wenn eine Nummer gefunden wird, ersetzen
			for (int i = partAfterLastNumber.Length - 5; i >= 0; i--)
			{
				string tmp = partAfterLastNumber.Substring(i, 5);
				tmp = tmp.Replace("one", "1");
				tmp = tmp.Replace("two", "2");
				tmp = tmp.Replace("three", "3");
				tmp = tmp.Replace("four", "4");
				tmp = tmp.Replace("five", "5");
				tmp = tmp.Replace("six", "6");
				tmp = tmp.Replace("seven", "7");
				tmp = tmp.Replace("eight", "8");
				tmp = tmp.Replace("nine", "9");

				for (int j = 0; j < tmp.Length; j++)
				{
					if (char.IsDigit(tmp[j]))
					{
						numberTwo = int.Parse(tmp[j].ToString());
						goto Ende;
					}
				}
			}
		} else {
			numberTwo = CombineTwoNumbersPartOne(line) % 10;
		}
		Ende:
		return int.Parse(numberOne.ToString() + numberTwo.ToString());
	}

	private int IdentifyNumber(string s)
	{
		if (s.Contains("one"))
		{
			return 1;
		} 
		if (s.Contains("two"))
		{
			return 2;
		} 
		if (s.Contains("three"))
		{
			return 3;
		} if (s.Contains("four"))
		{
			return 4;
		} 
		if (s.Contains("five"))
		{
			return 5;
		} 
		if (s.Contains("six"))
		{
			return 6;
		} 
		if (s.Contains("seven"))
		{
			return 7;
		} 
		if (s.Contains("eight"))
		{
			return 8;
		} 
		return 9;
	}

	private int IndexOfFirstNumber(string s)
	{
		for (int i = 0; i < s.Length; i++)
		{
			if (char.IsDigit(s[i])) return i;
		}

		return -1;
	}

	private int IndexOfLastNumber(string s)
	{
		for (int i = s.Length - 1; i >= 0; i--)
		{
			if (char.IsDigit(s[i])) return i;
		}

		return -1;
	}
}