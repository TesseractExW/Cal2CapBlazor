using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Domain.Accounts.ValueObjects;
public sealed record HashedPassword(string Value)
{
    public static readonly int MaximumLength = 90;

    public static Result<HashedPassword> Create(string hashedPassword)
    {
        if (hashedPassword is null)
        {
            throw new ArgumentNullException("The hashed password must never be null.");
        }
        else if (hashedPassword.Length > MaximumLength)
        {
            throw new ArgumentOutOfRangeException("The hased password exceeds 90 characters.");
        }
        
        return Result<HashedPassword>.Success(new HashedPassword(hashedPassword));
    }
}