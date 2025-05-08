using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Application.Services.Interfaces.Cart;
public interface IPaymentService
{
    Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> CartProducts, IEnumerable<ProcessCart> carts);
}
