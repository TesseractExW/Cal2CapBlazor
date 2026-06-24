namespace Cal2CapBlazor.Domain.Meals.Exceptions;

public abstract class MealValidationException : Exception 
{
    #region Configuration

    protected static readonly int MaxNameLength = 50;

    #endregion

    protected MealValidationException(string message)
        : base(message) {}

    protected MealValidationException(string message, Exception innerException)
        : base(message, innerException) {}
}