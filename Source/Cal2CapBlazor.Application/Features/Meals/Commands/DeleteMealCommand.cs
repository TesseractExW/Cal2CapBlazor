using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using MediatR;

namespace Cal2CapBlazor.Application.Meals.Commands;
public record DeleteMealCommand(Guid Id) : IRequest<Result>;

internal sealed class DeleteMealCommandHandler(
    IMealRepository mealRepository,
    IUserContext userContext)
    : IRequestHandler<DeleteMealCommand, Result>
{
    public async Task<Result> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
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

        return await mealRepository.DeleteMealAsync(meal);
    }
}