namespace Cal2CapBlazor.Application.Accounts.Exceptions;
public class AccountDTOValidationException : Exception {
    protected AccountDTOValidationException(string message)
        : base(message) {}
    protected AccountDTOValidationException(string message, Exception innerException)
        : base(message, innerException) {}
}