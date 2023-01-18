using System.Collections.Generic;
using Blackjack.Interfaces;

namespace Blackjack.Test;

public class ReaderFake : IReader
{
    public List<string> Commands { get; }
    private int _index;

    public ReaderFake(List<string> commands)
    {
        Commands = commands;
        _index = 0;
    }
    
    public string ReadLine()
    {
        var inputToTest = Commands[_index];
        _index++;
        return inputToTest;
    }
}