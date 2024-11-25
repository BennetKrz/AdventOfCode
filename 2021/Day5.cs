using System.Text;

namespace AdventOfCode._2021;

public class Day5 : AdventBase
{
	private readonly string[] input;

	public Day5() : base(2021, 5)
	{
		input = Input;
	}

	public override long PartOne()
	{
		List<(int x1, int y1, int x2, int y2)> lines = ParseInput();
		//Create Map
		int[,] map = new int[991, 991];


		for (int i = 0; i < lines.Count; i++)
		{
			(int x1, int y1, int x2, int y2) line = lines[i];
			if (line.x1 == line.x2)
			{
				if (line.y1 > line.y2)
				{
					int yT = line.y2;
					line.y2 = line.y1;
					line.y1 = yT;
				}

				for (int y = line.y1; y <= line.y2; y++)
				{
					map[line.x1, y]++;
				}
			}

			if (line.y1 == line.y2)
			{
				if (line.x1 > line.x2)
				{
					int xT = line.x2;
					line.x2 = line.x1;
					line.x1 = xT;
				}

				for (int x = line.x1; x <= line.x2; x++)
				{
					map[x, line.y1]++;
				}
			}
		}

		//DEBUGING: MAP PRINT OUT
		string output = string.Empty;
		for (int i = 0; i < map.GetLength(0); i++)
		{
			StringBuilder sb = new();
			for (int j = 0; j < map.GetLength(1); j++)
			{
				sb.Append(map[j, i]);
			}

			output += sb + "\n";
		}
#if DEBUG
		Console.WriteLine(output);
#endif
		return map.Cast<int>().Count(x => x > 1);
	}

	public override long PartTwo()
	{
		List<(int x1, int y1, int x2, int y2)> lines = ParseInput();
		//Create Map
		int[,] map = new int[991, 991];

		for (int i = 0; i < lines.Count; i++)
		{
			(int x1, int y1, int x2, int y2) line = lines[i];
			if (line.x1 == line.x2)
			{
				if (line.y1 > line.y2)
				{
					int yT = line.y2;
					line.y2 = line.y1;
					line.y1 = yT;
				}

				for (int y = line.y1; y <= line.y2; y++)
				{
					map[line.x1, y]++;
				}
			} else if (line.y1 == line.y2)
			{
				if (line.x1 > line.x2)
				{
					int xT = line.x2;
					line.x2 = line.x1;
					line.x1 = xT;
				}

				for (int x = line.x1; x <= line.x2; x++)
				{
					map[x, line.y1]++;
				}
			} else
			{
				if (line.x1 < line.x2 && line.y1 < line.y2)
				{
					int x = line.x1;
					int y = line.y1;
					while (x <= line.x2)
					{
						map[x, y]++;
						x++;
						y++;
					}
				} else if (line.x1 > line.x2 && line.y1 > line.y2)
				{
					int x = line.x1;
					int y = line.y1;
					while (x >= line.x2)
					{
						map[x, y]++;
						x--;
						y--;
					}
				} else if (line.x1 < line.x2 && line.y1 > line.y2)
				{
					int x = line.x1;
					int y = line.y1;
					while (x <= line.x2)
					{
						map[x, y]++;
						x++;
						y--;
					}
				} else if (line.x1 > line.x2 && line.y1 < line.y2)
				{
					int x = line.x1;
					int y = line.y1;
					while (x >= line.x2)
					{
						map[x, y]++;
						x--;
						y++;
					}
				}
			}
		}

		//DEBUGING: MAP PRINT OUT
		string output = string.Empty;
		for (int i = 0; i < map.GetLength(0); i++)
		{
			StringBuilder sb = new();
			for (int j = 0; j < map.GetLength(1); j++)
			{
				sb.Append(map[j, i]);
			}

			output += sb + "\n";
		}

		//Console.WriteLine(output);
		return map.Cast<int>().Count(x => x > 1);
	}

	private List<(int x1, int y1, int x2, int y2)> ParseInput()
	{
		List<(int x1, int y1, int x2, int y2)> output = new();
		for (int i = 0; i < input.Length; i++)
		{
			string[] tmp = input[i].Split("->");
			string[] tmp2 = tmp[0].Split(",");
			string[] tmp3 = tmp[1].Split(",");
			output.Add((int.Parse(tmp2[0]), int.Parse(tmp2[1]), int.Parse(tmp3[0]), int.Parse(tmp3[1])));
		}

		return output;
	}
}