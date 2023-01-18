using Blackjack.Validators;
using Xunit;

namespace Blackjack.Test.Validators;

public class UserValidationTest
{
    [Theory]
    [InlineData("", false)]
    [InlineData("abc", false)]
    [InlineData("0", true)]
    [InlineData("11", false)]
    [InlineData("1", true)]
    public void GivenUserInput_WhenCheckingInputIsValidOrNot_ShouldReturnValidity(string input, bool expectedResult)
    {
        var actualResult = UserValidation.IsOneOrZeroOnly(input);

        Assert.Equal(expectedResult, actualResult);
    }
}