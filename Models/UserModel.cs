using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models;

public class User
{
    [Key]
    public Guid UserID { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public required string Password { get; set; }

    [RegularExpression("^(Customer|Seller|Admin)$", ErrorMessage = "Invalid role.")]
    public string Role { get; set; } = "Customer";
}