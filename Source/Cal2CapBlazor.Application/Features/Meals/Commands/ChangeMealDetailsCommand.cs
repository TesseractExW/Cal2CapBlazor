using Cal2CapBlazor.Application.Common.Extensions;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Domain.Meals.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cal2CapBlazor.Application.Meals.Commands;
public record ChangeMealDetailsCommand(Guid Id, string MealDetails) : IRequest<Result>;

internal sealed class ChangeMealDetailsCommandHandler(
    IMealRepository mealRepository,
    IUserContext userContext)
    : IRequestHandler<ChangeMealDetailsCommand, Result>
{
    public async Task<Result> Handle(ChangeMealDetailsCommand request, CancellationToken cancellationToken)
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

        MealDetails mealDetails = MealDetails.Create(request.MealDetails).Value;
        Result updateResult = meal.UpdateMealDetails(mealDetails);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }
        return await mealRepository.UpdateMealAsync(meal);
    }
}

public class ChangeMealDetailsCommandValidator : AbstractValidator<ChangeMealDetailsCommand>
{
    public ChangeMealDetailsCommandValidator()
    {
        RuleFor(e => e.MealDetails).MustBeValueObject(MealDetails.Create);
    }
}