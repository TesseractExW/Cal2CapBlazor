using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Application.Meals;

namespace Cal2CapBlazor.Infrastructure.EFCore.Repositories;

public class EFCoreMealRepository : IMealRepository
{
    public async Task AddMealAsync(MealEntity meal, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMealAsync(MealEntity meal, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMealAsync(MealEntity meal, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<MealEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MealEntity>> GetMealsByDateTimeAsync(
        Guid accountId, DateTime startDateTime,
        DateTime endDateTime,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MealEntity>> GetPagedMealsAsync(
        Guid accountId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}