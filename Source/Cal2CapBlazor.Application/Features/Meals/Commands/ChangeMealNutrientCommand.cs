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

public record ChangeMealNutrientCommand(
    Guid Id, 
    // Macronutritions
    int? Calorie,
    int? Carbohydrate,
    int? Protein,
    int? Fat,
    // Minerals
    int? Calcium,
    int? Iron,
    int? Magnesium,
    int? Zinc,
    // Vitamins
    int? VitaminA,
    int? VitaminB,
    int? VitaminC,
    int? VitaminD,
    int? VitaminE)
    : IRequest<Result>;

[RequireRole("User")]
internal sealed class ChangeMealNutrientCommandHanslder(
    IMealRepository mealRepository,
    ICurrentUserService currentUser)
    : IRequestHandler<ChangeMealNutrientCommand, Result>
{
    public async Task<Result> Handle(ChangeMealNutrientCommand request, CancellationToken cancellationToken)
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

        NutrientProfile nutrient = new NutrientProfile(
            Calorie.Create(request.Calorie).Value,
            // Macronutrients
            Weight.Create(request.Carbohydrate).Value,
            Weight.Create(request.Protein).Value,
            Weight.Create(request.Fat).Value,
            // Minerals
            Weight.Create(request.Calcium).Value,
            Weight.Create(request.Iron).Value,
            Weight.Create(request.Magnesium).Value,
            Weight.Create(request.Zinc).Value,
            // Vitamins
            Weight.Create(request.VitaminA).Value,
            Weight.Create(request.VitaminB).Value,
            Weight.Create(request.VitaminC).Value,
            Weight.Create(request.VitaminD).Value,
            Weight.Create(request.VitaminE).Value
        );

        Result updateResult = meal.UpdateNutrientProfile(nutrient);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }

        return await mealRepository.UpdateMealAsync(meal);
    }
}

public class ChangeMealNutrientCommandValidator : AbstractValidator<ChangeMealNutrientCommand>
{
    public ChangeMealNutrientCommandValidator()
    {
        RuleFor(e => e.Calorie).MustBeValueObject(Calorie.Create);
        RuleFor(e => e.Carbohydrate).MustBeValueObject(Weight.Create);
        RuleFor(e => e.Protein).MustBeValueObject(Weight.Create);
        RuleFor(e => e.Fat).MustBeValueObject(Weight.Create);
        
        RuleFor(e => e.Calcium).MustBeValueObject(Weight.Create);
        RuleFor(e => e.Iron).MustBeValueObject(Weight.Create);
        RuleFor(e => e.Magnesium).MustBeValueObject(Weight.Create);
        RuleFor(e => e.Zinc).MustBeValueObject(Weight.Create);

        RuleFor(e => e.VitaminA).MustBeValueObject(Weight.Create);
        RuleFor(e => e.VitaminB).MustBeValueObject(Weight.Create);
        RuleFor(e => e.VitaminC).MustBeValueObject(Weight.Create);
        RuleFor(e => e.VitaminD).MustBeValueObject(Weight.Create);
        RuleFor(e => e.VitaminE).MustBeValueObject(Weight.Create);
    }
}