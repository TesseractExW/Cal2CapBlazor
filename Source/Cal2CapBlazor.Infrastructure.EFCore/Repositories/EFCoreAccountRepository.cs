using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Application.Accounts;

namespace Cal2CapBlazor.Infrastructure.EFCore.Repositories;

public class EFCoreAccountRepository : IAccountRepository 
{
    public async Task AddAccountAsync(AccountEntity account, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAccountAsync(AccountEntity account, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAccountAsync(AccountEntity account, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public async Task<AccountEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) 
    {
        throw new NotImplementedException();
    }
}
