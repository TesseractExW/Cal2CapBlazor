using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

public sealed record MealDetails(string Text)
{
    public static readonly int MaximumLength = 250;

    public static Result<MealDetails> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Result<MealDetails>.Failure(new EmptyError("MealDetails", "meal details"));
        }
        else if (text.Length > MaximumLength)
        {
            return Result<MealDetails>.Failure(new LengthError("MealDetails", "meal details", 0, MaximumLength));
        }

        return Result<MealDetails>.Success(new MealDetails(text));
    }
}