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
		long sum = 0;
		
		List<Part> parts = ParseInput();
		List<Part> possibleGears = parts.Where(p => p.PartSign == '*').ToList();
		for (int i = 0; i < possibleGears.Count; i++)
		{
			if(possibleGears.Count(p => possibleGears[i].PosX == p.PosX && possibleGears[i].PosY == p.PosY) == 2)
			{
				List<Part> both = possibleGears
				                  .Where(p => possibleGears[i].PosX == p.PosX && possibleGears[i].PosY == p.PosY)
				                  .ToList();
				sum += both[0].PartId * both[1].PartId;
			}
		}
		
		return sum / 2;
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
					(char sign, int posX, int posY) sign = GetPartSign((number, x - 1, y));
					if (sign.sign != '\0')
					{
						parts.Add(new Part(int.Parse(number), sign.sign, sign.posX, sign.posY));
					}
					number = string.Empty;
				}
			}
			if (number == string.Empty) continue;
			(char si, int posX, int poxY) result = GetPartSign((number, input[y].Length - 1, y)); 
			if (result.si != '\0')
			{
				parts.Add(new Part(int.Parse(number), result.si, result.posX, result.poxY));
			}
		}
		
		return parts;
	}

	private (char sign, int posX, int posY) GetPartSign((string number, int posX, int posY) data)
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
					return (input[data.posY - 1][i], i, data.posY - 1);
				}
			}

			if (data.posY < input.Length - 1)
			{
				if (IsSign(i, data.posY + 1))
				{
					return (input[data.posY + 1][i], i, data.posY + 1);
				}
			}
			
			//Wenn es die am weitesten rechts liegende Zahl ist, die nicht an der "wand" liegt:
			if (i < line.Length - 1 && !char.IsDigit(line[i + 1]))
			{
				if(IsSign(i + 1, data.posY))
				{
					return (input[data.posY][i + 1], i + 1, data.posY);
				}
				
				if (data.posY > 0)
				{ //Ist nicht in der obersten Zeile --> Prüfen
					if (IsSign(i + 1, data.posY - 1))
					{
						return (input[data.posY - 1][i + 1], i + 1, data.posY - 1);
					}
				}

				if (data.posY < input.Length - 1)
				{
					if (IsSign(i + 1, data.posY + 1))
					{
						return (input[data.posY + 1][i + 1], i + 1, data.posY + 1);
					}
				}
				
			} 
			//Zahl ist ganz außen links, außer an der "wand":
			if (i > 0 && !char.IsDigit(line[i - 1]))
			{
				if(IsSign(i - 1, data.posY))
				{
					return (input[data.posY][i - 1], i - 1, data.posY);
				}
				
				if (data.posY > 0)
				{ //Ist nicht in der obersten Zeile --> Prüfen
					if (IsSign(i - 1, data.posY - 1))
					{
						return (input[data.posY - 1][i - 1], i - 1, data.posY - 1);
					}
				}

				if (data.posY < input.Length - 1)
				{
					if (IsSign(i - 1, data.posY + 1))
					{
						return (input[data.posY + 1][i - 1], i - 1, data.posY + 1);
					}
				}
			}
			
			i--;
			if (i < 0)
			{
				break;
			}
		}

		return ((char)0x0, -1, -1);
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