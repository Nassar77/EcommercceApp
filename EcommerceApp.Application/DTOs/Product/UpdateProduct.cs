using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Application.DTOs.Product;

public class UpdateProduct : ProductBase
{
    [Required]
    public Guid Id { get; set; }
}
