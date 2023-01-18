using Blackjack.Interfaces;

namespace Blackjack.IO;

public class ConsoleReader : IReader
{
    public string ReadLine()
    {
        return Console.ReadLine()!;
    }
}