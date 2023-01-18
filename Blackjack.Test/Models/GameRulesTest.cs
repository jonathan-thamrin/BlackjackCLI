using Blackjack.Models;
using Blackjack.Types;
using Xunit;

namespace Blackjack.Test.Models;

public class GameRulesTest
{
    [Fact]
    public void GivenPlayerHand_WhenTheyHaveBust_ShouldReturnPlayerHasBust()
    {
        var player = new Player();
        player.AddCardToHand(new Card(Rank.Two, Suit.Diamond));
        player.AddCardToHand(new Card(Rank.Jack, Suit.Diamond));
        player.AddCardToHand(new Card(Rank.Queen, Suit.Diamond));
        
        Assert.True(GameRules.HasPlayerBust(player));
    }

    [Fact]
    public void GivenPlayerHand_WhenTheyHave21_ShouldReturnPlayerHasBlackjack()
    {
        var player = new Player();
        player.AddCardToHand(new Card(Rank.Ace, Suit.Diamond));
        player.AddCardToHand(new Card(Rank.Jack, Suit.Diamond));
        
        Assert.True(GameRules.HasBlackjack(player));
    }
}