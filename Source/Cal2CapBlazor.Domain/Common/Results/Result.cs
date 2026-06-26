namespace Cal2CapBlazor.Domain.Common;

public class Result
{
    #region Result Descriptions

    public bool         IsSuccess   { get; }
    public ResultError  Error       { get; } = ResultError.None;

    #endregion

    public Result(bool isSuccess, ResultError error)
    {
        if ((isSuccess  && error != ResultError.None) || 
            (!isSuccess && error == ResultError.None))
        {
            throw new ArgumentException("Invalid error state", nameof(error));
        }

        IsSuccess = isSuccess;
        Error     = error;
    }

    #region Static Methods

    public static Result Failure(ResultError error) => new Result(false, error);
    public static Result Success()                  => new Result(true, ResultError.None);

    #endregion
}