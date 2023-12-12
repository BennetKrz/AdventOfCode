namespace AdventOfCode._2023;
using Position = (int x, int y);

public class Day11 : IAdventDay
{
	private readonly List<string> input;

	public Day11()
	{
		input = Helper.GetInput(11).ToList();
	}

	public long PartOne()
	{
		List<string> map = ExpandStarMap();
		Dictionary<int, Position> positionsOfStars = GetPositionsOfStars(map);
		HashSet<(int starIdOne, int starIdTwo, int distance)> sumOfDistances = GetSumOfDistances(positionsOfStars);
		return sumOfDistances.Sum(d => d.distance);
	}
	
	public long PartTwo()
	{
		/*Idee/Ansatz:
		 * In einer Liste für alle Spalten und Reihen festhalten, welche sich erweitern würden
		 * Dann für jeden Stern in der Original Position gucken, über wieviele Spalten/Reihen man gehen muss, welche erweitert werden.
		 * Dann die Anzahl der Überschrittenen Reihen oder Spalten mit 1000000 multiplizieren.
		 * Somit hat man dann den neuen abstand der Positionen
		*/
		
		/*
		 * Anderer Ansatz:
		 * Beim Ausweiten der Sterne, nicht wirklich die Leere speichern, sondern nur die Sterne und deren Position
		 * somit würde man nur die Sterne extrem weit verschieben müssen, aber nicht den ganzen leeren Raum darstellen müssen
		 */
		return 0;
	}
	
	private HashSet<(int starIdOne, int starIdTwo, int distance)> GetSumOfDistances(Dictionary<int, Position> positionsOfStars)
	{
		HashSet<(int starIdOne, int starIdTwo, int distance)> result = new();
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

	private int CalculateDistanceBetweenStars(Position posStarOne, Position posStarTwo)
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

		List<string> result = new();
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

	private void Print(List<List<char>> map)
	{
		for (int y = 0; y < map.Count; y++)
		{
			for (int x = 0; x < map[y].Count; x++)
			{
				Console.Write(map[y][x]);
			}

			Console.Write("\n");
		}
	}
}