using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Accounts.Exceptions;
public class DisplayNameInvalidException : AccountValidationException {
    private static readonly int MinLimit = DisplayNameLength.Item1;
    private static readonly int MaxLimit = DisplayNameLength.Item2;

    public DisplayNameInvalidException()
        : base($"Display name must be between {MinLimit} and {MaxLimit} characters long.") {}

    public DisplayNameInvalidException(string message)
        : base(message) {}

    public DisplayNameInvalidException(string message, Exception innerException)
        : base(message, innerException) {}

    public static void ThrowIfInvalid(
        [NotNull] string? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(argument)) {
            throw new DisplayNameInvalidException(
                $"Invalid value for '{paramName}'. Display name cannot be null, empty, or consist entirely of whitespace."
            );
        } else if (argument.Length < MinLimit || argument.Length > MaxLimit) {
            throw new DisplayNameInvalidException(
                $"Display name length constraint violated for '{paramName}'. The password length is {argument.Length}, but it must be between {MinLimit} and {MaxLimit} characters."
            );
        }
    }
}