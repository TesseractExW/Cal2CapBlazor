using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Meals.Exceptions;
public class NameInvalidException : MealValidationException {
    public NameInvalidException()
        : base($"Meal name cannot be null, empty, or exceed {MaxNameLength} characters.") {}
    public NameInvalidException(string message)
        : base(message) {}
    public NameInvalidException(string message, Exception innerException)
        : base(message, innerException) {}

    public static void ThrowIfInvalid(
        [NotNull] string? argument, 
        [CallerArgumentExpression(nameof(argument))] string? paramName = null) 
    {
        if (string.IsNullOrWhiteSpace(argument)) {
            throw new NameInvalidException(
                $"Invalid meal name for '{paramName}'. Meal name cannot be null, empty, or consist only of white-space characters."
            );
        } else if (argument.Length > MaxNameLength) {
            throw new NameInvalidException(
                $"Invalid meal name length for '{paramName}'. Provided value exceeds the maximum limit of {MaxNameLength} characters (Length: {argument.Length})."
            );
        }
    }
}