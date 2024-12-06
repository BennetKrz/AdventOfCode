using Rule = (int x, int y);

namespace AdventOfCode._2024;

public class Day5UpdateComparer(List<Rule> rules) : IComparer<int>
{
    private readonly Func<int, int, Rule, bool> violatedRuleChecker = (x, y, rule) => rule.x == y && rule.y == x;
    private readonly Func<int, int, Rule, bool> correctRuleChecker = (x, y, rule) => rule.x == x && rule.y == y;
    
    public int Compare(int x, int y)
    {
        Rule violatedRule = GetCorrespondingRule(x, y, violatedRuleChecker);
        if (violatedRule != (-1, -1))
        {
            return 1;
        }
        Rule correctRule = GetCorrespondingRule(x, y, correctRuleChecker);
        return correctRule.x == x && correctRule.y == y ? -1 : 0;
    }

    private Rule GetCorrespondingRule(int x, int y, Func<int, int, Rule, bool> ruleChecker)
    {
        return rules.FirstOrDefault(rule => ruleChecker(x, y, rule), (-1, -1));
    }
}