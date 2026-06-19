using System.ComponentModel.DataAnnotations;

namespace Cal2CapBlazor.Application.DataTransferObjects;
public class UpdatePasswordDTO {
    public required int Id { get; set; }

    [Required(ErrorMessage = "Current password is required.")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "New password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [StringLength(20, ErrorMessage = "Password must be not exceed 20 characters")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}