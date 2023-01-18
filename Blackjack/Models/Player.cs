namespace Blackjack.Models;

public class Player
{
    public Hand Hand { get; }
    private bool _isDealer;

    public Player()
    {
        Hand = new Hand();
        _isDealer = false;
    }

    public void AddCardToHand(Card card)
    {
        Hand.AddCard(card);
    }
    
    public int GetHandSum()
    {
        return Hand.GetSum();
    }

    public bool IsDealer()
    {
        return _isDealer;
    }

    public void SetIsDealer()
    {
        _isDealer = true;
    }
}
