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
        ParseInput();
    }

    private void ParseInput()
    {
        rules = input
            .Where(s => s.Contains('|'))
            .Select(s => (int.Parse(s.Split("|")[0]), int.Parse(s.Split("|")[0])))
            .ToList();

        updates = input
            .Where(s => s.Contains(','))
            .Select(s => s.Split(',')
                .Select(int.Parse)
                .ToList())
            .ToArray();
    }
}