using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class PaymentsController(IPaymentMethodService paymentMethodService) : ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
    {
        var methods = await paymentMethodService.GetPaymentMethods();
        return methods is not null ?
            Ok(methods)
            : NotFound();
    }
}
