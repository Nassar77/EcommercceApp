using EcommerceApp.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Application.DTOs.Product;
public class GetProduct : ProductBase
{
    [Required]
    public Guid Id { get; set; }
    public GetCategory? Category { get; set; }
}
