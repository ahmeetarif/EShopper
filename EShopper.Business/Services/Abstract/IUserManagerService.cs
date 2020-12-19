using System.Collections.Generic;
using System.Threading.Tasks;
using EShopper.Contracts.V1.Responses.UserManager;

namespace EShopper.Business.Services.Abstract
{
    public interface IUserManagerService
    {
        Task<UserManagerResponseModel> AddUserRoleAsync(string roleName);
        Task<List<string>> GetMyRolesAsync();
    }
}