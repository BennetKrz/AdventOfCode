using AdventOfCode._2017;

namespace AdventOfCode._2017;

public class Day15 : IAdventDay
{
	private const int GeneratorAStartingValue = 512;
	private const int GeneratorBStartingValue = 191;
	private const int GeneratorAFactor = 16807;
	private const int GeneratorBFactor = 48271;
	private const int GeneratorAAcceptableCriteria = 4;
	private const int GeneratorBAcceptableCriteria = 8;
	
	public long PartOne()
	{
		int counter = 0;
		GeneratorPartOne genA = new GeneratorPartOne(GeneratorAStartingValue, GeneratorAFactor);
		GeneratorPartOne genB = new GeneratorPartOne(GeneratorBStartingValue, GeneratorBFactor);
		string resultGenAString;
		string resultGenBString;
		string compareA;
		string compareB;
		for (int i = 0; i < 40_000_000; i++)
		{
			long resultGenA = genA.GenerateNextValue();
			long resultGenB = genB.GenerateNextValue();
			resultGenAString = ConvertToBinaryString(resultGenA);
			resultGenBString = ConvertToBinaryString(resultGenB);
			compareA = resultGenAString[16..];
			compareB = resultGenBString[16..];
			if (compareA == compareB)
			{
				counter++;
			}
		}
		return counter;
	}

	public long PartTwo()
	{
		int counter = 0;
		GeneratorPartTwo genA = new GeneratorPartTwo(GeneratorAStartingValue, GeneratorAFactor, GeneratorAAcceptableCriteria);
		GeneratorPartTwo genB = new GeneratorPartTwo(GeneratorBStartingValue, GeneratorBFactor, GeneratorBAcceptableCriteria);
		for (int i = 0; i < 5_000_000; i++)
		{
			long resultGenA = genA.GenerateNextValue();
			long resultGenB = genB.GenerateNextValue();
			string resultGenAString = ConvertToBinaryString(resultGenA);
			string resultGenBString = ConvertToBinaryString(resultGenB);
			if (resultGenAString.Substring(16) == resultGenBString.Substring(16))
			{
				counter++;
			}
		}
		return counter;
	}
	
	private string ConvertToBinaryString(long number)
	{
		string s = Convert.ToString(number, 2);
		if (s.Length < 32)
		{
			string result = string.Empty;
			int amountOfExtraZeros = 32 - s.Length;
			for (int i = 0; i < amountOfExtraZeros; i++)
			{
				result += "0";
			}
			
			return result + s;
		}

		return s;
	}
}

public class GeneratorPartOne
{
	private long previousValue;
	private long factor;
	private const long Divider = 2147483647;
	
	public GeneratorPartOne(long previousValue, long factor)
	{
		this.previousValue = previousValue;
		this.factor = factor;
	}

	public long GenerateNextValue()
	{
		this.previousValue = (previousValue * factor) % Divider;
		return this.previousValue;
	}
}

public class GeneratorPartTwo
{
	private long previousValue;
	private long factor;
	private const long Divider = 2147483647;
	private int acceptableCriteria;
	
	public GeneratorPartTwo(long previousValue, long factor, int acceptableCriteria)
	{
		this.previousValue = previousValue;
		this.factor = factor;
		this.acceptableCriteria = acceptableCriteria;
	}

	public long GenerateNextValue()
	{
		this.previousValue = (previousValue * factor) % Divider;
		if (previousValue % acceptableCriteria != 0)
		{
			GenerateNextValue();
		}
		return this.previousValue;
	}
}