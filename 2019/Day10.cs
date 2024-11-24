namespace AdventOfCode._2019;

public class Day10 : AdventBase
{
	private readonly List<string> input;
	private (int x, int y) bestPosition = (-1, -1);

	public Day10() : base(2019, 10)
	{
		input = Input.ToList();
	}

	public override long PartOne()
	{
		HashSet<(int x, int y)> asteroids = ParseInput();
		int asteroidsCount = asteroids.Count;
		Dictionary<(int x, int y), int> visibleAsteroidsPerAstroid = new();
		for (int i = 0; i < asteroidsCount; i++)
		{
			asteroids = ParseInput();
			visibleAsteroidsPerAstroid.Add(asteroids.ToArray()[i],
			                               EvaluateAsteroids(asteroids.ToArray()[i], asteroids));
		}

		bestPosition = visibleAsteroidsPerAstroid.MaxBy(kvp => kvp.Value).Key;
		return visibleAsteroidsPerAstroid.MaxBy(kvp => kvp.Value).Value;
	}

	public override long PartTwo()
	{
		(int x, int y) position;
		Dictionary<int, (int x, int y)> destroyedIndex = new();
		if (bestPosition == (-1, -1))
		{
			//Get best meteor:
			HashSet<(int x, int y)> asteroids = ParseInput();
			int asteroidsCount = asteroids.Count;
			Dictionary<(int x, int y), int> visibleAsteroidsPerAstroid = new();
			for (int i = 0; i < asteroidsCount; i++)
			{
				asteroids = ParseInput();
				visibleAsteroidsPerAstroid.Add(asteroids.ToArray()[i],
				                               EvaluateAsteroids(asteroids.ToArray()[i], asteroids));
			}
			bestPosition = visibleAsteroidsPerAstroid.MaxBy(kvp => kvp.Value).Key;
		}

		position = bestPosition;
		//Get radians to every asteroid from the selected

		Dictionary<(int x, int y), double> degreesToEveryAsteroid = GetDegreesToEveryAsteroid(position);

		//Jetzt alle nach grad sortieren
		degreesToEveryAsteroid =
			degreesToEveryAsteroid.OrderBy(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
		//Nach Grad abarbeiten
		//wenn sichbar, dann löschen, dann weiter rotieren
		int internalCounter = 0;
		int asteroidDestroyedCounter = 0;
		while (degreesToEveryAsteroid.Count > 0)
		{
			(int x, int y) asteroidToCheck = degreesToEveryAsteroid.Keys.ToArray()[internalCounter];
			//Check if asteroid is visible
			if (AsteroidIsVisible(position, asteroidToCheck, degreesToEveryAsteroid.Keys.ToHashSet()))
			{
				if (degreesToEveryAsteroid[asteroidToCheck] ==
				    degreesToEveryAsteroid.Values.ToArray()[
					    internalCounter + 1 >= degreesToEveryAsteroid.Count ? 0 : internalCounter + 1])
					internalCounter++;
				
				destroyedIndex.Add(asteroidDestroyedCounter + 1, asteroidToCheck);
				degreesToEveryAsteroid.Remove(asteroidToCheck);
				asteroidDestroyedCounter++;
				if (internalCounter >= degreesToEveryAsteroid.Count) internalCounter = 0;
				continue;
			}

			internalCounter++;
			if (internalCounter >= degreesToEveryAsteroid.Count) internalCounter = 0;
		}

		return destroyedIndex[200].x * 100 + destroyedIndex[200].y;
	}

	private bool AsteroidIsVisible((int x, int y) pos, (int x, int y) targetPos, HashSet<(int x, int y)> asteroids)
	{
		(int x, int y) line = GetLine(targetPos, pos);
		int gcd = Math.Abs(CalculateGcd(line.x, line.y));
		line = (line.x / gcd, line.y / gcd);
		(int x, int y) currentPos = pos;
		while (currentPos.x < input[0].Length && currentPos.x >= 0 &&
		       currentPos.y < input.Count && currentPos.y >= 0)
		{
			(int x, int y) nextPos = (currentPos.x + line.x, currentPos.y + line.y);
			if (asteroids.Contains(nextPos) && nextPos != targetPos)
				return false;
			if (asteroids.Contains(nextPos) && nextPos == targetPos) return true;
			currentPos = nextPos;
		}

		return true;
	}

	private Dictionary<(int x, int y), double> GetDegreesToEveryAsteroid((int x, int y) pos)
	{
		Dictionary<(int x, int y), double> degreesToEveryAsteroid = new();
		HashSet<(int x, int y)> asteroids = ParseInput();
		asteroids.Remove(pos);
		for (int i = 0; i < asteroids.Count; i++)
		{
			double degrees = 0;
			(double dX, double dY) delta;
			(int x, int y) targetPos = asteroids.ToArray()[i];
			if (pos.x == targetPos.x)
			{
				degrees = pos.y > targetPos.y ? 0 : Math.PI;
				degreesToEveryAsteroid.Add(targetPos, degrees);
				continue;
			}

			if (pos.y == targetPos.y)
			{
				degrees = pos.x > targetPos.x ? Math.PI + Math.PI / 2 : Math.PI / 2;
				degreesToEveryAsteroid.Add(targetPos, degrees);
				continue;
			}

			//Fall 1
			if (targetPos.x > pos.x && targetPos.y < pos.y)
			{
				delta = (targetPos.x - pos.x, Math.Abs(targetPos.y - pos.y));
				degrees = Math.Atan2(delta.dX, delta.dY);
			}
			//Fall 2
			else if (targetPos.x > pos.x && targetPos.y > pos.y)
			{
				delta = (targetPos.x - pos.x, targetPos.y - pos.y);
				degrees = Math.Atan2(delta.dY, delta.dX) + Math.PI / 2;
			}
			//Fall 3
			else if (targetPos.x < pos.x && targetPos.y > pos.y)
			{
				delta = (Math.Abs(targetPos.x - pos.x), targetPos.y - pos.y);
				degrees = Math.Atan2(delta.dX, delta.dY) + Math.PI;
			}
			//Fall 4
			else if (targetPos.x < pos.x && targetPos.y < pos.y)
			{
				delta = (Math.Abs(targetPos.x - pos.x), Math.Abs(targetPos.y - pos.y));
				degrees = Math.Atan2(delta.dY, delta.dX) + Math.PI + Math.PI / 2;
			}

			degreesToEveryAsteroid.Add(targetPos, degrees);
		}

		return degreesToEveryAsteroid;
	}

	private int EvaluateAsteroids((int x, int y) positionToCheck, HashSet<(int x, int y)> asteroids)
	{
		int asteroidsVisible = 0;

		while (asteroids.Count > 0)
		{
			//Remove asteroid to test and get next nearest
			asteroids.Remove(positionToCheck);
			(int x, int y) nearestAsteroid = FindNearestAsteroid(positionToCheck, asteroids);

			//Get the gradient
			(int x, int y) line = GetLine(nearestAsteroid, positionToCheck);

			int gcd = Math.Abs(CalculateGcd(line.x, line.y));
			line = (line.x / gcd, line.y / gcd);

			//go this line until out of bounds and remove every asteroid in line
			(int x, int y) currentPos = positionToCheck;
			while (currentPos.x < input[0].Length && currentPos.x >= 0 &&
			       currentPos.y < input.Count && currentPos.y >= 0)
			{
				(int x, int y) nextPos = (currentPos.x + line.x, currentPos.y + line.y);
				if (asteroids.Contains(nextPos)) asteroids.Remove(nextPos);
				currentPos = nextPos;
			}

			asteroidsVisible++;
		}

		return asteroidsVisible;
	}

	private (int x, int y) GetLine((int x, int y) posOne, (int x, int y) posTwo)
	{
		return (posOne.x - posTwo.x, posOne.y - posTwo.y);
	}

	private (int x, int y) FindNearestAsteroid((int x, int y) startingPos, HashSet<(int x, int y)> asteroids)
	{
		Dictionary<(int x, int y), double> distanceToEachAsteroid = new();
		for (int i = 0; i < asteroids.Count; i++)
		{
			(int x, int y) posTwo = asteroids.ToArray()[i];
			distanceToEachAsteroid.Add(posTwo, CalculateDistanceBetweenAsteroids(startingPos, posTwo));
		}

		return distanceToEachAsteroid.MinBy(kvp => kvp.Value).Key;
	}

	private double CalculateDistanceBetweenAsteroids((int x, int y) posOne, (int x, int y) posTwo)
	{
		return Math.Sqrt(Math.Pow(Math.Abs(posOne.x - posTwo.x), 2) + Math.Pow(Math.Abs(posOne.y - posTwo.y), 2));
	}

	private HashSet<(int x, int y)> ParseInput()
	{
		HashSet<(int x, int y)> asteroids = new();
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				char charAt = input[y][x];
				if (charAt == '#') asteroids.Add((x, y));
			}
		}

		return asteroids;
	}

	private static int CalculateGcd(int a, int b)
	{
		while (b != 0)
		{
			int temp = b;
			b = a % b;
			a = temp;
		}

		return a;
	}
}