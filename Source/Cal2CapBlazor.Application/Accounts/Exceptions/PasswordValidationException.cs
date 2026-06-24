namespace Cal2CapBlazor.Application.Accounts.Exceptions;

public class PasswordValidationException : Exception
{
    public PasswordValidationException()
        : base("TODO") {}

    public PasswordValidationException(string message)
        : base(message) {}

    public PasswordValidationException(string message, Exception innerException)
        : base(message, innerException) {}
}