using Blackjack.Types;

namespace Blackjack.Models;

public class Card : IEquatable<Card>
{
    private readonly Rank _rank;
    private readonly Suit _suit;

    public Card(Rank rank, Suit suit)
    {
        _rank = rank;
        _suit = suit;
    }

    public Rank Rank()
    {
        return _rank;
    }

    public string Suit()
    {
        return _suit.ToString();
    }

    public bool Equals(Card? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _rank == other._rank && _suit == other._suit;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Card) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int) _rank, (int) _suit);
    }
}