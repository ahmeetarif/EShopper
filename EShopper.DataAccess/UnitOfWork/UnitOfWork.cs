using EShopper.DataAccess.Repository.Abstract;
using EShopper.DataAccess.Repository.Concrete;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EShopper.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RefreshTokenRepository _refreshTokenRepository;
        private UserDetailsRepository _userDetailsRepository;
        private SubCategoryRepository _subCategoryRepository;
        private CategoryRepository _categoryRepository;
        private UserPermissionsRepository _userPermissionsRepository;
        private PermissionRepository _permissionRepository;
        public UnitOfWork(EShopperDbContext context)
        {
            EShopperDbContext = context;
        }
        public IRefreshTokenRepository RefreshToken => _refreshTokenRepository = _refreshTokenRepository ?? new RefreshTokenRepository(EShopperDbContext);

        public IUserDetailsRepository UsersDetail => _userDetailsRepository = _userDetailsRepository ?? new UserDetailsRepository(EShopperDbContext);

        public ICategoryRepository Category => _categoryRepository = _categoryRepository ?? new CategoryRepository(EShopperDbContext);

        public ISubCategoryRepository SubCategory => _subCategoryRepository = _subCategoryRepository ?? new SubCategoryRepository(EShopperDbContext);

        public IPermissionRepository Permission => _permissionRepository = _permissionRepository ?? new PermissionRepository(EShopperDbContext);

        public IUserPermissionsRepository UserPermissions => _userPermissionsRepository = _userPermissionsRepository ?? new UserPermissionsRepository(EShopperDbContext);

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
