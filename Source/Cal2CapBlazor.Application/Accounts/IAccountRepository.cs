using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Accounts;
public interface IAccountRepository {
    Task AddAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);
    Task UpdateAccountAsync(AccountEntity account, CancellationToken cancellationToken = default);
    Task DeleteAccountAsync(int id, CancellationToken cancellationToken = default);
    Task<AccountEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}