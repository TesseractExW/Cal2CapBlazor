using Cal2CapBlazor.Domain.Entites;

namespace Cal2CapBlazor.Application.Interfaces.Repositories;
/// <summary>Defines data-access operations for managing user meals.</summary>
public interface IMealRepository {
    #region Retrieving meals
    /// <summary>Retrieves a paginated list of meals for a specific account.</summary>
    /// <param name="accountId">The unique identifier of the account owning the meals.</param>
    /// <param name="startIndex">The zero-based starting index (offset) of the records to retrieve.</param>
    /// <param name="pageSize">The maximum number of meal records to return.</param>
    /// <returns>A list of matching meal entities.</returns>
    Task<List<Meal>> GetPagedMealsIdAsync(int accountId, int startIndex, int pageSize);
    /// <summary>Retrieves meals logged within a specific date and time range for an account.</summary>
    /// <param name="accountId">The unique identifier of the account owning the meals.</param>
    /// <param name="startDate">The inclusive start date and time of the range.</param>
    /// <param name="endDate">The inclusive end date and time of the range.</param>
    /// <returns>A list of meal entities logged within the specified timeframe.</returns>
    Task<List<Meal>> GetMealsByDateRangeAsync(int accountId, DateTime startDate, DateTime endDate);
    #endregion

    #region Meal operations
    /// <summary>Adds a new meal to a specific user account.</summary>
    /// <param name="accountId">The unique identifier of the account the meal belongs to.</param>
    /// <param name="meal">The meal entity to create.</param>
    Task AddAsync(int accountId, Meal meal);
    /// <summary>Updates an existing meal's details.</summary>
    /// <param name="meal">The meal entity containing updated values.</param>
    Task UpdateAsync(Meal meal);
    /// <summary>Deletes a meal by its unique identifier.</summary>
    /// <param name="id">The unique identification of the meal to remove.</param>
    Task DeleteAsync(int id);
    /// <summary>Persists all tracked changes to the underlying data store.</summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>True if changes were successfully saved; otherwise, false.</returns>
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion
}