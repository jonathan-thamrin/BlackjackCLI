using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Interfaces;
using Blackjack.IO;
using Moq;
using Xunit;

namespace Blackjack.Test.IO;

public class UserInputTest
{
    private readonly WriterFake _writerFake;

    public UserInputTest()
    {
        _writerFake = new WriterFake();
    }

    [Theory]
    [InlineData("1")]
    [InlineData("0")]
    public void GivenPlayerHitsOrStays_ShouldReturnHitOrStayAsMove(string input)
    {
        var readerFake = new ReaderFake(new List<string> {input});
        var userInput = new UserInput(_writerFake, readerFake);

        var expectedReturnValue = int.Parse(input);
        var actualReturnValue = userInput.PromptPlayerMove();

        Assert.Equal(expectedReturnValue, actualReturnValue);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void GivenPlayerInvalidInputTwiceThenValidInput_ShouldDisplayErrorMessageAndPromptAgain(string[] inputs)
    {
        var readerMock = new Mock<IReader>();
        var sequenceSetup = readerMock.SetupSequence(reader => reader.ReadLine());
        
        foreach (var input in inputs)
        {
            sequenceSetup.Returns(input);
        }
        
        var userInput = new UserInput(_writerFake, readerMock.Object);

        var actualReturnValue = userInput.PromptPlayerMove();
        var expectedReturnValue = int.Parse(inputs.Last());
        var expectedConsoleOutput = new List<string>
        {
            "Hit or stay? (Hit = 1, Stay = 0): ",
            "Please enter a valid input. Hit = 1, Stay = 0: ",
            "Please enter a valid input. Hit = 1, Stay = 0: "
        };
        var actualConsoleOutput = _writerFake.StringBuffer;

        Assert.Equal(expectedConsoleOutput, actualConsoleOutput);
        Assert.Equal(expectedReturnValue, actualReturnValue);
    }

    private static IEnumerable<object[]> Data()
    {
        yield return new object[] {new string[] {"a", "[", "1"}};
        yield return new object[] {new string[] {"7", "=", "1"}};
    }
}