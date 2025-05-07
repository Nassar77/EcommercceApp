using EcommerceApp.Application.DTOs.Cart;

namespace EcommerceApp.Application.Services.Interfaces.Cart;
public interface IPaymentMethodService
{
    Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods();
}
