using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.UI.Installers
{
    public class AuthenticationInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("EShopperAuthentication")
                .AddCookie("EShopperAuthentication", options =>
                {
                    options.LoginPath = "/auth/login";
                    options.Cookie.Name = "EShopperUserAuthentication";
                });
        }
    }
}