using System.Text.RegularExpressions;

namespace Cal2CapBlazor.Domain.Accounts.Exceptions;

public abstract class AccountValidationException : Exception 
{
    #region Configuration

    protected static readonly (int, int)    PasswordLength      = (64, 72);
    protected static readonly (int, int)    DisplayNameLength   = (3,  24);
    protected static readonly Regex         EmailRegex          = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase, 
        TimeSpan.FromMilliseconds(250)
    );

    #endregion

    protected AccountValidationException(string message)
        : base(message) { }

    protected AccountValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}