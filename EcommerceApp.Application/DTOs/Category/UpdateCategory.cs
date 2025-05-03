using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Application.DTOs.Category;

public class UpdateCategory: CategoryBase
{
    [Required]
    public Guid Id { get; set; }
}