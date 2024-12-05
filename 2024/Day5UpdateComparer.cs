using Rule = (int x, int y);

namespace AdventOfCode._2024;

public class Day5UpdateComparer(List<Rule> rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        Rule violatedRule = GetViolatedRule(x, y);
        if (violatedRule == (-1, -1))
        {
            Rule correctRule = GetCorrectRule(x, y);
            if (correctRule.x == x && correctRule.y == y)
            {
                return -1;
            }
            return 0;
        }

        return 1;
    }

    private Rule GetViolatedRule(int x, int y)
    {
        foreach (Rule rule in rules)
        {
            if (rule.x == y && rule.y == x)
            {
                return rule;
            }
        }

        return (-1, -1);
    }

    private Rule GetCorrectRule(int x, int y)
    {
        foreach (Rule rule in rules)
        {
            if (rule.x == x && rule.y == y)
            {
                return rule;
            }
        }

        return (-1, -1);
    }
}