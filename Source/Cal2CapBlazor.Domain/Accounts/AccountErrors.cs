using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Accounts;

public static class AccountErrors
{
    public static readonly ResultError EmailNullOrWhiteSpace =
        new ResultError(
            "AccountErrors.EmailNullOrWhiteSpace", 
            "Email cannot be null, empty or whitespaces."
        );

    public static readonly ResultError EmailInvalidFormat =
        new ResultError(
            "AccountErrors.EmailInvalidFormat",
            "Email must follow AccountConstants.EmailRegex."
        );

    public static readonly ResultError DisplayNameLengthOutOfRange =
        new ResultError(
            "AccountErrors.DisplayNameLengthOutOfRange",
            "Length of display name must be between AccountConstants.MinimumDisplayNameLength and AccountConstants.MaximumDisplayNameLength"
        );

    public static readonly ResultError HashedPasswordLengthOutOfRange =
        new ResultError(
            "AccountErrors.HashedPasswordLengthOutOfRange",
            "Length of password must be between AccountConstants.MinimumHashedPasswordLength and AccountConstants.MaximumHashedPasswordLength"
        );
}