using EShopper.Common.Middleware;
using EShopper.DataAccess.Repository.Abstract;

namespace EShopper.Business.Identity.UserPermissionsManager
{
    public class UserPermissionsManager : IUserPermissionsManager
    {
        private readonly IUserPermissionsRepository _userPermissionsRepository;
        private readonly CurrentUser _currentUser;
        public UserPermissionsManager(IUserPermissionsRepository userPermissionsRepository,
                CurrentUser currentUser)
        {
            _userPermissionsRepository = userPermissionsRepository;
            _currentUser = currentUser;
        }

        public UserPermissionsManagerResponse AddPermission(string permissionName, string userId)
        {
            return null;
        }

    }
}