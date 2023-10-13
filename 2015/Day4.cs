using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015;

public class Day4 : IAdventDay
{
	private const string input = "ckczppom";

	public long PartOne()
	{
		for (int i = 0; i < 1_000_000; i++)
		{
			string s = input;
			s += i.ToString();
			string firstFiveCharsOfHash = getMD5Hash(s).Substring(0, 5);
			if (firstFiveCharsOfHash == "00000") return i;
		}

		return long.MinValue;
	}

	public long PartTwo()
	{
		for (int i = 0; i < 10_000_000; i++)
		{
			string s = input;
			s += i.ToString();
			string firstSixCharsOfHash = getMD5Hash(s).Substring(0, 6);
			if (firstSixCharsOfHash == "000000") return i;
		}

		return long.MinValue;
	}

	private string getMD5Hash(string input)
	{
		byte[] inputBytes = Encoding.ASCII.GetBytes(input);
		byte[] hashBytes = MD5.HashData(inputBytes);
		return Convert.ToHexString(hashBytes);
	}
}