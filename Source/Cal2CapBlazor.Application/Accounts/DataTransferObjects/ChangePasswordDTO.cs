namespace Cal2CapBlazor.Application.Accounts.DataTransferObjects;
public class ChangePasswordDTO {
    public Guid   Id                { get; set; }
    public string Password          { get; set; }
    public string NewPassword       { get; set; }
    public string ConfirmPassword   { get; set; }
}