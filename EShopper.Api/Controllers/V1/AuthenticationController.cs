using EShopper.Business.Services.Abstract;
using EShopper.Contracts.V1.Requests.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EShopper.Api.Controllers.V1
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("api/auth/register")]
        public async Task<IActionResult> Register(RegisterRequestModel registerRequestModel)
        {
            await _authenticationService.RegisterAsync(registerRequestModel);
            return Ok("Oldu");
        }

    }
}