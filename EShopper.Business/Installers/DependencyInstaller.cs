using EShopper.Business.Identity.Jwt.JwtManager;
using EShopper.Business.Services.Abstract;
using EShopper.Business.Services.Concrete;
using EShopper.DataAccess.Repository.Abstract;
using EShopper.DataAccess.Repository.Concrete;
using EShopper.DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopper.Business.Installers
{
    public class DependencyInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            InstallScopes(services);
        }

        #region Private Functions

        private void InstallScopes(IServiceCollection services)
        {
            services.AddScoped<IUsersDetailRepository, UsersDetailRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtManager, JwtManager>();
        }

        #endregion
    }
}