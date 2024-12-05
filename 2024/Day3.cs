using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day3 : AdventBase
{
    private readonly string[] input;
    
    public Day3() : base(2024, 3)
    {
        input = Input;
    }

    public override long PartOne()
    {
        Regex r = new("mul\\(\\d{1,3},\\d{1,3}\\)", RegexOptions.Compiled);
        
        long sum = 0;
        foreach (string line in input)
        {
            MatchCollection matches = r.Matches(line);
            sum += matches.Select(m =>
            {
                string value = m.Value.Replace("mul(", "").Replace(")", "");
                return value;
            }).Sum(s => int.Parse(s.Split(",")[0]) * int.Parse(s.Split(",")[1]));
        }
        
        return sum;
    }

    public override long PartTwo()
    {
        Regex r = new("mul\\(\\d{1,3},\\d{1,3}\\)|do\\(\\)|don't\\(\\)", RegexOptions.Compiled);
        
        long sum = 0;
        bool mulEnabled = true;
        foreach (string line in input)
        {
            MatchCollection matches = r.Matches(line);
            foreach (Match match in matches)
            {

                if (match.Value.Contains("mul") && mulEnabled)
                {
                    string value = match.Value.Replace("mul(", "").Replace(")", "");
                    sum += int.Parse(value.Split(",")[0]) * int.Parse(value.Split(",")[1]);
                } else if (match.Value == "do()")
                {
                    mulEnabled = true;
                }
                else
                {
                    mulEnabled = false;
                }
                
            }
        }
        return sum;
    }
}