namespace Cal2CapBlazor.Application.Common.Exceptions;

public class NotModifiedException : Exception
{
    public NotModifiedException()
        : base("TODO") {}

    public NotModifiedException(string message)
        : base(message) {}

    public NotModifiedException(string message, Exception innerException)
        : base(message, innerException) {}
}