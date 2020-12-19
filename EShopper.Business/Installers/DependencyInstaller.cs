using EShopper.Business.Identity.Jwt.JwtManager;
using EShopper.Business.Services.Abstract;
using EShopper.Business.Services.Concrete;
using EShopper.Common.Middleware;
using EShopper.DataAccess.Repository.Abstract;
using EShopper.DataAccess.Repository.Concrete;
using EShopper.DataAccess.UnitOfWork;
using EShopper.EmailManager;
using EShopper.EmailManager.Abstract;
using EShopper.EmailManager.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.Business.Installers
{
    public class DependencyInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            InstallScopes(services);
            InstallSingletons(services, configuration);
            InstallTransients(services);
        }

        #region Private Functions

        private void InstallScopes(IServiceCollection services)
        {
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IJwtManager, JwtManager>();

            services.AddScoped<CurrentUser>();
        }

        private void InstallSingletons(IServiceCollection services, IConfiguration configuration)
        {
            var emailOptions = configuration.GetSection(nameof(EmailOptions)).Get<EmailOptions>();
            services.AddSingleton(emailOptions);
        }

        private void InstallTransients(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
        }

        #endregion
    }
}