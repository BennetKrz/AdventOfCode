namespace AdventOfCode._2020;

public class Day2 : AdventBase
{
	private readonly string[] input;

	public Day2() : base(2020, 2)
	{
		input = Input;
	}

	public override long PartOne()
	{
		string[] localInput = new string[this.input.Length];
		this.input.CopyTo(localInput, 0);
		int[] lowest = new int[localInput.Length];
		int[] highest = new int[localInput.Length];
		string[] c = new string[localInput.Length];
		string[] passwords = new string[localInput.Length];
		int validPasswords = 0;
		for (int i = 0; i < localInput.Length; i++)
		{
			//seperate by '-'
			string s = localInput[i];
			int min = Convert.ToInt32(s.Substring(0, s.IndexOf('-')));
			s = s.Remove(0, s.IndexOf('-') + 1);
			int max = Convert.ToInt32(s.Substring(0, s.IndexOf(' ')));
			s = s.Remove(0, s.IndexOf(' ') + 1);
			lowest[i] = min;
			highest[i] = max;
			localInput[i] = s;
		}

		for (int i = 0; i < localInput.Length; i++)
		{
			//get the gesuchten char
			string s = localInput[i];
			c[i] = s.Substring(0, s.IndexOf(':'));
			s = s.Remove(0, s.IndexOf(':') + 2);
			localInput[i] = s;
		}

		for (int i = 0; i < localInput.Length; i++)
		{
			//Get all Passwords
			string s = localInput[i];
			passwords[i] = s;
		}

		//Jetzt überprüfen
		for (int i = 0; i < localInput.Length; i++)
		{
			string password = passwords[i];
			string gesuchterChar = c[i];
			char ch = Convert.ToChar(gesuchterChar);
			int freq = password.Count(f => f == ch);
			if (freq <= highest[i] && freq >= lowest[i]) validPasswords++;
		}

		return validPasswords;
	}

	public override long PartTwo()
	{
		string[] localInput = new string[this.input.Length];
		this.input.CopyTo(localInput, 0);
		int[] minPos = new int[localInput.Length];
		int[] maxPos = new int[localInput.Length];
		string[] c = new string[localInput.Length];
		string[] passwords = new string[localInput.Length];
		int validPasswords = 0;
		for (int i = 0; i < localInput.Length; i++)
		{
			//seperate by '-'
			string s = localInput[i];
			int min = Convert.ToInt32(s.Substring(0, s.IndexOf('-')));
			s = s.Remove(0, s.IndexOf('-') + 1);
			int max = Convert.ToInt32(s.Substring(0, s.IndexOf(' ')));
			s = s.Remove(0, s.IndexOf(' ') + 1);
			minPos[i] = min;
			maxPos[i] = max;
			localInput[i] = s;
		}

		for (int i = 0; i < localInput.Length; i++)
		{
			//get the gesuchten char
			string s = localInput[i];
			c[i] = s.Substring(0, s.IndexOf(':'));
			s = s.Remove(0, s.IndexOf(':') + 2);
			localInput[i] = s;
		}

		for (int i = 0; i < localInput.Length; i++)
		{
			//Get all Passwords
			string s = localInput[i];
			passwords[i] = s;
		}

		//Jetzt überprüfen
		for (int i = 0; i < localInput.Length; i++)
		{
			string password = passwords[i];
			string gesuchterChar = c[i];
			char ch = Convert.ToChar(gesuchterChar);
			if (password[minPos[i] - 1] == ch || password[maxPos[i] - 1] == ch)
				if ((password[minPos[i] - 1] == ch && password[maxPos[i] - 1] == ch) == false)
					validPasswords++;
		}

		return validPasswords;
	}
}