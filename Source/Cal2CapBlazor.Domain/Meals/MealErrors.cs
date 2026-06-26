using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Domain.Meals;

public sealed class MealErrors : ErrorContainer<MealErrors>
{
    public static ResultError MealNameNullOrWhiteSpace    =>
        Create("The meal name cannot be empty or whitespaces.");

    public static ResultError MealNameLengthOutOfRange    =>
        Create($"The length of meal name must not exceed {MealConstants.MaximumMealNameLength}.");

    public static ResultError MealDetailsLengthOutOfRange =>
        Create($"The length of meal details must not exceed {MealConstants.MaximumMealDetailsLength}.");

    public static ResultError IntakeTimeFutureOccurrence  =>
        Create("The intake time cannot be set to future.");

    public static ResultError NutritionNegativeValue      =>
        Create("A value of nutrition cannot be negative.");
}