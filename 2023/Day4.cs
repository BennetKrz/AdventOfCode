namespace AdventOfCode._2023;

public class Day4 : AdventBase
{
    private List<string> input;
    
    public Day4() : base(2023, 4)
    {
        input = Input.ToList();
    }
    public override long PartOne()
    {
        List<Card> cards = ParseInput();
        return cards.Sum(card => CalculateCardPoints(card));
    }

    public override long PartTwo()
    {
        List<Card> cards = ParseInput();

        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            int winningCards = GetWinnerAmount(card);

            for(int index = i + 1; (index <= i + winningCards) && (index < cards.Count); index++){
                
                Card oldCard = cards[index];
                cards[index] = new Card(oldCard.CardId, oldCard.CardNumbers, oldCard.WinningNumbers,
                                        oldCard.Amount + card.Amount);
            }
        }
        
        return cards.Sum(c => c.Amount);
    }

    private int CalculateCardPoints(Card card)
    {
        return GetWinnerAmount(card) > 0 ? (int)Math.Pow(2, GetWinnerAmount(card) - 1) : 0;
    }
    
    private int GetWinnerAmount(Card card)
    {
        return card.WinningNumbers
                    .Intersect(card.CardNumbers)
                    .ToHashSet()
                    .Count;
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
            cards.Add(new (i, cardNumbers, winningNumbers, 1L));

        }
        
        return cards;
    }

    private record Card(int CardId, HashSet<int> CardNumbers, HashSet<int> WinningNumbers, long Amount);
}