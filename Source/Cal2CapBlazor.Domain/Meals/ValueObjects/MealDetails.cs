using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

[Flags]
public enum MealType
{
    None        = 0,
    Breakfast   = 1 << 0,
    Lunch       = 1 << 1,
    Dinner      = 1 << 2,
    LateNight   = 1 << 3,
    Snack       = 1 << 4,
    Break       = 1 << 5,
    Clean       = 1 << 6,
    Heavy       = 1 << 7,
}

public sealed record MealDetails(string Text, MealType MealType)
{
    public static readonly int MaximumLength = 250;

    public static Result<MealDetails> Create(string text, MealType mealType)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Result<MealDetails>.Failure(new EmptyError("MealDetails", "meal details"));
        }
        else if (text.Length > MaximumLength)
        {
            return Result<MealDetails>.Failure(new LengthError("MealDetails", "meal details", 0, MaximumLength));
        }

        return Result<MealDetails>.Success(new MealDetails(text, mealType));
    }
}