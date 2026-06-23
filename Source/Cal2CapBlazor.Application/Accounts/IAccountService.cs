using Cal2CapBlazor.Application.Accounts.DataTransferObjects;

namespace Cal2CapBlazor.Application.Accounts;
public interface IAccountService {
    Task CreateAccount(CreateAccountDTO createDTO);
    Task ChangeEmail(ChangeEmailDTO emailDTO);
    Task ChangePassword(ChangePasswordDTO passwordDTO);
    Task ChangeDisplayName(ChangeDisplayNameDTO nameDTO);
    Task DeleteAccount(DeleteAccountDTO deleteDTO);
}