using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Application.Common.Exceptions;
using Cal2CapBlazor.Application.Accounts.Exceptions;
using Cal2CapBlazor.Application.Accounts.DataTransferObjects;
using Cal2CapBlazor.Application.Authentications;

namespace Cal2CapBlazor.Application.Accounts;
public class AccountService : IAccountService {
    protected IAccountRepository repository;
    protected IAuthentication    auth;

    public AccountService(IAccountRepository accountRepository, IAuthentication authentication) {
        repository = accountRepository;
        auth = authentication;
    }
    #region Private Helpers
    protected async Task<AccountEntity> GetAccountWithException() {
        if (!auth.IsAuthenticated) {
            throw new UnauthenticatedException();
        }
        AccountEntity? account = await repository.GetByIdAsync(auth.AccountId);
        if (account is null) {
            throw new NotFoundException(
                "The provided account cannot be found in database."
            );
        }
        return account;
    }
    protected void ThrowIfFailedToVerifyPassword(AccountEntity account, string password) {
        if (!BCrypt.Net.BCrypt.EnhancedVerify(password, account.Password)) {
            throw new PasswordMismatchException(
                "The provided password doesn't match the password in database."
            );
        }
    }
    #endregion

    #region Service Methods
    public async Task CreateAccount(CreateAccountDTO createDTO) {
        string email        = createDTO.Email;
        string password     = createDTO.Password;
        string displayName  = createDTO.DisplayName;

        if (await repository.GetByEmailAsync(email) is not null) {
            throw new DuplicatedException(
                "The provided email had already exists in database."
            );
        }
        AccountEntity account = new AccountEntity(email, password, displayName);
        await repository.AddAccountAsync(account);
    }
    public async Task ChangeEmail(ChangeEmailDTO emailDTO) {
        string newEmail = emailDTO.NewEmail;
        string password = emailDTO.Password;

        AccountEntity account = await GetAccountWithException();
        ThrowIfFailedToVerifyPassword(account, password);
        account.Email = newEmail;

        await repository.UpdateAccountAsync(account);
    }
    public async Task ChangePassword(ChangePasswordDTO passwordDTO) {
        string password             = passwordDTO.Password;
        string newPassword          = passwordDTO.NewPassword;
        string confirmPassword      = passwordDTO.ConfirmPassword;

        AccountEntity account = await GetAccountWithException();
        ThrowIfFailedToVerifyPassword(account, password);

        if (password == newPassword) {
            throw new NotModifiedException(
                "The new password is the same as previous one"
            );
        } else if (newPassword != confirmPassword) {
            throw new PasswordConfirmMismatchException();
        }
        string hashedNewPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword);
        account.Password = hashedNewPassword;

        await repository.UpdateAccountAsync(account);
    }
    public async Task ChangeDisplayName(ChangeDisplayNameDTO nameDTO) {
        string displayName  = nameDTO.DisplayName;

        AccountEntity account = await GetAccountWithException();
        account.DisplayName = displayName;

        await repository.UpdateAccountAsync(account);
    }
    public async Task DeleteAccount(DeleteAccountDTO deleteDTO) {
        string email    = deleteDTO.Email;
        string password = deleteDTO.Password;

        AccountEntity account = await GetAccountWithException();
        if (account.Email != email) {
            throw new NotFoundException(
                "The provided email doesn't match the email in database."
            );
        }
        ThrowIfFailedToVerifyPassword(account, password);

        await repository.DeleteAccountAsync(auth.AccountId);
    }
    #endregion
}