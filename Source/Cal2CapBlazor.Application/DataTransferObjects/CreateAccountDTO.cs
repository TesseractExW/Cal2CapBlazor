using System.ComponentModel.DataAnnotations;

namespace Cal2CapBlazor.Application.DataTransferObjects;
class CreateAccountDTO {
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
    public required string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [StringLength(20, ErrorMessage = "Password must be not exceed 20 characters")]
    public required string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Display name is required.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Display name must be between 3 and 20 characters.")]
    public required string DisplayName { get; set; } = string.Empty;
}