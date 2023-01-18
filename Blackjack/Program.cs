using Blackjack.Interfaces;
using Blackjack.IO;
using Blackjack.Models;

namespace Blackjack;

internal static class Program
{
    private static void Main(string[] args)
    {
        IWriter writer = new ConsoleWriter();
        IReader reader = new ConsoleReader();
        IUserInput userInput = new UserInput(writer, reader);
        IOutputMessenger outputMessenger = new OutputMessenger(writer);
        IRandomizer randomizer = new Randomizer();
        Game game = new Game(randomizer, outputMessenger, userInput);

        game.Initialise();
        game.Start();
    }
}