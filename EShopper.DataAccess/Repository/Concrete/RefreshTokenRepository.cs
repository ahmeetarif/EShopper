using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class RefreshTokenRepository : GenericRepository<RefreshTokens, string>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EShopperDbContext context)
            : base(context)
        {

        }
    }
}