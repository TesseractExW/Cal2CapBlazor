using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Accounts.ValueObjects;

public sealed record DisplayName(string Value)
{
    public static readonly int MinimumLength = 3;
    public static readonly int MaximumLength = 24;

    public static Result<DisplayName> Create(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            return Result<DisplayName>.Failure(new EmptyError("DisplayName", "display name"));
        }
        else if (displayName.Length < MinimumLength || 
                 displayName.Length > MaximumLength)
        {
            return Result<DisplayName>.Failure(new LengthError("DisplayName", "display name", MinimumLength, MaximumLength));
        }

        return Result<DisplayName>.Success(new DisplayName(displayName));
    }
}