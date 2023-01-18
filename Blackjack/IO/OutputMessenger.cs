using Blackjack.Interfaces;
using Blackjack.Models;

namespace Blackjack.IO;

public class OutputMessenger : IOutputMessenger
{
    private readonly IWriter _writer;

    public OutputMessenger(IWriter writer)
    {
        _writer = writer;
    }

    public void DisplayPlayerCards(Player player)
    {
        var allCardsAsText = "";
        var playersCards = player.Hand.GetCards();
        var playerIdentifier = player.IsDealer() ? "Dealer is" : "You are";
        var outcome = GameRules.HasPlayerBust(player) ? "a Bust" : player.GetHandSum().ToString();

        foreach (var card in playersCards)
        {
            allCardsAsText += $"[{card.Rank()}, {card.Suit()}]";
            allCardsAsText += card != playersCards.Last() ? ", " : "\n\n";
        }
        
        _writer.WriteLine($"\n{playerIdentifier} at {outcome}");
        _writer.Write($"with the hand {allCardsAsText}");
        
    }

    public void DisplayDrawnCard(Player player, Card card)
    {
        var suit = card.Suit();
        var rank = card.Rank().ToString();
        var isDealer = player.IsDealer();
        var playerIdentifier = isDealer ? "Dealer" : "You";

        _writer.WriteLine($"{playerIdentifier} drew [{rank}, {suit}]");
    }

    public void DisplayFinalOutcome(List<Player> players)
    {
        var player = players.First();
        var dealer = players.Last();
        var playerHandAndBlackjackDiff = GameRules.Blackjack - player.GetHandSum();
        var dealerHandAndBlackjackDiff = GameRules.Blackjack - dealer.GetHandSum();

        if (playerHandAndBlackjackDiff == dealerHandAndBlackjackDiff)
            DisplayTie();
        else
            DisplayWinner(playerHandAndBlackjackDiff > dealerHandAndBlackjackDiff
                ? dealer
                : player);
    }

    public void DisplayRespectiveWinner(List<Player> players, Player player)
    {
        if (GameRules.HasBlackjack(player))
            DisplayWinner(player);
        else
            DisplayWinner(player != players.First() ? players.First() : players.Last());
    }

    private void DisplayWinner(Player player)
    {
        var isDealer = player.IsDealer();

        _writer.WriteLine(isDealer ? "Dealer wins! Better luck next time." : "You beat the dealer! Congratulations.");
    }

    private void DisplayTie()
    {
        _writer.WriteLine("The game ended with a tie.");
    }
}