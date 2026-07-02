using System.Text.RegularExpressions;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Accounts.ValueObjects;
public partial record EmailAddress(string Value)
{
    [GeneratedRegex( @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
        RegexOptions.IgnoreCase | RegexOptions.Compiled, 
        matchTimeoutMilliseconds: 250)]
    public static partial Regex EmailFormat();

    public static readonly int MaximumLength = 90;

    public static Result<EmailAddress> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<EmailAddress>.Failure(new EmptyError("Email", "email address"));
        }
        else if (email.Contains(' '))
        {
            return Result<EmailAddress>.Failure(new WhitespaceError("Email", "email address"));
        }
        else if (email.Length > MaximumLength)
        {
            return Result<EmailAddress>.Failure(new WhitespaceError("Email", "email address"));
        }
        else if (!EmailFormat().IsMatch(email))
        {
            return Result<EmailAddress>.Failure(new ErrorResult("Email.FormatError", "The email adress is not a valid email address."));
        }

        return Result<EmailAddress>.Success(new EmailAddress(email));
    }
}