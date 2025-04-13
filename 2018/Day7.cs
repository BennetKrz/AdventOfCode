using System.Text.RegularExpressions;

namespace AdventOfCode._2018;

public class Day7 : AdventBase
{
    private readonly List<string> input;
    
    public Day7() : base(2018, 7)
    {
        this.input = base.Input.ToList();
    }

    public override long PartOne()
    {
        List<Step> steps = ParseInput();

        string orderedOutput = string.Empty;

        HashSet<Step> done = [];
        Step curStep = steps
            .Where(x => x.IsReady)
            .OrderBy(x => x.Name)
            .First();
        done.Add(curStep);
        orderedOutput += curStep.Name;

        while (done.Count < steps.Count)
        {
            curStep = steps
                .Where(x => !done.Contains(x) && x.AllBeforeReady())
                .OrderBy(x => x.Name)
                .First();
            
            curStep.IsReady = true;
            orderedOutput += curStep.Name;
            done.Add(curStep);
        }

        Console.WriteLine(orderedOutput);
        return long.MinValue;
    }

    private List<Step> ParseInput()
    {
        Dictionary<string, Step> steps = [];
        
        foreach ((string before, string after) stepNames in input.Select(GetStepNames))
        {
            EnsureExistence(steps, stepNames);
            steps[stepNames.after].AddStepBefore(steps[stepNames.before]);
        }
        
        return steps.Values.ToList();
    }

    private static void EnsureExistence(Dictionary<string, Step> steps, (string before, string after) stepNames)
    {
        if (!steps.ContainsKey(stepNames.before))
        {
            steps.Add(stepNames.before, new Step(stepNames.before));
        }

        if (!steps.ContainsKey(stepNames.after))
        {
            steps.Add(stepNames.after, new Step(stepNames.after));
        }
    }

    private (string, string) GetStepNames(string line)
    {
        Regex regexBefore = new Regex(@"Step (\b[A-Z]\b)");
        Regex regexAfter = new Regex(@"step (\b[A-Z]\b)");
        MatchCollection matchesBefore = regexBefore.Matches(line);
        MatchCollection matchesAfter = regexAfter.Matches(line);
        
        return (matchesBefore[0].Groups[1].Value, matchesAfter[0].Groups[1].Value);
    }
}

internal record Step(string Name)
{
    public bool IsReady { get; set; } = true;
    private readonly List<Step> mustBeReady = [];

    public bool AllBeforeReady()
    {
        return mustBeReady.All(x => x.IsReady);
    }
    
    public void AddStepBefore(Step stepBefore)
    {
        IsReady = false;
        mustBeReady.Add(stepBefore);
    }
    
    public virtual bool Equals(Step? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}