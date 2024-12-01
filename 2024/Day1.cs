namespace AdventOfCode._2024;

public class Day1 : AdventBase
{
    private readonly string[] input;

    private List<int> leftList = [];
    private List<int> rightList = [];
    private List<(int l, int r)> pairs = [];
    
    public Day1() : base(2024, 1)
    {
        this.input = Input;
        ParseInput();
    }

    public override long PartOne()
    {
        while (leftList.Count > 0)
        {
            int leftMin = leftList.Min();
            int rightMin = rightList.Min();
            leftList.Remove(leftMin);
            rightList.Remove(rightMin);
            
            pairs.Add((leftMin, rightMin));
        }
        
        return pairs.Select(x => Math.Abs(x.r - x.l)).Sum();
    }

    public override long PartTwo()
    {
        ParseInput();
        return leftList.Select(x => rightList.Count(r => r == x) * x).Sum();
    }

    private void ParseInput()
    {
        leftList = input.Select(x => x.Split("   ")[0]).Select(int.Parse).ToList();
        rightList = input.Select(x => x.Split("   ")[1]).Select(int.Parse).ToList();
    }
}