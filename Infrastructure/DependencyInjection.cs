using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Domain.Common;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuth(configuration);
            services.AddPersistence(configuration);
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }

        private static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddDbContext<GroupMineDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(GroupMineDbContext).Assembly.FullName)));

            services.AddScoped<IGroupMineDbContext>(provider => provider.GetService<GroupMineDbContext>()!);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DatetimeProvider>();
            return services;
        }
    }
}