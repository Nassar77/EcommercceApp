using EcommerceApp.Domain.Entities.Identity;
using EcommerceApp.Domain.Interfaces.Authentication;
using EcommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceApp.Infrastructure.Reposatories.Authentication;

public class UserManagement(UserManager<AppUser> userManager, IRoleManagement roleManagement, AppDbContext context) : IUserManagement
{
    public async Task<bool> CreateUser(AppUser user)
    {
        var _user = await GetUserByEmail(user.Email!);
        if (_user is not null) return false;

        return (await userManager.CreateAsync(user!, user!.PasswordHash!)).Succeeded;
    }

    public async Task<IEnumerable<AppUser>?> GetAllUsers() => await context.Users.ToListAsync();

    public async Task<AppUser?> GetUserByEmail(string email) => await userManager.FindByEmailAsync(email);

    public async Task<AppUser> GetUserById(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        return user!;
    }

    public async Task<List<Claim>> GetUserClaims(string email)
    {
        var _user = await GetUserByEmail(email);
        string? roleName = await roleManagement.GetUserRole(_user!.Email!);// why not email direct into parameter 

        List<Claim> claims =
        [
            new Claim("FullName",_user.FullName),
            new Claim(ClaimTypes.NameIdentifier,_user!.Id),
            new Claim(ClaimTypes.Email,_user!.Email!),
            new Claim(ClaimTypes.Role, roleName!)
        ];

        return claims;
    }

    public async Task<bool> LoginUseer(AppUser user)
    {
        var _user = await GetUserByEmail(user.Email!);
        if (_user is null) return false;

        string? roleName = await roleManagement.GetUserRole(_user!.Email!);
        if (roleName is null) return false;

        return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
    }

    public async Task<int> RemoveUserByEmail(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        context.Users.Remove(user);
        return await context.SaveChangesAsync();
    }
}
