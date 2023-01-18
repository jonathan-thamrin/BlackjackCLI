using Blackjack.Interfaces;
using Blackjack.Validators;

namespace Blackjack.IO;

public class UserInput : IUserInput
{
    private IWriter _writer;
    private IReader _reader;

    public UserInput(IWriter writer, IReader reader)
    {
        _writer = writer;
        _reader = reader;
    }

    public int PromptPlayerMove()
    {
        _writer.Write("Hit or stay? (Hit = 1, Stay = 0): ");
        var input = _reader.ReadLine();
        while (!UserValidation.IsOneOrZeroOnly(input))
        {
            _writer.Write("Please enter a valid input. Hit = 1, Stay = 0: ");
            input = _reader.ReadLine();
        }

        return int.Parse(input!);
    }
}