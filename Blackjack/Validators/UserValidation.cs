using System.Text.RegularExpressions;

namespace Blackjack.Validators;

public static class UserValidation
{
    public static bool IsOneOrZeroOnly(string? input)
    {
        return input != null && Regex.IsMatch(input, @"([0-1])") && input.Length == 1;
    }
}