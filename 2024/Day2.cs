namespace AdventOfCode._2024;

public class Day2 : AdventBase
{
    private readonly string[] input;

    public Day2() : base(2024, 2)
    {
        this.input = Input;
    }

    public override long PartOne()
    {
        return input.Select(t => t.Split(" ").Select(int.Parse).ToList()).LongCount(ValidTestReport);
    }

    public override long PartTwo()
    {
        long safeReports = 0;
        
        for (int i = 0; i < input.Length; i++)
        {
            bool oneBadValue = false;
            
            List<int> testReport = input[i].Split(" ").Select(int.Parse).ToList();
            for (int j = 0; j < testReport.Count; j++)
            {
                List<int> removed = testReport.Take(j).Concat(testReport.Skip(j + 1)).ToList();
                if (ValidTestReport(removed))
                {
                    oneBadValue = true;
                    break;
                }
            }

            if (ValidTestReport(testReport) || oneBadValue)
            {
                safeReports++;
            }
        }
        
        return safeReports;
    }

    private bool ValidTestReport(List<int> testReport)
    {
        List<int> diffs = [];

        for (int i = 1; i < testReport.Count; i++)
        {
            diffs.Add(testReport[i] - testReport[i - 1]);
        }

        bool increasing = diffs.All(d => d is >= 1 and <= 3);
        bool decreasing = diffs.All(d => d is <= -1 and >= -3);
        
        return increasing || decreasing;
    }

}