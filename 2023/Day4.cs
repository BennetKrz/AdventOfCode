namespace AdventOfCode._2023;

public class Day4 : IAdventDay
{
    private List<string> input;
    
    public Day4()
    {
        input = Helper.GetInput(4).ToList();
    }
    public long PartOne()
    {
        List<Card> cards = ParseInput();
        return cards.Sum(card => AnalyseCard(card));
    }

    public long PartTwo()
    {
        return 0;
    }

    private int AnalyseCard(Card card)
    {
        int numbersThatWonCount = card.WinningNumbers
            .Intersect(card.CardNumbers)
            .ToHashSet()
            .Count;
        return numbersThatWonCount > 0 ? (int)Math.Pow(2, numbersThatWonCount - 1) : 0;
    }

    private List<Card> ParseInput()
    {
        List<Card> cards = new();

        for (int i = 0; i < input.Count; i++)
        {
            string numbers = input[i].Split(":")[1];
            HashSet<int> winningNumbers = numbers
                .Split("|")[0]
                .Split(" ")
                .Where(n => n != "")
                .Select(n => int.Parse(n.Trim()))
                .ToHashSet();
            HashSet<int> cardNumbers = numbers
                .Split("|")[1]
                .Split(" ")
                .Where(n => n != "")
                .Select(n => int.Parse(n.Trim()))
                .ToHashSet();
            cards.Add(new (i, cardNumbers, winningNumbers));

        }
        
        return cards;
    }

    private record Card(int CardId, HashSet<int> CardNumbers, HashSet<int> WinningNumbers);
}