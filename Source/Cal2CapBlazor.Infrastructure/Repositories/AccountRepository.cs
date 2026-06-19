using Cal2CapBlazor.Domain.Entites;
using Cal2CapBlazor.Application.Interfaces.Repositories;
using Cal2CapBlazor.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Infrastructure.Repositories;
/// <summary> Implementation of <see cref="IAccountRepository"/> </summary>
public class AccountRepository : IAccountRepository {
    #region Private properties
    /// <summary> Factory used to generate short-lived database context instances. </summary>
    private readonly IDbContextFactory<AppMainDbContext> _factory;
    #endregion

    #region Class constructor
    /// <summary> Initializes the repository with a database context factory. </summary>
    /// <param name="factory"> The factory used to create thread-safe context instances. </param>
    public AccountRepository(IDbContextFactory<AppMainDbContext> factory) {
        _factory = factory;
    }
    #endregion

    #region Retriving account
    public async Task<Account?> GetByIdAsync(int id) {
        using var context = await _factory.CreateDbContextAsync();
        return await context.Accounts
            .AsNoTracking()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<Account?> GetByEmailAsync(string email) {
        using var context = await _factory.CreateDbContextAsync();
        return await context.Accounts
            .AsNoTracking()
            .Where(e => e.Email == email)
            .FirstOrDefaultAsync();
    }
    #endregion

    #region Account operations
    public async Task AddAsync(Account account) {
        using var context = await _factory.CreateDbContextAsync();
        context.Accounts.Add(account);
        await context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Account account) {
        using var context = await _factory.CreateDbContextAsync();
        context.Accounts.Update(account);
        await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        using var context = await _factory.CreateDbContextAsync();
        await context.Accounts
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();
    }
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) {
        using var context = await _factory.CreateDbContextAsync();
        return await context.SaveChangesAsync() > 0;
    }
    #endregion
}