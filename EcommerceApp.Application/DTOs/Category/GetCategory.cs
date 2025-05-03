using EcommerceApp.Application.DTOs.Product;

namespace EcommerceApp.Application.DTOs.Category;
public class GetCategory:CategoryBase
{
    public Guid Id { get; set; }
    public ICollection<GetProduct>? Products { get; set; }
}
