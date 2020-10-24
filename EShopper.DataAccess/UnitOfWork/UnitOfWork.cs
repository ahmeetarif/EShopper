using EShopper.DataAccess.Repository.Abstract;
using EShopper.DataAccess.Repository.Concrete;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EShopper.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RefreshTokenRepository _refreshTokenRepository;
        private UsersDetailRepository _usersDetailRepository;
        public UnitOfWork(EShopperDbContext context)
        {
            EShopperDbContext = context;
        }
        public IRefreshTokenRepository RefreshToken => _refreshTokenRepository = _refreshTokenRepository ?? new RefreshTokenRepository(EShopperDbContext);

        public IUsersDetailRepository UsersDetail => _usersDetailRepository = _usersDetailRepository ?? new UsersDetailRepository(EShopperDbContext);

        public EShopperDbContext EShopperDbContext { get; }

        public DatabaseFacade Transaction()
        {
            return EShopperDbContext.Database;
        }

        public void Complete()
        {
            EShopperDbContext.SaveChanges();
        }

        public void Dispose()
        {
            EShopperDbContext.Dispose();
        }
    }
}
