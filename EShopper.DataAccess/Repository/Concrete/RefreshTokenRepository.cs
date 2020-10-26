using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;
using System.Linq;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class RefreshTokenRepository : GenericRepository<RefreshTokens, string>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EShopperDbContext context)
            : base(context)
        {

        }

        public RefreshTokens GetByRefreshToken(string refreshToken)
        {
            RefreshTokens refreshTokens = _context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);
            return refreshTokens;
        }
    }
}