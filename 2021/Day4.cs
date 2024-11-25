namespace AdventOfCode._2021;

public class Day4 : AdventBase
{
	private readonly string[] input;

	private int[] numbers;

	public Day4() : base(2021, 4)
	{
		input = Input;
	}

	public override long PartOne()
	{
		List<int[][]> boards = ParseInput();
		for (int i = 0; i < numbers.Length; i++)
		{
			for (int n = 0; n < boards.Count; n++)
			{
				//Gucken, ob das Board, die entsprechende Zahl enthält:
				for (int j = 0; j < 5; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						if (boards[n][j][k] == numbers[i]) boards[n][j][k] = -1;
					}
				}
			}

			for (int board = 0; board < boards.Count; board++)
			{
				//Check for Win:
				//Horizontal:
				for (int j = 0; j < 5; j++)
				{
					bool rowIsWin = true;
					for (int k = 0; k < 5; k++)
					{
						if (boards[board][j][k] != -1) rowIsWin = false;
					}

					if (rowIsWin) return CalculateScore(boards[board], numbers[i]);
				}

				for (int j = 0; j < 5; j++)
				{
					bool rowIsWin = true;
					for (int k = 0; k < 5; k++)
					{
						if (boards[board][k][j] != -1) rowIsWin = false;
					}

					if (rowIsWin) return CalculateScore(boards[board], numbers[i]);
				}
			}
		}

		return 0;
	}

	public override long PartTwo()
	{
		List<int> allScores = new();
		List<int[][]> boards = ParseInput();
		for (int i = 0; i < numbers.Length; i++)
		{
			for (int n = 0; n < boards.Count; n++)
			{
				//Gucken, ob das Board, die entsprechende Zahl enthält:
				for (int j = 0; j < 5; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						if (boards[n][j][k] == numbers[i]) boards[n][j][k] = -1;
					}
				}
			}

			for (int board = 0; board < boards.Count; board++)
			{
				//Check for Win:
				//Horizontal:
				bool win = false;
				for (int j = 0; j < 5; j++)
				{
					bool rowIsWin = true;
					for (int k = 0; k < 5; k++)
					{
						if (boards[board][j][k] != -1) rowIsWin = false;
					}

					if (rowIsWin)
					{
						allScores.Add(CalculateScore(boards[board], numbers[i]));
						boards.RemoveAt(board);
						win = true;
						break;
					}
				}

				if (win) continue;
				for (int j = 0; j < 5; j++)
				{
					bool rowIsWin = true;
					for (int k = 0; k < 5; k++)
					{
						if (boards[board][k][j] != -1) rowIsWin = false;
					}

					if (rowIsWin)
					{
						allScores.Add(CalculateScore(boards[board], numbers[i]));
						boards.RemoveAt(board);
						win = true;
						break;
					}
				}

				if (win) continue;
			}
		}

		return allScores[^1];
	}

	private int CalculateScore(int[][] board, int number)
	{
		int score = 0;
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (board[i][j] != -1) score += board[i][j];
			}
		}

		return score * number;
	}


	private List<int[][]> ParseInput()
	{
		List<int[][]> returnValue = new();

		//get numbers
		string numbers = input[0];
		string[] tmp = numbers.Split(',');
		int[] tmp2 = new int[tmp.Length];
		for (int i = 0; i < tmp.Length; i++)
		{
			int.TryParse(tmp[i], out tmp2[i]);
		}

		this.numbers = tmp2;
		//Get Boards:

		List<string> nummern = new();
		string board = string.Empty;
		for (int i = 0; i < input.Length; i++)
		{
			if (i is 0 or 1) continue;
			if (input[i] == string.Empty)
			{
				nummern.Add(board);
				board = string.Empty;
				continue;
			}

			board += input[i] + " ";
			if (i == input.Length - 1) nummern.Add(board);
		}

		//Jetzt jedes board in ein 2d array einsortieren
		for (int i = 0; i < nummern.Count; i++)
		{
			List<string> test = nummern[i].Replace("  ", " ").Split(" ").ToList();
			if (test.Count != 25)
			{
				if (test[0] == "") test.RemoveAt(0);
				if (test[25] == "") test.RemoveAt(25);
			}

			int[][] fertigesBoard = new int[5][];
			int counter = -1;
			int counter2 = 0;
			for (int j = 0; j < 25; j++)
			{
				if (j % 5 == 0)
				{
					counter++;
					fertigesBoard[counter] = new int[5];
				}

				fertigesBoard[counter][counter2] = Convert.ToInt32(test[j]);
				counter2++;
				if (counter2 == 5) counter2 = 0;
			}

			returnValue.Add(fertigesBoard);
		}

		return returnValue;
	}
}