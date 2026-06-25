using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals;

public static class MealErrors
{
    public static readonly ResultError MealNameNullOrWhiteSpace =
        new ResultError(
            "MealErrors.MealNameNullOrWhiteSpace",
            "Meal name cannot be null, empty or whitespaces."
        );

    public static readonly ResultError MealNameLengthOutOfRange =
        new ResultError(
            "MealErrors.MealNameLengthOutOfRange",
            "Length of meal name must not exceed MealConstants.MaximumMealNameLength."
        );

    public static readonly ResultError MealDetailsLengthOutOfRange =
        new ResultError(
            "MealErrors.MealDetailsLengthOutOfRange",
            "Length of meal details must not exceed MealConstants.MaximumMealDetailsLength."
        );

    public static readonly ResultError IntakeTimeFutureOccurrence =
        new ResultError(
            "MealErrors.IntakeTimeFutureOccurrence",
            "Intake time cannot be set to future."
        );

    public static readonly ResultError NutritionNegativeValue =
        new ResultError(
            "MealErrosrs.NutritionNegativeAmount",
            "Value of nutrition cannot be negative."
        );
}