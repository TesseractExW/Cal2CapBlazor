using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Domain.Accounts;

public sealed class AccountErrors : ErrorContainer<AccountErrors>
{
    public static ResultError EmailNullOrWhiteSpace          => 
        Create("The email address cannot be empty or consist entirely of whitespace.");

    public static ResultError EmailInvalidFormat             => 
        Create("The provided email address is not in a valid format.");

    public static ResultError DisplayNameLengthOutOfRange    => 
        Create($"The display name length must be between {AccountConstants.MinimumDisplayNameLength} and {AccountConstants.MaximumDisplayNameLength} characters.");
}