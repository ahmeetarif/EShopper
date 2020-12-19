using EShopper.Common.Middleware.Statics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.Business.Installers
{
    public class AuthorizationInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateCategoryPolicy", options =>
                {
                    options.RequireClaim(UserClaimsType.CreateCategoryPermission, "true");
                    options.RequireAuthenticatedUser();
                });

                options.AddPolicy("AddUserToRolePolicy", option =>
                {
                    option.RequireClaim(UserClaimsType.AddUserToRolesPermission, "true");
                    option.RequireAuthenticatedUser();
                    option.RequireRole("ADMIN");
                });
            });
        }
    }
}