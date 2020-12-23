using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class UserPermissionsRepository : GenericRepository<UserPermissions, int>, IUserPermissionsRepository
    {
        public UserPermissionsRepository(EShopperDbContext context)
            : base(context)
        {
        }
    }
}