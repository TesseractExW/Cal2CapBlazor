namespace Cal2CapBlazor.Application.Common.Exceptions;
public class UnauthenticatedException : Exception {
    public UnauthenticatedException()
        : base("TODO") {}
    public UnauthenticatedException(string message)
        : base(message) {}
    public UnauthenticatedException(string message, Exception innerException)
        : base(message, innerException) {}
}