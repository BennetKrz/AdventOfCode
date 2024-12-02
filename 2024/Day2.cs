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
            List<int> testReport = input[i].Split(" ").Select(int.Parse).ToList();
            for (int j = 0; j < testReport.Count; j++)
            {
                List<int> localReport = new(testReport);
                localReport.Remove(j);
                if (ValidTestReport(localReport))
                {
                    safeReports++;
                    break;
                }
            }
        }
        
        return safeReports;
    }

    private bool ValidTestReport(List<int> testReport)
    {
        if (testReport[0] == testReport[1])
        {
            return false;
        }
        
        bool isIncreasing = testReport[0] < testReport[1];

        for (int i = 0; i < testReport.Count - 1; i++)
        {
            if (testReport[i] == testReport[i + 1])
            {
                return false;
            }

            if ((isIncreasing && testReport[i] > testReport[i + 1]) || (!isIncreasing && testReport[i] < testReport[i + 1]))
            {
                return false;
            }

            if (Math.Abs(testReport[i] - testReport[i + 1]) > 3)
            {
                return false;
            }
        }
        
        return true;
    }

}