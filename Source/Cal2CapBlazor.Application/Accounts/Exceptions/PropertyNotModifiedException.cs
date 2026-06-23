namespace Cal2CapBlazor.Application.Accounts.Exceptions;
public class PropertyNotModifiedException : AccountDTOValidationException {
    public PropertyNotModifiedException ()
        : base("The property has not been modified from the previous value.") {}
    public PropertyNotModifiedException (string message)
        : base(message) {}
    public PropertyNotModifiedException (string message, Exception innerException)
        : base(message, innerException) {}
}