using EShopper.ApiService.Abstract;
using EShopper.ApiService.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.UI.Installers
{
    public class DependencyInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Scopes

            services.AddScoped<IEShopperAuthenticationApiService, EShopperAuthenticationApiService>();

            #endregion
        }
    }
}