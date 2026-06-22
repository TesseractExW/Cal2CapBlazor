using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Meals.Exceptions;
public class InTakeTimeInvalidException : MealValidationException {
    public InTakeTimeInvalidException()
        : base("Meal intake time cannot be null or set to a future date and time.") {}
    public InTakeTimeInvalidException(string message)
        : base(message) {}
    public InTakeTimeInvalidException(string message, Exception innerException)
        : base(message, innerException) {}

    public static void ThrowIfInvalid(
        [NotNull] DateTime? argument, 
        [CallerArgumentExpression(nameof(argument))] string? paramName = null) 
    {
        if (argument is null) {
            throw new InTakeTimeInvalidException(
                $"Invalid intake time for '{paramName}'. The date and time cannot be null."
            );
        } else if (argument > DateTime.UtcNow) {
            throw new InTakeTimeInvalidException(
                $"Invalid intake time for '{paramName}'. Provided time '{argument:yyyy-MM-dd HH:mm:ss} UTC' cannot be in the future."
            );
        }
    }
}