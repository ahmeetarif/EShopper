using AutoMapper;
using EShopper.Business.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EShopper.Business.Installers
{
    public class MvcInstaller : IBaseInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                    options.Filters.Add<ValidationFilter>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}