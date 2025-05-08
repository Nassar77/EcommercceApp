using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.DTOs.Common;


namespace EcommerceApp.Application.Services.Interfaces.Cart;
public interface ICartService
{
    Task<ServiceResponse> SaveChakeoutHistory(IEnumerable<CreateAchieve> achives);
    Task<ServiceResponse> Chakeout(Checkout checkout);
}
