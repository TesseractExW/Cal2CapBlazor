using Cal2CapBlazor.Application.Common.Extensions;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Domain.Meals.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cal2CapBlazor.Application.Meals.Commands;
public record CreateMealCommand(
    string MealName, 
    string MealDetails,
    int MealType,
    DateTime InTakeTime)
    : IRequest<Result>;

internal sealed class CreateMealCommandHandler(
    IMealRepository mealRepository,
    IUserContext userContext)
    : IRequestHandler<CreateMealCommand, Result>
{
    public async Task<Result> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        Meal meal = new Meal(
            Guid.CreateVersion7(),
            userContext.Id,
            MealName.Create(request.MealName).Value,
            MealDetails.Create(request.MealDetails).Value,
            (MealType)request.MealType,
            request.InTakeTime,
            NutrientProfile.None
        );
        return await mealRepository.AddMealAsync(meal);
    }
}

public class CreateMealCommandValidator : AbstractValidator<CreateMealCommand>
{
    public CreateMealCommandValidator(){
        RuleFor(e => e.MealName).MustBeValueObject(MealName.Create);
        RuleFor(e => e.MealDetails).MustBeValueObject(MealDetails.Create);

        RuleFor(e => e.MealType)
            .LessThan((int)MealType.Heavy * 2)
            .WithErrorCode("The meal type provided is invalid.");
    }
}