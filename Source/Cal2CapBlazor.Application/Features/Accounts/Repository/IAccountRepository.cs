using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Accounts;

public interface IAccountRepository 
{
    Task<Result> AddAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);

    Task<Result> UpdateAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);

    Task<Result> DeleteAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);

    Task<Result<AccountEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result<AccountEntity>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}