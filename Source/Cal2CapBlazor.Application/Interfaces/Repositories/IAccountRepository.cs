using Cal2CapBlazor.Domain.Entites;

namespace Cal2CapBlazor.Application.Interfaces.Repositories;
/// <summary> Defines data-access operations for managing user Accounts.</summary>
public interface IAccountRepository {
    #region Retrieving account
    /// <summary> Retrieves an account by its unique identifier. </summary>
    /// <param name="id"> The unique identifier of the account. </param>
    /// <returns> The matching account, or null if not found. </returns>
    Task<Account?> GetByIdAsync(int id);
    /// <summary> Retrieves an account by email. </summary>
    /// <param name="email">The registered email address to search for. k</param>
    /// <returns> The matching account, or null if not found. </returns>
    Task<Account?> GetByEmailAsync(string email);
    #endregion

    #region Account operation
    /// <summary> Adds a new user account to the tracking context. </summary>
    /// <param name="account">The account entity to create. </param>
    Task AddAsync(Account account);
    /// <summary> Updates an existing user account's details. </summary>
    /// <param name="account">The account entity containing updated values. </param>
    Task UpdateAsync(Account account);
    /// <summary> Deletes an account by its unique identifier. </summary>
    /// <param name="id">The unique identifier of the account to remove. </param>
    Task DeleteAsync(int id);
    /// <summary> Persists all tracked changes to the underlying data store. </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete. </param>
    /// <returns> True if changes were successfully saved; otherwise, false. </returns>
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion
}