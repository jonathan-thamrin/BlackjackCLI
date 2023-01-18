using Blackjack.Interfaces;

namespace Blackjack.Models;

public class Randomizer : IRandomizer
{
    public int GenerateRandomNumber()
    {
        return new Random().Next();
    }
}