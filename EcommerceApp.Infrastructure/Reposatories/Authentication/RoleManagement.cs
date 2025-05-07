using EcommerceApp.Domain.Entities.Identity;
using EcommerceApp.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Infrastructure.Reposatories.Authentication;
public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
{
    public async Task<bool> AddUserToRole(AppUser user, string roleName) =>
        (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

    public async Task<string?> GetUserRole(string userEmail)
    {
        var user = await userManager.FindByEmailAsync(userEmail);
        return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
    }
}
