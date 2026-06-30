using Microsoft.EntityFrameworkCore;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Application.Meals;

namespace Cal2CapBlazor.Infrastructure.Persistance;

public class MealRepository(ApplicationDbContext dbContext) : IMealRepository
{
    public async Task<Result> AddMealAsync(Meal meal, CancellationToken cancellationToken)
    {
        dbContext.Meals.Add(meal);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdateMealAsync(Meal meal, CancellationToken cancellationToken)
    {
        dbContext.Meals.Update(meal);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteMealAsync(Meal meal, CancellationToken cancellationToken)
    {
        dbContext.Meals.Remove(meal);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<Meal>> GetMealById(Guid Id, CancellationToken cancellationToken)
    {
        Meal? meal = await dbContext.Meals.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
        if (meal is null)
        {
            return Result<Meal>.Failure(new ErrorResult("Meal.NotFound", "The meal does not exist."));
        }

        return Result<Meal>.Success(meal);
    }
}