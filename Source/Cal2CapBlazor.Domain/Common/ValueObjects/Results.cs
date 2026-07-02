using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Common;
public record Result
{
    public bool IsSuccess { get; }
    public ErrorResult Error { get; }

    public Result(bool isSuccess, ErrorResult error)
    {
        if ((isSuccess  && error != ErrorResult.None) ||
            (!isSuccess && error == ErrorResult.None))
        {
            throw new ArgumentException("Invalid error state", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Failure(ErrorResult error) => new Result(false, error);
    public static Result Success() => new Result(true, ErrorResult.None);
}

public record Result<TResult>(TResult value, bool isSuccess, ErrorResult error)
    : Result(isSuccess, error) where TResult : notnull
{
    private readonly TResult _value = value;

    public TResult Value { get => GetValue(); }

    private TResult GetValue()
    {
        if (!IsSuccess)
        {
            throw new InvalidOperationException("Cannot access the value of failure result.");
        }
        return _value;
    }

    public static new Result<TResult> Failure(ErrorResult error) => new Result<TResult>(default!, false, error);
    public static Result<TResult> Success(TResult value) => new Result<TResult>(value, true, ErrorResult.None);
}