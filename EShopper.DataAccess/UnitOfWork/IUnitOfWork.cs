using System;
using EShopper.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EShopper.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRefreshTokenRepository RefreshToken { get; }
        IUserDetailsRepository UsersDetail { get; }
        ICategoryRepository Category { get; }
        ISubCategoryRepository SubCategory { get; }

        EShopperDbContext EShopperDbContext { get; }

        void Complete();
        DatabaseFacade Transaction();
    }
}