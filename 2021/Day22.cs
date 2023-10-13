namespace AdventOfCode._2021;

public class Day22 : IAdventDay
{
	private readonly string[] input;

	public Day22()
	{
		input = Helper.GetInput(22);
	}

	public long PartOne()
	{
		Dictionary<(int x, int y, int z), bool> reactor = new();
		List<(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)> cubes = ParseInput();
		for (int i = 0; i < cubes.Count; i++)
		{
			(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn) cube = cubes[i];
			if (cube.x1 > 50 || cube.x1 < -50) continue;

			for (int x = cube.x1; x <= cube.x2; x++)
			{
				for (int y = cube.y1; y <= cube.y2; y++)
				{
					for (int z = cube.z1; z <= cube.z2; z++)
					{
						if (!reactor.ContainsKey((x, y, z)))
							reactor.Add((x, y, z), cube.turnOn);
						else
							reactor[(x, y, z)] = cube.turnOn;
					}
				}
			}
		}

		return reactor.Values.Where(x => x).Count();
	}

	public long PartTwo()
	{
		List<(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)> resultCubes = new();
		List<(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)> cubes = ParseInput();
		for (int i = 0; i < cubes.Count; i++)
		{
			List<(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)> cubesToAdd = new();
			if (cubes[i].turnOn) cubesToAdd.Add(cubes[i]);

			for (int j = 0; j < resultCubes.Count; j++)
			{
				//Wenn würfel an ist, dann alle wo doppelt an ist entfernen und wo off ist
				//Wenn würfel aus, dann doppelt aus gezählte und ans entfernen
				(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)? intersectingCube =
					DoCubesIntersect(cubes[i], resultCubes[j], !resultCubes[j].turnOn);
				if (intersectingCube != null) cubesToAdd.Add(intersectingCube.Value);
			}

			resultCubes.AddRange(cubesToAdd);
		}

		return resultCubes.Aggregate(0L, (totalVolumen, c) => totalVolumen + Volumen(c) * (c.turnOn ? 1 : -1));
	}

	private long Volumen((int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn) c)
	{
		return (c.x2 - c.x1 + 1L) * (c.y2 - c.y1 + 1L) * (c.z2 - c.z1 + 1L);
	}

	private (int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)? DoCubesIntersect(
		(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn) a,
		(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn) b, bool turnOn)
	{
		if (a.x1 > b.x2 || a.x2 < b.x1 || a.y1 > b.y2 || a.y2 < b.y1 || a.z1 > b.z2 || a.z2 < b.z1) return null;

		(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn) cube = new();
		cube.x1 = Math.Max(a.x1, b.x1);
		cube.y1 = Math.Max(a.y1, b.y1);
		cube.z1 = Math.Max(a.z1, b.z1);
		cube.x2 = Math.Min(a.x2, b.x2);
		cube.y2 = Math.Min(a.y2, b.y2);
		cube.z2 = Math.Min(a.z2, b.z2);
		cube.turnOn = turnOn;
		return cube;
	}

	private List<(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)> ParseInput()
	{
		List<(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn)> cubes = new();
		for (int i = 0; i < input.Length; i++)
		{
			string line = input[i];
			(int x1, int y1, int z1, int x2, int y2, int z2, bool turnOn) cube = new();
			if (line[1] == 'f')
			{
				cube.turnOn = false;
				line.Remove(0, 3);
			} else
			{
				cube.turnOn = true;
				line.Remove(0, 2);
			}

			string[] raw = line.Split(',');
			cube.x1 = int.Parse(raw[0].Split("..")[0].Substring(raw[0].IndexOf('=') + 1,
			                                                    raw[0].IndexOf("..") - (raw[0].IndexOf('=') + 1)));
			cube.y1 = int.Parse(raw[1].Split("..")[0].Substring(raw[1].IndexOf('=') + 1,
			                                                    raw[1].IndexOf("..") - (raw[1].IndexOf('=') + 1)));
			cube.z1 = int.Parse(raw[2].Split("..")[0].Substring(raw[2].IndexOf('=') + 1,
			                                                    raw[2].IndexOf("..") - (raw[2].IndexOf('=') + 1)));
			cube.x2 = int.Parse(raw[0].Split("..")[1]);
			cube.y2 = int.Parse(raw[1].Split("..")[1]);
			cube.z2 = int.Parse(raw[2].Split("..")[1]);
			cubes.Add(cube);
		}

		return cubes;
	}
}