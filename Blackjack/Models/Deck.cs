using Blackjack.Interfaces;
using Blackjack.Types;

namespace Blackjack.Models;

public class Deck
{
    private readonly List<Rank> _ranks = new()
    {
        Rank.Two, Rank.Three, Rank.Four, Rank.Five,
        Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine,
        Rank.Ten, Rank.Jack, Rank.Queen, Rank.King,
        Rank.Ace
    };

    private readonly List<Suit> _suits = new()
    {
        Suit.Heart, Suit.Diamond, Suit.Spade, Suit.Club
    };

    private List<Card> _deck = new();
    private readonly IRandomizer _randomizer;

    public Deck(IRandomizer randomizer)
    {
        _randomizer = randomizer;
        GenerateDeck();
    }

    private void GenerateDeck()
    {
        foreach (var rank in _ranks)
        foreach (var suit in _suits)
        {
            var cardToAdd = new Card(rank, suit);
            _deck.Add(cardToAdd);
        }
    }

    public List<Card> GetCards()
    {
        return _deck;
    }

    public Card DrawCard()
    {
        var drawnCard = _deck.Last();
        _deck.Remove(drawnCard);

        return drawnCard;
    }

    public void Shuffle()
    {
        _deck = _deck.OrderBy(card => _randomizer.GenerateRandomNumber()).ToList();
    }

    public void DealCards(Player player)
    {
        for (var i = 0; i < 2; i++) player.AddCardToHand(DrawCard());
    }
}