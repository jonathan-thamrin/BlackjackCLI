using Blackjack.Interfaces;

namespace Blackjack.Test;

public class RandomizerFake : IRandomizer
{
    public int GenerateRandomNumber()
    {
        return 0;
    }
}