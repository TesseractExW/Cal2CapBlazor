using Cal2CapBlazor.Application.Accounts.DataTransferObjects;

namespace Cal2CapBlazor.Application.Accounts;
public interface IAccountService {
    protected IAccountRepository repository { get; set; }

    Task CreateAccount(CreateAccountDTO createDTO);
    Task ChangeEmail(ChangeEmailDTO emailDTO);
    Task ChangePassword(ChangePasswordDTO passwordDTO);
    Task ChangeDisplayName(ChangeDisplayNameDTO nameDTO);
    Task DeleteAccount(DeleteAccountDTO deleteDTO);
}