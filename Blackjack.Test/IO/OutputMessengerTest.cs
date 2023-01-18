using System.Collections.Generic;
using System.Linq;
using Blackjack.IO;
using Blackjack.Models;
using Blackjack.Types;
using Xunit;

namespace Blackjack.Test.IO;

public class OutputMessengerTest
{
    private WriterFake _writerFake;
    private OutputMessenger _outputMessenger;
    private Player _player, _dealer;
    private List<Player> _players;

    public OutputMessengerTest()
    {
        _writerFake = new WriterFake();
        _outputMessenger = new OutputMessenger(_writerFake);
        _player = new Player();
        _dealer = new Player();
        _dealer.SetIsDealer();
        _players = new List<Player> {_player, _dealer};
    }

    private static void SetPlayersHandsToBust(Player player)
    {
        player.AddCardToHand(new Card(Rank.Ten, Suit.Diamond));
        player.AddCardToHand(new Card(Rank.Ten, Suit.Heart));
        player.AddCardToHand(new Card(Rank.Ten, Suit.Club));
    }

    [Theory]
    [InlineData(true, "Dealer drew [Ace, Diamond]\n")]
    [InlineData(false, "You drew [Ace, Diamond]\n")]
    public void GivenDisplayDrawnCard_WhenRespectivePlayerDrawsCard_ShouldDisplayCorrectPlayerAndCard(bool isDealer,
        string output)
    {
        var playerTestSubject = isDealer ? _dealer : _player;

        _outputMessenger.DisplayDrawnCard(playerTestSubject, new Card(Rank.Ace, Suit.Diamond));

        Assert.Equal(output, _writerFake.StringBuffer.First());
    }

    [Theory]
    [InlineData(true, "Dealer wins! Better luck next time.\n")]
    [InlineData(false, "You beat the dealer! Congratulations.\n")]
    public void GivenDisplayBlackjackOrBust_WhenRespectivePlayerHasBlackjack_ShouldDisplayCorrectPlayerAsWinner(
        bool isDealer, string output)
    {
        var playerTestSubject = isDealer ? _dealer : _player;
        playerTestSubject.AddCardToHand(new Card(Rank.Ace, Suit.Diamond));
        playerTestSubject.AddCardToHand(new Card(Rank.Jack, Suit.Diamond));

        _outputMessenger.DisplayRespectiveWinner(_players, playerTestSubject);

        Assert.Equal(output, _writerFake.StringBuffer.First());
    }

    [Theory]
    [InlineData(true, "You beat the dealer! Congratulations.\n")]
    [InlineData(false, "Dealer wins! Better luck next time.\n")]
    public void GivenDisplayBlackjackOrBust_WhenRespectivePlayerHasBust_ShouldDisplayCorrectPlayerAsWinner(
        bool isDealer, string output)
    {
        var playerTestSubject = isDealer ? _dealer : _player;
        SetPlayersHandsToBust(playerTestSubject);

        _outputMessenger.DisplayRespectiveWinner(_players, playerTestSubject);

        Assert.Equal(output, _writerFake.StringBuffer.First());
    }

    [Theory]
    [InlineData(true, true, "\nDealer is at a Bust\n", "with the hand [Ten, Diamond], [Ten, Heart], [Ten, Club]\n\n")]
    [InlineData(false, true, "\nYou are at a Bust\n", "with the hand [Ten, Diamond], [Ten, Heart], [Ten, Club]\n\n")]
    [InlineData(true, false, "\nDealer is at 11\n", "with the hand [Ace, Diamond]\n\n")]
    [InlineData(false, false, "\nYou are at 11\n", "with the hand [Ace, Diamond]\n\n")]
    public void GivenDisplayPlayerCards_WhenRespectivePlayerHasBustOrNot_ShouldDisplayCorrectPlayerAndTheirCards(
        bool isDealer, bool isBust, string firstMsg, string secondMsg)
    {
        var firstMsgIndex = 0;
        var secondMsgIndex = 1;
        var playerTestSubject = isDealer ? _dealer : _player;
        if (isBust)
        {
            SetPlayersHandsToBust(playerTestSubject);
        }
        else
        {
            playerTestSubject.AddCardToHand(new Card(Rank.Ace, Suit.Diamond));
        }

        _outputMessenger.DisplayPlayerCards(playerTestSubject);

        Assert.Equal(firstMsg, _writerFake.StringBuffer[firstMsgIndex]);
        Assert.Equal(secondMsg, _writerFake.StringBuffer[secondMsgIndex]);
    }

    [Theory]
    [InlineData(true, false, "The game ended with a tie.\n")]
    [InlineData(false, false, "Dealer wins! Better luck next time.\n")]
    [InlineData(false, true, "You beat the dealer! Congratulations.\n")]
    public void GivenDisplayFinalOutcome_WhenBothPlayersNeitherBustOrBlackjack_ShouldDisplayRespectiveWinnerOrTie(
        bool isTie, bool playerWon, string output)
    {
        var card = new Card(Rank.Eight, Suit.Club);
        var winningCard = new Card(Rank.Eight, Suit.Diamond);
        _player.AddCardToHand(card);
        _dealer.AddCardToHand(card);
        if (!isTie)
        {
            if (playerWon)
            {
                _player.AddCardToHand(winningCard);
            }
            else
            {
                _dealer.AddCardToHand(winningCard);
            }
        }

        _outputMessenger.DisplayFinalOutcome(_players);

        Assert.Equal(output, _writerFake.StringBuffer.First());
    }
}