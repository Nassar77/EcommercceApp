using EcommerceApp.Domain.Entities.Cart;

namespace EcommerceApp.Domain.Interfaces.Cart;
public interface ICart
{
    Task<int> SaveChakeoutHistory(IEnumerable<Achieve> checksouts);
}
