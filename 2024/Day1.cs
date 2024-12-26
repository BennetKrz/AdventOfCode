namespace AdventOfCode._2024;

public class Day1 : AdventBase
{
    private readonly string[] input;

    private List<int> leftList = [];
    private List<int> rightList = [];
    
    public Day1() : base(2024, 1)
    {
        this.input = Input;
        ParseInput();
    }

    public override long PartOne()
    {
        leftList.Sort();
        rightList.Sort();
        
        return Enumerable.Zip(leftList, rightList, (l, r) => Math.Abs(l - r)).Sum();
    }

    public override long PartTwo()
    {
        return leftList.Select(x => rightList.Count(r => r == x) * x).Sum();
    }

    private void ParseInput()
    {
        leftList = input.Select(x => x.Split("   ")[0]).Select(int.Parse).ToList();
        rightList = input.Select(x => x.Split("   ")[1]).Select(int.Parse).ToList();
    }
}