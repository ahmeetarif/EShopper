using EShopper.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace EShopper.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRefreshTokenRepository RefreshToken { get; }
        IUsersDetailRepository UsersDetail { get; }
        EShopperDbContext EShopperDbContext { get; }
        void Complete();
        DatabaseFacade Transaction();
    }
}