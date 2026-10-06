using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account;

public class ChangePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required] 
    public string NewPassword { get; set; } = string.Empty;
    
    [Required]
    [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}