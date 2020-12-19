using System.Threading.Tasks;
using EShopper.Business.Services.Abstract;
using EShopper.Contracts.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;
        public UserManagerController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpGet]
        [Route(ApiRoutes.UserManager.GetMyRoles)]
        public async Task<IActionResult> GetMyRoles()
        {
            var response = await _userManagerService.GetMyRolesAsync();

            return Ok(response);
        }
    }
}