using EShopper.ApiService.Abstract;
using EShopper.ApiService.Concrete;
using EShopper.UI.Options;
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

            InstallTransients(services, configuration);
        }

        #region Private Functions

        private void InstallTransients(IServiceCollection services, IConfiguration configuration)
        {
            var eshopperApiOptions = configuration.GetSection(nameof(EShopperApiOptions)).Get<EShopperApiOptions>();
            services.AddSingleton(eshopperApiOptions);
        }

        #endregion
    }
}