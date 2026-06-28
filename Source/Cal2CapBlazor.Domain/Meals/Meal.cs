using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals;

public class Meal(
    Guid id, 
    Guid accountId, 
    MealName name, 
    MealDetails details, 
    DateTime inTakeTime, 
    NutrientProfile nutrientProfile)
{
    public Guid Id { get; } = id;
    public Guid AccountId { get; } = accountId;

    public MealName MealName { get; private set; } = name;
    public MealDetails MealDetails { get; private set; } = details;

    public DateTime InTakeTime { get; private set; } = inTakeTime;
    public NutrientProfile NutritentProfile { get; private set; } = nutrientProfile;

    public Result UpdateMealName(MealName mealName)
    {
        if (mealName == MealName)
        {
            return Result.Failure(new UnchangedError("MealName", "meal name"));
        }

        return Result.Success();
    }

    public Result UpdateMealDetails(MealDetails mealDetails)
    {
        if (mealDetails == MealDetails)
        {
            return Result.Failure(new UnchangedError("MealDetails", "meal details"));
        }

        return Result.Success();
    }

    public Result UpdateIntakeTime(DateTime inTakeTime)
    {
        if (inTakeTime == InTakeTime)
        {
            return Result.Failure(new UnchangedError("InTakeTime", "intake time"));
        }

        return Result.Success();
    }

    public Result UpdateNutrientProfile(NutrientProfile nutrientProfile)
    {
        if (nutrientProfile == NutritentProfile)
        {
            return Result.Failure(new UnchangedError("NutrientProfile", "nutrient profile"));
        }

        return Result.Success();
    }
}