using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Category
{
    [Key]
    public Guid CategoryID { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(50, ErrorMessage = "The Name field must be between {2} and {1} characters long.", MinimumLength = 2)]
    public required string Name { get; set; }

    [StringLength(200, ErrorMessage = "The Description field must be maximum {1} characters long.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Color field is required.")]
    [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Please enter a valid hexadecimal RGB color value.")]
    public required string Color { get; set; }

    public virtual required ICollection<Product> Products { get; set; }
}