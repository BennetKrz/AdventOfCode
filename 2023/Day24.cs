using AdventOfCode._2023;

namespace AdventOfCode._2023;

using Position = (double x, double y);

public class Day24 : AdventBase
{
	private readonly List<string> input;

	public Day24() : base(2023, 24)
	{
		this.input = Input.ToList();
	}
	
	public override long PartOne()
	{
		const long min = 200000000000000;
		const long max = 400000000000000;
		List<Vector> vectors = ParseInput();
		int counter = 0;
		for (int i = 0; i < vectors.Count - 1; i++)
		{
			for (int j = i + 1; j < vectors.Count; j++)
			{
				Position intersectionPos = DoVectorsIntersect(vectors[i], vectors[j]);
				if (intersectionPos == (double.NegativeInfinity, double.NegativeInfinity))
				{
					continue;
				}

				if (intersectionPos.x is >= min and <= max && intersectionPos.y is >= min and <= max)
				{
					counter++;
				}
				
				Console.WriteLine($"Hailstone A: {vectors[i].PosX}, {vectors[i].PosY}, {vectors[i].PosZ} @ {vectors[i].VelX}, {vectors[i].VelY}, {vectors[i].VelZ}");
				Console.WriteLine($"Hailstone A: {vectors[j].PosX}, {vectors[j].PosY}, {vectors[j].PosZ} @ {vectors[j].VelX}, {vectors[j].VelY}, {vectors[j].VelZ}");
				Console.WriteLine($"Hailstones path will cross at (x = {intersectionPos.x}, y = {intersectionPos.y}");
				
			}
		}
		return counter;
	}

	public override long PartTwo()
	{
		return 0;
	}

	private Position DoVectorsIntersect(Vector a, Vector b)
	{
		double ma = GetGradient(a);
		double mb = GetGradient(b);
		double ba = GetYOffset(a, ma);
		double bb = GetYOffset(b, mb);
		return CalculateIntersection(a, ma, ba, b, mb, bb);
	}

	private Position CalculateIntersection(Vector a, double ma, double ba, Vector b, double mb, double bb)
	{
		double intersectingX = -(ba - bb) / (ma - mb);
		double intersectingY = Math.Round(ma * intersectingX + ba, 3);
		
		if (a.VelX > 0)
		{
			if (intersectingX < a.PosX)
			{
				return (double.NegativeInfinity, double.NegativeInfinity);
			}	
		} else
		{
			if (intersectingX > a.PosX)
			{
				return (double.NegativeInfinity, double.NegativeInfinity);
			}
		}

		if (b.VelX > 0)
		{
			if (intersectingX < b.PosX)
			{
				return (double.NegativeInfinity, double.NegativeInfinity);
			}
		} else
		{
			if (intersectingX > b.PosX)
			{
				return (double.NegativeInfinity, double.NegativeInfinity);
			}
		}
		
		return (Math.Round(intersectingX, 3), intersectingY);
	}


	private double GetYOffset(Vector v, double m)
	{
		return v.PosY - m * v.PosX;
	}
	private double GetGradient(Vector v)
	{
		return ((v.PosY + v.VelY) - v.PosY) / (double)((v.PosX + v.VelX) - v.PosX);
	}

	private List<Vector> ParseInput()
	{
		return input.Select(line => line.Replace(",", "")
		                                .Replace("@", "")
		                                .Split(" ")
		                                .Select(s => s.Trim())
		                                .Where(s => s != "")
		                                .Select(s => long.Parse(s))
		                                .ToList())
		            .Select(data => new Vector(data[0], data[1], data[2], data[3], data[4], data[5]))
		            .ToList();
	}

	private record Vector(long PosX, long PosY, long PosZ, long VelX, long VelY, long VelZ);
}