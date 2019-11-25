using System;
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
        Task<bool> UpdateUserRoles(string userId, List<string> userRoles);
        Task<List<UserRoles>> GetUserRoles(string userId);
        Task<AuthenticationResult> RegisterAndAddSuperAdminRole(string email, string password);
        Task<IdentityUser> GetUserById(string userId);
        Task<IdentityUser> GetUserByEmail(string email);
    }
}
