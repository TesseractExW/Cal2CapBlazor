using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Meals;

namespace Cal2CapBlazor.Application.Meals;

public interface IMealRepository 
{
    Task<Result> AddMealAsync(Meal meal, CancellationToken cancellationToken = default);

    Task<Result> UpdateMealAsync(Meal meal, CancellationToken cancellationToken = default);

    Task<Result> DeleteMealAsync(Meal meal, CancellationToken cancellationToken = default);

    Task<Result<Meal>> GetMealById(Guid Id, CancellationToken cancellationToken = default);
}