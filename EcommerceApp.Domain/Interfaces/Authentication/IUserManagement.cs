using EcommerceApp.Domain.Entities.Identity;
using System.Security.Claims;

namespace EcommerceApp.Domain.Interfaces.Authentication;

public interface IUserManagement
{
    Task<bool> CreateUser(AppUser user);
    Task<bool> LoginUseer(AppUser user);
    Task<AppUser?> GetUserByEmail(string email);
    Task<AppUser> GetUserById(string id);
    Task<IEnumerable<AppUser>?> GetAllUsers();
    Task<int> RemoveUserByEmail(string email);
    Task<List<Claim>> GetUserClaims(string email);

}
