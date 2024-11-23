using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015;

public class Day4 : AdventBase
{
	private readonly string input;

	public Day4() : base(2015, 4)
	{
		input = InputOneLine;
	}

	public override long PartOne()
	{
		for (int i = 0; i < 1_000_000; i++)
		{
			string s = input;
			s += i.ToString();
			string firstFiveCharsOfHash = GetMd5Hash(s)[..5];
			if (firstFiveCharsOfHash == "00000") return i;
		}

		return long.MinValue;
	}

	public override long PartTwo()
	{
		for (int i = 0; i < 10_000_000; i++)
		{
			string s = input;
			s += i.ToString();
			string firstSixCharsOfHash = GetMd5Hash(s)[..6];
			if (firstSixCharsOfHash == "000000") return i;
		}

		return long.MinValue;
	}

	private string GetMd5Hash(string inputToMd5)
	{
		byte[] inputBytes = Encoding.ASCII.GetBytes(inputToMd5);
		byte[] hashBytes = MD5.HashData(inputBytes);
		return Convert.ToHexString(hashBytes);
	}
}