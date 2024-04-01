using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Product
{
    [Key]
    public Guid ProductID { get; set; }

    [Required(ErrorMessage = "The Title field is required.")]
    [StringLength(100, ErrorMessage = "The Title field must be between {2} and {1} characters long.", MinimumLength = 1)]
    public required string Title { get; set; }

    [Required(ErrorMessage = "The Description field is required.")]
    [StringLength(500, ErrorMessage = "The Description field must be maximum {1} characters long.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "The Image field is required.")]
    public required string Image { get; set; }

    [Required(ErrorMessage = "The Stock field is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "The Stock field must be a positive number.")]
    public int Stock { get; set; }

    [ForeignKey("Category")]
    public Guid CategoryID { get; set; }
    public virtual Category? Category { get; set; }
}