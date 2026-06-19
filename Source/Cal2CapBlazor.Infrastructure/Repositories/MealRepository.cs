using Cal2CapBlazor.Domain.Entites;
using Cal2CapBlazor.Application.Interfaces.Repositories;
using Cal2CapBlazor.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Infrastructure.Repositories;
/// <summary> Implementation of <see cref="IMealRepository"/> </summary>
public class MealRepository : IMealRepository {
    #region Private properties
    /// <summary> Factory used to generate short-lived database context instances.</summary>
    private readonly IDbContextFactory<AppMainDbContext> _factory;
    #endregion

    #region Class constructor
    /// <summary> Initializes the repository with a database context factory. </summary>
    /// <param name="factory"> The factory used to create thread-safe context instances. </param>
    public MealRepository(IDbContextFactory<AppMainDbContext> factory) {
        _factory = factory;
    }
    #endregion

    #region Retriving meals
    public async Task<List<Meal>> GetMealsByDateRangeAsync(int accountId, DateTime startDate, DateTime endDate) {
        using var context = await _factory.CreateDbContextAsync();
        return await context.Meals
            .AsNoTracking()
            .Where(e => e.AccountId == accountId)
            .Where(e => startDate <= e.InTakeTime && e.InTakeTime <= endDate)
            .ToListAsync();
    }
    public async Task<List<Meal>> GetPagedMealsIdAsync(int accountId, int startIndex, int pageSize) {
        using var context = await _factory.CreateDbContextAsync();
        return await context.Meals
            .AsNoTracking()
            .Where(e => e.AccountId == accountId)
            .OrderBy(e => e.InTakeTime)
            .Skip(startIndex)
            .Take(pageSize)
            .ToListAsync();
    }
    #endregion

    #region Meal operations
    public async Task AddAsync(int accountId, Meal meal) {
        using var context = await _factory.CreateDbContextAsync();
        context.Meals.Add(meal);
        await context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Meal meal) {
        using var context = await _factory.CreateDbContextAsync();
        context.Meals.Update(meal);
        await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        using var context = await _factory.CreateDbContextAsync();
        await context.Meals
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();
    }
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) {
        using var context = await _factory.CreateDbContextAsync();
        return await context.SaveChangesAsync() > 0;
    }
    #endregion
}