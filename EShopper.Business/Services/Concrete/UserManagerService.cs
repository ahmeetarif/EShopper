using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopper.Business.Identity;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using EShopper.Common.Middleware;
using EShopper.Contracts.V1.Requests.UserManager;
using EShopper.Contracts.V1.Responses.UserManager;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;

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

        public async Task<UserManagerResponseModel> AddUserRoleAsync(AddUserToRoleRequestModel addUserToRoleRequestModel)
        {
            if (string.IsNullOrEmpty(addUserToRoleRequestModel.Rolename) && string.IsNullOrEmpty(addUserToRoleRequestModel.Username) || string.IsNullOrWhiteSpace(addUserToRoleRequestModel.Rolename) && string.IsNullOrWhiteSpace(addUserToRoleRequestModel.Username))
            {
                throw new EShopperException("Please provide required information!");
            }

            EShopperUser getUserInformation = await _eShopperUserManager.FindByNameAsync(addUserToRoleRequestModel.Username);
            if (getUserInformation == null) throw new EShopperException($"User : {addUserToRoleRequestModel.Username} not found!");

            bool isRoleExist = await _roleManager.RoleExistsAsync(addUserToRoleRequestModel.Rolename);

            if (!isRoleExist)
                throw new EShopperException($"Role : {addUserToRoleRequestModel.Rolename} not exist!");

            IdentityResult addToRoleResult = await _eShopperUserManager.AddToRoleAsync(getUserInformation, addUserToRoleRequestModel.Rolename);

            if (addToRoleResult.Succeeded)
            {
                return new UserManagerResponseModel
                {
                    Message = $"User added to Role : {addUserToRoleRequestModel.Rolename}"
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