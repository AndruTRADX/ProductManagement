using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models;

public class User
{
    [Key]
    public Guid UserID { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = string.Empty;

    [RegularExpression("^(Customer|Seller|Admin)$", ErrorMessage = "Invalid role.")]
    public string Role { get; set; } = "Customer";
}