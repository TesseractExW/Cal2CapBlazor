using Cal2CapBlazor.Application.Accounts;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Infrastructure.Persistence.Repositories;

public class AccountRepository(ApplicationDbContext dbContext) : IAccountRepository
{
    public async Task<Result> AddAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        await dbContext.Accounts.AddAsync(account, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    public async Task<Result> UpdateAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        dbContext.Accounts.Update(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        dbContext.Accounts.Remove(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<Account>> GetByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default)
    {
        Account? account = await dbContext.Accounts
            .FirstOrDefaultAsync(e => e.EmailAddress == email, cancellationToken);
        if (account is null)
        {
            return Result<Account>.Failure(new ErrorResult("Account.NotFound", "The account does not exist."));
        }

        return Result<Account>.Success(account);
    }

    public async Task<Result<Account>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Account? account = await dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (account is null)
        {
            return Result<Account>.Failure(new ErrorResult("Account.NotFound", "The account does not exist."));
        }

        return Result<Account>.Success(account);
    }
}