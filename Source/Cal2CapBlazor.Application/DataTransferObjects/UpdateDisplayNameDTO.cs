using System.ComponentModel.DataAnnotations;

namespace Cal2CapBlazor.Application.DataTransferObjects;
public class UpdateAccountDTO {
    public required int Id { get; set; }

    [Required(ErrorMessage = "Display name is required.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Display name must be between 3 and 20 characters.")]
    public required string DisplayName { get; set; } = string.Empty;
}