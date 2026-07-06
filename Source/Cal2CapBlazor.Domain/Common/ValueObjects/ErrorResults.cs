namespace Cal2CapBlazor.Domain.Common.ValueObjects;
public record ErrorResult(string Id, string Message)
{
    public static readonly ErrorResult None = new ErrorResult(string.Empty, string.Empty);
}

public record EmptyError(string ClassName, string Name, string? Message = null)
    : ErrorResult(
        $"{ClassName}.EmptyError",
        Message ?? $"The {Name} cannot be empty or consist only of whitespaces."
    );

public record FormatError(string ClassName, string Name, string? Message = null)
    : ErrorResult(
        $"{ClassName}.FormatError",
        Message ?? $"The provided {Name} is not a valid format."
    );

public record LengthError(
    string ClassName,
    string Name,
    int MinimumLength,
    int MaximumLength,
    string? Message = null)
    : ErrorResult(
        $"{ClassName}.LengthError",
        Message ?? (MinimumLength == 0 
            ? $"The {Name} cannot exceed {MaximumLength} characters." 
            : $"The {Name} must be between {MinimumLength} and {MaximumLength} characters.")
    );

public record UnchangedError(string ClassName, string Name, string? Message = null)
    : ErrorResult(
        $"{ClassName}.UnchangedError",
        Message ?? $"The new {Name} cannot be identical to current {Name}."
    );

public record WhitespaceError(string ClassName, string Name, string? Message = null)
    : ErrorResult(
        $"{ClassName}.WhitespaceError",
        Message ?? $"The {Name} cannot contain any whitespaces."
    );

public record NegativeError(
    string ClassName,
    string Name,
    string? Message = null)
    : ErrorResult(
        $"{ClassName}.NegativeError",
        Message ?? $"The {Name} cannot be negative."
    );