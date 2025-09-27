using System.ComponentModel.DataAnnotations;

namespace IdentityDataProtectionDemo.Models;

public class User
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    // Hash saklanacak (düz şifre değil!)
    [Required]
    public string Password { get; set; } = null!;
}
