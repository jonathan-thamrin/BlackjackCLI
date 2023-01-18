namespace Blackjack.Models;

public static class GameRules
{
    public const int Blackjack = 21;
    public const int DealerLimit = 17;
    public const int Hit = 1;
    public const int CourtValue = 10;

    public static bool HasPlayerBust(Player player)
    {
        return player.GetHandSum() > Blackjack;
    }

    public static bool HasBlackjack(Player player)
    {
        return player.GetHandSum() == Blackjack;
    }

    public static bool BlackjackOrBust(Player player)
    {
        return HasBlackjack(player) || HasPlayerBust(player);
    }
}