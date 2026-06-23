namespace Cal2CapBlazor.Application.Accounts.Exceptions;
public class PropertyDuplicatedException : AccountDTOValidationException {
    public PropertyDuplicatedException ()
        : base("The property value has already existed in database.") {}
    public PropertyDuplicatedException (string message)
        : base(message) {}
    public PropertyDuplicatedException (string message, Exception innerException)
        : base(message, innerException) {}
}