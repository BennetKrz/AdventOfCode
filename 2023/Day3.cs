using AdventOfCode._2023;

namespace AdventOfCode._2023;

public class Day3 : IAdventDay
{
	char[][] input;

	public Day3()
	{
		this.input = Helper.GetInput(3).Select(s => s.Select(c => c).ToArray()).ToArray();
	}

	public long PartOne()
	{
		List<Part> parts = ParseInput();
		return parts.Select(p => p.PartId).Sum();
	}

	public long PartTwo()
	{
		return 0;
	}

	private List<Part> ParseInput()
	{
		List<Part> parts = new();
		
		for (int y = 0; y < input.Length; y++)
		{
			
			string number = string.Empty;
			
			for (int x = 0; x < input[y].Length; x++)
			{
				char cur = input[y][x];
				if (char.IsDigit(cur))
				{
					number += cur;
				} else
				{
					if (number == string.Empty) continue;
					char sign = GetPartSign((number, x - 1, y));
					if (sign != '\0')
					{
						parts.Add(new Part(int.Parse(number), sign, x - 1, y));
					}
					number = string.Empty;
				}
			}
			if (number == string.Empty) continue;
			if (GetPartSign((number, input[y].Length - 1, y)) != '\0')
			{
				parts.Add(new Part(int.Parse(number), GetPartSign((number, input[y].Length - 1, y)), input[y].Length - 1, y));
			}
		}
		
		return parts;
	}

	private char GetPartSign((string number, int posX, int posY) data)
	{
		char sign;
		string line = GetStringFromArray(data.posY);
		int i = data.posX;
		while (char.IsDigit(line[i]))
		{
			if (data.posY > 0)
			{ //Ist nicht in der obersten Zeile --> Prüfen
				if (IsSign(i, data.posY - 1))
				{
					return input[data.posY - 1][i];
				}
			}

			if (data.posY < input.Length - 1)
			{
				if (IsSign(i, data.posY + 1))
				{
					return input[data.posY + 1][i];
				}
			}
			
			//Wenn es die am weitesten rechts liegende Zahl ist, die nicht an der "wand" liegt:
			if (i < line.Length - 1 && !char.IsDigit(line[i + 1]))
			{
				if(IsSign(i + 1, data.posY))
				{
					return input[data.posY][i + 1];
				}
				
				if (data.posY > 0)
				{ //Ist nicht in der obersten Zeile --> Prüfen
					if (IsSign(i + 1, data.posY - 1))
					{
						return input[data.posY - 1][i + 1];
					}
				}

				if (data.posY < input.Length - 1)
				{
					if (IsSign(i + 1, data.posY + 1))
					{
						return input[data.posY + 1][i + 1];
					}
				}
				
			} 
			//Zahl ist ganz außen links, außer an der "wand":
			if (i > 0 && !char.IsDigit(line[i - 1]))
			{
				if(IsSign(i - 1, data.posY))
				{
					return input[data.posY][i - 1];
				}
				
				if (data.posY > 0)
				{ //Ist nicht in der obersten Zeile --> Prüfen
					if (IsSign(i - 1, data.posY - 1))
					{
						return input[data.posY - 1][i - 1];
					}
				}

				if (data.posY < input.Length - 1)
				{
					if (IsSign(i - 1, data.posY + 1))
					{
						return input[data.posY + 1][i - 1];
					}
				}
			}
			
			i--;
			if (i < 0)
			{
				break;
			}
		}

		return (char)0x0;
	}

	private string GetStringFromArray(int dataPosY)
	{
		string result = string.Empty;
		foreach (char c in input[dataPosY])
		{
			result += c;
		}

		return result;
	}

	private bool IsSign(int x, int y)
	{
		return !char.IsDigit(input[y][x]) && input[y][x] != '.';
	}

	private record Part(int PartId, char PartSign, int PosX, int PosY);
}