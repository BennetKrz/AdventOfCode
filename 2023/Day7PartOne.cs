namespace AdventOfCode._2023;

public class Day7PartOne : IAdventDay
{
	private readonly List<string> input;

	public Day7PartOne()
	{
		input = Helper.GetInput(7).ToList();
	}

	public long PartOne()
	{
		long sum = 0;
		List<CardDeck> allDecks = ParseInput();
		allDecks.Sort();
		allDecks.Reverse();
		for (int i = 0; i < allDecks.Count; i++)
		{
			sum += allDecks[i].Bid * (i + 1);
		}

		return sum;
	}

	public long PartTwo()
	{
		return long.MinValue;
	}

	private List<CardDeck> ParseInput()
	{
		List<CardDeck> allDecks = new();
		for (int i = 0; i < input.Count; i++)
		{
			long bid = long.Parse(input[i].Split(" ")[1]);
			List<Card> cards = DetermineCards(input[i].Substring(0, 5));
			allDecks.Add(new CardDeck(cards, bid));
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
					result.Add(Card.Ace);
					break;
				case 'K':
					result.Add(Card.King);
					break;
				case 'Q':
					result.Add(Card.Queen);
					break;
				case 'J':
					result.Add(Card.Jack);
					break;
				case 'T':
					result.Add(Card.Ten);
					break;
				case '9':
					result.Add(Card.Nine);
					break;
				case '8':
					result.Add(Card.Eight);
					break;
				case '7':
					result.Add(Card.Seven);
					break;
				case '6':
					result.Add(Card.Six);
					break;
				case '5':
					result.Add(Card.Five);
					break;
				case '4':
					result.Add(Card.Four);
					break;
				case '3':
					result.Add(Card.Three);
					break;
				case '2':
					result.Add(Card.Two);
					break;
			}
		}

		return result;
	}

	private record CardDeck(List<Card> Cards, long Bid) : IComparable<CardDeck>
	{
		public int CompareTo(CardDeck? other)
		{
			Kind thisKind = DetermineKind(this.Cards);
			Kind otherKind = DetermineKind(other.Cards);
			if ((int)thisKind - (int)otherKind == 0) return InternalCompare(other.Cards);

			return (int)thisKind - (int)otherKind;
		}

		private int InternalCompare(List<Card> otherCards)
		{
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (this.Cards[i] == otherCards[i]) continue;

				return (int)this.Cards[i] - (int)otherCards[i];
			}

			return 0;
		}

		private Kind DetermineKind(List<Card> cards)
		{
			if (IsFiveKind(cards)) return Kind.Five;

			if (IsFourKind(cards)) return Kind.Four;

			if (IsFullHouse(cards)) return Kind.FullHouse;

			if (IsThreeKind(cards)) return Kind.Three;

			if (IsTwoPair(cards)) return Kind.TwoPair;

			if (IsOnePair(cards)) return Kind.OnePair;

			if (IsHighCard(cards)) return Kind.HighCard;

			return Kind.HighCard;
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

	private enum Card
	{
		Ace,
		King,
		Queen,
		Jack,
		Ten,
		Nine,
		Eight,
		Seven,
		Six,
		Five,
		Four,
		Three,
		Two
	}
	
	private enum Kind
	{
		Five,
		Four,
		FullHouse,
		Three,
		TwoPair,
		OnePair,
		HighCard
	}
}