using Cal2CapBlazor.Domain.Common.Results;
using Cal2CapBlazor.Domain.Meals.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals;

public class MealEntity {
    #region Description Backing Fields

    private DateTime   _inTakeTime;
    private string     _mealName    = string.Empty;
    private string     _mealDetails = string.Empty;
    private Nutritions _nutritions  = Nutritions.None;
    
    #endregion

    #region Descriptions

    public Guid       Id            { get; }
    public Guid       AccountId     { get; }
    public DateTime   InTakeTime    { get => _inTakeTime;   }
    public string     MealName      { get => _mealName;     }
    public string     MealDetails   { get => _mealDetails;  }
    public Nutritions Nutritions    { get => _nutritions;   }

    #endregion

    protected MealEntity(Guid accountId)
    {
        AccountId = accountId;
        Id = Guid.CreateVersion7();
    }

    public static Result<MealEntity> Create(
        Guid       accountId,
        DateTime   inTakeTime,
        string     mealName,
        string     mealDetails,
        Nutritions nutritions)
    {
        Result result;
        MealEntity meal = new MealEntity(accountId);

        result = meal.SetInTakeTime(inTakeTime);
        if (!result.IsSuccess)
        {
            return Result<MealEntity>.Failure(result.Error);
        }

        result = meal.SetMealName(mealName);
        if (!result.IsSuccess)
        {
            return Result<MealEntity>.Failure(result.Error);
        }

        result = meal.SetMealDetails(mealDetails);
        if (!result.IsSuccess)
        {
            return Result<MealEntity>.Failure(result.Error);
        }

        return Result<MealEntity>.Success(meal);
    }
    #region Setters

    public Result SetInTakeTime(DateTime inTakeTime)
    {
        if (inTakeTime > DateTime.UtcNow)
        {
            return Result.Failure(MealErrors.IntakeTimeFutureOccurrence);
        }

        _inTakeTime = inTakeTime;
        return Result.Success();
    }

    public Result SetMealName(string mealName)
    {
        if (string.IsNullOrWhiteSpace(mealName))
        {
            return Result.Failure(MealErrors.MealNameNullOrWhiteSpace);
        }
        else if (mealName.Length > MealConstants.MaximumMealNameLength)
        {
            return Result.Failure(MealErrors.MealNameLengthOutOfRange);
        }

        _mealName = mealName;
        return Result.Success();
    }

    public Result SetMealDetails(string mealDetails)
    {
        if (mealDetails.Length > MealConstants.MaximumMealDetailsLength)
        {
            return Result.Failure(MealErrors.MealDetailsLengthOutOfRange);
        }

        _mealDetails = mealDetails;
        return Result.Success();
    }

    #endregion
}