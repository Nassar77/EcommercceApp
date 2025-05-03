using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Application.DTOs.Category;

public class CategoryBase
{
    [Required]
    public string? Name { get; set; }
}
