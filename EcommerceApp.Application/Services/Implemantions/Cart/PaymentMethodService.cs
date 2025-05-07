using AutoMapper;
using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.Services.Interfaces.Cart;
using EcommerceApp.Domain.Entities.Cart;
using EcommerceApp.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.Implemantions.Cart;
public class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper) : IPaymentMethodService
{
    public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
    {
       var methodes= await paymentMethod.GetPaymentMethods();
        if(!methodes.Any())
            return [];
        return mapper.Map<IEnumerable<GetPaymentMethod>>(methodes);
    }
}
