using EcommerceApp.Domain.Entities.Cart;

namespace EcommerceApp.Domain.Interfaces.Cart;
public interface IPaymentMethod
{
    Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
}
