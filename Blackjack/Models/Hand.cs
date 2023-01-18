using Blackjack.Types;

namespace Blackjack.Models;

public class Hand
{
    private readonly List<Card> _cards;

    public Hand()
    {
        _cards = new List<Card>();
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public void AddCard(Card drawnCard)
    {
        _cards.Add(drawnCard);
    }

    public int GetSum()
    {
        var sum = 0;
        var noOfAces = 0;

        foreach (var rank in _cards.Select(card => card.Rank()))
        {
            switch (rank)
            {
                case Rank.Ace:
                    noOfAces++;
                    break;
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    sum += GameRules.CourtValue;
                    break;
                default:
                    sum += (int) rank;
                    break;
            }
        }

        for (var i = 0; i < noOfAces; i++)
        {
            var aceAsEleven = 11;

            if (sum + aceAsEleven <= GameRules.Blackjack)
                sum += aceAsEleven;
            else
                sum += (int) Rank.Ace;
        }

        return sum;
    }
}