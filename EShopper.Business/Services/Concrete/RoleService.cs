using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace EShopper.Business.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || string.IsNullOrWhiteSpace(roleName)) throw new EShopperException("Please provide required information!");

            IdentityRole isRoleExist = await _roleManager.FindByNameAsync(roleName);

            if (isRoleExist != null) throw new EShopperException("Role exist!");

            IdentityRole identityRole = new IdentityRole(roleName);

            IdentityResult createRoleResult = await _roleManager.CreateAsync(identityRole);

            if (createRoleResult.Succeeded)
            {
                return "Role Created!";
            }

            throw new EShopperException();
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<string> DeleteRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || string.IsNullOrWhiteSpace(roleName)) throw new EShopperException("Please provide required information!");

            IdentityRole identityRole = await _roleManager.FindByNameAsync(roleName);

            if (identityRole == null) throw new EShopperException("Role not found!");

            IdentityResult deleteRoleResult = await _roleManager.DeleteAsync(identityRole);

            if (deleteRoleResult.Succeeded)
            {
                return "Role Deleted";
            }

            throw new EShopperException();
        }
    }
}