namespace Cal2CapBlazor.Application.Accounts.Exceptions;
public class AccountNotFoundException : AccountDTOValidationException {
    protected AccountNotFoundException(string message)
        : base(message) {}
    protected AccountNotFoundException(string message, Exception innerException)
        : base(message, innerException) {}
}