using System.Diagnostics;

namespace AdventOfCode._2023;

public class Day1 : IAdventDay
{
    private List<string> input;

    private List<string> numbers = new List<string>()
        { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    public Day1()
    {
        input = Helper.GetInput(1).ToList();
    }

    public long PartOne()
    {
        return 0;
        long sum = 0;
        foreach (string line in input)
        {
            sum += CombineTwoNumbers(line);
        }
        return sum;
    }

    public long PartTwo()
    {
        
        ReplaceNumbers();
        long sum = 0;
        foreach (string line in input)
        {
            int result = CombineTwoNumbers(line);
            sum += result;
        }
        return sum;
    }

    private int CombineTwoNumbers(string line)
    {
        string numberOne = string.Empty;
        string numberTwo = string.Empty;
        //Get first Number;
        for (int i = 0; i < line.Length; i++)
        {
            if (Char.IsDigit(line[i]))
            {
                numberOne = line[i].ToString();
                break;
            }
        }
        //Get second digit;
        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (Char.IsDigit(line[i]))
            {
                numberTwo = line[i].ToString();
                break;
            }
        }

        return int.Parse(numberOne + numberTwo);
    }

    private void ReplaceNumbers()
    {
        List<string> result = new();
        for (int i = 0; i < input.Count; i++)
        {
            string line = input[i];
            //Erste Zahl ersetzen
            for (int j = 0; j < line.Length - 5; j++)
            {
                string part = line.Substring(j, 5);
                if(numbers.Any(s => part.Contains(s)))
                {
                    part = part.Replace("one", "1");
                    part = part.Replace("two", "2");
                    part = part.Replace("three", "3");
                    part = part.Replace("four", "4");
                    part = part.Replace("five", "5");
                    part = part.Replace("six", "6");
                    part = part.Replace("seven", "7");
                    part = part.Replace("eight", "8");
                    part = part.Replace("nine", "9");
                    line = line.Substring(0, j) + part + line.Substring(j + 5);
                    break;
                }
            }
            //Zweite Zahl ersetzen:
            for (int j = line.Length - 5; j >= 0; j--)
            {
                string part = line.Substring(j, 5);
                if(numbers.Any(s => part.Contains(s)))
                {
                    part = part.Replace("one", "1");
                    part = part.Replace("two", "2");
                    part = part.Replace("three", "3");
                    part = part.Replace("four", "4");
                    part = part.Replace("five", "5");
                    part = part.Replace("six", "6");
                    part = part.Replace("seven", "7");
                    part = part.Replace("eight", "8");
                    part = part.Replace("nine", "9");
                    line = line.Substring(0, j) + part + line.Substring(j + 5);
                    break;
                }
            }
            
            result.Add(line);
        }
        input = result;
    }
}