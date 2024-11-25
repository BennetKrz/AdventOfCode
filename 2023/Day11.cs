namespace AdventOfCode._2023;
using Position = (long x, long y);

public class Day11 : AdventBase
{
	private readonly List<string> input;

	public Day11() : base(2023, 11)
	{
		input = Input.ToList();
	}

	public override long PartOne()
	{
		List<string> map = ExpandStarMap();
		Dictionary<int, Position> positionsOfStars = GetPositionsOfStars(map);
		HashSet<(int starIdOne, int starIdTwo, long distance)> distances = GetSumOfDistances(positionsOfStars);
		return distances.Sum(d => d.distance);
	}
	
	public override long PartTwo()
	{
		Dictionary<int, Position> positionOfStars = GetPositionsOfStars(input);
		positionOfStars = UpdatePositions(positionOfStars, 1_000_000 - 1);
		HashSet<(int starIdOne, int starIdTwo, long distance)> distances = GetSumOfDistances(positionOfStars);
		return distances.Sum(d => d.distance);
	}

	private Dictionary<int,Position> UpdatePositions(Dictionary<int,Position> positionOfStars, int offset)
	{
		int found = 0;
		for (int y = 0; y < input.Count; y++)
		{
			if (input[y].Any(c => c != '.')) continue;
			List<int> starsToMove = GetRequiredStarsY(positionOfStars, y + offset * found);
			foreach (int index in starsToMove)
			{
				positionOfStars[index] = (positionOfStars[index].x, positionOfStars[index].y + offset);
			}

			found++;
		}

		found = 0;
		for (int x = 0; x < input[0].Length; x++)
		{
			bool empty = true;
			foreach (string line in input)
			{
				if (line[x] != '.')
				{
					empty = false;
				}
			}

			if (!empty) continue;
			List<int> starsToMove = GetRequiredStarsX(positionOfStars, x + offset * found);
			foreach (int index in starsToMove)
			{
				positionOfStars[index] = (positionOfStars[index].x + offset, positionOfStars[index].y);
			}

			found++;
		}
		
		
		return positionOfStars;
	}
	
	private List<int> GetRequiredStarsX(Dictionary<int, Position> positionOfStars, long pos)
	{
		List<int> result = new();
		foreach (KeyValuePair<int,Position> star in positionOfStars)
		{
			if (star.Value.x > pos)
			{
				result.Add(star.Key);
			}
		}

		return result;
	}

	private List<int> GetRequiredStarsY(Dictionary<int, Position> positionOfStars, long pos)
	{
		return positionOfStars.Where(star => star.Value.y > pos)
			.Select(star => star.Key)
			.ToList();
	}

	private HashSet<(int starIdOne, int starIdTwo, long distance)> GetSumOfDistances(Dictionary<int, Position> positionsOfStars)
	{
		HashSet<(int starIdOne, int starIdTwo, long distance)> result = new();
		for (int i = 0; i < positionsOfStars.Count; i++)
		{
			for (int j = i; j < positionsOfStars.Count; j++)
			{
				if (positionsOfStars[i] == positionsOfStars[j])
				{
					continue;
				}

				result.Add((i, j, CalculateDistanceBetweenStars(positionsOfStars[i], positionsOfStars[j])));
			}
		}
		return result;
	}

	private long CalculateDistanceBetweenStars(Position posStarOne, Position posStarTwo)
	{
		return (Math.Abs(posStarOne.x - posStarTwo.x) + Math.Abs(posStarOne.y - posStarTwo.y));
	}

	private Dictionary<int, Position> GetPositionsOfStars(List<string> map)
	{
		Dictionary<int, Position> starPositions = new();
		int starCount = 0;
		for (int y = 0; y < map.Count; y++)
		{
			for (int x = 0; x < map[y].Length; x++)
			{
				if (map[y][x] != '#') continue;
				starPositions.Add(starCount, (x, y));
				starCount++;
			}
		}
		return starPositions;
	}
	
	private List<string> ExpandStarMap()
	{
		List<List<char>> resultMap = new();
		for (int x = 0; x < input[0].Length; x++)
		{
			bool allEmpty = true;
			for (int y = 0; y < input.Count; y++)
			{
				if (resultMap.Count < input.Count)
				{
					resultMap.Add(new List<char>());	
				}
				if (input[y][x] != '.') allEmpty = false;
				resultMap[y].Add(input[y][x]);
			}
			if(!allEmpty) continue;
			for (int y = 0; y < input.Count; y++)
			{
				resultMap[y].Add('.');
			}
		}
		
		for (int y = 0; y < resultMap.Count; y++)
		{
			if (resultMap[y].All(c => c == '.'))
			{
				resultMap.Insert(y + 1, resultMap[y]);
				y++;
			}
		}

		List<string> result = [];
		foreach (List<char> list in resultMap)
		{
			string line = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				line += list[i];
			}
			result.Add(line);
		}
		return result;
	}
}