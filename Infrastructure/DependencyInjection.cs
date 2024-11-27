using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
           ConfigurationManager configurationManager)
        {
            services.Configure<JwtSetting>(configurationManager.GetSection("JwtSetting"));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DatetimeProvider>();
            return services;
        }
    }
}
