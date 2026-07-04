using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals;
public class Meal(
    Guid id, 
    Guid accountId, 
    MealName mealName, 
    MealDetails mealDetails, 
    MealType mealType,
    DateTime inTakeTime, 
    NutrientProfile nutrientProfile)
{
    public Guid Id { get; } = id;
    public Guid AccountId { get; } = accountId;

    public MealName MealName { get; private set; } = mealName;
    public MealDetails MealDetails { get; private set; } = mealDetails; 
    public MealType MealType { get; set; } = mealType;

    public DateTime InTakeTime { get; private set; } = inTakeTime;
    public NutrientProfile NutrientProfile { get; private set; } = nutrientProfile;

    public Result UpdateMealName(MealName mealName)
    {
        if (mealName == MealName)
        {
            return Result.Failure(new UnchangedError("MealName", "meal name"));
        }

        MealName = mealName;
        return Result.Success();
    }

    public Result UpdateMealDetails(MealDetails mealDetails)
    {
        if (mealDetails == MealDetails)
        {
            return Result.Failure(new UnchangedError("MealDetails", "meal details"));
        }

        MealDetails = mealDetails;
        return Result.Success();
    }


    public Result UpdateMealType(MealType mealType)
    {
        MealType = mealType;
        return Result.Success();
    }

    public Result UpdateIntakeTime(DateTime inTakeTime)
    {
        if (inTakeTime == InTakeTime)
        {
            return Result.Failure(new UnchangedError("InTakeTime", "intake time"));
        }

        InTakeTime = inTakeTime;
        return Result.Success();
    }

    public Result UpdateNutrientProfile(NutrientProfile nutrientProfile)
    {
        if (nutrientProfile == NutrientProfile)
        {
            return Result.Failure(new UnchangedError("NutrientProfile", "nutrient profile"));
        }

        NutrientProfile = nutrientProfile;
        return Result.Success();
    }
}