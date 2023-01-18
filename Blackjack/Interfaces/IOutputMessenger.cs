using Blackjack.Models;

namespace Blackjack.Interfaces;

public interface IOutputMessenger
{
    public void DisplayPlayerCards(Player player);
    public void DisplayDrawnCard(Player player, Card card);
    public void DisplayFinalOutcome(List<Player> players);
    public void DisplayRespectiveWinner(List<Player> players, Player player);
}