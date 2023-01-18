using Blackjack.Interfaces;
using Blackjack.Models;
using Blackjack.Types;
using Xunit;

namespace Blackjack.Test.Models;

public class DeckTest
{
    private readonly IRandomizer _randomizer;

    public DeckTest()
    {
        _randomizer = new Randomizer();
    }

    [Fact]
    public void GivenADeckOfCards_WhenCreatingADeckForAGame_ShouldContain52CardsInTotal()
    {
        var deck = new Deck(_randomizer);

        var expectedCardCount = 52;
        var actualCardCount = deck.GetCards().Count;
        
        Assert.Equal(expectedCardCount, actualCardCount);
    }

    [Fact]
    public void GivenADeckOfCards_WhenDrawingACard_ShouldDecreaseDeckByOne()
    {
        var deck = new Deck(_randomizer);

        deck.DrawCard();
        var expectedCardCount = 51;
        var actualCardCount = deck.GetCards().Count;
        
        Assert.Equal(expectedCardCount, actualCardCount);
    }

    [Fact]
    public void GivenADeckOfCards_WhenDrawingACard_ShouldReturnLastCardInDeck()
    {
        var deck = new Deck(_randomizer);
        var expectedRank = Rank.Ace;
        var expectedSuit = Suit.Club.ToString();

        var actualCardDrawn = deck.DrawCard();

        Assert.Equal(expectedRank, actualCardDrawn.Rank());
        Assert.Equal(expectedSuit, actualCardDrawn.Suit());
    }
    
    [Fact]
    public void GivenADeckOfCards_WhenDrawingCards_ShouldDrawDifferentCardEachTime()
    {
        var deck = new Deck(_randomizer);
      
        var firstDrawnCard = deck.DrawCard();
        var secondDrawnCard = deck.DrawCard();
        var thirdDrawnCard = deck.DrawCard();
        
        Assert.NotEqual(firstDrawnCard, secondDrawnCard);
        Assert.NotEqual(firstDrawnCard, thirdDrawnCard);
        Assert.NotEqual(secondDrawnCard, thirdDrawnCard);
    }

    [Fact]
    public void GivenADeckOfCards_WhenShufflingCards_ShouldRandomiseDeckOrder()
    {
        var shuffledDeck = new Deck(_randomizer);
        var unShuffledDeck = new Deck(_randomizer);

        shuffledDeck.Shuffle();

        Assert.NotEqual( shuffledDeck.GetCards(), unShuffledDeck.GetCards());
    }
}