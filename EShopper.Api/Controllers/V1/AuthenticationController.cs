using System.Threading.Tasks;
using EShopper.Business.Services.Abstract;
using EShopper.Contracts.V1;
using EShopper.Contracts.V1.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        #region Register

        [AllowAnonymous]
        [Route(ApiRoutes.Authentication.Register)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            var response = await _authenticationService.RegisterAsync(registerRequestModel);
            return Ok(response);
        }

        #endregion

        #region Login

        [AllowAnonymous]
        [Route(ApiRoutes.Authentication.Login)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            var response = await _authenticationService.LoginAsync(loginRequestModel);
            return Ok(response);
        }

        #endregion

        #region Email Confirmation

        [AllowAnonymous]
        [HttpPost]
        [Route(ApiRoutes.Authentication.SendEmailConfirmationLink)]
        public async Task<IActionResult> SendEmailConfirmation(string email)
        {
            var response = await _authenticationService.SendEmailConfirmationLinkAsync(email);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route(ApiRoutes.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequestModel confirmEmailRequestModel)
        {
            var response = await _authenticationService.ConfirmEmailAsync(confirmEmailRequestModel);
            return Ok(response);
        }

        #endregion

        #region Reset Password

        [AllowAnonymous]
        [HttpPost]
        [Route(ApiRoutes.Authentication.ForgotPassword)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            var response = await _authenticationService.SendResetPasswordLinkAsync(forgotPasswordRequestModel);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route(ApiRoutes.Authentication.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequestModel resetPasswordRequestModel)
        {
            var response = await _authenticationService.ResetPasswordAsync(resetPasswordRequestModel);
            return Ok(response);
        }

        #endregion

    }
}