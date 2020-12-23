using System.Collections.Generic;
using EShopper.Entities.Models;

namespace EShopper.Business.Identity.PermissionManager
{
    public interface IPermissionManager
    {
        PermissionManagerResponse CreatePermission(string permissionName);
        PermissionManagerResponse DeletePermission(string permissionName);
        IEnumerable<Permission> GetPermissions();
        Permission GetByName(string permissionName);
        PermissionManagerResponse EditPermissionName(string permissionName, string newPermissioName);
    }
}