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
		long resultGenA;
		long resultGenB;
		for (int i = 0; i < 40_000_000; i++)
		{
			resultGenA = genA.GenerateNextValue();
			resultGenB = genB.GenerateNextValue();
			//Schnellster Weg die ersten 16 bit zu bekommen.
			if((resultGenA & 0xFFFF) == (resultGenB & 0xFFFF))
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
		long resultGenB;
		long resultGenA;
		for (int i = 0; i < 5_000_000; i++)
		{
			resultGenB = genA.GenerateNextValue();
			resultGenA = genB.GenerateNextValue();
			//Schnellster Weg die ersten 16 bit zu bekommen.
			if ((resultGenA & 0xFFFF) == (resultGenB & 0xFFFF))
			{
				counter++;
			}
		}
		return counter;
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