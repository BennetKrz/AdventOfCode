namespace AdventOfCode._2024;

using Rule = (int x, int y);

public class Day5 : AdventBase
{
    private readonly string[] input;
    
    private List<Rule> rules;
    private List<int>[] updates;
    
    public Day5() : base(2024, 5)
    {
        this.input = base.Input;
        ParseInput();
    }

    public override long PartOne()
    {
        return updates
            .Where(UpdateIsInOrder)
            .Sum(MiddlePageNumber);
    }

    public override long PartTwo()
    {
        IComparer<int> comparer = new Day5UpdateComparer(rules);
        return updates
            .Where(u => !UpdateIsInOrder(u))
            .Select(l => l
                .Order(comparer)
                .ToList())
            .Sum(MiddlePageNumber);
    }

    private bool UpdateIsInOrder(List<int> update)
    {
        return rules
            .Where(rule => update.Contains(rule.x) && update.Contains(rule.y))
            .All(rule => update.IndexOf(rule.x) < update.IndexOf(rule.y));
    }

    private int MiddlePageNumber(List<int> update)
    {
        return update[update.Count / 2];
    }

    private void ParseInput()
    {
        rules = input
            .Where(s => s.Contains('|'))
            .Select(s => (int.Parse(s.Split("|")[0]), int.Parse(s.Split("|")[1])))
            .ToList();
        
        updates = input
            .Where(s => s.Contains(','))
            .Select(s => s.Split(',')
                .Select(int.Parse)
                .ToList())
            .ToArray();
    }
}