namespace Cal2CapBlazor.Application.Accounts.DataTransferObjects;
public class DeleteAccountDTO {
    public Guid   Id            { get; set; } 
    public string Email         { get; set; }
    public string Password      { get; set; }
}