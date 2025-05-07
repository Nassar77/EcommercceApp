using EcommerceApp.Domain.Entities.Cart;
using EcommerceApp.Domain.Interfaces.Cart;
using EcommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Infrastructure.Reposatories.Cart;
public class PaymentMethodReposatory(AppDbContext context) : IPaymentMethod
{
    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()=>
        await context.paymentMethods.AsNoTracking().ToListAsync();
   
}