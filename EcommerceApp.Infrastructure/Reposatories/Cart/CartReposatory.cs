using EcommerceApp.Domain.Entities.Cart;
using EcommerceApp.Domain.Interfaces.Cart;
using EcommerceApp.Infrastructure.Data;

namespace EcommerceApp.Infrastructure.Reposatories.Cart;
public class CartReposatory(AppDbContext context) : ICart
{
    public async Task<int> SaveChakeoutHistory(IEnumerable<Achieve> checksouts)
    {
        context.CheckoutAchieves.AddRange(checksouts);
        return await context.SaveChangesAsync();
    }
}
