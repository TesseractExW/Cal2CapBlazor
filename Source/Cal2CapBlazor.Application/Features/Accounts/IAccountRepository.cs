using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Application.Accounts;

public interface IAccountRepository 
{
    Task<Result> AddAccountAsync(Account account, CancellationToken cancellationToken = default);

    Task<Result> UpdateAccountAsync(Account account, CancellationToken cancellationToken = default);

    Task<Result> DeleteAccountAsync(Account account, CancellationToken cancellationToken = default);

    Task<Result<Account>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result<Account>> GetByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default);
}