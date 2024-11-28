namespace AdventOfCode._2018;

public class Day5() : AdventBase(2018, 5)
{
    public override long PartOne()
    {
        return React(InputOneLine).Length;
    }

    public override long PartTwo()
    {
        HashSet<char> chars = InputOneLine.ToLower().ToHashSet();
        
        string original = InputOneLine;
        
        List<int> counts = [];
        foreach (char c in chars)
        {
            string cleanString = original.Replace(c.ToString().ToLower(), "").Replace(c.ToString().ToUpper(), "");
            counts.Add(React(cleanString).Length);
        }

        return counts.Min();
    }

    private string React(string polymereChain)
    {
        int i = 0;
        while(i < polymereChain.Length - 1)
        {
            char c1 = polymereChain[i];
            char c2 = polymereChain[i + 1];
            
            if (c1 - 32 == c2 || c1 == c2 - 32)
            {
                polymereChain = polymereChain.Remove(i, 2);
                i = 0;
                continue;
            }

            i++;
        }

        return polymereChain;
    }
}