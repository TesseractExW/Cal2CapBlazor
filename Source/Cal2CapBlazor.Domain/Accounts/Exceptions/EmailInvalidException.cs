using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Accounts.Exceptions;
public class EmailInvalidException : AccountValidationException {
    public EmailInvalidException()
        : base("Meal name cannot be null, empty or consists only of white-space characters.") {}
    public EmailInvalidException(string message)
        : base(message) {}
    public EmailInvalidException(string message, Exception innerException)
        : base(message, innerException) {}

    public static void ThrowIfInvalid(
        [NotNull] string? argument, 
        [CallerArgumentExpression(nameof(argument))] string? paramName = null) 
    {
        if (string.IsNullOrWhiteSpace(argument)) {
            throw new EmailInvalidException(
                $"Invalid account email for '{paramName}'. Email cannot be null, empty, or consist only of white-space characters."
            );
        } else if (!EmailRegex.IsMatch(argument)) {
            throw new EmailInvalidException(
                $"Invalid account email format for '{paramName}'. Provided value '{argument}' is not a valid email address."
            );
        }
    }
}