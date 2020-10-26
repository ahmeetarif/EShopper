using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Abstract
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshTokens, string>
    {
        RefreshTokens GetByRefreshToken(string refreshToken);
    }
}