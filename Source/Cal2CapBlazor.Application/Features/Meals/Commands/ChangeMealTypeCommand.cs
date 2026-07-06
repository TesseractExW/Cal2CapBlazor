using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Domain.Meals.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cal2CapBlazor.Application.Meals.Commands;
public record ChangeMealTypeCommand(Guid Id, int MealType) : IRequest<Result>;

internal sealed class ChangeMealTypeCommandHandler(
    IMealRepository mealRepository,
    IUserContext userContext)
    : IRequestHandler<ChangeMealTypeCommand, Result>
{
    public async Task<Result> Handle(ChangeMealTypeCommand request, CancellationToken cancellationToken)
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

        bool isDefined = Enum.IsDefined((MealType)request.MealType);
        if (!isDefined)
        {
            return Result.Failure(new ErrorResult("ChangeMeal.InvalidMealEnum", "The meal type is corrupted."));
        }

        Result updateResult = meal.UpdateMealType((MealType)request.MealType);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }
        return await mealRepository.UpdateMealAsync(meal);
    }
}

public class ChangeMealTypeCommandValidator : AbstractValidator<ChangeMealTypeCommand>
{

    public ChangeMealTypeCommandValidator()
    {
        RuleFor(e => e.MealType)
            .LessThan((int)MealType.Heavy * 2 - 1)
            .WithErrorCode("The meal type provided is invalid.");
    }
}