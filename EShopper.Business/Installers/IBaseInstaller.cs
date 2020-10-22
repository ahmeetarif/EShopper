using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopper.Business.Installers
{
    public interface IBaseInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}