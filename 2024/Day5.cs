namespace AdventOfCode._2024;

using Rule = (int x, int y);

public class Day5 : AdventBase
{
    private string[] input;
    
    private List<Rule> rules;
    private List<int>[] updates;
    
    public Day5() : base(2024, 5)
    {
        this.input = base.Input;
    }

    public override long PartOne()
    {
        long sum = 0;
        
        ParseInput();
        for (int i = 0; i < updates.Length; i++)
        {
            List<int> update = updates[i];
            if (UpdateIsInOrder(update))
            {
                sum += MiddlePageNumber(update);
            }
        }

        return sum;
    }

    private bool UpdateIsInOrder(List<int> update)
    {
        foreach (Rule rule in rules)
        {
            if (!update.Contains(rule.x) || !update.Contains(rule.y))
            {
                continue;
            }

            if (update.IndexOf(rule.x) > update.IndexOf(rule.y))
            {
                return false;
            }
        }

        return true;
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