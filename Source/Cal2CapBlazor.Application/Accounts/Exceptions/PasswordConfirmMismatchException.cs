namespace Cal2CapBlazor.Application.Accounts.Exceptions;
public class PasswordConfirmMismatchException : PasswordValidationException {
    public PasswordConfirmMismatchException()
        : base("The new password and confirmation password don't match.") {}
    public PasswordConfirmMismatchException(string message)
        : base(message) {}
    public PasswordConfirmMismatchException(string message, Exception innerException)
        : base(message, innerException) {}
}