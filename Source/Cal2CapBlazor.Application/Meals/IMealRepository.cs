using Cal2CapBlazor.Domain.Meals;

namespace Cal2CapBlazor.Application.Meals;

public interface IMealRepository 
{
    // TODO : Label regions
    Task AddMealAsync(MealEntity meal, CancellationToken cancellationToken = default);

    Task UpdateMealAsync(MealEntity meal, CancellationToken cancellationToken = default);

    Task DeleteMealAsync(MealEntity meal, CancellationToken cancellationToken = default);

    Task<MealEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<MealEntity>> GetMealsByDateTimeAsync(
        Guid accountId,
        DateTime startDateTime,
        DateTime endDateTime,
        CancellationToken cancellationToken = default
    );

    Task<List<MealEntity>> GetPagedMealsAsync(
        Guid accountId, 
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default
    );
}