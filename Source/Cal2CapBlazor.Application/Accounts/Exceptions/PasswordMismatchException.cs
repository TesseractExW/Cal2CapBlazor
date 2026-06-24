namespace Cal2CapBlazor.Application.Accounts.Exceptions;

public class PasswordMismatchException : PasswordValidationException 
{
    public PasswordMismatchException()
        : base("Account verification failed. Please check your credentials or verification token.") {}

    public PasswordMismatchException(string message)
        : base(message) {}

    public PasswordMismatchException(string message, Exception innerException)
        : base(message, innerException) {}
}