using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Accounts.Exceptions;
public class PasswordInvalidException : AccountValidationException {
    private static readonly int MinLimit = PasswordLength.Item1;
    private static readonly int MaxLimit = PasswordLength.Item2;

    public PasswordInvalidException()
        : base($"Password must be between {MinLimit} and {MaxLimit} characters long.") {}

    public PasswordInvalidException(string message)
        : base(message) {}

    public PasswordInvalidException(string message, Exception innerException)
        : base(message, innerException) {}

    public static void ThrowIfInvalid(
        [NotNull] string? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(argument)) {
            throw new PasswordInvalidException(
                $"Invalid value for '{paramName}'. Password cannot be null, empty, or consist entirely of whitespace."
            );
        } else if (argument.Length < MinLimit || argument.Length > MaxLimit) {
            throw new PasswordInvalidException(
                $"Password length constraint violated for '{paramName}'. The password length is {argument.Length}, but it must be between {MinLimit} and {MaxLimit} characters."
            );
        }
    }
}