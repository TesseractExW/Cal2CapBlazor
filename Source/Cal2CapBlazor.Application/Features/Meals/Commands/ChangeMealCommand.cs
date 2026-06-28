using FluentValidation;
using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Domain.Meals.ValueObjects;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Common.Security;
using Cal2CapBlazor.Application.Common.Extensions;

namespace Cal2CapBlazor.Application.Meals.Commands;

public record ChangeMealNameCommand(Guid Id, string MealName) : IRequest<Result>;

[RequireRole("User")]
internal sealed class ChangeMealNameCommandHandler(
    IMealRepository mealRepository,
    ICurrentUserService currentUser)
    : IRequestHandler<ChangeMealNameCommand, Result>
{
    public async Task<Result> Handle(ChangeMealNameCommand request, CancellationToken cancellationToken)
    {
        Result<Meal> mealResult = await mealRepository.GetMealById(request.Id);
        if (!mealResult.IsSuccess)
        {
            return Result.Failure(new ErrorResult("ChangeMeal.MealNotFound", "The meal cannot be found."));
        }

        Meal meal = mealResult.Value;
        if (meal.AccountId != currentUser.AccountId)
        {
            return Result.Failure(new ErrorResult("ChangeMeal.InvalidOwnership", "The meal belongs to different ownership."));
        }

        MealName mealName = MealName.Create(request.MealName).Value;
        Result updateResult = meal.UpdateMealName(mealName);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }

        return await mealRepository.UpdateMealAsync(meal);
    }
}

public class ChangeMealNameCommandValidator : AbstractValidator<ChangeMealNameCommand>
{
    public ChangeMealNameCommandValidator()
    {
        RuleFor(e => e.MealName).MustBeValueObject(MealName.Create);
    }
}