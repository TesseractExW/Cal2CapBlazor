namespace Cal2CapBlazor.Domain.Common.ValueObjects;

public record ResultError(string Id, string Message)
{
    public static readonly ResultError None = new ResultError(string.Empty, string.Empty);
}