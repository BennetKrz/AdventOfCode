using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day9 : AdventBase
{
	List<string> input;

	public Day9() : base(2023, 9)
	{
		this.input = Input.ToList();
	}

	public override long PartOne()
	{
		List<Sequence> sequences = ParseInputPartOne();
		for (int i = 0; i < sequences.Count; i++)
		{
			//Generate Differences
			while (!LastSequenceDiffIsZero(sequences[i]))
			{
				sequences[i] = GenerateNextSequenceDifferences(sequences[i]);
			}
			//Extrapolating Data:
			sequences[i] = ExtrapolateData(sequences[i], sequences[i].Values.Count - 1);
		}
		return sequences.Sum(s => s.Values[0][^1]);
	}
	
	public override long PartTwo()
	{                                                               
		List<Sequence> sequences = ParseInputPartTwo();
		for (int i = 0; i < sequences.Count; i++)
		{
			//Generate Differences
			while (!LastSequenceDiffIsZero(sequences[i]))
			{
				sequences[i] = GenerateNextSequenceDifferences(sequences[i]);
			}
			//Extrapolating Data:
			sequences[i] = ExtrapolateData(sequences[i], sequences[i].Values.Count - 1);
		}
		return sequences.Sum(s => s.Values[0][^1]);
	}
	
	private Sequence ExtrapolateData(Sequence sequence, int depth)
	{
		if (depth == 0)
		{
			return sequence;
		}

		List<int> currentRow = sequence.Values[depth];
		List<int> rowAbove = sequence.Values[depth - 1];
		
		//Extraplate
		int curEnd = currentRow[^1];
		int nextEnd = curEnd + rowAbove[^1];
		currentRow.Add(0);
		rowAbove.Add(nextEnd);
		return ExtrapolateData(sequence, depth - 1);
	}
	
	private Sequence GenerateNextSequenceDifferences(Sequence sequence)
	{
		List<int> data = sequence.Values[^1];
		List<int> nextRow = new();
		for (int i = 0; i < data.Count - 1; i++)
		{
			nextRow.Add(data[i + 1] - data[i]);
		}
		sequence.Values.Add(nextRow);
		return sequence;
	}
	
	private bool LastSequenceDiffIsZero(Sequence sequence)
	{
		return sequence.Values[^1].All(n => n == 0);
	}
	
	private List<Sequence> ParseInputPartOne()
	{
		List<Sequence> sequences = new();
		foreach (string line in input)
		{
			List<int> values = line
			                   .Split(" ")
			                   .Select(int.Parse)
			                   .ToList();
			List<List<int>> data =
			[
				values
			];
			sequences.Add(new Sequence(data));
		}

		return sequences;
	}
	
	private List<Sequence> ParseInputPartTwo()
	{
		List<Sequence> sequences = new();
		foreach (string line in input)
		{
			List<int> values = line
			                   .Split(" ")
			                   .Select(int.Parse)
			                   .Reverse()
			                   .ToList();
			List<List<int>> data =
			[
				values
			];
			sequences.Add(new Sequence(data));
		}

		return sequences;
	}

	private record Sequence(List<List<int>> Values);
}