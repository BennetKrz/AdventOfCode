using System.Net.Security;
using System.Security.Cryptography;

namespace AdventOfCode._2019;

public class Day10 : IAdventDay
{
	private List<string> input = Helper.GetInput(10).ToList();
	public long PartOne()
	{
		HashSet<(int x, int y)> asteroids = ParseInput();
		for (int i = 0; i < asteroids.Count; i++)
		{
			
		}
		return 0;
	}

	public long PartTwo()
	{
		return 0;
	}

	int EvaluateAsteroids((int x, int y) position, HashSet<(int x, int y)> asteroids)
	{
		int asteroidsVisible = 0;
		
		//Nächsten Astereoiden finden. 
		
		//"Steigung" zu diesem berechnen
		
		//Dann steigung folgen
		
		//Alle in dieser linie löschen.
		
		//Dann den ausgewählten behalten und dem counter hinzufügen.
		
		//Dann wieder bei schritt 1 anfangen, bis es keine astereoiden mehr gibt
		return asteroidsVisible;
	}
	
	HashSet<(int x, int y)> ParseInput()
	{
		HashSet<(int x, int y)> asteroids = new();
		for (int y = 0; y < this.input.Count; y++)
		{
			for (int x = 0; x < this.input[y].Length; x++)
			{
				char charAt = this.input[y][x];
				if (charAt == '#')
				{
					asteroids.Add((x, y));
				}
			}
		}

		return asteroids;
	} 
}