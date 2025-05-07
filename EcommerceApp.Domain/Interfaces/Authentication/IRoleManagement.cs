using EcommerceApp.Domain.Entities.Identity;

namespace EcommerceApp.Domain.Interfaces.Authentication;
public interface IRoleManagement
{
    Task<string?> GetUserRole(string userEmail);
    Task<bool> AddUserToRole(AppUser user, string roleName);
}
