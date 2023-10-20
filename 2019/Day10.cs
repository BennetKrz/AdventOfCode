using System.Net.Security;
using System.Security.Cryptography;

namespace AdventOfCode._2019;

public class Day10 : IAdventDay
{
	private List<string> input = Helper.GetInput(10).ToList();
	public long PartOne()
	{
		HashSet<(int x, int y)> asteroids = ParseInput();
		int asteroidsCount = asteroids.Count;
		Dictionary<(int x, int y), int> visibleAsteroidsPerAstroid = new();
		for (int i = 0; i < asteroidsCount; i++)
		{
			asteroids = ParseInput();
			visibleAsteroidsPerAstroid.Add(asteroids.ToArray()[i], EvaluateAsteroids(asteroids.ToArray()[i], asteroids));
		}
		return visibleAsteroidsPerAstroid.MaxBy(kvp => kvp.Value).Value;
	}

	public long PartTwo()
	{
		//Get best meteor:
		HashSet<(int x, int y)> asteroids = ParseInput();
		int asteroidsCount = asteroids.Count;
		Dictionary<(int x, int y), int> visibleAsteroidsPerAstroid = new();
		for (int i = 0; i < asteroidsCount; i++)
		{
			asteroids = ParseInput();
			visibleAsteroidsPerAstroid.Add(asteroids.ToArray()[i], EvaluateAsteroids(asteroids.ToArray()[i], asteroids));
		}
		//Get radians to every asteroid from the selected
		(int x, int y) position = visibleAsteroidsPerAstroid.MaxBy(kvp => kvp.Value).Key;
		Dictionary<(int x, int y), double> degreesToEveryAsteroid = new();
		degreesToEveryAsteroid = GetDegreesToEveryAsteroid(position);
		
		//Jetzt alle nach grad sortieren
		degreesToEveryAsteroid =
			degreesToEveryAsteroid.OrderBy(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
		//jetzt gucken, welche alle sichbar sind
		//Dann denn wählen, der dran ist wenn man sich im uhrzeiger sinn dreht
		
		return 0;
	}

	Dictionary<(int x, int y), double> GetDegreesToEveryAsteroid((int x, int y) pos)
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
			} else if (pos.y == targetPos.y)
			{
				degrees = pos.x > targetPos.x ? Math.PI + (Math.PI / 2) : Math.PI / 2;
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
				degrees = Math.Atan2(delta.dY, delta.dX) + (Math.PI / 2);
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
				degrees = Math.Atan2(delta.dY, delta.dX) + Math.PI + (Math.PI / 2);
			}
			degreesToEveryAsteroid.Add(targetPos, degrees);
		}

		return degreesToEveryAsteroid;
	}

	int EvaluateAsteroids((int x, int y) positionToCheck, HashSet<(int x, int y)> asteroids)
	{
		int asteroidsVisible = 0;

		while(asteroids.Count > 0)
		{
			//Remove asteroid to test and get next nearest
			asteroids.Remove(positionToCheck);
			(int x, int y) nearestAsteroid = FindNearestAsteroid(positionToCheck, asteroids);
			
			//Get the gradient
			(int x, int y) line = GetLine(nearestAsteroid, positionToCheck);
			
			int gcd = Math.Abs(CalculateGCD(line.x, line.y));
			line = (line.x / gcd, line.y / gcd);
			
			//go this line until out of bounds and remove every asteroid in line
			(int x, int y) currentPos = positionToCheck;
			while (currentPos.x < input[0].Length && currentPos.x >= 0 &&
			       currentPos.y < input.Count && currentPos.y >= 0)
			{
				(int x, int y) nextPos = (currentPos.x + line.x, currentPos.y + line.y);
				if (asteroids.Contains(nextPos))
				{
					asteroids.Remove(nextPos);
				}

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
	
	HashSet<(int x, int y)> ParseInput()
	{
		HashSet<(int x, int y)> asteroids = new();
		for (int y = 0; y < input.Count; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				char charAt = input[y][x];
				if (charAt == '#')
				{
					asteroids.Add((x, y));
				}
			}
		}

		return asteroids;
	} 
	
	private int CalculateGCD(int a, int b)
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