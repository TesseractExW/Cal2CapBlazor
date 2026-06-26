namespace Cal2CapBlazor.Domain.Common;

public class Result<T> : Result where T : notnull
{
    private readonly T _value;

    public T Value { get => GetValue(); }

    public Result(T value, bool isSuccess, ResultError error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    #region Getter
    
    private T GetValue()
    {
        if (!IsSuccess)
        {
            throw new InvalidOperationException("Cannot access the value of failure result.");
        }
        return _value;
    }

    #endregion

    #region Static Methods

    public static new Result<T> Failure(ResultError error)  => new Result<T>(default!, false, error);
    public static Result<T>     Success(T value)            => new Result<T>(value, true, ResultError.None);

    #endregion
}