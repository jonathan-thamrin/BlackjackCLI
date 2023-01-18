using System.Collections.Generic;
using Blackjack.Interfaces;
using Blackjack.IO;
using Blackjack.Models;
using Xunit;

namespace Blackjack.Test.Models;

public class GameTest
{

    private RandomizerFake _randomizerFake;
    private WriterFake _writerFake;
    private IOutputMessenger _outputMessenger;


    public GameTest()
    {
        _randomizerFake = new RandomizerFake();
        _writerFake = new WriterFake();
        _outputMessenger = new OutputMessenger(_writerFake);
    }

    [Fact]
    public void GivenAGame_WhenDealerBusts_PlayerShouldWinGameAndDealerShouldLoseGame()
    {
        var readerFake = new ReaderFake(new List<string> {"0"});
        var userInput = new UserInput(_writerFake, readerFake);
        var game = new Game(_randomizerFake, _outputMessenger, userInput);
        
        game.Initialise();
        game.Start();
        
        Assert.Equal("\nDealer is at a Bust\n", _writerFake.StringBuffer[6]);
        Assert.Equal("with the hand [Ace, Diamond], [Ace, Heart], [King, Club]\n\n", _writerFake.StringBuffer[7]);
        Assert.Equal("You beat the dealer! Congratulations.\n", _writerFake.StringBuffer[8]);
    }
    
    [Fact]
    public void GivenAGame_WhenPlayerBusts_DealerShouldWinGameAndPlayerShouldLoseGame()
    {
        var readerFake = new ReaderFake(new List<string> {"1"});
        var userInput = new UserInput(_writerFake, readerFake);
        var game = new Game(_randomizerFake, _outputMessenger, userInput);
        
        game.Initialise();
        game.Start();
        
        Assert.Equal("\nYou are at a Bust\n", _writerFake.StringBuffer[4]);
        Assert.Equal("with the hand [Ace, Club], [Ace, Spade], [King, Club]\n\n", _writerFake.StringBuffer[5]);
        Assert.Equal("Dealer wins! Better luck next time.\n", _writerFake.StringBuffer[6]);
    }
}