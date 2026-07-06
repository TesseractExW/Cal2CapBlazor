using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Domain.Meals.ValueObjects;
using MediatR;

namespace Cal2CapBlazor.Application.Meals.Commands;
public record ChangeMealInTakeTimeCommand(
    Guid Id, 
    DateTime InTakeTime) 
    : IRequest<Result>;

internal sealed class ChangeMealInTakeTimeCommandHandler(
    IMealRepository mealRepository,
    IUserContext userContext)
    : IRequestHandler<ChangeMealInTakeTimeCommand, Result>
{
    public async Task<Result> Handle(ChangeMealInTakeTimeCommand request, CancellationToken cancellationToken)
    {
        Result<Meal> mealResult = await mealRepository.GetMealById(request.Id);
        if (!mealResult.IsSuccess)
        {
            return Result.Failure(new ErrorResult("ChangeMeal.MealNotFound", "The meal cannot be found."));
        }

        Meal meal = mealResult.Value;
        if (meal.AccountId != userContext.Id)
        {
            return Result.Failure(new ErrorResult("ChangeMeal.InvalidOwnership", "The meal belongs to different ownership."));
        }

        Result updateResult = meal.UpdateIntakeTime(request.InTakeTime);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }
        return await mealRepository.UpdateMealAsync(meal);
    }
}