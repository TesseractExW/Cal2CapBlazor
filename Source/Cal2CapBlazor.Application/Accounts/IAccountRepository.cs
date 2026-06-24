using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Accounts;

public interface IAccountRepository 
{
    Task AddAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);

    Task UpdateAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);

    Task DeleteAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);

    Task<AccountEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<AccountEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}