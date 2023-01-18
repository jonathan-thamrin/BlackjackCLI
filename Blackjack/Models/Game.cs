using Blackjack.Interfaces;

namespace Blackjack.Models;

public class Game
{
    private Deck _deck;
    private List<Player> _players;
    private IOutputMessenger _outputMessenger;
    private IUserInput _userInput;

    public Game(IRandomizer randomizer, IOutputMessenger outputMessenger, IUserInput userInput)
    {
        _deck = new Deck(randomizer);
        _players = new List<Player> {new(), new()};
        _outputMessenger = outputMessenger;
        _userInput = userInput;
    }
    
    public void Initialise()
    {
        _deck.Shuffle();
        foreach (var player in _players) _deck.DealCards(player);
        _players.Last().SetIsDealer();
    }

    public void Start()
    {
        var allPlayersPlayed = false;

        foreach (var player in _players)
        {
            _outputMessenger.DisplayPlayerCards(player);
            if (player.IsDealer())
            {
                while (player.GetHandSum() < GameRules.DealerLimit) DrawAndDisplayCard(player);
                allPlayersPlayed = true;
            }
            else
            {
                int playerMove;
                do
                {
                    playerMove = _userInput.PromptPlayerMove();
                    if (playerMove == GameRules.Hit) DrawAndDisplayCard(player);
                } while (playerMove == GameRules.Hit && !GameRules.BlackjackOrBust(player));
            }

            if (GameRules.BlackjackOrBust(player))
            {
                _outputMessenger.DisplayRespectiveWinner(_players, player);
                break;
            }

            if (allPlayersPlayed) _outputMessenger.DisplayFinalOutcome(_players);
        }
    }

    private void DrawAndDisplayCard(Player player)
    {
        var drawnCard = _deck.DrawCard();
        player.AddCardToHand(drawnCard);

        _outputMessenger.DisplayDrawnCard(player, drawnCard);
        _outputMessenger.DisplayPlayerCards(player);
    }
}