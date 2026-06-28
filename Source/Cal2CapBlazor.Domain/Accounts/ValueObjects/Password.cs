using System.Text.RegularExpressions;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Accounts.ValueObjects;

public sealed record Password(string Value)
{
    private static readonly Regex PasswordRegex = new Regex(
        @"^[^\s'""\\\/<>;\u0080-\uFFFF]+$", 
        RegexOptions.Compiled | RegexOptions.CultureInvariant
    );
    public static readonly int MinimumLength = 8;
    public static readonly int MaximumLength = 24;

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return Result<Password>.Failure(new EmptyError("Password", "password"));
        }
        else if (password.Contains(' '))
        {
            return Result<Password>.Failure(new WhitespaceError("Password", "password"));
        }
        else if (password.Length < MinimumLength || 
                 password.Length > MaximumLength)
        {
            return Result<Password>.Failure(new LengthError("Password", "password", MinimumLength, MaximumLength));
        }
        else if (!PasswordRegex.IsMatch(password))
        {
            return Result<Password>.Failure(new FormatError("Password", "password", "The password cannot contain any \'\"\\/<>; or non english character"));
        }

        return Result<Password>.Success(new Password(password));
    }
}