using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Brand
{
    [Key]
    public Guid BrandID { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(50, ErrorMessage = "The Name field must be between {2} and {1} characters long.", MinimumLength = 2)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The Logo field is required.")]
    public required string Logo { get; set; }

    public virtual required ICollection<Product> Products { get; set; }
}