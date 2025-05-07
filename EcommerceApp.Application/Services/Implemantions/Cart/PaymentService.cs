using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Application.Services.Interfaces.Cart;
using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Application.Services.Implemantions.Cart;
public class PaymentService : IPaymentService
{
    public Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> CartProducts, IEnumerable<ProcessCart> carts)
    {
        throw new NotImplementedException();
    }
}
