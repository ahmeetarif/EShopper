using System.Threading.Tasks;
using EShopper.Business.Services.Abstract;
using EShopper.Contracts.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.Api.Controllers.V1
{
    [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route(ApiRoutes.Roles.GetRoles)]
        public IActionResult GetRoles()
        {
            var response = _roleService.GetRoles();
            return Ok(response);
        }

        [HttpPost]
        [Route(ApiRoutes.Roles.CreateRole)]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var response = await _roleService.CreateRole(roleName);
            return Ok(response);
        }

        [HttpPost]
        [Route(ApiRoutes.Roles.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromBody] string roleName)
        {
            var response = await _roleService.DeleteRole(roleName);
            return Ok(response);
        }

    }
}
