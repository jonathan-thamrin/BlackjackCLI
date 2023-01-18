using Blackjack.Models;
using Blackjack.Types;
using Xunit;

namespace Blackjack.Test.Models;

public class CardTest
{
    [Fact]
    public void GivenASpecificCard_WhenCreatingACard_ThenShouldReturnCorrectRankAndSuit()
    {
        var rank = Rank.Ten;
        var suit = Suit.Heart;
        var card = new Card(rank, suit);

        var actualRank = (int) card.Rank();
        var actualSuit = card.Suit();
        var expectedRank = (int) rank;
        var expectedSuit = suit.ToString();

        Assert.Equal(expectedRank, actualRank);
        Assert.Equal(expectedSuit, actualSuit);
    }
}