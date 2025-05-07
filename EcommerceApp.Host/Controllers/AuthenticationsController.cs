using EcommerceApp.Application.DTOs.Identity;
using EcommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticationsController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult>CreateUser(CreateUser user)
    {
        var result=await authenticationService.CreateUser(user);
        return result.success? Ok(result):BadRequest(result);
    }
    [HttpPost("")]
    public async Task<IActionResult> LoginUser(LoginUser user)
    {
        var result = await authenticationService.LoginUser(user);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpGet("{token}")]
    public async Task<IActionResult> RevievToken(string token)
    {
        var result = await authenticationService.ReviveToken(token);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
