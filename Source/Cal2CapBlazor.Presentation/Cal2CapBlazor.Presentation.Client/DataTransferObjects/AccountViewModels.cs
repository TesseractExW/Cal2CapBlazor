namespace Cal2CapBlazor.Presentation.Client.DataTransferObjects;
public class AccountProfileView
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}

public class RegisterAccountViewModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class LoginViewModel
{
    public string EmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ChangeDisplayNameViewModel
{
    public string NewDisplayName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ChangeEmailAddressViewModel
{
    public string NewEmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}