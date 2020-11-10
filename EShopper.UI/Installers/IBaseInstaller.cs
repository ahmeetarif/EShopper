using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.UI.Installers
{
    public interface IBaseInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}