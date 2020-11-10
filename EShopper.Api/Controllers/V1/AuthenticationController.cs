using EShopper.Business.Services.Abstract;
using EShopper.Contracts.V1;
using EShopper.Contracts.V1.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EShopper.Api.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [Route(ApiRoutes.Authentication.Register)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            var response = await _authenticationService.RegisterAsync(registerRequestModel);
            return Ok(response);
        }

        [AllowAnonymous]
        [Route(ApiRoutes.Authentication.Login)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            var response = await _authenticationService.LoginAsync(loginRequestModel);
            return Ok(response);
        }
    }
}