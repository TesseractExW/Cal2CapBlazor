using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

public sealed record MealName(string Value)
{
    public static readonly int MaximumLength = 50;

    public static Result<MealName> Create(string mealName)
    {
        if (string.IsNullOrWhiteSpace(mealName))
        {
            return Result<MealName>.Failure(new EmptyError("MealNamel", "meal name"));
        }
        else if (mealName.Length > MaximumLength)
        {
            return Result<MealName>.Failure(new LengthError("MealNamel", "meal name", 0, MaximumLength));
        }

        return Result<MealName>.Success(new MealName(mealName));
    }
}