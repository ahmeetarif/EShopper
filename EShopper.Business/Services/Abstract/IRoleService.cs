using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EShopper.Business.Services.Abstract
{
    public interface IRoleService
    {
        Task<string> CreateRole(string roleName);
        IEnumerable<IdentityRole> GetRoles();
        Task<string> DeleteRole(string roleName);
    }
}
