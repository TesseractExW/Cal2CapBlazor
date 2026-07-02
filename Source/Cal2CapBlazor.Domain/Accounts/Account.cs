using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;

namespace Cal2CapBlazor.Domain.Accounts;

public class Account(
    Guid id,
    EmailAddress emailAddress, 
    HashedPassword hashedPassword, 
    DisplayName displayName)
{
    public Guid Id { get; } = id;

    public EmailAddress EmailAddress { get; private set; } = emailAddress;
    public DisplayName DisplayName { get; private set; } = displayName;
    public HashedPassword HashedPassword { get; private set; } = hashedPassword;

    public Result UpdateEmailAddress(EmailAddress emailAddress)
    {
        if (emailAddress == EmailAddress)
        {
            return Result.Failure(new UnchangedError("Email", "email"));
        }
        return Result.Success();
    }

    public Result UpdateHashedPassword(HashedPassword hashedPassword)
    {
        if (hashedPassword == HashedPassword)
        {
            throw new ArgumentException("The new hashed password is the same as current hashed password.");
        }
        return Result.Success();
    }

    public Result UpdateDisplayName(DisplayName displayName)
    {
        if (displayName == DisplayName)
        {
            return Result.Failure(new UnchangedError("DisplayName", "display name"));
        }
        return Result.Success();
    }
}