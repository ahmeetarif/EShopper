using EShopper.UI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.UI.Installers
{
    public class OptionsInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EShopperApiOptions>(options => configuration.GetSection(nameof(EShopperApiOptions)).Bind(options));
        }
    }
}