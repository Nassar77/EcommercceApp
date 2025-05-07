using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Application.DTOs.Identity;

namespace EcommerceApp.Application.Services.Interfaces.Authentication;
public interface IAuthenticationService
{
    Task<ServiceResponse> CreateUser(CreateUser user);
    Task<LoginResponse>LoginUser(LoginUser user);
    Task<LoginResponse> ReviveToken(string refreshToken);
}
