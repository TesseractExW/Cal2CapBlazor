using System.ComponentModel.DataAnnotations;

namespace Cal2CapBlazor.Application.DataTransferObjects;
class UpdateEmailDTO {
    public required int ID { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
    public required string Email { get; set; } = string.Empty;
}