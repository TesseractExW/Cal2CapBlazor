namespace Cal2CapBlazor.Application.Common.Exceptions;
public class DuplicatedException : Exception {
    public DuplicatedException()
        : base("TODO") {}
    public DuplicatedException(string message)
        : base(message) {}
    public DuplicatedException(string message, Exception innerException)
        : base(message, innerException) {}
}