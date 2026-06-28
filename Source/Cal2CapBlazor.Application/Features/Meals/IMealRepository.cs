using Cal2CapBlazor.Domain.Meals;

namespace Cal2CapBlazor.Application.Meals;

public interface IMealRepository 
{
    Task AddMealAsync(Meal meal, CancellationToken cancellationToken = default);

    Task UpdateMealAsync(Meal meal, CancellationToken cancellationToken = default);

    Task DeleteMealAsync(Meal meal, CancellationToken cancellationToken = default);
}