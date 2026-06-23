using Cal2CapBlazor.Application.Accounts.DataTransferObjects;

namespace Cal2CapBlazor.Application.Accounts;
public interface AccountService : IAccountService {
    protected IAccountRepository repository;

    public AccountService(IAccountRepository accountRepository) {
        repository = accountRepository;
    }

    #region Service Methods
    public async Task CreateAccount(CreateAccountDTO createDTO) {
        string email        = createDTO.Email;
        string password     = createDTO.Password;
        string displayName  = createDTO.DisplayName;

        if (repository.GetByEmailAsync(email) is not null) {
            throw new PropertyDuplicatedException(
                "The provided email is already exists in database."
            );
        }
        AccountEntity account = new AccountEntity(email, password, displayName);
        repository.AddAccountAsync(account);
    }
    public async Task ChangeEmail(ChangeEmailDTO emailDTO) {
        string id       = emailDTO.Id;
        string email    = emailDTO.Email;
        
        AccountEntity? account = repository.GetByIdAsync(id);
        if (account is null) {
            // TODO
        }
    }
    public async Task ChangePassword(ChangePasswordDTO passwordDTO) {
        // TODO
    }
    public async Task ChangeDisplayName(ChangeDisplayNameDTO nameDTO) {
        // TODO
    }
    public async Task DeleteAccount(DeleteAccountDTO deleteDTO) {
        // TODO
    }
    #endregion
}