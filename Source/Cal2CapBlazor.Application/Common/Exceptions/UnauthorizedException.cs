namespace Cal2CapBlazor.Application.Common.Exceptions;

public class UnauthorizedExeption : Exception
{
    public UnauthorizedExeption()
        : base("TODO") {}

    public UnauthorizedExeption(string message)
        : base(message) {}

    public UnauthorizedExeption(string message, Exception innerException)
        : base(message, innerException) {}
}