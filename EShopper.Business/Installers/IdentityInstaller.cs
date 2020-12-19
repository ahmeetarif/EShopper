using EShopper.Business.Identity;
using EShopper.DataAccess;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.Business.Installers
{
    public class IdentityInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<EShopperUser, IdentityRole>(options =>
            {
                // TODO : Simplify
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            })
                .AddDefaultTokenProviders()
                .AddUserManager<EShopperUserManager>()
                .AddEntityFrameworkStores<EShopperDbContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddTokenProvider("EShopperAuthentication", typeof(DataProtectorTokenProvider<EShopperUser>));
        }
    }
}