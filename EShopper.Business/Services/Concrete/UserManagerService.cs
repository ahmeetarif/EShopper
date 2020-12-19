using System.Collections.Generic;
using System.Threading.Tasks;
using EShopper.Business.Identity;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using EShopper.Common.Middleware;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using EShopper.Contracts.V1.Responses.UserManager;

namespace EShopper.Business.Services.Concrete
{
    public class UserManagerService : IUserManagerService
    {
        private readonly EShopperUserManager _eShopperUserManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CurrentUser _currentUser;
        public UserManagerService(EShopperUserManager eShopperUserManager,
                RoleManager<IdentityRole> roleManager,
                CurrentUser currentUser)
        {
            _eShopperUserManager = eShopperUserManager;
            _roleManager = roleManager;
            _currentUser = currentUser;
        }

        public async Task<UserManagerResponseModel> AddUserRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || string.IsNullOrWhiteSpace(roleName)) throw new EShopperException("Please provide required information!");

            bool isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExist) throw new EShopperException($"Role : {roleName} not exist");

            EShopperUser user = await _eShopperUserManager.FindByIdAsync(_currentUser.Id);

            IdentityResult addToRoleResult = await _eShopperUserManager.AddToRoleAsync(user, roleName);

            if (addToRoleResult.Succeeded)
            {
                // Something ..
                return new UserManagerResponseModel
                {
                    Message = $"User is now in Role : {roleName}"
                };
            }

            throw new EShopperException();
        }

        public async Task<List<string>> GetMyRolesAsync()
        {
            EShopperUser user = await _eShopperUserManager.FindByIdAsync(_currentUser.Id);
            IEnumerable<string> roles = await _eShopperUserManager.GetRolesAsync(user);


            return roles.ToList();
        }

    }
}