using Blackjack.Models;
using Blackjack.Types;
using Xunit;

namespace Blackjack.Test.Models;

public class HandTest
{
    [Fact]
    public void GivenAHand_WhenCardsAreAdded_ThenHandShouldHaveMultipleCards()
    {
        var noOfCardsDealt = 2;
        var cardOne = new Card(Rank.Ace, Suit.Club);
        var cardTwo = new Card(Rank.Two, Suit.Spade);
        var hand = new Hand();

        hand.AddCard(cardOne);
        hand.AddCard(cardTwo);
        var actualNoOfCardsDealt = hand.GetCards().Count;

        Assert.Equal(noOfCardsDealt, actualNoOfCardsDealt);
    }
    
    [Theory]
    [InlineData(21, new[] {Rank.Eight, Rank.Two, Rank.Ace},
        new[] {Suit.Spade, Suit.Heart, Suit.Club})]
    [InlineData(20, new[] {Rank.Ten, Rank.Nine, Rank.Ace},
        new[] {Suit.Spade, Suit.Heart, Suit.Club})]
    [InlineData(17, new[] {Rank.Ace, Rank.Ace, Rank.Five},
        new[] {Suit.Spade, Suit.Heart, Suit.Club})]
    [InlineData(13, new[] {Rank.Ace, Rank.Ace, Rank.Ace},
        new[] {Suit.Spade, Suit.Heart, Suit.Club})]
    [InlineData(23, new[] {Rank.Ten, Rank.Ten, Rank.Two, Rank.Ace},
        new[] {Suit.Spade, Suit.Heart, Suit.Club, Suit.Club})]
    public void GivenCardsInHand_WhenDeterminingSumOfCards_ShouldReturnNumericalValueOfCards(int expectedSumOfHand,
        Rank[] ranks, Suit[] suites)
    {
        var noOfCardsToAddToHand = ranks.Length;
        var hand = new Hand();
        for (var i = 0; i < noOfCardsToAddToHand; i++)
        {
            var card = new Card(ranks[i], suites[i]);
            hand.AddCard(card);
        }

        var actualSumOfHand = hand.GetSum();

        Assert.Equal(expectedSumOfHand, actualSumOfHand);
    }
}