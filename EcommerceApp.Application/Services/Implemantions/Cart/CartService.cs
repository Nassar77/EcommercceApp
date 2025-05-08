using AutoMapper;
using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Application.Services.Interfaces.Cart;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Entities.Cart;
using EcommerceApp.Domain.Interfaces;
using EcommerceApp.Domain.Interfaces.Cart;

namespace EcommerceApp.Application.Services.Implemantions.Cart;
public class CartService(ICart cartInterface, 
    IMapper mapper,
    IGeneric<Product>productInterface,
    IPaymentMethodService paymentMethodService,
    IPaymentService paymentService) : ICartService
{
    public async Task<ServiceResponse> Chakeout(Checkout checkout)
    {
        var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
        var paymentMethod = await paymentMethodService.GetPaymentMethods();
        if(checkout.PaymentMethodId==paymentMethod.FirstOrDefault()!.Id)
            return await paymentService.Pay(totalAmount, products, checkout.Carts);
       else
            return new ServiceResponse(false, "Invalid payment method");
    }

    public async Task<ServiceResponse> SaveChakeoutHistory(IEnumerable<CreateAchieve> achives)
    {
        var mappedData = mapper.Map<IEnumerable<Achieve>>(achives);
        var result = await cartInterface.SaveChakeoutHistory(mappedData);
        return result > 0 ? new ServiceResponse(true, "checkout acheived") :
            new ServiceResponse(false, "Error ocured in saving");
    }
    private async Task<(IEnumerable<Product>,decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
    {
        if (!carts.Any())
            return ([],0);
        var products=await productInterface.GetAllAsync();

        if (!products.Any())
            return ([], 0);

        var cartProducts=carts
            .Select(cartItems=>products.FirstOrDefault(p=>p.Id==cartItems.ProductId))
            .Where(product=>product is not null).ToList();

        var totalAmount = carts
            .Where(cartitem => cartProducts.Any(p => p.Id == cartitem.ProductId))
            .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);

        return (cartProducts!, totalAmount);

    }

}