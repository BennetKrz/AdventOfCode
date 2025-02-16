namespace AdventOfCode._2020;

public class Day7 : AdventBase
{
    private readonly string[] input;
    
    public Day7() : base(2020, 7)
    {
        this.input = base.Input;
    }

    public override long PartOne()
    {
        List<Rule> rules = ParseInput();

        HashSet<string> colorsFound = [];
        List<string> toLookForNext = ["shiny gold"];
        
        while (toLookForNext.Count > 0)
        {
            List<string> tmp = rules.Where(x => x.BagsContained.Any(y => y.Item1 == toLookForNext[0]))
                .Select(x => x.Color)
                .ToList();

            toLookForNext.RemoveAt(0);
            toLookForNext.AddRange(tmp);
            
            toLookForNext.ForEach(x => colorsFound.Add(x));
        }
        
        return colorsFound.Count;
    }

    public override long PartTwo()
    {
        List<Rule> rules = ParseInput();
        return GetCostsFrom("shiny gold", rules);
    }

    private long GetCostsFrom(string color, List<Rule> rules)
    {
        return GetCostsFrom(color, 1, rules) - 1;
    }

    private long GetCostsFrom(string color, long amount, List<Rule> rules)
    {
        Rule rule = rules.Find(x => x.Color == color) ?? throw new InvalidOperationException();

        long cost = 0;
        
        if (rule.BagsContained.Count == 0)
        {
            return amount;
        }

        foreach ((string color, int amount) bag in rule.BagsContained)
        {
            cost += GetCostsFrom(bag.color, amount * bag.amount, rules);
        }

        return cost + amount;
    }

    private List<Rule> ParseInput()
    {
        List<Rule> rules = [];
        
        foreach (string line in input)
        {
            string colorOfBag = line.Substring(0, line.IndexOf("bags") - 1);
            string[] contained = line.Split("contain ")[1]
                .Split(", ")
                .Select(x => x.Replace(".", "")
                    .Replace("bags", "")
                    .Replace("bag", "")
                    .Trim())
                .ToArray();

            List<(string, int)> bagsContained = [];
            
            for (int i = 0; i < contained.Length; i++)
            {
                if (contained[i] == "no other")
                {
                    break;
                }
                bagsContained.Add((contained[i].Substring(contained[i].IndexOf(" ") + 1), int.Parse(contained[i].Split(" ")[0])));
            }

            rules.Add(new(colorOfBag, bagsContained));
        }

        return rules;
    }

    private record Rule(string Color, List<(string, int)> BagsContained);
}