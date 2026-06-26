using System.Runtime.CompilerServices;

namespace Cal2CapBlazor.Domain.Common;

public abstract class ErrorContainer<T> where T : class
{
    protected static ResultError Create(string message, [CallerMemberName] string propertyName = "")
    {
        return new ResultError($"{typeof(T).Name}.{propertyName}", message);
    }
}