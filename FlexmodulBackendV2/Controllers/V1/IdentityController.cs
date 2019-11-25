using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Services;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost(ApiRoutes.Identity.SetRole)]
        public async Task<IActionResult> SetUserRole([FromRoute] string userId,[FromBody] string roleId)
        { 
            var roles = new List<string>{roleId};
            var result = await _identityService.UpdateUserRoles(userId,roles);

            if (result == false)
                return NotFound();
            return Ok(true);
        }

        //Todo: Uncomment
        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet(ApiRoutes.Identity.GetRoles)]
        public async Task<IActionResult> GetUserRoles([FromRoute] string userId)
        {
            var result = await _identityService.GetUserRoles(userId);
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [HttpGet(ApiRoutes.Identity.GetById)]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            var result = await _identityService.GetUserById(userId);
            if (result == null)
                return NotFound();

            var responseObj = new UserResponse()
            {
                Id = result.Id,
                Email = result.Email,
                UserName = result.UserName
            };

            return Ok(responseObj);

        }
        [HttpGet(ApiRoutes.Identity.GetByUsername)]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string email)
        {
            var result = await _identityService.GetUserByEmail(email);

            if (result == null)
                return NotFound();

            var responseObj = new UserResponse()
            {
                Id = result.Id,
                Email = result.Email,
                UserName = result.UserName
            };

            return Ok(responseObj);

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

            Domain.AuthenticationResult authResponse;

            //Throw this out later in the project - this is only for testing purposes
            if (request.SecretPassword == "SuperSecretPassword")
            { 
                authResponse = await _identityService.RegisterAndAddSuperAdminRole(request.Email, request.Password);
            }
            else
                authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

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

        private async Task<IActionResult> AddRoleToRegisteredUser(string userId, string roleId)
        {
            var roles = new List<string> { roleId };
            var result = await _identityService.UpdateUserRoles(userId, roles);

            if (result == false)
                return NotFound();
            return Ok(true);
        }
    }
}
