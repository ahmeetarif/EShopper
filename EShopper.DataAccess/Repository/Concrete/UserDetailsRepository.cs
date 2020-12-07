using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class UserDetailsRepository : GenericRepository<UserDetails, string>, IUserDetailsRepository
    {
        public UserDetailsRepository(EShopperDbContext context)
            : base(context)
        {

        }
    }
}