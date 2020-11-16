using EShopper.UI.Filters;
using EShopper.UI.Options;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using AutoMapper;

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
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();

            // TODO: Reconfigure base address
            EShopperApiOptions eShopperApiOptions = new EShopperApiOptions();
            configuration.GetSection(nameof(EShopperApiOptions)).Bind(eShopperApiOptions);
            services.AddHttpClient("eshopperApi", config =>
            {
                config.BaseAddress = new Uri(eShopperApiOptions.BaseAddress);
            });

        }
    }
}