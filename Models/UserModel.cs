using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Models;

public class User : IdentityUser
{
    [Key]
    public Guid UserId { get; set; }

    public string? Name { get; set; }

    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string? Password { get; set; }

    [RegularExpression("^(Customer|Seller|Admin)$", ErrorMessage = "Invalid role.")]
    public string Role { get; set; } = "Customer";
}