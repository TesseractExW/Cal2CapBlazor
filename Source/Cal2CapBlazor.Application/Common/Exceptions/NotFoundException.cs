namespace Cal2CapBlazor.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base("TODO") {}

    public NotFoundException(string message)
        : base(message) {}

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException) {}
}