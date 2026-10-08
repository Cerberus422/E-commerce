using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account;

public class DeleteAccountDto
{
    [Required]
    public string Password { get; set; } = string.Empty;
}