using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Application.Services.Interfaces.Cart;
using EcommerceApp.Domain.Entities;
using Stripe.Checkout;

namespace EcommerceApp.Infrastructure.Service;
public class StripePaymentService : IPaymentService
{
    public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> CartProducts, IEnumerable<ProcessCart> carts)
    {
        try
        {
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in CartProducts)
            {
                var pQuantity = carts.FirstOrDefault(x => x.ProductId == item.Id);
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Description
                        },
                        UnitAmount = (long)(item.Price * 100),
                    },
                    Quantity = pQuantity!.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["usd"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = " https://localhost:7067/payment-success",
                CancelUrl = " https://localhost:7067/payment-cancel"
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return new ServiceResponse(true, session.Url);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, ex.Message);
        }

    }
}

