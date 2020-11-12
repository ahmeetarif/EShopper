using EShopper.UI.Filters;
using EShopper.UI.Options;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace EShopper.UI.Installers
{
    public class BaseInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });


            services.AddHttpContextAccessor();

            EShopperApiOptions eShopperApiOptions = new EShopperApiOptions();
            services.AddHttpClient("eshopperApi", config =>
            {
                config.BaseAddress = new Uri(eShopperApiOptions.BaseAddress);
            });

        }
    }
}