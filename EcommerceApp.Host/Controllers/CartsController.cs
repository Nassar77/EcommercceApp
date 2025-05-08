using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CartsController(ICartService cartService) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult>Checkout(Checkout checkout)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);

        var result=await cartService.Chakeout(checkout);
        return result.success? Ok(result):BadRequest(result);
    }

    [HttpPost("")]
    public async Task<IActionResult>SaveChackeout(IEnumerable<CreateAchieve> achieves)
    {
        var result=await cartService.SaveChakeoutHistory(achieves);
        return result.success? Ok(result):BadRequest(result);
    }
}
