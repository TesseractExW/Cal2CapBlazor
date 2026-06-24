using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Meals.Exceptions;

public class NegativeQuantityException : MealValidationException
{
    public NegativeQuantityException()
        : base("Meal quantity or nutritional value cannot be negative.") {}

    public NegativeQuantityException(string message)
        : base(message) {}

    public NegativeQuantityException(string message, Exception innerException)
        : base(message, innerException) {}

    public static void ThrowIfNegative(
        int? argument, 
        [CallerArgumentExpression(nameof(argument))] string? paramName = null) 
    {
        if (argument is not null && argument < 0)
        {
            throw new NegativeQuantityException(
                $"Property/Parameter '{paramName}' failed validation: Value cannot be negative (Value passed: {argument})."
            );
        }
    }
}