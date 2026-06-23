namespace Cal2CapBlazor.Application.Accounts.DataTransferObjects;
public class CreateAccountDTO {
    public string Email         { get; set; } = string.Empty;
    public string Password      { get; set; } = string.Empty;
    public string DisplayName   { get; set; } = string.Empty;
}