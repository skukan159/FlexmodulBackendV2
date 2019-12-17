using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<IEnumerable<string>> GetRoles();
        Task<List<UserRoles>> GetUserRoles(string userId);
        Task<bool> UpdateUserRoles(string userId, IEnumerable<string> userRoles);
        Task<IdentityUser> GetUserById(string userId);
        Task<List<IdentityUser>> GetUsers();
        Task<IdentityUser> GetUserByEmail(string email);
        Task<bool> DeleteUser(string userId);
    }
}
