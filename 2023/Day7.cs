namespace AdventOfCode._2023;

public class Day7 : IAdventDay
{
    private List<string> input;

    public Day7()
    {
        this.input = Helper.GetInput(7).ToList();
    }
    
    public long PartOne()
    {
        List<CardDeck> allDecks = ParseInput();
        allDecks.Sort();
        return 0;
    }

    public long PartTwo()
    {
        return 0;
    }

    private List<CardDeck> ParseInput()
    {
        List<CardDeck> allDecks = new();
        for (int i = 0; i < input.Count; i++)
        {
            long bid = long.Parse(input[i].Split(" ")[1]);
            List<Card> cards = DetermineCards(input[i].Substring(0, 5));
            allDecks.Add(new (cards, bid));
        }

        return allDecks;
    }

    private List<Card> DetermineCards(string cards)
    {
        List<Card> result = new();
        foreach (char card in cards)
        {
            switch (card)
            {
                case 'A':
                    result.Add(Card.ACE);
                    break;
                case 'K':
                    result.Add(Card.KING);
                    break;
                case 'Q':
                    result.Add(Card.QUEEN);
                    break;
                case 'J':
                    result.Add(Card.JACK);
                    break;
                case 'T':
                    result.Add(Card.TEN);
                    break;
                case '9':
                    result.Add(Card.NINE);
                    break;
                case '8':
                    result.Add(Card.EIGHT);
                    break;
                case '7':
                    result.Add(Card.SEVEN);
                    break;
                case '6':
                    result.Add(Card.SIX);
                    break;
                case '5':
                    result.Add(Card.FIVE);
                    break;
                case '4':
                    result.Add(Card.FOUR);
                    break;
                case '3':
                    result.Add(Card.THREE);
                    break;
                case '2':
                    result.Add(Card.TWO);
                    break;
            }
        }

        return result;
    }

    private record CardDeck(List<Card> Cards, long Bid) : IComparable<CardDeck>
    {
        public int CompareTo(CardDeck? other)
        {
            Kind thisKind = DeterminKind(this.Cards);
            Kind otherKind = DeterminKind(other.Cards);
            if ((int)thisKind - (int)otherKind == 0)
            {
                
            }

            return (int)thisKind - (int)otherKind;
        }

        private Kind DeterminKind(List<Card> cards)
        {
            if (IsFiveKind(cards))
            {
                return Kind.FIVE;
            }

            if (IsFourKind(cards))
            {
                return Kind.FOUR;
            }

            if (IsFullHouse(cards))
            {
                return Kind.FULL_HOUSE;
            }

            if (IsThreeKind(cards))
            {
                return Kind.THREE;
            }

            if (IsTwoPair(cards))
            {
                return Kind.TWO_PAIR;
            }

            if (IsOnePair(cards))
            {
                return Kind.ONE_PAIR;
            }

            if (IsHighCard(cards))
            {
                return Kind.HIGH_CARD;
            }

            return Kind.HIGH_CARD;
        }
        
        private bool IsFiveKind(List<Card> cards)
        {
            return CountAndOrderByCards(cards).Count == 1;
        }

        private bool IsFourKind(List<Card> cards)
        {
            return CountAndOrderByCards(cards).Values.ToArray()[0] == 4;
        }

        private bool IsFullHouse(List<Card> cards)
        {
            Dictionary<Card, int> ordered = CountAndOrderByCards(cards);
            return ordered.Values.ToArray()[0] == 3 &&
                   ordered.Values.ToArray()[1] == 2;
        }

        private bool IsThreeKind(List<Card> cards)
        {
            return CountAndOrderByCards(cards).Values.ToArray()[0] == 3;
        }

        private bool IsTwoPair(List<Card> cards)
        {
            Dictionary<Card, int> ordered = CountAndOrderByCards(cards);
            return ordered.Values.ToArray()[0] == 2 &&
                   ordered.Values.ToArray()[1] == 2;
        }

        private bool IsOnePair(List<Card> cards)
        {
            return CountAndOrderByCards(cards).Values.ToArray()[0] == 2;
        }

        private bool IsHighCard(List<Card> cards)
        {
            return CountAndOrderByCards(cards).Values.ToArray()[0] == 1;
        }

        private Dictionary<Card, int> CountAndOrderByCards(List<Card> cards)
        {
            return cards.GroupBy(c => c)
                .ToDictionary(x => x.Key, y => y.Count())
                .OrderByDescending(x => x.Value)
                .ToDictionary();
        }
    }

    private enum Kind
    {
        FIVE,
        FOUR,
        FULL_HOUSE,
        THREE,
        TWO_PAIR,
        ONE_PAIR,
        HIGH_CARD
    }

    private enum Card
    {
        ACE,
        KING,
        QUEEN,
        JACK,
        TEN,
        NINE,
        EIGHT,
        SEVEN,
        SIX,
        FIVE,
        FOUR,
        THREE,
        TWO
    }
}