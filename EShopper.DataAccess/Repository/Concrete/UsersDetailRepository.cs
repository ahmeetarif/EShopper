using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class UsersDetailRepository : GenericRepository<UserDetails, string>, IUsersDetailRepository
    {
        public UsersDetailRepository(EShopperDbContext context)
            : base(context)
        {

        }
    }
}