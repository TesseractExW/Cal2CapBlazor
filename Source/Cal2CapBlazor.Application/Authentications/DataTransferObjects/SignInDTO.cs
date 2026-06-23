namespace Cal2CapBlazor.Application.Authentications.DataTransferObjects;
public class SignInDTO {
    public string Email         { get; set; } = string.Empty;
    public string Password      { get; set; } = string.Empty;
    public bool   RememberMe    { get; set; }
}