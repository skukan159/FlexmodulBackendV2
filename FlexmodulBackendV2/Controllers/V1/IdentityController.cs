﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO;
using FlexmodulBackendV2.Contracts.V1.ResponseDTO;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors("MyPolicy")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpPost(ApiRoutes.Identity.SetUserRole)]
        public async Task<IActionResult> SetUserRole([FromBody] SetUserRoleRequest setUserRoleRequest)
        {
            var result = await _identityService.UpdateUserRoles(setUserRoleRequest.UserId, setUserRoleRequest.Roles);

            if (result == false)
                return NotFound("Updating user role failed.");
            return Ok(true);
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet(ApiRoutes.Identity.GetUserRoles)]
        public async Task<IActionResult> GetUserRoles([FromRoute] string userId)
        {
            var result = await _identityService.GetUserRoles(userId);
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet(ApiRoutes.Identity.GetRoles)]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _identityService.GetRoles();
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [Authorize(Roles = Roles.Employee + "," + Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpGet(ApiRoutes.Identity.GetUsers)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _identityService.GetUsers();
            if (result == null)
                return NotFound("Cannot find users");

            var userResponses = new List<UserResponse>();

            foreach (var user in result)
            {
                var userResponse = await IdentityUserToUserResponse(user);
                userResponses.Add(userResponse);
            }

            return Ok(userResponses);

        }

        [HttpGet(ApiRoutes.Identity.GetById)]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            var user = await _identityService.GetUserById(userId);
            if (user == null)
                return NotFound("Cannot find user with this ID");

            var userResponse = await IdentityUserToUserResponse(user);

            return Ok(userResponse);

        }

        [HttpGet(ApiRoutes.Identity.GetByUsername)]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string email)
        {
            var user = await _identityService.GetUserByEmail(email);

            if (user == null)
                return NotFound();

            var userResponse = await IdentityUserToUserResponse(user);

            return Ok(userResponse);
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet(ApiRoutes.Identity.DeleteUser)]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            var result = await _identityService.DeleteUser(userId);

            if (result == false)
                return NotFound("Could not delete user. Check if the right user ID was inserted.");

            return Ok();

        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
                return BadRequest(new AuthFailedResponse { Errors = authResponse.Errors});
            

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }



        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {

            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Login([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        private async Task<UserResponse> IdentityUserToUserResponse(IdentityUser user)
        {
            var roles = await _identityService.GetUserRoles(user.Id);
            var roleNames = roles.Select(role => role.RoleName);
            return IdentityUserToUserResponse(user, roleNames);
        }

        private static UserResponse IdentityUserToUserResponse(IdentityUser user, IEnumerable<string> roles)
        {
            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles
            };
        }
    }
}
